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

import { ToastContainer } from 'react-toastify'
import 'react-toastify/dist/ReactToastify.css'
import Register from 'pages/auth/register/Register'
import Login from 'pages/auth/login/Login'
import { SetUserIsOffline, SetUserIsOnline } from 'store/User/UserSlice'

function App() {
	const [connection, setConnection] = useState<HubConnection | undefined>()
	const [chat, setChat] = useState<IMessage[]>([])
	const dispatch = useAppDispatch()

	const ConnectToHub = async (Login: string) => {
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

		conn.on('UserIsOffline', (login: string) => {
			console.log('user is offline: ' + login)
			dispatch(SetUserIsOffline(login))
		})

		conn.on('UserIsOnline', (login: string) => {
			console.log('user is online: ' + login)
			dispatch(SetUserIsOnline(login))
		})

		try {
			await conn.start()
			connection?.invoke('Connect', Login)
			setConnection(conn)
		} catch (err) {
			console.error(
				'Error while establishing connection or sending message:',
				err
			)
		}
	}

	return (
		<div>
			<ToastContainer position='top-right' closeOnClick theme='dark' />
			<Routes>
				<Route path='/' element={<WaitingRoom />} />
				<Route
					path='/login'
					element={<Login ConnectToHub={ConnectToHub} />}
				/>
				<Route path='/register' element={<Register />} />
				<Route
					path='/chat'
					element={<ChatRoom chat={chat} connection={connection} />}
				/>
				<Route path='*' element={<Error />} />
				{/* <Route path='/' element={<WaitingRoom />} /> */}
			</Routes>
		</div>
	)
}

export default App
