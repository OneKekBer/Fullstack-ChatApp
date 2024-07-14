import { useState } from 'react'
import { Route, Routes } from 'react-router-dom'

import WaitingRoom from './pages/waitingRoom/WaitingRoom'
import Error from './pages/error/Error'
import ChatRoom from './pages/chatRoom/ChatRoom'
import {
	HubConnection,
	HubConnectionBuilder,
	LogLevel,
} from '@microsoft/signalr'

function App() {
	const [connection, setConnection] = useState<HubConnection | undefined>()
	const [chat, setChat] = useState<string[]>([])

	const JoinChat = async (chatName: string, name: string) => {
		const conn = new HubConnectionBuilder()
			.withUrl('https://localhost:7181/chatHub')
			.configureLogging(LogLevel.Information)
			.withAutomaticReconnect()
			.build()

		conn.on('ReciveMessage', (msg: string) => {
			console.log('msg:' + msg)
			setChat(messages => [...messages, msg])
		})

		try {
			await conn.start()
			await conn.invoke('JoinChat', chatName, name)
			// console.log(conn)
			setConnection(conn)
		} catch (err) {
			console.error(
				'Error while establishing connection or sending message:',
				err
			)
		}
	}
	return (
		<Routes>
			<Route path='/' element={<WaitingRoom JoinChat={JoinChat} />} />
			<Route
				path='/:chatName'
				element={<ChatRoom chat={chat} connection={connection} />}
			/>
			<Route path='*' element={<Error />} />
			{/* <Route path='/' element={<WaitingRoom />} /> */}
		</Routes>
	)
}

export default App
