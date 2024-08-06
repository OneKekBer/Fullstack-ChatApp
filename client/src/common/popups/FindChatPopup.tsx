import { Button, Input } from '@chakra-ui/react'
import { HubConnection } from '@microsoft/signalr'
import { useState } from 'react'
import { toast } from 'react-toastify'
import { useAppSelector } from 'store/hooks'

interface SearchChatPopupProps {
	toggleSearchPopup: () => void
	isSearchPopupOpen: boolean
	connection: HubConnection | undefined
}
const FindChatPopup: React.FC<SearchChatPopupProps> = ({
	isSearchPopupOpen,
	toggleSearchPopup,
	connection,
}) => {
	const [chatName, setChatName] = useState('')
	const login = useAppSelector(state => state.user.Login)

	const findChat = async () => {
		try {
			connection?.invoke('JoinChat', { login: login, chatName: chatName })
			toast.success(`You joined chat ${chatName}`)
			toggleSearchPopup()
		} catch (error) {
			console.error('Fetch error:', error)
		}
	}

	const formSubmit = (e: React.FormEvent) => {
		e.preventDefault()
		setChatName('')
		findChat()
	}

	return (
		<div className={`popup__bg ${isSearchPopupOpen ? 'flex' : 'hidden'}`}>
			<div className='glass w-[500px]  bg-blue-100 p-5'>
				<div className='flex justify-end w-full'>
					<div onClick={toggleSearchPopup}>close</div>
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

export default FindChatPopup
