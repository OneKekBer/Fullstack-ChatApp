import { useState } from 'react'
import { Button, Input } from '@chakra-ui/react'
import { useNavigate } from 'react-router-dom'
import { SuccessToast } from 'src/common/toasts/SuccessToast'

interface FormProps {
	FetchRegister: (Login: string, Password: string) => Promise<void>
}

const Form: React.FC<FormProps> = ({ FetchRegister }) => {
	const [login, setLogin] = useState('')
	const [password, setPassword] = useState('')
	const navigate = useNavigate()

	const handleSubmitForm = (e: React.FormEvent<HTMLFormElement>) => {
		e.preventDefault()
		FetchRegister(login, password)
	}

	return (
		<form
			onSubmit={handleSubmitForm}
			className='flex flex-col gap-3'
			action=''
		>
			<Input
				isInvalid={false}
				placeholder='Enter your login'
				value={login}
				onChange={e => setLogin(e.target.value)}
				type='text'
				size='md'
			/>
			<Input
				placeholder='Enter password'
				value={password}
				onChange={e => setPassword(e.target.value)}
				type='text'
				size='md'
			/>
			<Button type='submit' colorScheme='brand'>
				Submit
			</Button>
		</form>
	)
}

export default Form