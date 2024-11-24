import axios, { AxiosError } from "axios";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

function SignUp() {
    const [username, setUsername] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [showPassword, setShowPassword] = useState(false);

    const navigate = useNavigate();

    function registerUser(e: any) {
        e.preventDefault();

        axios.post("http://localhost:5182/user/create",
            {
                username: username,
                password: password,
                email: email
            }
        )
        .then(() => navigate("/signin"))
        .catch((error: AxiosError) => alert(`${error.response?.statusText}: ${error.response?.data}`))

        // fetch("http://localhost:5182/user/create", {
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
        //     if (!response.ok) {
        //         alert(`${response.text}`);
        //     } else {
        //         navigate("/signin");
        //     }
        // });
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
                    <input type={showPassword ? "text" : "password"} name="password" onChange={(e: any) => setPassword(e.target.value)} placeholder="Set a password" required />
                    <button type="button" onClick={(e: any) => setShowPassword(!showPassword)}>SHOW</button>
                </div>
                <div>
                    <input type="submit" value="Sign Up" />
                </div>
            </form>
        );
}

export default SignUp;