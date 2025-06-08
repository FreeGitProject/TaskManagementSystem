function TaskList({ tasks }) {
  if (!tasks.length) return <p>No tasks found.</p>;

  return (
    <ul>
      {tasks.map((task) => (
        <li key={task.id}>
          <strong>{task.title}</strong>: {task.description}
        </li>
      ))}
    </ul>
  );
}

export default TaskList;
