import { createSlice } from '@reduxjs/toolkit'
import type { PayloadAction } from '@reduxjs/toolkit'
import IChat from 'interfaces/IChat'

// Define a type for the slice state
interface CounterState {
	chats: IChat[]
}

const initialState: CounterState = {
	chats: [
		// {
		// 	messages: [
		// 		{ author: 'Anton', message: 'hello' },
		// 		{ author: 'Anton', message: 'hello' },
		// 		{
		// 			author: 'Ilya',
		// 			message:
		// 				'hello, hello hello hello hello hello hello hello hello hello hello',
		// 		},
		// 		{ author: 'Anton', message: 'hello hello hello hello hello' },
		// 		{ author: 'Anton', message: 'hello' },
		// 		{ author: 'Ilya', message: 'hello' },
		// 		{ author: 'Anton', message: 'hello' },
		// 		{ author: 'Anton', message: 'hello' },
		// 		{
		// 			author: 'Anton',
		// 			message:
		// 				'hello hello hello hello hello hello hello hello hello hello hello hello hello hello hello hello hello hello hello hello hello hello hello hello hello',
		// 		},
		// 		{ author: 'Anton', message: 'hello' },
		// 		{ author: 'Anton', message: 'hello' },
		// 		{
		// 			author: 'Ilya',
		// 			message:
		// 				'hello, hello hello hello hello hello hello hello hello hello hello',
		// 		},
		// 		{ author: 'Anton', message: 'hello hello hello hello hello' },
		// 		{ author: 'Anton', message: 'hello' },
		// 		{ author: 'Ilya', message: 'hello' },
		// 		{ author: 'Anton', message: 'hello' },
		// 		{ author: 'Anton', message: 'hello' },
		// 		{ author: 'Anton', message: 'hello hello hello hello hello' },
		// 	],
		// 	name: 'Anton',
		// 	users: [],
		// },
		// { messages: [], name: 'Ilya', users: [] },
		// { messages: [], name: '3', users: [] },
		// { messages: [], name: '4', users: [] },
		// { messages: [], name: '5', users: [] },
		// { messages: [], name: '6', users: [] },
		// { messages: [], name: '7', users: [] },
		// { messages: [], name: '8', users: [] },
		// { messages: [], name: '9', users: [] },
		// { messages: [], name: '10', users: [] },
		// { messages: [], name: '11', users: [] },
		// { messages: [], name: '12', users: [] },
		// { messages: [], name: '13', users: [] },
		// { messages: [], name: '14', users: [] },
		// { messages: [], name: '15', users: [] },
	],
}

export const roomSlice = createSlice({
	name: 'chat',
	// `createSlice` will infer the state type from the `initialState` argument
	initialState,
	reducers: {
		// AddMessageToRoom: (
		// 	state,
		// 	action: PayloadAction<AddMessageToRoomProps>
		// ) => {
		// 	state.rooms.map(room => {
		// 		if (room.RoomName == action.payload.RoomName)
		// 			room?.Messages?.push(action.payload.Message)
		// 	})
		// 	action.payload.RoomName
		// },
		CreateRoom: (state, action: PayloadAction<IChat>) => {
			state.chats.push(action.payload)
		},
	},
})

export const { CreateRoom } = roomSlice.actions

export default roomSlice.reducer
