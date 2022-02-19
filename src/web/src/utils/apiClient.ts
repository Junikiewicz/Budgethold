import axios, { AxiosError, AxiosResponse } from "axios";
import { useAppDispatch } from "../store/hooks";
import { logout } from "../store/slices/authSlice";

export const apiClient = axios.create({
  baseURL: (window as any)._env_.API_BASE_URL,
  withCredentials: true,
});

apiClient.interceptors.response.use(
  (value: AxiosResponse<unknown>) => {
    return value;
  },
  (err: AxiosError) => {
    const status = err.response?.status;

    if (status === 401) {
      const dispatch = useAppDispatch();

      dispatch(logout());
    }
    return Promise.reject(err);
  }
);
