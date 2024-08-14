import { Button } from '@chakra-ui/react'
import { useNavigate } from 'react-router-dom'

interface WaitingRoom {}

const WaitingRoom: React.FC<WaitingRoom> = () => {
	const navigate = useNavigate()

	return (
		<div className='bg'>
			<div className='bg-bg shadow-2xl w-[500px] h-[300px] p-[40px]'>
				<div className='text-center text-h1 text-[40px]'>
					Welcome To Viber
				</div>
				<div className='flex justify-center gap-2'>
					<Button
						colorScheme='yellow'
						style={{ color: 'var(--button-text)' }}
						className='text-buttonText'
						size='lg'
						onClick={() => {
							navigate('/login')
						}}
					>
						Login
					</Button>
					<Button
						style={{ color: 'var(--button-text)' }}
						colorScheme='yellow'
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
