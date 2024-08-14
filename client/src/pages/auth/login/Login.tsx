import { useAppDispatch } from 'store/hooks'
import Form from './components/Form'
import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { LogIn } from 'store/User/UserSlice'
import { toast } from 'react-toastify'
import { SetChats } from 'store/Chat/RoomSlice'

interface LoginProps {
	ConnectToHub: (Login: string) => Promise<void>
}

const Login: React.FC<LoginProps> = ({ ConnectToHub }) => {
	const [messageError, setMessageError] = useState('')
	const dispatch = useAppDispatch()
	const navigate = useNavigate()
	const FetchLogin = async (Login: string, Password: string) => {
		try {
			const res = await fetch(`${import.meta.env.VITE_API_URL}auth/login`, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json', // Corrected header name
				},
				body: JSON.stringify({ Login, Password }),
			})

			const data = await res.json()
			console.log(data)
			if (!res.ok) {
				setMessageError(data.Error)
				return
			}
			console.log(data)
			navigate('/chat')
			ConnectToHub(Login) //сделать проверку на успешное подключение
			dispatch(LogIn(Login))
			dispatch(SetChats(data))
			toast.success('You loged in successfully')
		} catch (error) {
			console.error('Fetch error: ', error)
			setMessageError(
				'An unexpected error occurred. Please try again later.'
			)
		}
	}

	return (
		<div className='bg'>
			<div className='bg-bg w-[500px] h-[600px] p-5'>
				<h1 className='text-center text-[30px] mb-5'>Login</h1>
				<div className='text-red-500'>{messageError}</div>
				<Form FetchLogin={FetchLogin} />
				<div
					className='font-light text-p underline cursor-pointer'
					onClick={() => {
						navigate('/register')
					}}
				>
					you dont have account?
				</div>
			</div>
		</div>
	)
}

export default Login
