import { useContext, useEffect, useState } from "react";
import { Input } from "../../components/Input";
import { login } from "./api";
import { LoginContext } from "../../components/state/context";
import { useNavigate } from "react-router-dom";
import logo from "../../assets/images.png";
import "./login.css";
import { storeLoginState } from "../../components/state/storage";
import { toast, ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

export function Login() {
  const { onLoginSuccess } = useContext(LoginContext);
  const [emailOrUsername, setUsernameOrEmail] = useState("");
  const [password, setPassword] = useState("");
  const [apiProgress, setApiProgress] = useState(false);
  const [errors, setErrors] = useState({});
  const [generalError, setGeneralError] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    setErrors((prevErrors) => ({
      ...prevErrors,
      emailOrUsername: undefined,
    }));
  }, [emailOrUsername]);

  useEffect(() => {
    setErrors((prevErrors) => ({
      ...prevErrors,
      password: undefined,
    }));
  }, [password]);

  const handleSubmit = async (event) => {
    event.preventDefault();
    setGeneralError("");
    setApiProgress(true);

    if (!emailOrUsername.trim()) {
      setErrors((prevErrors) => ({
        ...prevErrors,
        emailOrUsername: "Kullanıcı adı veya email boş geçilemez.",
      }));
      setApiProgress(false);
      return;
    }

    if (!password.trim()) {
      setErrors((prevErrors) => ({
        ...prevErrors,
        password: "Şifre boş geçilemez.",
      }));
      setApiProgress(false);
      return;
    }

    try {
      const response = await login({ emailOrUsername, password });
      const userData = response;
      console.log("storageye giden veri", userData);
      onLoginSuccess(userData);
      storeLoginState(userData); // storage.js dosyasına veri kaydetme
      toast.success("Giriş başarılı!");
      setTimeout(() => {
        navigate("/building");
      }, 2000);
    } catch (axiosError) {
      console.log("Gelen Hata Mesajı", axiosError.response.data.errors);
      if (axiosError.response && axiosError.response.status === 400) {
        const validationErrors = axiosError.response.data.errors;
        const errorMessages = {};

        // Gelen hataları errors state'ine ekleyelim
        for (const key in validationErrors) {
          if (Object.prototype.hasOwnProperty.call(validationErrors, key)) {
            errorMessages[key.toLowerCase()] = validationErrors[key].join(" ");
          }
        }

        setErrors(errorMessages);
        setGeneralError(axiosError.response.data.errors.Password);
      } else {
        setGeneralError(
          "Beklenmedik bir hata oluştu. Lütfen tekrar deneyiniz."
        );
      }
    } finally {
      setApiProgress(false);
    }
  };

  return (
    <div className="container">
      <div className="card">
        <img src={logo} alt="Logo" className="logo" />
        <div className="card-header text-center">
          <h1>Giriş Yap</h1>
        </div>
        <div className="card-body">
          <form onSubmit={handleSubmit}>
            <div className="mb-3">
              <Input
                id="emailOrUsernameLogin"
                label="Kullanıcı Adı veya E-mail"
                error={errors.emailOrUsername}
                onChange={(event) => setUsernameOrEmail(event.target.value)}
                autoComplete="username"
              />
            </div>
            <div className="mb-3">
              <Input
                id="passwordLogin"
                label="Şifre"
                error={errors.password}
                onChange={(event) => setPassword(event.target.value)}
                type="password"
                autoComplete="current-password"
              />
            </div>
            {generalError && (
              <div className="alert alert-danger">{generalError}</div>
            )}
            <div className="text-center">
              <button
                className="btn btn-outline-success"
                disabled={apiProgress || !emailOrUsername || !password}
              >
                {apiProgress && (
                  <span
                    className="spinner-border spinner-border-sm"
                    aria-hidden="true"
                  ></span>
                )}
                Giriş
              </button>
            </div>
          </form>
        </div>
        <div className="text-center mt-3">
          <span>Hesabın yok mu? </span>
          <button
            className="btn btn-link p-0"
            onClick={() => navigate("/signup")}
          >
            Kaydol
          </button>
        </div>
      </div>
      <ToastContainer />
    </div>
  );
}
