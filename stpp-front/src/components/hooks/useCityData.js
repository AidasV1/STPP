import { useQuery } from 'react-query'
import axios from 'axios'

const fetchCity = (cityId) => {
    return axios.get(`https://stppapiapp.azurewebsites.net/api/cities/${cityId}`)
}

export const useCityData = (cityId) => {
    return useQuery(['listing', cityId], () => fetchCity(cityId))
}