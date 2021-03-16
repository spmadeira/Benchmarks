const User = require('./User')
const mongoose = require('mongoose')

require('dotenv').config()

mongoose.connect(process.env.DB_CONNECT, {useNewUrlParser: true, useUnifiedTopology: true, useCreateIndex: true, useFindAndModify: false }, () =>
  console.log('Connected to db')
);


(async () => {
    await User.deleteMany({}).catch((e) => console.error(e))
    const users = [...Array(1000).keys()].map(i => {
        return {
            _id: i+1,
            name: `Usuario ${i}`,
            email: `user${i+1}@email.com`
        }
    })
    await User.insertMany(users).catch((e) => console.error(e))
    console.log("done")
    process.exit()
})()
