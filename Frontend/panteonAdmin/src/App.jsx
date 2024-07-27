import { Routes, Route } from "react-router-dom";
import { Login } from "./pages/Login";
import { SignUp } from "./pages/SignUp";
import { LoginationProvider } from "./components/state/context";
import Build from "./pages/Build";

function App() {
  return (
    <LoginationProvider>
      <Routes>
        <Route path="/" element={<SignUp />} />
        <Route path="/login" element={<Login />} />
        <Route path="/signup" element={<SignUp />} />
        <Route path="/building" element={<Build />} />

      </Routes>
    </LoginationProvider>
  );
}

export default App;
