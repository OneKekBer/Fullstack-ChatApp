import { HubConnection } from '@microsoft/signalr'
import { createSlice } from '@reduxjs/toolkit'
import type { PayloadAction } from '@reduxjs/toolkit'

// Define a type for the slice state
interface CounterState {
	chat: string[]
	connection: HubConnection | null
}

// Define the initial state using that type
const initialState: CounterState = {
	chat: [],
	connection: null,
}

export const chatSlice = createSlice({
	name: 'chat',
	// `createSlice` will infer the state type from the `initialState` argument
	initialState,
	reducers: {
		addConnection: (state, action: PayloadAction<HubConnection>) => {
			state.connection = action.payload
		},

		addMessageInChat: (state, action: PayloadAction<string>) => {
			state.chat.push(action.payload)
		},
	},
})

export const { addMessageInChat } = chatSlice.actions

export default chatSlice.reducer
