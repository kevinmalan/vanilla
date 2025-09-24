import { useEffect, useState } from "react";
import { api } from "../api";

type UserDto = {
  uniqueId: string;
  username: string;
  firstname: string;
  lastname: string;
  status: number;
  statusReason: string | null;
  role: number;
};

export default function DashboardMain() {
  const [user, setUser] = useState<UserDto | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    (async () => {
      try {
        const res = await api.get<UserDto>("/User");
        setUser(res.data);
      } catch (err: any) {
        // 401/403 are handled globally; show other errors locally
        if (err?.response?.status !== 401 && err?.response?.status !== 403) {
          setError(err?.response?.data ?? err?.message ?? "Failed to load user");
        }
      } finally {
        setLoading(false);
      }
    })();
  }, []);

  if (loading) return <div className="container py-4">Loading…</div>;
  if (error) return <div className="container py-4"><div className="alert alert-danger">{error}</div></div>;
  if (!user) return null;

  return (
    <div className="container py-4">
      <h1 className="h4 mb-3">Dashboard</h1>
      <div className="card">
        <div className="card-body">
          <div className="table-responsive">
            <table className="table table-sm mb-0">
              <tbody>
                <tr><th>Unique Id</th><td>{user.uniqueId}</td></tr>
                <tr><th>Username</th><td>{user.username}</td></tr>
                <tr><th>First name</th><td>{user.firstname}</td></tr>
                <tr><th>Last name</th><td>{user.lastname}</td></tr>
                <tr><th>Status</th><td>{user.status}</td></tr>
                <tr><th>Status Reason</th><td>{user.statusReason ?? "—"}</td></tr>
                <tr><th>Role</th><td>{user.role}</td></tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  );
}
