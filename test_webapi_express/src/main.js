const express = require('express')
const User = require('./User')
const mongoose = require('mongoose')

require('dotenv').config()
const app = express()

mongoose.connect(process.env.DB_CONNECT, {useNewUrlParser: true, useUnifiedTopology: true, useCreateIndex: true, useFindAndModify: false }, () =>
  console.log('Connected to db')
);

app.get('/users', async (req, res) => {
  return res.send(await User.find())
})

app.listen(5555)