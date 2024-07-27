import { useState, useEffect } from "react";
import { signUp } from "./api";
import { useNavigate } from "react-router-dom";
import logo from "../../assets/images.png"; // Görüntüyü içe aktarma
import "./signup.css"; // CSS dosyasını dahil etme
import { toast, ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

export function SignUp() {
  const [userName, setUserName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [passwordRepeat, setPasswordRepeat] = useState("");
  const [apiProgress, setApiProgress] = useState(false);
  const [successMessage, setSuccessMessage] = useState("");
  const [errors, setErrors] = useState({});
  const [generalError, setGeneralError] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    setErrors((lastError) => ({
      ...lastError,
      userName: undefined,
    }));
  }, [userName]);

  useEffect(() => {
    setErrors((lastError) => ({
      ...lastError,
      email: undefined,
    }));
  }, [email]);

  useEffect(() => {
    setErrors((lastError) => ({
      ...lastError,
      password: undefined,
    }));
  }, [password]);

  const onSubmit = async (event) => {
    event.preventDefault();
    setSuccessMessage("");
    setGeneralError("");
    setApiProgress(true);

    // Şifre ve Şifre Tekrarı'nın aynı olup olmadığını kontrol etme
    if (password !== passwordRepeat) {
      setErrors((prevErrors) => ({
        ...prevErrors,
        passwordRepeat: "Şifreler uyuşmuyor",
      }));
      setApiProgress(false);
      return;
    }

    // Verileri consola yazdırma
    console.log({ userName, email, password });

    try {
      const response = await signUp({
        userName,
        email,
        password,
      });
      setSuccessMessage(response.message);
      toast.success("Kayıt başarılı!"); // Başarılı mesajını göster
      setTimeout(() => {
        navigate("/login");
      }, 2000); // 2 saniye gecikme
    } catch (axiosError) {
      console.log('Gelen Hata Mesajı', axiosError.response.data.errors); // Gelen hata mesajını konsola yazma
      if (axiosError.response && axiosError.response.status === 400) {
        const validationErrors = axiosError.response.data.errors;
        const errorMessages = {};

        // Gelen hataları errors state'ine ekleyelim
        for (const key in validationErrors) {
          if (Object.prototype.hasOwnProperty.call(validationErrors, key)) {
            errorMessages[key.toLowerCase()] = validationErrors[key][0]; // Mesajın ilk elemanını kullan
          }
        }

        setErrors(errorMessages);
        toast.error("Validation hatası: Lütfen aşağıdaki hataları kontrol edin."); // Hata mesajını göster
      } else {
        setGeneralError("Beklenmedik bir hata oluştu. Lütfen tekrar deneyiniz.");
      }
    } finally {
      setApiProgress(false);
    }
  };

  return (
    <div className="container">
      <ToastContainer /> {/* ToastContainer bileşeni */}
      <div className="card signup-card">
        <img src={logo} alt="Logo" className="logo" />
        <div className="card-header text-center">
          <h1>Kayıt Ol</h1>
        </div>
        <div className="card-body">
          <form onSubmit={onSubmit}>
            <div className="mb-3">
              <label htmlFor="usernameSignup" className="form-label">
                Kullanıcı Adı
              </label>
              <input
                id="usernameSignup"
                className="form-control"
                onChange={(event) => setUserName(event.target.value)}
                autoComplete="username"
              />
              {errors.username && (
                <div className="alert alert-danger">{errors.username}</div>
              )}
            </div>
            <div className="mb-3">
              <label htmlFor="emailSignup" className="form-label">
                E-mail
              </label>
              <input
                id="emailSignup"
                className="form-control"
                type="email"
                onChange={(event) => setEmail(event.target.value.toLowerCase())}
                autoComplete="email"
              />
              {errors.email && (
                <div className="alert alert-danger">{errors.email}</div>
              )}
            </div>
            <div className="mb-3">
              <label htmlFor="passwordSignup" className="form-label">
                Şifre
              </label>
              <input
                id="passwordSignup"
                type="password"
                className="form-control"
                onChange={(event) => setPassword(event.target.value)}
                autoComplete="new-password"
              />
              {errors.password && (
                <div className="alert alert-danger">{errors.password}</div>
              )}
            </div>
            <div className="mb-3">
              <label htmlFor="passwordRepeatSignup" className="form-label">
                Şifre Tekrarı
              </label>
              <input
                id="passwordRepeatSignup"
                type="password"
                className="form-control"
                onChange={(event) => setPasswordRepeat(event.target.value)}
                autoComplete="new-password"
              />
              {errors.passwordRepeat && (
                <div className="alert alert-danger">{errors.passwordRepeat}</div>
              )}
            </div>
            {successMessage && (
              <div className="alert alert-success">{successMessage}</div>
            )}
            {generalError && (
              <div className="alert alert-danger">{generalError}</div>
            )}
            <div className="text-center">
              <button
                className="btn btn-outline-success"
                disabled={apiProgress}
              >
                {apiProgress && (
                  <span
                    className="spinner-border spinner-border-sm"
                    aria-hidden="true"
                  ></span>
                )}
                Kayıt Ol
              </button>
            </div>
          </form>
          <div className="text-center mt-3">
            <span>Zaten bir hesabın var mı? </span>
            <button
              className="btn btn-link p-0"
              onClick={() => navigate("/login")}
            >
              Giriş Yap
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}
