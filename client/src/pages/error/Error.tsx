import { Button } from '@chakra-ui/react'
import { useNavigate } from 'react-router-dom'

const Error = () => {
	const navigate = useNavigate()
	return (
		<div className='w-screen h-screen bg-gray-300 flex justify-center items-center '>
			<div className='bg-gray-100 shadow-lg w-[500px] flex flex-col h-[300px] p-[40px]'>
				<h1 className='text-[30px] text-center font-semibold mb-5'>
					Something went wrong! sry
				</h1>
				<Button
					onClick={() => {
						navigate('/')
					}}
					colorScheme='blue'
				>
					Back to the waiting room
				</Button>
			</div>
		</div>
	)
}

export default Error
