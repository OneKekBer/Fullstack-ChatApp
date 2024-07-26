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

import { useAppDispatch } from 'store/hooks'
import { toast, ToastContainer } from 'react-toastify'
import 'react-toastify/dist/ReactToastify.css'
import Register from 'pages/auth/register/Register'
import Login from 'pages/auth/login/Login'
import { SetUserIsOnline } from 'store/User/UserSlice'
import IConnectUserData from 'interfaces/IConnectUserData'
import IChat from 'interfaces/IChat'
import { CreateRoom, SetChats, UpdateChat } from 'store/Chat/RoomSlice'
import SearchChatPopup from 'common/popups/SearchChatPopup'

function App() {
	const [connection, setConnection] = useState<HubConnection | undefined>()
	const [isSearchPopupOpen, setIsSearchPopupOpen] = useState(false)

	const dispatch = useAppDispatch()

	const toggleSearchPopup = () => {
		setIsSearchPopupOpen(prev => !prev)
	}

	const ConnectToHub = async (Login: string) => {
		const conn = new HubConnectionBuilder()
			.withUrl('https://localhost:7181/chatHub')
			.configureLogging(LogLevel.Information)
			.withAutomaticReconnect()
			.build()

		conn.on('ReceiveChats', (chats: IChat[]) => {
			dispatch(SetChats(chats))
		})

		conn.on('UpdateChat', (chat: IChat) => {
			dispatch(UpdateChat(chat))
		})

		conn.on('CreateNewChat', (newChat: IChat) => {
			dispatch(CreateRoom(newChat))

			console.log('CreateNewChat: ' + newChat)
			console.log(newChat.name)
			newChat.users.map(user => {
				console.log('user: ' + user)
				console.log(user.login)
			})
			// dispatch(SetUserIsOffline(login))
		})

		conn.on('OnlineUsers', async (onlineUsers: IConnectUserData[]) => {
			dispatch(SetUserIsOnline(onlineUsers))
			// console.log('user is online: ' + login)
		})

		try {
			await conn.start()
			await conn?.invoke('Connect', Login)
			setConnection(conn)
		} catch (err) {
			toast.error('something went wrong')

			console.error(
				'Error while establishing connection or sending message:',
				err
			)
		}
	}

	return (
		<div>
			<SearchChatPopup
				isSearchPopupOpen={isSearchPopupOpen}
				toggleSearchPopup={toggleSearchPopup}
			/>
			<ToastContainer
				position='top-right'
				autoClose={2000}
				closeOnClick
				theme='dark'
			/>
			<Routes>
				<Route path='/' element={<WaitingRoom />} />
				<Route
					path='/login'
					element={<Login ConnectToHub={ConnectToHub} />}
				/>
				<Route path='/register' element={<Register />} />
				<Route
					path='/chat'
					element={
						<ChatRoom
							toggleSearchPopup={toggleSearchPopup}
							connection={connection}
						/>
					}
				/>
				<Route path='*' element={<Error />} />
				{/* <Route path='/' element={<WaitingRoom />} /> */}
			</Routes>
		</div>
	)
}

export default App
