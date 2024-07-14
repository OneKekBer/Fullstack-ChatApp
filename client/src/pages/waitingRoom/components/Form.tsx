import { useState } from 'react'
import { Button, Input } from '@chakra-ui/react'
import { useNavigate } from 'react-router-dom'

const Form = ({
	JoinChat,
}: {
	JoinChat: (chatName: string, name: string) => Promise<void>
}) => {
	const [name, setName] = useState('')
	const [groupName, setGroupName] = useState('')

	const navigate = useNavigate()

	const handleSubmitForm = (e: React.FormEvent<HTMLFormElement>) => {
		e.preventDefault()
		JoinChat(groupName, name)
		navigate(`/${groupName}`)
	}

	return (
		<form
			onSubmit={handleSubmitForm}
			className='flex gap-3 flex-col'
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
			<Button type='submit' colorScheme='green'>
				Submit
			</Button>
		</form>
	)
}

export default Form
