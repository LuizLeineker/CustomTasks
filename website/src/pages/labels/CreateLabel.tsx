import { useEffect, useState } from "react";
import { Label } from "../../models/Label";

function CreateLabel(){
    const [labelName, setLabelName] = useState("");
    const [userId, setUserId] = useState("");

    function newLabel(e : any){
        e.preventDefault();

        const label : Label = {
            labelName : labelName,
            userId : Number(userId)
        };

        fetch("http://localhost:5182/label/create", 
            {
                method : "POST",
                headers : {
                    "Content-Type" : "application/json"
                },
                body : JSON.stringify(label)
            })
            .then(resposta => {
                return resposta.json();
            })
           
    }
    return (
        <div>
            <h1>Insira um nome para o seu Rótulo: </h1>
            <form onSubmit={newLabel}>
                <div>
                    <input type="text" name="name" onChange={(e: any) => setLabelName(e.target.value)} placeholder="Insert name" required />
                </div>
                <div>
                    <input type="number" name="userId" onChange={(e: any) => setUserId(e.target.value)} placeholder="User ID" required />
                </div>
                <div>
                    <input type="submit" value="CREATE" />
                </div>
            </form>

        </div>
    


    );
}


export default CreateLabel;