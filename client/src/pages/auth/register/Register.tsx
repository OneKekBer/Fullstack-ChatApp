import { useNavigate } from 'react-router-dom'
import { useState } from 'react'
import Form from './components/Form'
import SuccessToast from 'src/common/toasts/SuccessToast'
import { toast } from 'react-toastify'

const Register = () => {
	const navigate = useNavigate()
	const [messageError, setMessageError] = useState('')

	const FetchRegister = async (Login: string, Password: string) => {
		try {
			const res = await fetch(
				`${import.meta.env.VITE_API_URL}auth/register`,
				{
					method: 'POST',
					headers: {
						'Content-Type': 'application/json',
					},
					body: JSON.stringify({ Login: Login, Password: Password }),
				}
			)

			if (!res.ok) {
				const errorText = await res.text()
				setMessageError(errorText)
				throw new Error('Cannot register')
			}

			const responseText = await res.text()
			console.log('Registration successful:', responseText)

			toast.success('You registered successfully')
			navigate('/login')
		} catch (error) {
			console.error('Fetch error:', error)
			setMessageError(
				'An unexpected error occurred. Please try again later.'
			)
		}
	}

	return (
		<div className='bg'>
			<div className='glass w-[500px] h-[600px]'>
				<h1 className='text-center'>Register</h1>
				<div className='text-red-500'>{messageError}</div>
				<Form FetchRegister={FetchRegister} />
				<div
					className='font-light text-blue-300 underline cursor-pointer'
					onClick={() => {
						navigate('/login')
					}}
				>
					you already have account?
				</div>
			</div>
		</div>
	)
}

export default Register
