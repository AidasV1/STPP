import { useQuery } from 'react-query'
import axios from 'axios'

const fetchListing = (cityId, listingId) => {
    return axios.get(`https://stppapiapp.azurewebsites.net/api/cities/${cityId}/ads/${listingId}`)
}

export const useListingData = (cityId, listingId) => {
    return useQuery(['listing', listingId], () => fetchListing(cityId, listingId))
}