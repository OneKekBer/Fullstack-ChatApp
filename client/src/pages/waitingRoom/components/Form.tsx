import { useState } from 'react'
import { Button, Input } from '@chakra-ui/react'
import { useNavigate } from 'react-router-dom'
import { IJoinChatData } from '../../../interfaces/IJoinChatData'

interface FormProps {
	JoinChat: (joinChatData: IJoinChatData) => Promise<void>
}

const Form: React.FC<FormProps> = ({ JoinChat }) => {
	const [name, setName] = useState('')
	const [groupName, setGroupName] = useState('')

	const navigate = useNavigate()

	const handleSubmitForm = (e: React.FormEvent<HTMLFormElement>) => {
		e.preventDefault()
		JoinChat({ GroupName: groupName, Name: name })
		navigate(`/${groupName}`)
	}

	return (
		<form
			onSubmit={handleSubmitForm}
			className='flex flex-col gap-3'
			action=''
		>
			<Input
				isInvalid={false}
				placeholder='Enter your name'
				value={name}
				onChange={e => setName(e.target.value)}
				type='text'
				size='md'
			/>
			<Input
				placeholder='Enter chat name'
				value={groupName}
				onChange={e => setGroupName(e.target.value)}
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
