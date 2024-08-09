import { Button } from '@chakra-ui/react'
import { useNavigate } from 'react-router-dom'

interface WaitingRoom {}

const WaitingRoom: React.FC<WaitingRoom> = () => {
	const navigate = useNavigate()

	return (
		<div className='bg'>
			<div className='glass w-[500px] h-[300px] p-[40px]'>
				<div className='text-center text-[40px]'>Welcome To Viber</div>
				<div className='flex justify-center gap-2'>
					<Button
						size='lg'
						onClick={() => {
							navigate('/login')
						}}
					>
						Login
					</Button>
					<Button
						size='lg'
						onClick={() => {
							navigate('/register')
						}}
					>
						Register
					</Button>
				</div>
			</div>
		</div>
	)
}

export default WaitingRoom
