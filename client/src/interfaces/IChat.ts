import { IMessage } from './IMessage'

export default interface IChat {
	messages: IMessage[]
	userIds: string[]
	name: string
}
