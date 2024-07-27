/* eslint-disable react/prop-types */
import { createContext, useState } from "react";
import { storeLoginState, loadLoginState } from "./storage";

export const LoginContext = createContext();

export const LoginationProvider = ({ children }) => {
  const [isLoggedIn, setIsLoggedIn] = useState(loadLoginState().id !== 0);
  const [user, setUser] = useState(loadLoginState());

  const onLoginSuccess = (userData) => {
    setIsLoggedIn(true);
    setUser(userData);
    storeLoginState(userData);
  };

  const onLogout = () => {
    const updatedUser = { id: 0 };
    setIsLoggedIn(false);
    setUser(updatedUser);
    storeLoginState(updatedUser);
  };

  return (
    <LoginContext.Provider value={{ isLoggedIn, user, onLoginSuccess, onLogout }}>
      {children}
    </LoginContext.Provider>
  );
};
