import { useEffect, useState } from "react";

function SignIn() {
    const [username, setUsername] = useState("")
    const [email, setEmail] = useState("")
    const [password, setPassword] = useState("")

    function userLogin() {
        // Trocar a URL do fetch pela do endpoint correto criado
        fetch("o", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                username: username,
                email: email,
                password: password 
            })
        })
        .then(response => {
            // Alterar a lógica do if para que faça sentido com o login
            if (!response.ok) {
                // Alterar a mensagem exibida para alguma que faça sentido com o contexto de login
                return window.alert("User already exists!");
            }
            console.log("Registrado!"); // Não faz sentido
        })
    }

    return (
        <div>
        <h1>LOGIN</h1>   
        <form onSubmit={userLogin}>
            <div>
                <input type="text" name="username" onChange={(e: any) => setUsername(e.target.value)} required />
            </div>
            <div>
                <input type="text" name="email" onChange={(e: any) => setEmail(e.target.value)} required />
            </div>
            <div>
                <input type="password" name="password" onChange={(e: any) => setPassword(e.target.value)} required />
            </div>
            <div>
                <input type="submit" value="Sign in" />
            </div>
        </form>
        </div>
    )
}

export default SignIn;