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
import IChat from 'interfaces/IChat'
import { addChat, SetChats, UpdateChat } from 'store/Chat/RoomSlice'
import { IMessage } from 'interfaces/IMessage'
import CreateChatPopup from 'common/popups/CreateChatPopup'
import FindChatPopup from 'common/popups/FindChatPopup'

function App() {
	const [connection, setConnection] = useState<HubConnection | undefined>()
	const [isSearchPopupOpen, setIsSearchPopupOpen] = useState(false)
	const [isCreatePopupOpen, setIsCreatePopupOpen] = useState(false)

	const dispatch = useAppDispatch()

	const toggleSearchPopup = () => {
		setIsSearchPopupOpen(prev => !prev)
	}
	const toggleCreatePopup = () => {
		setIsCreatePopupOpen(prev => !prev)
	}

	const ConnectToHub = async (Login: string) => {
		const conn = new HubConnectionBuilder()
			.withUrl('https://localhost:7181/chatHub')
			.configureLogging(LogLevel.Information)
			.withAutomaticReconnect()
			.build()

		conn.on('SendError', (error: string) => {
			toast.error(error)
		})

		conn.on('SendChatMessages', (chatName: string, messages: IMessage[]) => {
			console.log('send chat messages is wotking!!')
			dispatch(addChat({ chatName, messages }))
		})

		conn.on('UpdateChat', (chatName: string, message: IMessage) => {
			dispatch(UpdateChat({ chatName, message }))
		})

		conn.on('GetAllChats', (chats: IChat[]) => {
			dispatch(SetChats(chats))
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
			<CreateChatPopup
				isSearchPopupOpen={isCreatePopupOpen}
				toggleSearchPopup={toggleCreatePopup}
			/>
			<FindChatPopup
				isSearchPopupOpen={isSearchPopupOpen}
				toggleSearchPopup={toggleSearchPopup}
				connection={connection}
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
							toggleCreatePopup={toggleCreatePopup}
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
