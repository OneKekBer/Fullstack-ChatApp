import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import './index.css'
import { Provider } from 'react-redux'
import { store } from './store/store'
import { BrowserRouter } from 'react-router-dom'
import { ChakraProvider } from '@chakra-ui/react'
import { extendTheme } from '@chakra-ui/react'

const theme = extendTheme({
	colors: {
		yellow: {
			50: '#ffffe5', // Very light shade
			100: '#fffccc', // Light shade
			200: '#fff999', // Lighter shade
			300: '#fff566', // Light medium shade
			400: '#fff233', // Medium shade
			500: '#ffd803', // Base color
			600: '#ccad02', // Slightly darker
			700: '#997f02', // Darker shade
			800: '#665201', // Much darker
			900: '#332900', // Very dark
		},
		customScheme: {
			50: '#e5fcf1',
			100: '#c7f7de',
			200: '#9ff0c8',
			300: '#70e7ae',
			400: '#46dc93',
			500: '#2cc37b', // base color
			600: '#20a466',
			700: '#168351',
			800: '#0d613d',
			900: '#044029',
		},
	},
})

export default theme

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
