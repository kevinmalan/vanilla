import axios, { isAxiosError } from "axios";

export const api = axios.create({
    baseURL: import.meta.env.VITE_API_URL ?? "https://localhost:7026/api",
    withCredentials: true,
    headers: { "Content-Type": "application/json" }
});

api.interceptors.response.use(
    response => response,
    error => {
        if (isAxiosError(error)) {
            const status = error.response?.status;
            if (status === 401 || status === 403) {
                // Prevent loop if already login
                if (!window.location.pathname.startsWith("/login")) {
                    const from = encodeURIComponent(window.location.pathname + window.location.search);
                    window.location.href = `/login?from=${from}`;
                }
            }
        }
        return Promise.reject(error);
    }
)