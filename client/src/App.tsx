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
import { IJoinChatData } from './interfaces/IJoinChatData'
import { IMessage } from './interfaces/IMessage'
import { useAppDispatch } from 'store/hooks'
import { CreateRoom } from 'store/Chat/RoomSlice'

function App() {
	const [connection, setConnection] = useState<HubConnection | undefined>()
	const [chat, setChat] = useState<IMessage[]>([])
	const dispatch = useAppDispatch()

	const JoinChat = async (JoinChatData: IJoinChatData) => {
		const conn = new HubConnectionBuilder()
			.withUrl('https://localhost:7181/chatHub')
			.configureLogging(LogLevel.Information)
			.withAutomaticReconnect()
			.build()

		conn.on('ReceiveMessage', (userName, message) => {
			console.log(userName + ': ' + message)
			setChat(messages => [
				...messages,
				{ Author: userName, Message: message },
			])
		})

		try {
			await conn.start()

			const { GroupName, Name } = JoinChatData
			await conn.invoke('JoinChat', { GroupName, Name })
			dispatch(CreateRoom(GroupName, null))

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
