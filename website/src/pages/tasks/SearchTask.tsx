import { useEffect, useState } from "react";
import { Task } from "../../models/Task";
import { useParams } from "react-router-dom";
import axios from "axios";

function SearchTask() {
  const [tasks, setTasks] = useState<Task[]>([]);
  const { username } = useParams<{ username: string }>();

  useEffect(() => {
    fetch(`http://localhost:5182/tasks/list/${username}`)
      .then((resposta) => {
        if (!resposta.ok) {
          throw new Error("Erro na resposta do servidor");
        }
        return resposta.json();
      })
      .then((tasks) => {
        setTasks(tasks);
      });
  }, [username]);

  function status(taskId: number) {
    axios.put(`http://localhost:5182/tasks/status/${taskId}`);
    console.log(`Id: ${taskId} completed`);
  }

  function remove(taskId: number) {
    axios.delete(`http://localhost:5182/tasks/delete/${taskId}`);
  }

  return (
    <div>
      <h1>Task for User - Insira o nome na URL</h1>
      <table>
        <thead>
          <tr>
            <th>Título</th>
            <th>Descrição</th>
            <th>Completada</th>
            <th>Data de Criação</th>
            <th>Rótulos</th>
            <th>Status</th>
            <th>Deletar Task</th>
          </tr>
        </thead>
        <tbody>
          {tasks.map((task) => (
            <tr key={task.taskId}>
              <td>{task.name}</td>
              <td>{task.description}</td>
              <td>{task.isCompleted ? "Sim" : "Não"}</td>
              <td>{task.createdAt}</td>
              <td>
                {task.labels && task.labels.length > 0
                  ? task.labels.map((label) => label.labelName).join(", ")
                  : "Sem rótulos"}
              </td>
              <td>
                <button
                  onClick={() => {
                    status(task.taskId!);
                  }}
                >
                  Update
                </button>
              </td>
              <td>
                <button
                  onClick={() => {
                    remove(task.taskId!);
                  }}
                >
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>



    );
}



export default SearchTask;