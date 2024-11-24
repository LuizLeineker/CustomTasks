import axios, { AxiosError } from "axios";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

function SignIn() {
    const [username, setUsername] = useState("")
    const [email, setEmail] = useState("")
    const [password, setPassword] = useState("")
    const [showPassword, setShowPassword] = useState(false);

    const navigate = useNavigate();

    function userLogin(e: any) {
        e.preventDefault();

        axios.post("http://localhost:5182/user/login", 
            {
                username: username,
                email: email,
                password: password
            }
        )
        .then((response) => navigate(`/dashboard/${response.data}`))
        .catch((error: AxiosError) => alert(`${error.response?.statusText}: ${error.response?.data}`))
    }
        // Trocar a URL do fetch pela do endpoint correto criado
        // fetch("o", {
        //     method: "POST",
        //     headers: {
        //         "Content-Type": "application/json"
        //     },
        //     body: JSON.stringify({
        //         username: username,
        //         email: email,
        //         password: password 
        //     })
        // })
        // .then(response => {
        //     // Alterar a lógica do if para que faça sentido com o login
        //     if (!response.ok) {
        //         // Alterar a mensagem exibida para alguma que faça sentido com o contexto de login
        //         return window.alert("User already exists!");
        //     }
        //     navigate("/dashboard");
        // })

    return (
        <form onSubmit={userLogin}>
            <div>
                <input type="text" name="username" onChange={(e: any) => setUsername(e.target.value)} required />
            </div>
            <div>
                <input type="text" name="email" onChange={(e: any) => setEmail(e.target.value)} required />
            </div>
            <div>
                <input type={showPassword ? "text" : "password"} name="password" onChange={(e: any) => setPassword(e.target.value)} required />
                <button type="button" onClick={(e: any) => setShowPassword(!showPassword)}>SHOW</button>
            </div>
            <div>
                <input type="submit" value="Sign in" />
            </div>
        </form>
    )
}

export default SignIn;