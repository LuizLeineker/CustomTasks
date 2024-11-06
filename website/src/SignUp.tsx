import { useEffect, useState } from "react";

function SignUp() {
    const [username, setUsername] = useState("")
    const [email, setEmail] = useState("")
    const [password, setPassword] = useState("")

    function criarUsuario() {
        fetch("http://localhost:5182/user/create", {
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
            if (!response.ok) {
                return window.alert("User already exists!");
            }
            console.log("Registrado!");
        })
    }

    return (
        <div>
            <div><input type="text" name="username" onChange={(e: any) => setUsername(e.target.value)} /></div>
            <div><input type="text" name="email" onChange={(e: any) => setEmail(e.target.value)} /></div>
            <div><input type="text" name="password" onChange={(e: any) => setPassword(e.target.value)} /></div>
            <div><input type="submit" value="Sign-Up" onClick={criarUsuario} /></div>
        </div>
    )
}

export default SignUp;