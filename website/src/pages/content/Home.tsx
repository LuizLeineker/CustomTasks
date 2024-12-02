import { Link } from "react-router-dom";

function Home() {
    return (
        <div style={{justifyContent: 'center', textAlign: 'center'}}>

            <h1 style={{fontSize: '100px'}}> Custom Task</h1>

            <h2 style={{fontSize: '80px'}}>Seja Bem-Vindo, anote suas tarefas e facilice sua vida! </h2>
            <ul>
                <li style={{fontSize: '60px'}}>
                    <Link to="/signup">Signup</Link>
                </li>
                <li style={{fontSize: '60px'}}>
                    <Link to="/signin">Signin</Link>
                </li>
            </ul>  
        </div>
    );
}

export default Home;