import { Fab, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Tooltip } from "@mui/material";
import { useEffect, useState } from "react";
import { Label } from "../../models/Label";
import { useNavigate, useParams } from "react-router-dom";
import axios from "axios";
import { Add } from "@mui/icons-material";

function ListLabel() {
    const [labels, setUsers] = useState<Label[]>([]);
    const { username } = useParams<{ username: string }>();
    const navigate = useNavigate();
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
            <h1>Rótulos das Tarefas</h1>
            <TableContainer>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell> Nome do Rótulo </TableCell>
                                <TableCell>Deletar Rótulo </TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {
                                labels.map(({ labelId, labelName}) => 
                                    (
                                        <TableRow key={labelId}>
                                            <TableCell>{labelName}</TableCell>
                                            <TableCell> 
                                                <button onClick={() => {
                                                    remove(labelId!);    }}>
                                                Delete
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

            <Tooltip title="Create Label">
                <Fab
                color="primary"
                aria-label="add"
                onClick={() => navigate("/label/create")}
                >
                    <Add />
                </Fab>
            </Tooltip>
            
            </div>
        </div>
    );
    
}


export default ListLabel;