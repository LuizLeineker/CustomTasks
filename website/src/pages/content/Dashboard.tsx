import { Fab, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Tooltip } from "@mui/material";
import { Add } from "@mui/icons-material";
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
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {
                                userTasks.map(({ taskId, name, description, isCompleted, createdAt, labels }) => 
                                    (
                                        <TableRow key={taskId}>
                                            <TableCell>{taskId}</TableCell>
                                            <TableCell>{name}</TableCell>
                                            <TableCell>{description}</TableCell>
                                            <TableCell>{isCompleted ? "Yes" : "No"}</TableCell>
                                            <TableCell>{createdAt!.replace("T", " ")}</TableCell>
                                            <TableCell>{labels?.map(l => l.labelName).join(", ")}</TableCell>
                                        </TableRow>
                                    )
                                )
                            }
                        </TableBody>
                    </Table>
            </TableContainer>
            <Tooltip title="Create task">
                <Fab
                color="primary"
                aria-label="add"
                onClick={() => navigate(`/tasks/create/${username}`)}
                >
                    <Add />
                </Fab>
            </Tooltip>
        </>
    );
}

export default Dashboard;