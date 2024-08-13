import React from 'react'
import Form from './Form'
import Message from './Message'
import IChat from 'interfaces/IChat'
import { HubConnection } from '@microsoft/signalr'

interface CurrentChatComponentProps {
	currentChat: IChat | undefined
	messagesContainerRef: React.RefObject<HTMLDivElement>
	connection: HubConnection | undefined
	messagesEndRef: React.RefObject<HTMLDivElement>
	login: string
}

const CurrentChatComponent: React.FC<CurrentChatComponentProps> = ({
	currentChat,
	messagesContainerRef,
	messagesEndRef,
	connection,
	login,
}) => {
	return (
		<div>
			{currentChat === undefined ? (
				<div>please choose chat</div>
			) : (
				<div>
					<p className='text-[30px] ml-5'>{currentChat?.name}</p>
					<h4 className='text-[15px] text-blue-300 ml-5'>
						{currentChat?.connectionIds?.length == 0 ? (
							<div></div>
						) : (
							<div>
								{currentChat?.connectionIds?.length} users online
							</div>
						)}
					</h4>
					<div
						ref={messagesContainerRef}
						className='h-[50vh] overflow-y-scroll divHideScroll px-4 flex gap-4 flex-col'
					>
						{currentChat?.messages?.map((msg, i) => {
							return <Message key={i} message={msg} login={login} />
						})}
						<div ref={messagesEndRef} />
					</div>
					<div className='mt-5 md:px-10'>
						<Form chatName={currentChat.name} connection={connection} />
					</div>
				</div>
			)}
		</div>
	)
}

export default CurrentChatComponent