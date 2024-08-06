import { IMessage } from 'interfaces/IMessage'
import React from 'react'

interface MessageProps {
	message: IMessage
	login: string
}

const Message: React.FC<MessageProps> = ({ message, login }) => {
	const convertData = (data: string) => {
		const utcDate = new Date(data)
		const nowDate = new Date()

		const timeDifference = nowDate.getTime() - utcDate.getTime()

		// Переводим разницу в минуты
		const minutesDifference = Math.floor(timeDifference / (1000 * 60))

		const localDate = utcDate.toLocaleString()

		if (minutesDifference < 1) {
			return 'right now'
		} else if (minutesDifference < 15) {
			return `${minutesDifference} minutes ago`
		} else {
			return localDate // Здесь 'ru-RU' можно заменить на ваш локальный формат
		}
	}
	return (
		<div
			className={`${
				message.authorLogin === login
					? 'place-self-end text-right'
					: ' place-self-start text-left'
			} `}
		>
			<div>{message.authorLogin == login ? 'you' : message.authorLogin}</div>
			<p
				className={`${
					message.authorLogin === login ? ' bg-blue-500' : 'bg-blue-950 '
				} px-3 py-2 rounded-lg relative shadow-xl`}
			>
				{message.text}
			</p>
			<div className={`text-slate-300`}>
				{convertData(message.createdAt)}
			</div>
		</div>
	)
}

export default Message
