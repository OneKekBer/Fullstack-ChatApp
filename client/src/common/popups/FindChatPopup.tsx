import { Button, Input } from '@chakra-ui/react'
import { HubConnection } from '@microsoft/signalr'
import CloseButton from 'common/buttons/CloseButton'
import IChat from 'interfaces/IChat'
import { useEffect, useState } from 'react'
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
	const [chatInput, setChatInput] = useState('')
	const login = useAppSelector(state => state.user.Login)
	const [references, setReferences] = useState<IChat[]>([])

	const joinChat = (chatName: string) => {
		try {
			connection
				?.invoke('JoinChat', { login: login, chatName: chatName })
				.then(item => console.log(item))
			// toast.success(`You joined chat ${chatName}`)
			toggleSearchPopup()
		} catch (error) {
			console.error('Fetch error:', error)
		}
	}

	const findChat = async (chatName: string) => {
		try {
			const res = await fetch(`${import.meta.env.VITE_API_URL}chat/find`, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json',
				},
				body: JSON.stringify({ chatName: chatName }),
			})

			if (!res.ok) {
				throw new Error('smth went wrong')
			}

			const references = await res.json()
			setReferences(references)
			console.log('references', references)
		} catch (error) {
			console.error('Fetch error:', error)
		}
	}

	useEffect(() => {
		findChat('')
	}, [isSearchPopupOpen])

	const formSubmit = (e: React.FormEvent) => {
		e.preventDefault()
		findChat(chatInput)
	}

	return (
		<div className={`popup__bg ${isSearchPopupOpen ? 'flex' : 'hidden'}`}>
			<div className='glass relative w-[90vw] md:w-[500px] bg-blue-100 p-5'>
				<div className='flex justify-end w-full'>
					<CloseButton onClick={toggleSearchPopup} />
				</div>
				<form onSubmit={formSubmit} className='grid grid-cols-3 gap-2'>
					<Input
						className='col-span-2'
						value={chatInput}
						onChange={e => setChatInput(e.target.value)}
					/>
					<Button type='submit' className='col-span-1' colorScheme='blue'>
						Find
					</Button>
				</form>
				<div className='mt-4'>
					{references.length === 0 ? (
						<div>there are no chats....</div>
					) : (
						<div className='flex max-h-[200px] divHideScroll overflow-hidden  overflow-y-visible flex-col gap-4'>
							{references.map((item, i) => {
								return (
									<div
										className='flex items-center w-full gap-5 rounded-2xl'
										key={i}
									>
										<div className='text-[20px]'>{item.name}</div>
										<Button
											className='px-4 py-1'
											size='m'
											colorScheme='blue'
											onClick={() => {
												joinChat(item.name)
												setReferences([])
												setChatInput('')
											}}
										>
											join
										</Button>
									</div>
								)
							})}
						</div>
					)}
				</div>
			</div>
		</div>
	)
}

export default FindChatPopup
