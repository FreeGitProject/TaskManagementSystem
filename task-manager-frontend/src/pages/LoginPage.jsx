import LoginForm from '../components/LoginForm';
import { useNavigate } from 'react-router-dom';

function LoginPage() {
  const navigate = useNavigate();

  const handleLogin = () => {
    navigate('/tasks');
  };

  return (
    <div>
      <h2>Login</h2>
      <LoginForm onLogin={handleLogin} />
      <p>
        Don't have an account? <a href="/register">Register</a>
      </p>
    </div>
  );
}

export default LoginPage;
