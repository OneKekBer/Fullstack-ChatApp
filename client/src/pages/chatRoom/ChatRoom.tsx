import { HubConnection } from '@microsoft/signalr'
import { useNavigate, useParams } from 'react-router-dom'
import Form from './components/Form'

import { IMessage } from 'interfaces/IMessage'
import Error from 'pages/error/Error'

interface ChatRoomProps {
	connection: HubConnection | undefined
	chat: IMessage[]
}

const ChatRoom: React.FC<ChatRoomProps> = ({ connection, chat }) => {
	const { chatName } = useParams()
	const navigate = useNavigate()
	return (
		<>
			{connection ? (
				<div className='flex items-center justify-center w-screen h-screen bg-gray-300'>
					<div className='bg-gray-100 shadow-lg w-[600px] h-[600px] p-[40px]'>
						<div className='flex justify-between'>
							<h1 className='mb-2'>ChatRoom:{chatName}</h1>
							<div
								className='cursor-pointer'
								onClick={() => navigate('/')}
							>
								exit
							</div>
						</div>
						<div className='h-[70%] overflow-y-scroll mb-4'>
							{chat.length != 0 ? (
								<div className='flex flex-col mb-5 gap-y-2'>
									{chat.map((message, i) => {
										return (
											<div
												key={i}
												className={`px-2 ${
													message.Author == 'Admin'
														? 'bg-gray-500'
														: 'bg-blue-300'
												}  rounded-[10px] shadow-xl  h-auto text-white`}
											>
												{message.Author}:<br />
												{message.Message}
											</div>
										)
									})}
								</div>
							) : (
								<div>there are no messages</div>
							)}
						</div>
						<Form connection={connection} />
					</div>
				</div>
			) : (
				<Error />
			)}
		</>
	)
}

export default ChatRoom
