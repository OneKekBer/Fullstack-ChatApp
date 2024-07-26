interface SearchChatPopupProps {
	toggleSearchPopup: () => void
	isSearchPopupOpen: boolean
}
const SearchChatPopup: React.FC<SearchChatPopupProps> = ({
	isSearchPopupOpen,
	toggleSearchPopup,
}) => {
	return (
		<div className={`popup__bg ${isSearchPopupOpen ? 'flex' : 'hidden'}`}>
			<div className='popup__main w-[500px] h-[300px] bg-blue-500'>
				<div onClick={toggleSearchPopup}>close</div>
				<div>hello</div>
			</div>
		</div>
	)
}

export default SearchChatPopup
