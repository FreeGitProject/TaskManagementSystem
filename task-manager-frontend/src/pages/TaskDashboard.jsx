import { useEffect, useState } from 'react';
import axios from '../services/api';
import TaskList from '../components/TaskList';
import TaskForm from '../components/TaskForm';
import { useNavigate } from 'react-router-dom';

function TaskDashboard() {
  const [tasks, setTasks] = useState([]);
  const navigate = useNavigate();

  const fetchTasks = async () => {
    try {
      const res = await axios.get('/api/task');
      setTasks(res.data);
    } catch (error) {
      alert('Failed to fetch tasks. Please login again.');
      localStorage.removeItem('token');
      navigate('/login');
    }
  };

  useEffect(() => {
    fetchTasks();
  }, []);

  const addTask = async (task) => {
    try {
      const res = await axios.post('/tasks', task);
      setTasks([...tasks, res.data]);
    } catch {
      alert('Failed to add task');
    }
  };
  // Add this deleteTask function inside TaskDashboard component
const deleteTask = async (id) => {
  try {
    await axios.delete(`/tasks/${id}`);
    setTasks(tasks.filter((task) => task.id !== id));
  } catch {
    alert('Failed to delete task');
  }
};


  return (
    <div>
      <h2>Task Dashboard</h2>
      <TaskForm onAdd={addTask} />
      <TaskList tasks={tasks} />
    </div>
  );
}

export default TaskDashboard;
