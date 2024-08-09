import { createSlice } from '@reduxjs/toolkit'
import type { PayloadAction } from '@reduxjs/toolkit'
import IChat from 'interfaces/IChat'
import { IMessage } from 'interfaces/IMessage'

interface CounterState {
	chats: IChat[]
}

const initialState: CounterState = {
	chats: [],
}

export const roomSlice = createSlice({
	name: 'chat',
	initialState,
	reducers: {
		addChat: (
			state,
			action: PayloadAction<{ chatName: string; messages: IMessage[] }>
		) => {
			const existingChatIndex = state.chats.findIndex(
				item => item.name === action.payload.chatName
			)
			console.log('im working!!')
			if (existingChatIndex !== -1) {
				// Update existing chat
				state.chats[existingChatIndex] = {
					...state.chats[existingChatIndex],
					messages: action.payload.messages || [],
				}
			} else {
				// Add new chat
				state.chats.push({
					name: action.payload.chatName,
					messages: action.payload.messages,
				})
			}
		},
		SetChats: (state, action: PayloadAction<IChat[]>) => {
			state.chats = []
			action.payload.map(chat => {
				if (!chat.messages) chat.messages = []
			})
			state.chats = action.payload
		},
		UpdateChat: (
			state,
			action: PayloadAction<{ chatName: string; message: IMessage }>
		) => {
			const chat = state.chats.find(
				ch => ch.name === action.payload.chatName
			)
			if (chat) {
				if (!chat.messages) {
					chat.messages = []
				}
				chat.messages.push(action.payload.message)
			}
		},
	},
})

export const { UpdateChat, SetChats, addChat } = roomSlice.actions

export default roomSlice.reducer
