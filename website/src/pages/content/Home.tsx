import { TextField } from "@mui/material";
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
                    <Link to="/dashboard">COLOCAR DIRETO NO LOGIN PRA DIRECIONAR PRA DASH</Link>
                </li>
            </ul>
        </div>
    );
}

export default Home;