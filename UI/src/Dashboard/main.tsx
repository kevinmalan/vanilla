import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
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
  const navigate = useNavigate();

  useEffect(() => {
    let cancelled = false;

    (async () => {
      try {
        const res = await api.get<UserDto>("/User");
        if (!cancelled) setUser(res.data);
      } catch {
        navigate("/login", { replace: true, state: { from: "/dashboard" } });
      } finally {
        if (!cancelled) setLoading(false);
      }
    })();

    return () => { cancelled = true; };
  }, [navigate]);

  if (loading) return <div className="container py-4">Loading…</div>;
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