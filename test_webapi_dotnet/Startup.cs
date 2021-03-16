using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace test_webapi_dotnet
{
    [ApiController][Route("test")]
    public class TestController : ControllerBase {

        private DataContext _dataContext;

        public TestController(DataContext dataContext){
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(){
            return Ok(_dataContext.Users.AsNoTracking());    
        }
    }

    public class User {
        [Key]
        public int Id {get;set;}
        public string Name {get;set;}
        public string Email {get;set;}
    }

    public class DataContext : DbContext {
        public DataContext(DbContextOptions<DataContext> options) : base(options){

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var mockUsers = Enumerable.Range(0, 1000).Select(u => {
                return new User{
                    Id = u+1,
                    Name = $"Usuario {u+1}",
                    Email = $"user{u+1}@email.com"
                };
            }).ToArray();

            modelBuilder.Entity<User>().HasData(mockUsers);
        }

        public DbSet<User> Users {get;set;}
    }

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options => {
                options.UseSqlServer("Server=[::1];Database=benchmarks;User Id=sa;Password=D3vpassword;");
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
