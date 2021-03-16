class UserController < ApplicationController
    def get
        return render status: 200, json: User.all
    end
end
