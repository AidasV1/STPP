import React, { useState } from "react";
import { TextInput, Icon } from 'react-materialize';
import { useNavigate } from 'react-router-dom';
import backend from "../components/backend/backend.tsx";
import AuthService from "../services/auth.service";
import 'materialize-css/dist/css/materialize.css'
import { useForm } from "react-hook-form";

function CityCreate() {
    const navigate = useNavigate();
    const [cityName, setCityName] = useState('');
    const [cityDescription, setCityDescription] = useState('');

    const { register, handleSubmit, formState: { errors } } = useForm();
    const onSubmit = async (data) => {
        console.log(data);
        const a = AuthService.getCurrentUser();
        const headers = { 
            'Authorization': `Bearer ${a.accessToken}`
        };
        const url = `https://stppapiapp.azurewebsites.net/api/cities`;
        await backend.post(url, { name: cityName, description: cityDescription }, { headers });
        navigate("/api/cities");
    }

    let html =
        <div className="container">
            <div className="section">

                <center>
                    <h5 className="blue-text">Įveskite miesto duomenis</h5>
                    <div className="section"></div>

                    {/* <form onSubmit={submitListingCreate}> */}
                    <form onSubmit={handleSubmit(onSubmit)}>
                        <div className="container">
                            <div className="login-register-page z-depth-1 grey lighten-4 row">
                                <div className='row'>
                                    <div className='col s12'></div>
                                </div>
                                
                                <div className='row'>
                                    <div className='input-field col s12'>
                                        <TextInput
                                            id="cityNameInput"
                                            label="Miestas"
                                            {...register("cityName", { required: true, maxLength: 40 })}
                                            onChange={e => setCityName(e.target.value)}
                                        />
                                    </div>
                                </div>
                                {errors.cityName && <p>Įveskite miestą</p>}

                                <div className='row'>
                                    <div className='input-field col s12'>
                                        <TextInput
                                            id="cityDescriptionInput"
                                            label="Aprašymas"
                                            {...register("desc1", { required: true })}
                                            onChange={e => setCityDescription(e.target.value)}
                                        />
                                    </div>
                                </div>
                                {errors.desc1 && <p>Įveskite aprašymą</p>}

                                <br/>
                                <center>
                                    <div className="row center">
                                        <button className="btn-large waves-effect waves-light blue" type="submit">Išsaugoti<Icon right>save</Icon></button>
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
    
export default CityCreate;