import { Button, Input } from '@chakra-ui/react'
import IChat from 'interfaces/IChat'
import React, { useState } from 'react'
import loop from 'icons/loop/black.png'
import plus from 'icons/plus/black.png'
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
	const [filter, setFilter] = useState('')

	const filteredChat = chats.filter(chat =>
		chat.name.toLowerCase().includes(filter.toLowerCase())
	)

	return (
		<div className='relative'>
			<section className='pr-5 mt-4'>
				<Input
					value={filter}
					onChange={e => {
						setFilter(e.target.value)
					}}
					placeholder='Sort chats'
					className='text-p'
				/>
				<div className='flex mt-4 bottom-0 left-0 gap-5'>
					<Button
						className='font-light rounded-full w-[50px] h-[50px]'
						onClick={toggleCreatePopup}
						colorScheme='yellow'
					>
						<img
							className='aspect-square object-contain '
							src={plus}
							alt=''
						/>
					</Button>

					<Button
						className='font-light rounded-full w-[50px] h-[50px]'
						onClick={toggleSearchPopup}
						colorScheme='yellow'
					>
						<img
							className='aspect-square object-contain '
							src={loop}
							alt=''
						/>
					</Button>
				</div>
				<div className='max-h-[50vh] flex flex-col mt-4 overflow-y-scroll'>
					{filteredChat.map((chat, i) => {
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
			</section>
		</div>
	)
}

export default ChatList
