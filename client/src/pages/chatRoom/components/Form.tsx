import { useState, FormEvent } from 'react' // Import FormEvent from 'react'
import { Button, Input } from '@chakra-ui/react'
import { HubConnection } from '@microsoft/signalr'

const Form = ({
	chatName,
	connection,
}: {
	chatName: string | undefined
	connection: HubConnection | undefined
}) => {
	const [message, setMessage] = useState('')

	const handleSubmitForm = async (e: FormEvent<HTMLFormElement>) => {
		e.preventDefault()
		await connection
			?.invoke('SendMessage', chatName, message)
			.catch(err => console.log(err))
		setMessage('')
	}

	return (
		<form
			onSubmit={handleSubmitForm}
			className='flex gap-3 flex-col'
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
