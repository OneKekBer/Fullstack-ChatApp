// vite.config.ts or vite.config.js
import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import path from 'path'

export default defineConfig({
	plugins: [react()],
	resolve: {
		alias: {
			interfaces: path.resolve(__dirname, 'src/interfaces'),
			store: path.resolve(__dirname, 'src/store'),
			pages: path.resolve(__dirname, 'src/pages'),
			// Add more aliases as needed
		},
	},
})
