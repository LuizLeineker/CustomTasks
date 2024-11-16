import { Link } from "react-router-dom";

function Home() {
    return (
        <div>
            <h1>Home do website CustomTasks!</h1>
            <ul>
                <li>
                    <Link to="/signup">Signup</Link>
                </li>
                <li>
                    <Link to="/signin">Signin</Link>
                </li>
                <li>
                    <Link to="/list">List Users</Link>
                </li>
                <li>
                    <Link to="/tasks/create">Create Task</Link>
                </li>
                <li>
                    <Link to="/dashboard">Dashboard</Link>
                </li>
            </ul>
        </div>
    );
}

export default Home;