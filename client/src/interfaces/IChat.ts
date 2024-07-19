import { IMessage } from './IMessage'
import IUser from './IUser'

export default interface IChat {
	messages: IMessage[]
	users: IUser[]
	name: string
}
