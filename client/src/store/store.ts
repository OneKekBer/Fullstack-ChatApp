import { configureStore } from '@reduxjs/toolkit'
import roomReducer from './Chat/RoomSlice'
import userReducer from './User/UserSlice'

export const store = configureStore({
	reducer: {
		rooms: roomReducer,
		user: userReducer,
	},
	devTools: true,
})

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch
