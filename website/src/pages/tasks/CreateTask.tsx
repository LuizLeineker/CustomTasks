import { useEffect, useState } from "react";
import { Task } from "../../models/Task";
import { Label } from "../../models/Label";

function CreateTask() {
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [userId, setUserId] = useState("");
  const [availableLabels, setAvailableLabels] = useState<Label[]>([]);
  const [selectedLabel, setSelectedLabel] = useState<number | undefined>(undefined);

  useEffect(() => {
    fetch("http://localhost:5182/label/list")
      .then((response) => response.json())
      .then((labels) => setAvailableLabels(labels))
  }, []);

  function createTask(e: any) {
    e.preventDefault();

    const task: Task = {
      name: name,
      description: description,
      userId: Number(userId),
      labelId: selectedLabel,
    };

    fetch("http://localhost:5182/tasks/create", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(task),
    })
      .then((resposta) => {
        if (resposta.ok) {
          alert("Tarefa criada com sucesso!");
          setName("");
          setDescription("");
          setUserId("");
          setSelectedLabel(undefined);
        } else {
          alert("Erro ao criar tarefa.");
        }
        return resposta.json();
      })
      .catch((err) => console.error("Erro:", err));
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
          <select
            onChange={(e: any) => setSelectedLabel(Number(e.target.value))}value={selectedLabel}required
          >
            <option value={undefined} disabled>Selecione um rótulo</option>{availableLabels.map((label) => (
              <option key={label.labelId} value={label.labelId}>{label.labelName}</option>
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
