// Import Express
const express = require('express')
const app = express()
const http = require('http').Server(app)
const path = require('path')
// Import Socket.io
const io = require('socket.io')(http)


app.get('/', (req, res) => {
  res.sendFile(__dirname + '/client/index.html')
})

// Setup serving static files
// Assets -> Graphics Resources
app.use('/assets', express.static(path.join(__dirname, 'client/assets')))
// Build -> Scripts (Generated by Webpack)
app.use('/build', express.static(path.join(__dirname, 'client/build')))

// Listen for a connection
io.on('connection', (socket) => {
  console.log('a user connected')
})

// Start server
app.listen(3000, () => {
  console.log('listening on *:3000')
})
