import { useAppSelector } from 'store/hooks'
import { IJoinChatData } from '../../interfaces/IJoinChatData'

import Form from './components/Form'

interface WaitingRoom {
	JoinChat: (joinChatData: IJoinChatData) => Promise<void>
}

const WaitingRoom: React.FC<WaitingRoom> = ({ JoinChat }) => {
	const rooms = useAppSelector(state => state.rooms.rooms)

	console.log(rooms)

	return (
		<div className='flex items-center justify-center w-screen h-screen bg-gray-300 '>
			<div className='bg-gray-100 shadow-lg w-[500px] h-[300px] p-[40px]'>
				<h1 className='text-[30px] text-center font-semibold mb-5'>
					Welcome into VIBEr!
				</h1>
				<Form JoinChat={JoinChat} />
				<div>Your last rooms:</div>
			</div>
		</div>
	)
}

export default WaitingRoom
