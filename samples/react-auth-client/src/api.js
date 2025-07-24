const apiUrl = 'https://localhost:5001';

export async function login(userName, password) {
  const res = await fetch(`${apiUrl}/login`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ userName, password })
  });
  if (res.ok) {
    const data = await res.json();
    return data.token;
  }
  return null;
}

export async function changePassword(currentPassword, newPassword) {
  const token = localStorage.getItem('token');
  const res = await fetch(`${apiUrl}/change-password`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    },
    body: JSON.stringify({ currentPassword, newPassword })
  });
  return res.ok;
}
