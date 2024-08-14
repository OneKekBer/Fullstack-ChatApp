import { HubConnection } from '@microsoft/signalr'
import { useNavigate } from 'react-router-dom'
import { useEffect, useRef, useState } from 'react'
import { useAppSelector } from 'store/hooks'
import IChat from 'interfaces/IChat'
import ChatList from './components/ChatList'
import CurrentChatComponent from './components/CurrentChatComponent'
import { toast } from 'react-toastify'

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

	console.log(chats[0] ? chats[0] : undefined)

	const messagesEndRef = useRef<HTMLDivElement>(null)
	const messagesContainerRef = useRef<HTMLDivElement>(null)

	const [isMobileMenuOpen, setIsMobileMenuOpen] = useState(false)

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
				<div className='bg-bg overflow-hidden shadow-lg max-w-[1300px] w-[90vw] h-[90vh] md:w-[80vw] md:h-[70vh] p-5 md:grid grid-cols-10'>
					<div
						onClick={() => {
							setIsMobileMenuOpen(!isMobileMenuOpen)
						}}
						className='absolute block cursor-pointer top-5 right-5 md:hidden'
					>
						menu
					</div>

					{/* mobile */}
					<section
						className={`${
							isMobileMenuOpen ? 'block' : 'hidden'
						} h-full md:hidden`}
					>
						<ChatList
							currentChat={currentChat}
							chats={chats}
							handleChatClick={handleChatClick}
							toggleCreatePopup={toggleCreatePopup}
							toggleSearchPopup={toggleSearchPopup}
						/>
					</section>

					{/* pc */}
					<section className='hidden md:col-span-3 md:block'>
						<ChatList
							currentChat={currentChat}
							chats={chats}
							handleChatClick={handleChatClick}
							toggleCreatePopup={toggleCreatePopup}
							toggleSearchPopup={toggleSearchPopup}
						/>
					</section>

					<section
						className={`${
							isMobileMenuOpen ? 'hidden' : 'block'
						} md:col-span-7 `}
					>
						<CurrentChatComponent
							connection={connection}
							messagesEndRef={messagesEndRef}
							messagesContainerRef={messagesContainerRef}
							login={login}
							currentChat={currentChat}
						/>
					</section>
				</div>
			</div>
		</>
	)
}

export default ChatRoom
