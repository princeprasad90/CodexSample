import React, { useState } from 'react';
import { Routes, Route, useNavigate, Link } from 'react-router-dom';
import Login from './Login';
import ChangePassword from './ChangePassword';

function App() {
  const [token, setToken] = useState(localStorage.getItem('token'));
  const navigate = useNavigate();

  const logout = () => {
    localStorage.removeItem('token');
    setToken(null);
    navigate('/login');
  };

  return (
    <div>
      <nav>
        {token && <button onClick={logout}>Logout</button>}
        {token && <Link to="/change-password">Change Password</Link>}
      </nav>
      <Routes>
        <Route path="/login" element={<Login onLogin={t => { localStorage.setItem('token', t); setToken(t); navigate('/'); }} />} />
        <Route path="/change-password" element={<ChangePassword />} />
        <Route path="/" element={<h2>Welcome</h2>} />
      </Routes>
    </div>
  );
}

export default App;
