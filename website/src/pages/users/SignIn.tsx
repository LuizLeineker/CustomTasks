import { Email, EmailOutlined, LockOutlined, Login, Lock, LoginOutlined, Person, PersonOutline, Visibility, VisibilityOff } from "@mui/icons-material";
import { Box, Button, InputAdornment, Paper, TextField } from "@mui/material";
import axios from "axios";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

function SignIn() {
    const [username, setUsername] = useState("")
    const [email, setEmail] = useState("")
    const [password, setPassword] = useState("")
    const [showPassword, setShowPassword] = useState(false); // Define se a senha vai ser mostrada ou não

    const [hasError, setHasError] = useState(false); // Define se houve algum erro no preenchimento do formulário com os campos de nome e email
    const [errorMsg, setErrorMsg] = useState(""); // Armazena a mensagem de erro para os campos de nome e email

    const [passwordError, setPasswordError] = useState(false); // Define se houve algum erro no preenchimento do formulário com o campo de senha
    const [passwordErrorMsg, setPasswordErrorMsg] = useState(""); // Armazena a mensagem de erro para o campo de senha

    const navigate = useNavigate();

    function userLogin(e: any) {
        e.preventDefault();

        axios.post<string>("http://localhost:5182/user/login", 
            {
                username: username,
                email: email,
                password: password
            }
        )
        .then(({ data }) => navigate(`/dashboard/${data}`))
        .catch(error =>
            {
                const data = error.response?.data as string;
                if (data.includes("password")) {
                    setPasswordError(true);
                    setPasswordErrorMsg(data);
                    setHasError(false);
                }
                else {
                    setHasError(true);
                    setErrorMsg(data);
                    setPasswordError(false);
                }
            }
        );
    }

    return (
        <Box
        display="flex"
        justifyContent="center"
        alignItems="center"
        >
            <Paper sx={{padding: "15px"}}>
                <form onSubmit={userLogin}>
                    <div style={{margin: "15px"}}>
                        <TextField
                        type="text"
                        variant="standard"
                        label="Username"
                        autoFocus // Renderiza o componente focando nessa caixa
                        fullWidth
                        size="small"
                        onChange={(e: any) => setUsername(e.target.value)}
                        required
                        error={hasError}
                        helperText={hasError ? errorMsg : "Must have 5-30 characters length."}
                        slotProps={{input:
                            {
                                startAdornment: (
                                    <InputAdornment position="start">
                                       {username.length > 0 ? <Person /> : <PersonOutline />}
                                    </InputAdornment>
                                )
                            }
                        }}
                        />
                    </div>
                    <div style={{margin: "15px"}}>
                        <TextField
                        type="email"
                        variant="standard"
                        label="Email"
                        fullWidth
                        size="small"
                        onChange={(e: any) => setEmail(e.target.value)}
                        error={hasError}
                        helperText={hasError ? errorMsg : ""}
                        required
                        slotProps={{input:
                            {
                                startAdornment: (
                                    <InputAdornment position="start">
                                        {email.length > 0 ? <Email /> : <EmailOutlined />}
                                    </InputAdornment>
                                )
                            }
                        }}
                        />
                    </div>
                    <div style={{margin: "15px"}}>
                        <TextField
                        type={showPassword ? "text" : "password"}
                        variant="standard"
                        label="Password"
                        fullWidth
                        size="small"
                        onChange={(e: any) => setPassword(e.target.value)}
                        required
                        error={passwordError}
                        helperText={passwordError ? passwordErrorMsg : "Must have 5-30 characters length."}
                        slotProps={{input:
                            {
                                startAdornment: (
                                    <InputAdornment position="start">
                                        {password.length > 0 ? <Lock /> : <LockOutlined />}
                                    </InputAdornment>
                                ),
                                endAdornment: (
                                    <InputAdornment position="end">
                                        <span onClick={() => setShowPassword(!showPassword)} style={{cursor: "pointer"}}>
                                            {showPassword ? <VisibilityOff /> : <Visibility />}
                                        </span>
                                    </InputAdornment>
                                )
                            }
                        }}
                        />
                    </div>
                    <div style={{margin: "15px"}}>
                        <Button
                        type="submit"
                        variant="contained"
                        size="small"
                        fullWidth
                        endIcon={<LoginOutlined />}
                        sx={{"&:hover": 
                            {
                                backgroundColor: "lightskyblue"
                            }
                        }}
                        >
                            Sign In
                        </Button>
                    </div>
                </form>
            </Paper>
        </Box>
    );
}

export default SignIn;