import { useEffect, useState } from "react";
import { Task } from "../../models/Task";
import { Label } from "../../models/Label";
import { useParams } from "react-router-dom";
import axios from "axios";

function CreateTask() {
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [userId, setUserId] = useState("");
  const [label, setLabel] = useState<Label[]>([]);
  const [labelId, setLabelId] = useState(0);
  const { username } = useParams<{ username: string }>();

  useEffect(() => {
    axios
      .get<Label[]>(`http://localhost:5182/label/list/Barone`)
      .then((resposta) => {
        setLabel(resposta.data);
      });
  });

  function createTask(e: any) {
    e.preventDefault();

    const task: Task = {
      name: name,
      description: description,
      userId: Number(userId),
      labelId: labelId,
    };

   
    fetch("http://localhost:5182/tasks/create", 
      {
          method : "POST",
          headers : {
              "Content-Type" : "application/json"
          },
          body : JSON.stringify(task)
      })
      .then(resposta => {
          console.log("Task Create");
          return resposta.json();
      })
  }

  return (
    <div>
      <h1>Para Criar uma nova Tarefa, insira os dados abaixo:</h1>
      <form onSubmit={createTask}>
        <div>
          <input
            type="text"
            name="name"
            onChange={(e: any) => setName(e.target.value)}
            placeholder="Nome da tarefa"
            value={name}
            required
          />
        </div>
        <div>
          <input
            type="text"
            name="description"
            onChange={(e: any) => setDescription(e.target.value)}
            placeholder="Descrição da tarefa"
            value={description}
          />
        </div>
        <div>
          <input
            type="number"
            name="userId"
            onChange={(e: any) => setUserId(e.target.value)}
            placeholder="ID do usuário"
            value={userId}
            required
          />
        </div>
        <div>
          <label htmlFor="prioridade">Rotúlos Criados</label>
            <select
                onChange={(e: any) => setLabelId(e.target.value)}>
                    
                    {label.map((labels) => (
                        <option
                          value={labels.labelId}
                          key={labels.labelId}>
                          {labels.labelName}
                        </option>
                      ))}
            </select>
        </div>
        <div>
          <input type="submit" value="CRIAR TAREFA" />
        </div>
      </form>
    </div>
  );
}


export default CreateTask;
