import { useState, FormEvent } from 'react' // Import FormEvent from 'react'
import { Button, Input } from '@chakra-ui/react'
import { HubConnection } from '@microsoft/signalr'

interface FormProps {
	// chatName: string | undefined
	connection: HubConnection | undefined
}

const Form: React.FC<FormProps> = ({ connection }) => {
	const [message, setMessage] = useState('')

	const handleSubmitForm = async (e: FormEvent<HTMLFormElement>) => {
		e.preventDefault()
		await connection
			?.invoke('SendMessage', message)
			.catch(err => console.log(err))
		setMessage('')
	}

	return (
		<form
			onSubmit={handleSubmitForm}
			className='flex flex-col gap-3'
			action=''
		>
			<Input
				placeholder='Enter your message'
				value={message}
				onChange={e => setMessage(e.target.value)}
				type='text'
				size='md'
			/>
			<Button type='submit' colorScheme='green'>
				Submit
			</Button>
		</form>
	)
}

export default Form
