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
				<div className='w-full min-h-[50vh] text-[30px] text-p flex justify-center items-center'>
					please choose chat
				</div>
			) : (
				<div className=''>
					<p className='text-[35px] text-h1 ml-5'>{currentChat?.name}</p>
					<h4 className='text-[15px] text-p mb-2 ml-5'>
						{currentChat?.connectionIds?.length == 0 &&
							currentChat?.connectionIds && (
								<div>
									{currentChat?.connectionIds?.length} users online
								</div>
							)}
					</h4>
					<div
						ref={messagesContainerRef}
						className='h-[50vh] overflow-y-scroll bg-slate-100 divHideScroll px-4 flex gap-4 flex-col'
					>
						{currentChat?.messages?.map((msg, i) => {
							return <Message key={i} message={msg} login={login} />
						})}
						<div ref={messagesEndRef} />
					</div>
					<div className='mt-5 md:px-2'>
						<Form chatName={currentChat.name} connection={connection} />
					</div>
				</div>
			)}
		</div>
	)
}

export default CurrentChatComponent
