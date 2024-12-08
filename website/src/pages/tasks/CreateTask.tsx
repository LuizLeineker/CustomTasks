import { useEffect, useState } from "react";
import { Task } from "../../models/Task";
import { Label } from "../../models/Label";
import { useNavigate, useParams } from "react-router-dom";
import axios from "axios";
import { Select, MenuItem, Checkbox, ListItemText, Chip, Box, InputLabel, FormControl } from "@mui/material";

function CreateTask() {
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [userId, setUserId] = useState("");
  const [labels, setLabels] = useState<Label[]>([]);
  const [labelsId, setLabelsIds] = useState<number[]>([]);

  const { username } = useParams<{ username: string }>();

  const navigate = useNavigate();

  useEffect(() => {
    axios
      .get<Label[]>(`http://localhost:5182/label/list/${username}`)
      .then(({ data }) => setLabels(data));
  }, [username]);

  function createTask(e: React.FormEvent) {
    e.preventDefault();

    const task: Task = {
      name: name,
      description: description,
      userId: Number(userId),
      labels: labels.filter((l) => labelsId.includes(l.labelId!)),
    };

    axios
      .post("http://localhost:5182/tasks/create", task)
      .then(() => {
        console.log("Task created successfully.");
        navigate(`/dashboard/${username}`);
      });
  }

  return (
    <div>
      <h1>Para Criar uma nova Tarefa, insira os dados abaixo:</h1>
      <form onSubmit={createTask}>
        <div>
          <input
            type="text"
            name="name"
            onChange={(e) => setName(e.target.value)}
            placeholder="Nome da tarefa"
            value={name}
            required
          />
        </div>
        <div>
          <input
            type="text"
            name="description"
            onChange={(e) => setDescription(e.target.value)}
            placeholder="Descrição da tarefa"
            value={description}
          />
        </div>
        <div>
          <input
            type="number"
            name="userId"
            onChange={(e) => setUserId(e.target.value)}
            placeholder="ID do usuário"
            value={userId}
            required
          />
        </div>
        <div>
          <FormControl sx={{ m: 1, width: 300 }} fullWidth>
            <InputLabel id="ui">Selecione as etiquetas</InputLabel>
            <Select
              multiple
              labelId="ui"
              value={labelsId} // Configura o valor selecionado
              onChange={e => setLabelsIds(e.target.value as number[])}
              renderValue={(selected) =>
                <Box sx={{ display: "flex", flexWrap: "wrap", gap: 0.5 }}>
                {
                  labels.filter(({ labelId }) => selected.includes(labelId!))
                    .map(({ labelId, labelName}) => (
                  <Chip key={labelId} label={labelName} />
                    )
                )}
              </Box>
              }
            >
                {labels.map(({ labelId, labelName }) => (
                  <MenuItem key={labelId} value={labelId}>
                    <Checkbox checked={labelsId.includes(labelId!)} />
                    <ListItemText primary={labelName} />
                  </MenuItem>
              ))}
            </Select>
          </FormControl>
        </div>
        <div>
          <input style={{ width: "178px" }} type="submit" value="CRIAR TAREFA" />
        </div>
      </form>
    </div>
  );
}

export default CreateTask;
