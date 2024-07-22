import React, { useState } from 'react';
import axios from 'axios';

const Login = () => {
  console.log("the Login component is rendered");
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');

  const handleSubmit = async (event) => {
    event.preventDefault();

    console.log("the handleSubmit function is called when the form is submitted");
    try {
        const response = await axios.post('https://localhost:7001/Form/Login2', { username, password });
        console.log(response);
        if (response.status === 200) {
            window.location.href = response.data.redirectUrl;
        }
  
    } catch (error) {
      // Handle login error (e.g., show error message)
      if (axios.isAxiosError(error)) {
        let errorMessage = 'Login failed brudda\n';

        if (error.response) {
          console.error(errorMessage, error.response.data);
        }
        else if (error.request) {
          console.error(errorMessage, 'No response received but requesr made \n');
        }
        else {
          console.error('Error logging in', error);
        }
      }
    }
  };

  return (
    <div>
      <h2>Login</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Username:</label>
          <input
            type="text"
            name="username"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />
        </div>
        <div>
          <label>Password:</label>
          <input
            type="password"
            name="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </div>
        <button type="submit">Login</button>
      </form>
    </div>
  );
};

export default Login;
