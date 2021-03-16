const mongoose = require('mongoose');

const userSchema = new mongoose.Schema({
    _id: Number,
    name: {
        type: String,
    },
    email: {
        type: String,
    },
});

module.exports = mongoose.model('User', userSchema);
