import { useEffect, useState } from "react";
import { Task } from "../../models/Task";
import { useParams } from "react-router-dom";
import axios from "axios";

function SearchTask() {
    const [tasks, setUsers] = useState<Task[]>([]);
    const { username } = useParams<{ username: string }>();
    useEffect(() =>{
        fetch(`http://localhost:5182/tasks/list/${username}`)	
            .then(resposta => {
                if (!resposta.ok) {
                    throw new Error('Erro na resposta do servidor');
                  }
                return resposta.json();
            })
            .then(tasks => {
                setUsers(tasks)
            });
        
    });


    function status(taskId: number) {
        console.log(`Id: ${taskId}`);
        axios
          .put(`http://localhost:5182/tasks/status/${taskId}`)
      }

    function remove(taskId: number) {
        axios
          .delete(`http://localhost:5182/tasks/delete/${taskId}`)
      }

    
    return (
        <div>
        <h1>Task for User - Insira o nome na url</h1>
        <table >
            <thead>
                <tr>
                    <th>Titulo</th>
                    <th>Descrição</th>
                    <th>Completada</th>
                    <th>Data de Crianção</th>
                    <th>Status</th>
                    <th>Deletar Task</th>
                </tr>
            </thead>
            <tbody>
                {tasks.map(task => (
                    <tr key={task.taskId}>
                        <td>{task.name}</td>
                        <td>{task.description}</td>
                        <td> {task.isCompleted}</td>
                        <td> {task.createdAt}</td>
                        <td>
                            <button
                            onClick={() => {
                                status(task.taskId!);
                            }} >
                            Update
                            </button>
                        </td>
                        <td>
                            <button
                            onClick={() => {
                                remove(task.taskId!);
                            }} >
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