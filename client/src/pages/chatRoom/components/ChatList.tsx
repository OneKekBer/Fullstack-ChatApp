import { Button } from '@chakra-ui/react'
import IChat from 'interfaces/IChat'
import React from 'react'

interface ChatListProps {
	chats: IChat[]
	toggleCreatePopup: () => void
	toggleSearchPopup: () => void
	handleChatClick: (chatName: string) => Promise<void>
	currentChat: IChat | undefined
}

const ChatList: React.FC<ChatListProps> = ({
	chats,
	toggleCreatePopup,
	toggleSearchPopup,
	handleChatClick,
	currentChat,
}) => {
	return (
		<div className=''>
			<div className='flex gap-5'>
				<Button
					className='font-light'
					onClick={toggleCreatePopup}
					colorScheme='yellow'
				>
					New chat
				</Button>

				<Button
					className='font-light'
					onClick={toggleSearchPopup}
					colorScheme='yellow'
				>
					Find chat
				</Button>
			</div>
			<div className='h-[60vh] mt-4 flex flex-col overflow-y-scroll divHideScroll'>
				{chats.map((chat, i) => {
					return (
						<div
							onClick={() => {
								handleChatClick(chat.name)
							}}
							className={`px-2 hover:bg-slate-50 hover:text-button duration-200 cursor-pointer py-4 text-[22px] ${
								currentChat?.name == chat.name
									? 'text-button bg-slate-50'
									: ''
							}`}
							key={i}
						>
							{chat.name}
						</div>
					)
				})}
			</div>
		</div>
	)
}

export default ChatList
