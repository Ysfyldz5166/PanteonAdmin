import axiosInstance from '../../axiosInstance';

export async function getAllBuildings() {
  const response = await axiosInstance.get('/Building/buildings');
  return response.data;
}

export async function addBuilding(building) {
  const response = await axiosInstance.post('/Building', building);
  return response.data;
}

export async function updateBuilding(id, building) {
  const response = await axiosInstance.put(`/Building/${id}`, { id, ...building });
  return response.data;
}

export async function deleteBuilding(id) {
  const response = await axiosInstance.delete(`/Building/${id}`);
  return response.data;
}
