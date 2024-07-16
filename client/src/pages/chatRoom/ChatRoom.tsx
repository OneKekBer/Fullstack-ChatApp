import { HubConnection } from '@microsoft/signalr'
import { useNavigate } from 'react-router-dom'
import Form from './components/Form'
import { IMessage } from 'interfaces/IMessage'
import { useEffect } from 'react'
import { useAppSelector } from 'store/hooks'

interface ChatRoomProps {
	connection: HubConnection | undefined
	chat: IMessage[]
}

const ChatRoom: React.FC<ChatRoomProps> = ({ connection, chat }) => {
	const navigate = useNavigate()
	const login = useAppSelector(state => state.user.Login)
	const onlineUsers = useAppSelector(state => state.user.OnlineUsers)

	useEffect(() => {
		if (login === '') navigate('/login')
	}, [])

	return (
		<>
			<div className='bg'>
				<div className='glass shadow-lg w-[900px] h-[600px] p-[40px]'>
					<div>
						Users online:
						{onlineUsers.map((user, i) => {
							return (
								<div key={i}>
									<div>{user}</div>
								</div>
							)
						})}
					</div>
					<Form connection={connection} />
				</div>
			</div>
		</>
	)
}

export default ChatRoom
