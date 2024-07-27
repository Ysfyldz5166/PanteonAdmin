// src/axiosInstance.js
import axios from 'axios';

const axiosInstance = axios.create({
  baseURL: 'https://panteonapi.azurewebsites.net/api', 
  headers: {
    'Content-Type': 'application/json',
    'Accept-Language': 'tr',
  },
});

export default axiosInstance;
