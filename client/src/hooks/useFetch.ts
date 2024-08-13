// import { useState, useEffect } from 'react'

// const useFetch = <T>(url: string) => {
// 	const [data, setData] = useState<T | null>(null)
// 	const [loading, setLoading] = useState<boolean>(true)
// 	const [error, setError] = useState<string | null>(null)

// 	useEffect(() => {
// 		const fetchData = async () => {
// 			try {
// 				const response = await fetch(url)
// 				if (!response.ok) {
// 					throw new Error('Network response was not ok')
// 				}
// 				const result = await response.json()
// 				setData(result)
// 			} catch (err) {
// 				setError(err.message)
// 			} finally {
// 				setLoading(false)
// 			}
// 		}

// 		fetchData()
// 	}, [url])

// 	return { data, loading, error }
// }

// export default useFetch