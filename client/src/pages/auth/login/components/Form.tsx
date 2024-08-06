import { useState } from 'react'
import { Button, Input } from '@chakra-ui/react'

interface FormProps {
	FetchLogin: (Login: string, Password: string) => Promise<void>
}

const Form: React.FC<FormProps> = ({ FetchLogin }) => {
	const [login, setLogin] = useState('')
	const [password, setPassword] = useState('')

	const handleSubmitForm = (e: React.FormEvent<HTMLFormElement>) => {
		e.preventDefault()
		FetchLogin(login, password)
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
