import { Button, Input, InputGroup, InputRightElement } from '@chakra-ui/react'
import { HubConnection } from '@microsoft/signalr'
import IChat from 'interfaces/IChat'
import { useEffect, useState } from 'react'
import { useAppSelector } from 'store/hooks'
import close from 'icons/close/black.png'

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
			<div className='bg-bg relative w-[90vw] md:w-[500px] p-5'>
				<form onSubmit={formSubmit} className='grid grid-cols-7 gap-2'>
					<InputGroup className='col-span-6'>
						<Input
							className=''
							value={chatInput}
							placeholder='Enter chat name'
							onChange={e => setChatInput(e.target.value)}
						/>
						<InputRightElement>
							<div className='w-[30px] h-[30px] cursor-pointer'>
								<img
									src={close}
									alt='close'
									onClick={toggleSearchPopup}
								/>
							</div>
						</InputRightElement>
					</InputGroup>

					<Button
						type='submit'
						className='col-span-1'
						colorScheme='yellow'
					>
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
											colorScheme='yellow'
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
