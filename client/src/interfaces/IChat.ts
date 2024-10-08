import { IMessage } from './IMessage'

export default interface IChat {
	messages: IMessage[]
	name: string
	connectionIds: []
}
