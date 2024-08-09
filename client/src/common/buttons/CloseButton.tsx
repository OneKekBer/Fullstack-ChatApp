import React from 'react'
import close from 'public/icons/close/white.png'

interface CloseButtonProps {
	onClick: () => void
}

const CloseButton: React.FC<CloseButtonProps> = ({ onClick }) => {
	return (
		<button
			onClick={onClick}
			className='p-2  rounded-full cursor-pointer bg-transparent hover:bg-slate-800 duration-300'
			aria-label='Close'
		>
			<img
				className='object-cover h-[30px] w-[30px]'
				src={close}
				alt='Close'
			/>
		</button>
	)
}

export default CloseButton
