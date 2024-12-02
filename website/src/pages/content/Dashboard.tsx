import { Fab, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Tooltip } from "@mui/material";
import { Add, Search, Edit } from "@mui/icons-material";
import { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import { Task } from "../../models/Task";
import axios from "axios";

function Dashboard() {
    const [userTasks, setUserTasks] = useState<Task[]>([]);

    const navigate = useNavigate();

    const { username } = useParams<string>();

    useEffect(() =>
        {
            axios.get<Task[]>(`http://localhost:5182/tasks/list/${username}`)

            .then(({ data }) => setUserTasks(data)) 
            .catch(error => alert(`${error.response.statusText}: ${error.response.data}`));
        }
    );

    function status(id: number) {
        axios.put(`http://localhost:5182/tasks/status/${id}`);
        console.log(`Id: ${id} completed`);
      }
      
      function update(taskId: number) {
        navigate(`/tasks/update/${taskId}`)
      }  
      
    function remove(id: number) {
        axios.delete(`http://localhost:5182/tasks/delete/${id}`)
      }

    return (
        <>
            <TableContainer>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>#</TableCell>
                                <TableCell>Name</TableCell>
                                <TableCell>Description</TableCell>
                                <TableCell>Completed</TableCell>
                                <TableCell>Created</TableCell>
                                <TableCell>Labels</TableCell>
                                <TableCell>Status</TableCell>
                                <TableCell>Update Task</TableCell>
                                <TableCell>Delete Task</TableCell>
                                <TableCell>Look at Labels</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {
                                userTasks.map(({ taskId, name, description, isCompleted, createdAt, labels, userId}) => 
                                    (
                                        <TableRow key={taskId}>
                                            <TableCell>{taskId}</TableCell>
                                            <TableCell>{name}</TableCell>
                                            <TableCell>{description}</TableCell>
                                            <TableCell>{isCompleted ? "Yes" : "No"}</TableCell>
                                            <TableCell>{createdAt!.replace("T", " ")}</TableCell>
                                            <TableCell>{labels?.map(l => l.labelName).join(", ")}</TableCell>
                                            <TableCell> 
                                                <button onClick={() => {
                                                    status(taskId!);    }}>
                                                Complete
                                                </button>
                                            </TableCell> 
                                            <TableCell> 
                                                <button onClick={() => {
                                                    update(taskId!);    }}>
                                                Update
                                                </button>
                                            </TableCell>  
                                            <TableCell> 
                                                <button onClick={() => {
                                                    remove(taskId!);    }}>
                                                Delete
                                                </button>
                                            </TableCell>  
                                            <TableCell> 
                                                <button onClick={() =>  navigate(`/label/list/${taskId}`) }>
                                                    LABELS
                                                </button>
                                            </TableCell>    
                                        </TableRow>
                                    )
                                )
                            }
                        </TableBody>
                    </Table>
            </TableContainer>
            <div style={{ display: 'flex', justifyContent: 'center', position: 'fixed', bottom: '30px', left: '95%', transform: 'translateX(-50%)', gap: '16px' }}>

           

            <Tooltip title="Create Task">
                <Fab
                color="primary"
                aria-label="add"
                onClick={() => navigate("/tasks/create")}
                sx={{ marginRight: 3 }} 
                >
                    <Add />
                </Fab>
            </Tooltip>

            </div>
        </>
    );
}

export default Dashboard;