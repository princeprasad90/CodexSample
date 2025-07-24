import React, { useState } from 'react';
import { login } from './api';

function Login({ onLogin }) {
  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');

  const handleSubmit = async e => {
    e.preventDefault();
    if (!userName || !password) {
      setError('Username and password are required');
      return;
    }
    const token = await login(userName, password);
    if (token) {
      onLogin(token);
    } else {
      setError('Invalid credentials');
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <div>
        <label>User Name</label>
        <input value={userName} onChange={e => setUserName(e.target.value)} />
      </div>
      <div>
        <label>Password</label>
        <input type="password" value={password} onChange={e => setPassword(e.target.value)} />
      </div>
      {error && <div style={{color:'red'}}>{error}</div>}
      <button type="submit">Login</button>
    </form>
  );
}

export default Login;
