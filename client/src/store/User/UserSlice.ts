import { createSlice } from '@reduxjs/toolkit'
import type { PayloadAction } from '@reduxjs/toolkit'

interface CounterState {
	Login: string
}

// Define the initial state using that type
const initialState: CounterState = {
	Login: '',
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
	},
})

export const { LogIn, LogOut } = userSlice.actions

export default userSlice.reducer
