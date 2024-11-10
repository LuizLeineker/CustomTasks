import { useState } from "react";
import { useNavigate } from "react-router-dom";

function SignUp() {
    const [username, setUsername] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const navigate = useNavigate();

    function registerUser(e: any) {
        e.preventDefault();

        if (username.length < 5 || username.length > 30) {
            return window.alert("The username must have at least 5 characters and does not exceed 30.");
        }

        if (email.length > 254) {
            return window.alert("The maximum length of an email address is 254 characters.");
        }

        if (password.length < 5 || username.length > 30) {
            return window.alert("The password must have at least 5 characters and does not exceed 30.");
        }

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
                window.alert("User already exists!");
            } else {
                console.log("User created successfully");
                navigate("/teste");
            }
        });
    }

        return (
            <form onSubmit={registerUser}>
                <div>
                    <input type="text" name="username" onChange={(e: any) => setUsername(e.target.value)} placeholder="Choose a username" required />
                </div>
                <div>
                    <input type="email" name="email" onChange={(e: any) => setEmail(e.target.value)} placeholder="Enter an email address" required />
                </div>
                <div>
                    <input type="password" name="password" onChange={(e: any) => setPassword(e.target.value)} placeholder="Set a password" required />
                </div>
                <div>
                    <input type="submit" value="Sign Up" />
                </div>
            </form>
        );
}

export default SignUp;