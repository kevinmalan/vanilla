import { useState } from "react";
import { api } from "../api";
import { useNavigate, useLocation } from "react-router-dom";

export default function Login() {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [okMsg, setOkMsg] = useState<string | null>(null);

    const navigate = useNavigate();
    const location = useLocation() as { state?: { from?: string } };

    async function handleSubmit(e: React.FormEvent) {
        e.preventDefault();
        setError(null);
        setOkMsg(null);
        setLoading(true);
        try {
            await api.post("/Auth", { username, password });
            setOkMsg("Logged in successfully.");
            const dest = location.state?.from || "/dashboard";
            navigate(dest, { replace: true });
        } catch (err: any) {
            const msg =
                err?.response?.data?.message ??
                err?.response?.data ??
                err?.message ??
                "Login failed";
            setError(msg);
        } finally {
            setLoading(false);
        }
    }
    return (
        <div className="container py-5">
            <div className="row justify-content-center">
                <div className="col-12 col-sm-10 col-md-8 col-lg-6 col-xl-5">
                    <div className="card shadow-sm">
                        <div className="card-body p-4">
                            <h1 className="h4 mb-4 text-center">Login</h1>

                            {error && (
                                <div className="alert alert-danger" role="alert">
                                    {error}
                                </div>
                            )}
                            {okMsg && (
                                <div className="alert alert-success" role="alert">
                                    {okMsg}
                                </div>
                            )}

                            <form onSubmit={handleSubmit} noValidate>
                                <div className="mb-3">
                                    <label htmlFor="username" className="form-label">
                                        Username
                                    </label>
                                    <input
                                        id="username"
                                        className="form-control"
                                        type="text"
                                        autoComplete="username"
                                        value={username}
                                        onChange={(e) => setUsername(e.target.value)}
                                        required
                                    />
                                </div>

                                <div className="mb-3">
                                    <label htmlFor="password" className="form-label">
                                        Password
                                    </label>
                                    <input
                                        id="password"
                                        className="form-control"
                                        type="password"
                                        autoComplete="current-password"
                                        value={password}
                                        onChange={(e) => setPassword(e.target.value)}
                                        required
                                    />
                                </div>

                                <button
                                    type="submit"
                                    className="btn btn-primary w-100"
                                    disabled={loading}
                                >
                                    {loading ? "Signing in…" : "Submit"}
                                </button>
                            </form>
                        </div>
                    </div>

                    <p className="text-center text-muted mt-3 mb-0" style={{ fontSize: 12 }}>
                        Posting to <code>{(import.meta.env.VITE_API_URL ?? "https://localhost:7026/api") + "/Auth"}</code>
                    </p>
                </div>
            </div>
        </div>
    );
}