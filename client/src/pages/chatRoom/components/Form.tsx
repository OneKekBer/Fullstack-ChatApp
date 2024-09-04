import { useState, FormEvent } from 'react' // Import FormEvent from 'react'
import { HubConnection } from '@microsoft/signalr'
import { useAppSelector } from 'store/hooks'

interface FormProps {
	// chatName: string | undefined
	connection: HubConnection | undefined
	chatName: string
}

const Form: React.FC<FormProps> = ({ connection, chatName }) => {
	const login = useAppSelector(state => state.user.Login)
	const [message, setMessage] = useState('')

	const handleSubmitForm = async (e: FormEvent<HTMLFormElement>) => {
		e.preventDefault()
		console.log(login + ' ' + message + ' ' + chatName)
		await connection
			?.invoke('SendMessage', login, message, chatName)
			.catch(err => console.log(err))
		setMessage('')
	}

	return (
		<form onSubmit={handleSubmitForm} className='' action=''>
			<input
				placeholder='Enter your message'
				value={message}
				onChange={e => setMessage(e.target.value)}
				type='text'
				className='w-full rounded-xl px-4 text-p border-slate-600 border h-[40px]'
			/>
			{/* <Button type='submit' className='' colorScheme='blue'>
				<img className=' h-[30px]' src={sendIcon} alt='' />
			</Button> */}
		</form>
	)
}

export default Form
