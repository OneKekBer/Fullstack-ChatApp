import { Bounce, toast } from 'react-toastify'
import 'react-toastify/dist/ReactToastify.css' // Make sure to import the styles

// Define the configuration for the success toast
const toastConfig = {
	autoClose: 5000,
	hideProgressBar: false,
	closeOnClick: true,
	draggable: true,
	transition: Bounce,
}

// Success toast function with TypeScript typing for the message parameter
const SuccessToast = (msg: string) => {
	return toast.success(msg, toastConfig)
}

export default SuccessToast
