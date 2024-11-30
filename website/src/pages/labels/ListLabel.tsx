import { useEffect, useState } from "react";
import { Label } from "../../models/Label";
import { useParams } from "react-router-dom";
import axios from "axios";

function ListLabel() {
    const [labels, setUsers] = useState<Label[]>([]);
    const { username } = useParams<{ username: string }>();
    useEffect(() =>{
        fetch(`http://localhost:5182/label/list/${username}`)
            .then(resposta => {
                if (!resposta.ok) {
                    throw new Error('Erro na resposta do servidor');
                  }
                return resposta.json();
            })
            .then(labels => {
                setUsers(labels)
            });
        
    });

    function remove(id: number) {
        axios
          .delete(`http://localhost:5182/label/delete/${id}`)
      }

    return (
        <div>
        <h1>Rótulos das Tarefas - Insira o nome na url</h1>
        <table >
            <thead>
                <tr>
                    <th> Nome do Rótulo </th>
                    <th> Deletar Rótulo </th>
                </tr>
            </thead>
            <tbody>
                {labels.map(label => (
                    <tr key={label.labelId}>
                        <td>{label.labelName}</td>
                        <td>
                            <button
                            onClick={() => {
                                remove(label.labelId!);
                            }}>
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


export default ListLabel;