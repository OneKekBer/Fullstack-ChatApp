import { Button } from '@chakra-ui/react'
import IChat from 'interfaces/IChat'
import React from 'react'

interface ChatListProps {
	chats: IChat[]
	toggleCreatePopup: () => void
	toggleSearchPopup: () => void
	handleChatClick: (chatName: string) => Promise<void>
}

const ChatList: React.FC<ChatListProps> = ({
	chats,
	toggleCreatePopup,
	toggleSearchPopup,
	handleChatClick,
}) => {
	return (
		<div className=''>
			<div className='flex gap-5'>
				<Button onClick={toggleCreatePopup} colorScheme='blue' className=''>
					New chat
				</Button>

				<Button onClick={toggleSearchPopup} colorScheme='blue' className=''>
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
	)
}

export default ChatList
