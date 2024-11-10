import { Link } from "react-router-dom";

function Home() {
    return (
        <div>
            <h1>Home do website CustomTasks!</h1>
            <Link to="/signup">Signup</Link>
        </div>
    );
}

export default Home;