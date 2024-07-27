
import axiosInstance from '../../axiosInstance';

export async function login(body) {
  const response = await axiosInstance.post('/User/login', body);
  console.log('Sunucudan gelen cevap:', response); 
  return response.data;
}


