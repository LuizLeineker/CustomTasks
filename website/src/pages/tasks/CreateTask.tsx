import { useEffect, useState } from "react";
import { Task } from "../../models/Task";
import { Label } from "../../models/Label";

function CreateTask({userLabels}: {userLabels: Label[]}){
    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const [userId, setUserId] = useState("");

    function createTask(e : any){
        e.preventDefault();

        const task : Task = {
            name : name,
            description : description,
            userId : Number(userId),
        };

        fetch("http://localhost:5182/tasks/create", 
            {
                method : "POST",
                headers : {
                    "Content-Type" : "application/json"
                },
                body : JSON.stringify(task)
            })
            .then(resposta => resposta.json())
            .then(produto => {
                console.log("Task Create", produto);
            });
    }
    
    return (
        <div>
            <form onSubmit={createTask}>
                <div>
                    <input type="text" name="name" onChange={(e: any) => setName(e.target.value)} placeholder="Insert name" required />
                </div>
                <div>
                    <input type="text" name="description" onChange={(e: any) => setDescription(e.target.value)} placeholder="Insert description"/>
                </div>
                <div>
                    <input type="number" name="userId" onChange={(e: any) => setUserId(e.target.value)} placeholder="User ID" required />
                </div>
                <select>
                    {userLabels.map((ul => (
                        <option key={ul.labelId} value={ul.labelId}>{ul.labelName}</option>
                    )))}
                </select>
                <div>
                    <input type="submit" value="CREATE" />
                </div>
            </form>
        </div>
    );
}


export default CreateTask;