import { useState, useEffect, useContext } from "react";
import { Button, Modal, Form, Table, Alert } from "react-bootstrap";
import { addBuilding, getAllBuildings, updateBuilding, deleteBuilding } from "./api";
import { useNavigate } from "react-router-dom";
import { LoginContext } from "../../components/state/context";
import { toast, ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import "./build.css";
import logo from "../../assets/images.png"; // Logoyu içe aktarma
import { loadLoginState } from "../../components/state/storage"; // Local storage'dan veri yükleme

const BuildingTypes = ["Farm", "Academy", "Headquarters", "LumberMill", "Barracks"];

export default function Build() {
  const [buildings, setBuildings] = useState([]);
  const [showModal, setShowModal] = useState(false);
  const [buildingType, setBuildingType] = useState("");
  const [buildingCost, setBuildingCost] = useState("");
  const [constructionTime, setConstructionTime] = useState("");
  const [errors, setErrors] = useState({});
  const { onLogout } = useContext(LoginContext);
  const navigate = useNavigate();
  const [editingBuildingId, setEditingBuildingId] = useState(null);

  useEffect(() => {
    const loginState = loadLoginState();
    if (loginState.id === 0) {
      navigate("/login");
    } else {
      fetchBuildings();
    }
  }, [navigate]);

  const fetchBuildings = async () => {
    try {
      const response = await getAllBuildings();
      setBuildings(response);
    } catch (error) {
      toast.error("Binalar alınırken hata oluştu.");
      console.error("Binalar alınırken hata oluştu:", error);
    }
  };

  const handleAddBuilding = async () => {
    setErrors({}); // Hataları sıfırlama
    try {
      const newBuilding = {
        id: editingBuildingId, // Güncelleme için id değerini ekliyoruz
        buildingType,
        buildingCost: parseFloat(buildingCost),
        constructionTime: parseInt(constructionTime),
      };

      if (editingBuildingId) {
        console.log("Güncelleme için gönderilen veri:", newBuilding);
        await updateBuilding(editingBuildingId, newBuilding);
        setBuildings(buildings.map(b => b.id === editingBuildingId ? { ...b, ...newBuilding } : b));
        toast.success("Bina başarıyla güncellendi.");
      } else {
        const addedBuilding = await addBuilding(newBuilding);
        setBuildings([...buildings, addedBuilding]);
        toast.success("Bina başarıyla eklendi.");
      }

      setShowModal(false);
      setEditingBuildingId(null);
      setBuildingType("");
      setBuildingCost("");
      setConstructionTime("");
    } catch (error) {
      if (error.response && error.response.status === 400) {
        console.log(error.response);
        const validationErrors = error.response.data.errors;
        const errorMessages = {};
        for (const key in validationErrors) {
          errorMessages[key.toLowerCase()] = validationErrors[key].join(' ');
        }
        setErrors(errorMessages);
      } else {
        toast.error("Bina eklenirken bir hata oluştu.");
      }
    }
  };

  const handleDeleteBuilding = async (id) => {
    try {
      await deleteBuilding(id);
      setBuildings(buildings.filter(b => b.id !== id));
      toast.success("Bina başarıyla silindi.");
    } catch (error) {
      toast.error("Bina silinirken bir hata oluştu.");
    }
  };

  const handleEditBuilding = (building) => {
    setEditingBuildingId(building.id);
    setBuildingType(building.buildingType);
    setBuildingCost(building.buildingCost);
    setConstructionTime(building.constructionTime);
    setShowModal(true);
  };

  const handleLogout = () => {
    onLogout();
    navigate("/login");
  };

  const allBuildingTypesAdded = BuildingTypes.every(type => buildings.some(b => b.buildingType === type));

  return (
    <div className="buildings-container">
      <img src={logo} alt="Logo" className="logo" />
      <Button onClick={() => setShowModal(true)} className="mb-3" disabled={allBuildingTypesAdded}>
        Bina Ekle
      </Button>
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>Bina Tipi</th>
            <th>Bina Maliyeti</th>
            <th>İnşaat Süresi</th>
            <th>İşlemler</th>
          </tr>
        </thead>
        <tbody>
          {buildings.map((building, index) => (
            <tr key={index}>
              <td>{building.buildingType}</td>
              <td>{building.buildingCost}</td>
              <td>{building.constructionTime}</td>
              <td>
                <div className="button-group">
                  <Button variant="warning" onClick={() => handleEditBuilding(building)}>Düzenle</Button>
                  <Button variant="danger" onClick={() => handleDeleteBuilding(building.id)}>Sil</Button>
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
      <Button variant="danger" onClick={handleLogout} className="mt-3">Çıkış Yap</Button>

      <Modal show={showModal} onHide={() => setShowModal(false)}>
        <Modal.Header closeButton>
          <Modal.Title>{editingBuildingId ? "Bina Düzenle" : "Bina Ekle"}</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group controlId="formBuildingType">
              <Form.Label style={{ color: 'black' }}>Bina Tipi</Form.Label>
              <Form.Control as="select" value={buildingType} onChange={(e) => setBuildingType(e.target.value)}>
                <option value="">Bina Tipi Seçin</option>
                {BuildingTypes.filter(type => !buildings.some(b => b.buildingType === type) || type === buildingType).map((type, index) => (
                  <option key={index} value={type}>{type}</option>
                ))}
              </Form.Control>
              {errors.buildingtype && <Alert variant="danger">{errors.buildingtype}</Alert>}
            </Form.Group>

            <Form.Group controlId="formBuildingCost">
              <Form.Label style={{ color: 'black' }}>Bina Maliyeti</Form.Label>
              <Form.Control type="number" value={buildingCost} onChange={(e) => setBuildingCost(e.target.value)} />
              {errors.buildingcost && <Alert variant="danger">{errors.buildingcost}</Alert>}
            </Form.Group>

            <Form.Group controlId="formConstructionTime">
              <Form.Label style={{ color: 'black' }}>İnşaat Süresi (saniye)</Form.Label>
              <Form.Control type="number" value={constructionTime} onChange={(e) => setConstructionTime(e.target.value)} />
              {errors.constructiontime && <Alert variant="danger">{errors.constructiontime}</Alert>}
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={() => setShowModal(false)}>Kapat</Button>
          <Button variant="primary" onClick={handleAddBuilding}>{editingBuildingId ? "Güncelle" : "Kaydet"}</Button>
        </Modal.Footer>
      </Modal>
      <ToastContainer />
    </div>
  );
}
