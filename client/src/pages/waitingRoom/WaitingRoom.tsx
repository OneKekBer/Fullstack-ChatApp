import Form from './components/Form'

const WaitingRoom = ({
	JoinChat,
}: {
	JoinChat: (chatName: string, name: string) => Promise<void>
}) => {
	return (
		<div className='w-screen h-screen bg-gray-300 flex justify-center items-center '>
			<div className='bg-gray-100 shadow-lg w-[500px] h-[300px] p-[40px]'>
				<h1 className='text-[30px] text-center font-semibold mb-5'>
					Welcome into VIBEr!
				</h1>
				<Form JoinChat={JoinChat} />
			</div>
		</div>
	)
}

export default WaitingRoom
