import React, { useState } from "react";
import { TextInput, Icon } from 'react-materialize';
import { useNavigate, useParams } from 'react-router-dom';
import backend from "../components/backend/backend.tsx";
import AuthService from "../services/auth.service";
import 'materialize-css/dist/css/materialize.css'
import { useForm } from "react-hook-form";

function ListingCreate() {
    const { cityId } = useParams();
    const navigate = useNavigate();
    const [price, setPrice] = useState('');
    const [bedCount, setBedCount] = useState('');

    const { register, handleSubmit, formState: { errors } } = useForm();
    const onSubmit = async (data) => {
        console.log(data);
        const a = AuthService.getCurrentUser();
        const headers = { 
            'Authorization': `Bearer ${a.accessToken}`
        };
        const url = `https://stppapiapp.azurewebsites.net/api/cities/${cityId}/ads`;
        await backend.post(url, { price: price, bedCount: bedCount }, { headers });
        navigate(`/api/cities/${cityId}/ads`);
    }

    let html =
        <div className="container">
            <div className="section">

            <center>
                <h5 className="blue-text">Sukurkite skelbimą</h5>
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
                                        type="number"
                                        id="priceInput"
                                        label="Kaina"
                                        {...register("price1", { required: true })}
                                        onChange={e => setPrice(e.target.value)}
                                    />
                                </div>
                            </div>
                            {errors.price1 && <p>Įveskite kainą</p>}

                            <div className='row'>
                                <div className='input-field col s12'>
                                    <TextInput
                                        type="number"
                                        id="bedCountInput"
                                        label="Lovų skaičius"
                                        {...register("bed1", { required: true })}
                                        onChange={e => setBedCount(e.target.value)}
                                    />
                                </div>
                            </div>
                            {errors.bed1 && <p>Įveskite lovų kiekį</p>}

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
    
export default ListingCreate;