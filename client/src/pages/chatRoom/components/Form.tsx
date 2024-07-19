import { useState, FormEvent } from 'react' // Import FormEvent from 'react'
import { Button, Input } from '@chakra-ui/react'
import { HubConnection } from '@microsoft/signalr'

import sendIcon from 'public/icons/send/send.png'

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
			className='grid grid-cols-7 gap-3'
			action=''
		>
			<Input
				placeholder='Enter your message'
				value={message}
				onChange={e => setMessage(e.target.value)}
				type='text'
				size='md'
				className='col-span-6'
			/>
			<Button type='submit' className='col-span-1' colorScheme='blue'>
				<img className=' h-[30px]' src={sendIcon} alt='' />
			</Button>
		</form>
	)
}

export default Form
