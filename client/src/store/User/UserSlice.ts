import { createSlice } from '@reduxjs/toolkit'
import type { PayloadAction } from '@reduxjs/toolkit'

interface CounterState {
	Login: string
	OnlineUsers: string[]
}

// Define the initial state using that type
const initialState: CounterState = {
	Login: '',
	OnlineUsers: [],
}

export const userSlice = createSlice({
	name: 'user',
	initialState,
	reducers: {
		LogIn: (state, action: PayloadAction<string>) => {
			state.Login = action.payload
		},
		LogOut: state => {
			state.Login = ''
		},

		SetUserIsOnline: (state, action: PayloadAction<string>) => {
			state.OnlineUsers.push(action.payload)
		},
		SetUserIsOffline: (state, action: PayloadAction<string>) => {
			state.OnlineUsers = state.OnlineUsers.filter(
				user => user !== action.payload
			)
		},
	},
})

export const { LogIn, LogOut, SetUserIsOffline, SetUserIsOnline } =
	userSlice.actions

export default userSlice.reducer
