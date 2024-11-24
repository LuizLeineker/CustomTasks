import { useEffect, useState } from "react";
import { Task } from "../models/Task";
import CreateTask from "./tasks/CreateTask";
import { Label } from "../models/Label";
import axios, { AxiosError } from "axios";
import { useParams } from "react-router-dom";

function Dashboard() {
    const [userTasks, setUserTasks] = useState<Task[]>([]);
    const [userLabels, setUserLabels] = useState<Label[]>([]);


    const {username} = useParams();

    useEffect(() => 
        {
            axios.get(`http://localhost:5182/user/${username}`)
            .then(response => response.data)
            .then(({tasks, labels}) =>
                {
                    setUserTasks(tasks), setUserLabels(labels)
                }
            )
            .catch((error: AxiosError) => alert(`${error.response?.statusText}: ${error.response?.data}`))
        }
    )

        // fetch("http://localhost:5182/user/1")
        // .then(response => response.json())
        // .then(user => {
        //     setUserLabels(user.labels), setUserTasks(user.tasks)
        // });

    return (
        <div>
            <div id="task-container">
                {userTasks.map(ut => (
                    <div key={ut.taskId}>
                        <h1>{ut.name}</h1>
                        <p>{ut.description}</p>
                    </div>
                ))}
            </div>
            <CreateTask userLabels={userLabels}/>
        </div>
    );
}

export default Dashboard;