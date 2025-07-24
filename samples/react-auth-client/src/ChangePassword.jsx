import React, { useState } from 'react';
import { changePassword } from './api';

function ChangePassword() {
  const [current, setCurrent] = useState('');
  const [next, setNext] = useState('');
  const [confirm, setConfirm] = useState('');
  const [msg, setMsg] = useState('');

  const handleSubmit = async e => {
    e.preventDefault();
    if (next.length < 6) {
      setMsg('New password must be at least 6 characters');
      return;
    }
    if (next !== confirm) {
      setMsg('Passwords do not match');
      return;
    }
    const ok = await changePassword(current, next);
    setMsg(ok ? 'Password changed' : 'Invalid current password');
  };

  return (
    <form onSubmit={handleSubmit}>
      <div>
        <label>Current Password</label>
        <input type="password" value={current} onChange={e => setCurrent(e.target.value)} />
      </div>
      <div>
        <label>New Password</label>
        <input type="password" value={next} onChange={e => setNext(e.target.value)} />
      </div>
      <div>
        <label>Confirm New Password</label>
        <input type="password" value={confirm} onChange={e => setConfirm(e.target.value)} />
      </div>
      {msg && <div style={{color:'red'}}>{msg}</div>}
      <button type="submit">Change Password</button>
    </form>
  );
}

export default ChangePassword;
