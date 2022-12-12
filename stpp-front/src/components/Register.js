import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { TextInput } from 'react-materialize';
import 'materialize-css/dist/css/materialize.css'
import "../styles/main.css";
import { useForm } from "react-hook-form";

function Register() {
    const navigate = useNavigate();
    const [username, setUserName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

const { register, handleSubmit, formState: { errors } } = useForm();
    const onSubmit = async (data) => {
        console.log(data);
        await fetch('https://stppapiapp.azurewebsites.net/api/register', {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({
                email,
                username,
                password
            })
        });
        navigate("/api/login");
    }

    let html =
        <div className="container">
            <div className="section">

            <center>
                <h5 className="blue-text">Sukurkite paskyrą</h5>
                <div className="section"></div>

                <form onSubmit={handleSubmit(onSubmit)}>
                    <div className="container">
                        <div className="login-register-page z-depth-1 grey lighten-4 row">
                            <div className='row'>
                                <div className='col s12'></div>
                            </div>
                            
                            <div className='row'>
                                <div className='input-field col s12'>
                                    <TextInput
                                        id="emailInput"
                                        label="El. paštas"
                                        email
                                        {...register("emailCheck", { required: true })}
                                        onChange={e => setEmail(e.target.value)}
                                    />
                                </div>
                            </div>
                            {errors.emailCheck && <p>Įveskite elektroninį paštą</p>}

                            <div className='row'>
                                <div className='input-field col s12'>
                                    <TextInput
                                        id="usernameInput"
                                        label="Naudotojo vardas"
                                        {...register("userNameCheck", { required: true, maxLength: 40 })}
                                        onChange={e => setUserName(e.target.value)}
                                    />
                                </div>
                            </div>
                            {errors.userNameCheck && <p>Įveskite naudotojo vardą</p>}

                            <div className='row'>
                                <div className='input-field col s12'>
                                    <TextInput
                                        id="passwordInput"
                                        label="Slaptažodis"
                                        password
                                        {...register("passwordCheck", { required: true })}
                                        onChange={e => setPassword(e.target.value)}
                                    />
                                </div>
                            </div>
                            {errors.passwordCheck && <p>Įveskite slaptažodį</p>}

                            <br/>
                            <center>
                                <div className="row center">
                                    <button className="btn-large waves-effect waves-light blue" type="submit">Registruotis</button>
                                </div>
                            </center>
                        </div>
                    </div>
                </form>
            </center>

            </div>
            <br/><br/>
        </div>

    return html;
}
    
export default Register;