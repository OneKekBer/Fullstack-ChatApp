import { Button, Input } from '@chakra-ui/react'
import CloseButton from 'common/buttons/CloseButton'
import { useState } from 'react'
import { toast } from 'react-toastify'
import { useAppSelector } from 'store/hooks'

interface SearchChatPopupProps {
	toggleSearchPopup: () => void
	isSearchPopupOpen: boolean
}
const CreateChatPopup: React.FC<SearchChatPopupProps> = ({
	isSearchPopupOpen,
	toggleSearchPopup,
}) => {
	const [chatName, setChatName] = useState('')
	const login = useAppSelector(state => state.user.Login)

	const createChat = async (chatName: string) => {
		try {
			const res = await fetch(`${import.meta.env.VITE_API_URL}chat`, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json',
				},
				body: JSON.stringify({ Login: login, Name: chatName }),
			})
			console.log(`${import.meta.env.VITE_API_URL}chat`)
			console.log(login + '' + chatName)

			if (!res.ok) {
				throw new Error('Cannot register')
			}

			const responseText = await res.text()
			console.log('Registration successful:', responseText)

			toast.success('You create chat successfully')
		} catch (error) {
			console.error('Fetch error:', error)
		}
	}

	const formSubmit = (e: React.FormEvent) => {
		e.preventDefault()
		createChat(chatName)
		toggleSearchPopup()
	}

	return (
		<div className={`popup__bg ${isSearchPopupOpen ? 'flex' : 'hidden'}`}>
			<div className='glass w-[500px]  bg-blue-100 p-5'>
				<div className='flex justify-end w-full'>
					<CloseButton onClick={toggleSearchPopup} />
				</div>
				<form
					onSubmit={formSubmit}
					className='grid grid-cols-3 gap-2'
					action=''
				>
					<Input
						className='col-span-2'
						value={chatName}
						onChange={e => setChatName(e.target.value)}
					/>
					<Button type='submit' className='col-span-1' colorScheme='blue'>
						Send
					</Button>
				</form>
			</div>
		</div>
	)
}

export default CreateChatPopup
