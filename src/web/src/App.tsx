import { useEffect, useState } from "react";
import { Route, Routes, Navigate } from "react-router-dom";
import "./App.css";
import LoadingElement from "./components/LoadingElement/LoadingElement";
import AuthPage from "./pages/Auth/AuthPage";
import MainAppFrame from "./pages/MainApp/MainAppFrame/MainAppFrame";
import { useAppDispatch } from "./store/hooks";
import { login, logout } from "./store/slices/authSlice";
import { apiClient } from "./utils/apiClient";

function App() {
  const dispatch = useAppDispatch();
  const [isAuthStatusChecked, setIsAuthStatusChecked] = useState(false);

  useEffect(() => {
    apiClient
      .get("/api/auth/is-logged")
      .then(() => {
        dispatch(login());
      })
      .catch(() => {
        dispatch(logout());
      })
      .finally(() => {
        setIsAuthStatusChecked(true);
      });
  }, [dispatch]);

  if (isAuthStatusChecked) {
    return (
      <Routes>
        <Route path="/app/*" element={<MainAppFrame />} />
        <Route path="/auth/*" element={<AuthPage />} />
        <Route path="*" element={<Navigate to="auth" />} />
      </Routes>
    );
  } else {
    return <LoadingElement />;
  }
}

export default App;
