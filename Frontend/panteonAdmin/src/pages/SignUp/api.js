import axiosInstance from '../../axiosInstance';

export async function signUp(body) {
  const response = await axiosInstance.post('/User/add', body);
  return response.data;
}
