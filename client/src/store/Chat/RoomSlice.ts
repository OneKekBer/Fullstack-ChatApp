import { createSlice } from '@reduxjs/toolkit'
import type { PayloadAction } from '@reduxjs/toolkit'
import IChat from 'interfaces/IChat'

// Define a type for the slice state
interface CounterState {
	chats: IChat[]
}

const initialState: CounterState = {
	chats: [],
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

		SetChats: (state, action: PayloadAction<IChat[]>) => {
			state.chats = []
			state.chats = action.payload
		},
		UpdateChat: (state, action: PayloadAction<IChat>) => {
			const chatIndex = state.chats.findIndex(
				chat => chat.name === action.payload.name
			)

			if (chatIndex !== -1) {
				state.chats[chatIndex] = action.payload
			}
		},
	},
})

export const { CreateRoom, UpdateChat, SetChats } = roomSlice.actions

export default roomSlice.reducer
