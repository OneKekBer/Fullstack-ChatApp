import { createSlice } from '@reduxjs/toolkit'
import type { PayloadAction } from '@reduxjs/toolkit'
import { IMessage } from '../../interfaces/IMessage'

// Define a type for the slice state

interface Room {
	RoomName: string
	Messages: IMessage[] | null
}

interface CounterState {
	rooms: Room[]
}

interface AddMessageToRoomProps {
	RoomName: string
	Message: IMessage
}

// Define the initial state using that type
const initialState: CounterState = {
	rooms: [],
}

export const roomSlice = createSlice({
	name: 'rooms',
	// `createSlice` will infer the state type from the `initialState` argument
	initialState,
	reducers: {
		AddMessageToRoom: (
			state,
			action: PayloadAction<AddMessageToRoomProps>
		) => {
			state.rooms.map(room => {
				if (room.RoomName == action.payload.RoomName)
					room?.Messages?.push(action.payload.Message)
			})
			action.payload.RoomName
		},
		CreateRoom: (state, action: PayloadAction<Room>) => {
			state.rooms.push(action.payload)
		},
	},
})

export const { CreateRoom } = roomSlice.actions

export default roomSlice.reducer
