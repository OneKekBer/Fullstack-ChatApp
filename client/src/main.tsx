import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import './index.css'
import { Provider } from 'react-redux'
import { store } from './store/store'
import { BrowserRouter } from 'react-router-dom'
import { ChakraProvider } from '@chakra-ui/react'
import { extendTheme } from '@chakra-ui/react'

export const theme = extendTheme({
	colors: {
		brand: {
			50: '#f9fbfd', // very lightest
			100: '#f1f5f9', // lightest
			200: '#e2e8f0', // very light
			300: '#cfd8e3', // light
			400: '#a6b1c2', // medium-light
			500: '#6d7b91', // medium
			600: '#4a5568', // medium-dark
			700: '#384455', // dark-medium
			800: '#2a3542', // dark
			900: '#1e2935', // very dark
		},
	},
})

ReactDOM.createRoot(document.getElementById('root')!).render(
	<React.StrictMode>
		<ChakraProvider theme={theme}>
			<Provider store={store}>
				<BrowserRouter>
					<App />
				</BrowserRouter>
			</Provider>
		</ChakraProvider>
	</React.StrictMode>
)
