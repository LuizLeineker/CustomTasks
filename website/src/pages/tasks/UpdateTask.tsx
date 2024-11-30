import { useEffect, useState } from "react";
import { Task } from "../../models/Task";
import { useParams } from "react-router-dom";

function UpdateTask(){
    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const [userId, setUserId] = useState("");

    const { taskId } = useParams();
    
    function update(e : any){
        e.preventDefault();

        const task : Task = {
            name : name,
            description : description,
            userId : Number(userId)
        };

        fetch(`http://localhost:5182/tasks/update/${taskId}`, 
            {
                method : "PUT",
                headers : {
                    "Content-Type" : "application/json"
                },
                body : JSON.stringify(task)
            })
            .then(resposta => {
                console.log("Task Update");
                return resposta.json();
            })
           
    }
    return (
        <div>
            <form onSubmit={update}>
                <div>
                    <input type="text" name="name" onChange={(e: any) => setName(e.target.value)} placeholder="Insert name" required />
                </div>
                <div>
                    <input type="text" name="description" onChange={(e: any) => setDescription(e.target.value)} placeholder="Insert description"/>
                </div>
                
                <div>
                    <input type="submit" value="UPDATE" />
                </div>
            </form>

        </div>
    


    );
}


export default UpdateTask;