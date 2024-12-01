import { Link } from "react-router-dom";

function Dashboard() {
    return (
        <div>
            <h1>Seu menu de Tarefas!</h1>
            <ul>  
                <li>
                    <Link to="/tasks/create">Create Task</Link>
                </li>
                <li>
                    <Link to="/tasks/list/username">Search</Link>
                </li>
                <li>
                    <Link to="/tasks/update/taskId">Update</Link>
                </li>
                <li>
                    <Link to="/label">Labels - Colocar aqui o de list Label direto?</Link> 
                </li>
            </ul>
        </div>
    );
}

export default Dashboard;