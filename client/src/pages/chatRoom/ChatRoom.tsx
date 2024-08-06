import { HubConnection } from '@microsoft/signalr'
import { useNavigate } from 'react-router-dom'
import Form from './components/Form'
import { useEffect, useRef, useState } from 'react'
import { useAppSelector } from 'store/hooks'
import { toast } from 'react-toastify'
import { Button } from '@chakra-ui/react'
import IChat from 'interfaces/IChat'
import Message from './components/Message'

interface ChatRoomProps {
	connection: HubConnection | undefined
	toggleCreatePopup: () => void
	toggleSearchPopup: () => void
}

const ChatRoom: React.FC<ChatRoomProps> = ({
	connection,
	toggleCreatePopup,
	toggleSearchPopup,
}) => {
	const navigate = useNavigate()
	const login = useAppSelector(state => state.user.Login)
	const chats = useAppSelector(state => state.rooms.chats)
	const [currentChat, setCurrentChat] = useState<IChat | undefined>(undefined)
	const messagesEndRef = useRef<HTMLDivElement>(null)
	const messagesContainerRef = useRef<HTMLDivElement>(null)

	const findChatByName = (chatName: string | undefined): IChat | undefined => {
		return chats?.find(chat => chat.name === chatName)
	}

	const handleChange = () => {
		const newChat: IChat | undefined = findChatByName(currentChat?.name)

		if (newChat?.messages?.length !== currentChat?.messages.length)
			setCurrentChat(newChat)
		if (messagesEndRef.current && messagesContainerRef.current) {
			messagesContainerRef.current.scrollTop =
				messagesEndRef.current.offsetTop
		}
	}

	handleChange()

	const handleChatClick = async (chatName: string) => {
		const chat = findChatByName(chatName)
		setCurrentChat(chat)
	}

	useEffect(() => {
		const fetchChatMessages = async () => {
			try {
				const chat = findChatByName(currentChat?.name)
				await connection?.invoke('GetChatMessages', {
					chatName: currentChat?.name,
				})
				setCurrentChat(chat)
			} catch (error) {
				console.error('Error fetching chat messages:', error)
			}
		}

		if (currentChat?.name) {
			fetchChatMessages()
		}
	}, [currentChat])

	useEffect(() => {
		if (login === '' || login === undefined) {
			navigate('/login')
			toast.info('For using messenger you should login')
		}
	}, [])

	return (
		<>
			<div className='overflow-hidden bg'>
				<div className='glass overflow-hidden shadow-lg max-w-[1300px] w-[80vw] h-[70vh] p-5 grid grid-cols-10'>
					<div className='col-span-3 border-r  border-gray-500'>
						<h1 className='text-[30px] mb-5 '>Your vibes</h1>
						<div className='flex gap-5'>
							<Button
								onClick={toggleCreatePopup}
								colorScheme='blue'
								className=''
							>
								New chat
							</Button>

							<Button
								onClick={toggleSearchPopup}
								colorScheme='blue'
								className=''
							>
								Find chat
							</Button>
						</div>
						<div className='h-[60vh] flex flex-col overflow-y-scroll divHideScroll'>
							{chats.map((chat, i) => {
								return (
									<div
										onClick={() => {
											handleChatClick(chat.name)
										}}
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
					<div className='col-span-7 '>
						{currentChat === undefined ? (
							<div>please choose chat</div>
						) : (
							<div>
								<h1 className='text-[30px] ml-5'>
									{currentChat?.name}
								</h1>
								<div
									ref={messagesContainerRef}
									className='h-[50vh] overflow-y-scroll divHideScroll px-4 flex gap-4 flex-col'
								>
									{currentChat?.messages?.map((msg, i) => {
										return (
											<Message key={i} message={msg} login={login} />
										)
									})}
									<div ref={messagesEndRef} />
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
				</div>
			</div>
		</>
	)
}

export default ChatRoom
