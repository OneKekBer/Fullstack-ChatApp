import { HubConnection } from '@microsoft/signalr'
import { useNavigate } from 'react-router-dom'
import Form from './components/Form'
import { useEffect, useState } from 'react'
import { useAppSelector } from 'store/hooks'
import { toast } from 'react-toastify'
import { Button } from '@chakra-ui/react'

import userIcon from 'public/icons/user/user.png'
import IChat from 'interfaces/IChat'

interface ChatRoomProps {
	connection: HubConnection | undefined
	toggleSearchPopup: () => void
}

const ChatRoom: React.FC<ChatRoomProps> = ({
	connection,
	toggleSearchPopup,
}) => {
	const navigate = useNavigate()
	const login = useAppSelector(state => state.user.Login)
	const onlineUsers = useAppSelector(state => state.user.OnlineUsers)
	const chats = useAppSelector(state => state.rooms.chats)
	const [currentChat, setCurrentChat] = useState<IChat | undefined>(undefined)

	const FindChatByName = (chatName: string) => {
		return chats.find(chat => chat.name === chatName)
	}

	const HandleChatClick = (chatName: string) => {
		const chat = FindChatByName(chatName)
		setCurrentChat(chat)
	}

	useEffect(() => {
		// if (login === '' || login === undefined) {
		// 	navigate('/login')
		// 	toast.info('For using messenger you should login')
		// }
	}, [])

	const HandleTextToUser = async (connectionId: string) => {
		try {
			connection?.invoke('CreateGroup', connectionId)
		} catch (error) {
			console.log(error)
		}
	}

	const NewChatButtonHandler = async () => {
		try {
			const res = await fetch(`${import.meta.env.VITE_API_URL}chat`, {
				method: 'POST',
				body: JSON.stringify({ Login: login, Name: 'help' }),
				headers: { 'Content-Type': 'application/json' },
			})

			if (res.ok) {
				toast.success('Group create successfully')
			}
		} catch (err) {
			toast.error('Something went wrong')
		}
	}

	return (
		<>
			<div className='overflow-hidden bg'>
				<div className='glass overflow-hidden  shadow-lg max-w-[1300px] w-[80vw] h-[70vh] grid grid-cols-10'>
					<div className='col-span-3 border-r border-gray-500'>
						<h1 className='text-[30px] p-4'>Your vibes</h1>
						<Button
							onClick={toggleSearchPopup}
							colorScheme='blue'
							className='p-4 mx-4'
						>
							New chat
						</Button>
						<div className='h-[60vh] flex flex-col overflow-y-scroll divHideScroll'>
							{chats.map((chat, i) => {
								return (
									<div
										onClick={() => HandleChatClick(chat.name)}
										className={`px-2 py-4 text-[22px] ${
											i === 0 ? '' : 'border-t  border-gray-500'
										}`}
										key={i}
									>
										<div>{chat.name}</div>
									</div>
								)
							})}
						</div>
					</div>
					<div className='col-span-5 '>
						{currentChat === undefined ? (
							<div>please choose chat</div>
						) : (
							<div>
								<h1 className='text-[30px] p-4'>{currentChat?.name}</h1>
								<div className='h-[50vh] overflow-y-scroll divHideScroll px-4 flex gap-4 flex-col'>
									{currentChat?.messages?.map((msg, i) => {
										return (
											<div
												className={`${
													msg.author === login
														? 'place-self-end bg-blue-500'
														: 'bg-blue-950 place-self-start'
												} px-3 py-2 rounded-lg shadow-xl`}
												key={i}
											>
												{msg.message}
											</div>
										)
									})}
								</div>
								<div className='px-10 mt-5'>
									<Form
										chatName={currentChat.name}
										connection={connection}
									/>
								</div>
							</div>
						)}
					</div>
					<div className='col-span-2 border-l border-gray-500'>
						<h1 className='text-[30px] p-4'>Users online:</h1>
						<div className='h-[60vh] overflow-y-scroll divHideScroll'>
							{onlineUsers?.map((user, i) => {
								return (
									<div
										key={i}
										className='flex items-center justify-between gap-2 p-3 m-4 border border-gray-500 rounded-xl'
									>
										<div className='flex items-center gap-2'>
											<img
												src={userIcon}
												className='aspect-square h-[30px]'
												alt=''
											/>
											<div>{user.login}</div>
										</div>
										<Button
											className='px-3 py-1'
											colorScheme='blue'
											size='small'
											onClick={() =>
												HandleTextToUser(user.connectionId)
											}
										>
											Text
										</Button>
									</div>
								)
							})}
						</div>
					</div>
				</div>
			</div>
		</>
	)
}

export default ChatRoom
