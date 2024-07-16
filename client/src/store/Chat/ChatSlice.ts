import { createSlice } from '@reduxjs/toolkit'
import type { PayloadAction } from '@reduxjs/toolkit'
import { IMessage } from '../../interfaces/IMessage'

// Define a type for the slice state

interface CounterState {
	rooms: IMessage[]
}

// Define the initial state using that type
const initialState: CounterState = {
	rooms: [],
}

export const chatSlice = createSlice({
	name: 'chat',
	// `createSlice` will infer the state type from the `initialState` argument
	initialState,
	reducers: {
		// addConnection: (state, action: PayloadAction<HubConnection>) => {
		// 	state.connection = action.payload
		// },

		addMessageInChat: (state, action: PayloadAction<string>) => {
			state.chat.push(action.payload)
		},
	},
})

export const { addMessageInChat } = chatSlice.actions

export default chatSlice.reducer
