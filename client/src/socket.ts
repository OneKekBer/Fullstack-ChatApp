// import { io } from 'socket.io-client'

// // URL to your SignalR hub
// const URL = 'https://localhost:7181'

// export const socket = io(URL, {
// 	withCredentials: true,
// 	transports: ['websocket'],
// 	reconnection: true,
// 	reconnectionAttempts: Infinity,
// 	reconnectionDelay: 1000,
// 	timeout: 20000,
// })

// // Event listeners for connection events
// socket.on('connect', () => {
// 	console.log('Connected to the chat hub')
// })

// socket.on('disconnect', () => {
// 	console.log('Disconnected from the chat hub')
// })

// // Example of listening for a message event
// socket.on('message', message => {
// 	console.log('New message:', message)
// })

// // Example of emitting an event to the server
// socket.emit('sendMessage', 'Hello from client!')
