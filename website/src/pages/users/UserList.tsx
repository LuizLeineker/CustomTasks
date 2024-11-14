import { useEffect, useState } from "react";
import { User } from "../../models/User";



/* NÃO TEM SENTIDO TER UMA FUNÇÃO DE LIST, POREM É PROVISORIA  */
function UserList(){
    const [users, setUsers] = useState<User[]>([]);
    useEffect(() =>{
        fetch('http://localhost:5182/user/list')
            .then(resposta => {
                if (!resposta.ok) {
                    throw new Error('Erro na resposta do servidor');
                  }
                return resposta.json();
            })
            .then(users => {
                setUsers(users)
            });
        
    });
    return (
        <div>
        <h1>USERS</h1>
        <table>
            <thead>
                <tr>
                    <th>NAME</th>
                    <th>EMAIL</th>
                </tr>
            </thead>
            <tbody>
                {users.map(user => (
                    <tr>
                        <td>{user.username}</td>
                        <td>{user.email}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    </div>
    


    );
}




export default UserList;