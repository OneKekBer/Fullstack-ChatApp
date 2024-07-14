import { HubConnection } from '@microsoft/signalr'
import { useParams } from 'react-router-dom'
import Form from './components/Form'
import Error from '../error/Error'

const ChatRoom = ({
	connection,
	chat,
}: {
	connection: HubConnection | undefined
	chat: string[]
}) => {
	const { chatName } = useParams()

	return (
		<>
			{connection ? (
				<div className='w-screen h-screen bg-gray-300 flex justify-center items-center'>
					<div className='bg-gray-100 shadow-lg w-[600px] h-[600px] p-[40px]'>
						<div>ChatRoom:</div>
						<div>
							{chat.length != 0 ? (
								<div>
									{chat.map((message, i) => {
										return (
											<div
												key={i}
												className={`${i % 2 == 0 ? 'bg-white' : ''}
                                       p-1
                                       `}
											>
												{message}
											</div>
										)
									})}
								</div>
							) : (
								<div>there are no messages</div>
							)}
						</div>
						<Form chatName={chatName} connection={connection} />
					</div>
				</div>
			) : (
				<Error />
			)}
		</>
	)
}

export default ChatRoom
