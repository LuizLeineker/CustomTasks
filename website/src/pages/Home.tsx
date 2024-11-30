import { Link } from "react-router-dom";

function Home() {
    return (
        <div>
            <header> Custom Task - mexer com css </header>
            <h1>Home do website CustomTasks!</h1>
            <ul>
                <li>
                    <Link to="/signup">Signup</Link>
                </li>
                <li>
                    <Link to="/signin">Signin</Link>
                </li>
            </ul>  

            <ul>  
                <li>
                    <Link to="/tasks/create">Create Task</Link>
                </li>
                <li>
                    <Link to="/tasks/list/username">Search</Link>
                </li>
                <li>
                    <Link to="tasks/update/taskId">Update</Link>
                </li>
            </ul>

            <ul>    
                <li>
                    <Link to="/label/create">New Label</Link>
                </li>
                <li>
                    <Link to="/label/list/username">Labels</Link>
                </li>
            </ul>
        </div>
    );
}

export default Home;