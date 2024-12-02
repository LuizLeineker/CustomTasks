import { Email, EmailOutlined, Lock, LockOutlined, Person, PersonOutline, SendOutlined, Visibility, VisibilityOff } from "@mui/icons-material";
import { Box, Button, InputAdornment, Paper, TextField } from "@mui/material";
import axios from "axios";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

function SignUp() {
    const [username, setUsername] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [showPassword, setShowPassword] = useState(false); // Define se a senha vai ser mostrada ou não
    const [hasError, setHasError] = useState(false); // Define se houve algum erro no preenchimento do formulário
    const [errorMsg, setErrorMsg] = useState(""); // Armazena a mensagem retornada da API

    const navigate = useNavigate();

    function registerUser(e: any) {
        e.preventDefault();

        axios.post("http://localhost:5182/user/create",
            {
                username: username,
                email: email,
                password: password
            }
        )
        .then(()=> navigate("/signin"))
        .catch(error => 
            {
                setHasError(true); 
                setErrorMsg(error.response?.data as string);
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
                    <form onSubmit={registerUser}>
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
                            helperText={"Must have 5-30 characters length."}
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
                            endIcon={<SendOutlined />}
                            sx={{"&:hover": 
                                {
                                    backgroundColor: "lightskyblue"
                                }
                            }}
                            >
                                Sign Up
                            </Button>
                        </div>
                    </form>
                </Paper>
            </Box>
        );
}

export default SignUp;