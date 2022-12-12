import React, {useState, useEffect, Fragment} from "react";
import { Modal, Button, TextInput, Icon } from 'react-materialize';
import { Link } from 'react-router-dom';
import backend from "../components/backend/backend.tsx";
import AuthService from "../services/auth.service";

function Cities() {
    const currentUser = AuthService.getCurrentUser();
    const [cities, setCities] = useState([]);
    const [cityDescription, setCityDescription] = useState('');
    const [errorDescription, setErrorDescription] = useState("1");
    // const [errorPriceOnCreate, setErrorPriceOnCreate] = useState("");

    const descriptionError = (data) => {
        if (data.length < 1) {
            setErrorDescription("Aprašymas negali būti tuščias");
        } else {
            setErrorDescription("");
            //setErrorPriceOnCreate("");
            setCityDescription(data);
        }
    };

    const onSubmitCityEdit = async (e, cityId) => {
        const a = AuthService.getCurrentUser();
        const headers = { 
            'Authorization': `Bearer ${a.accessToken}`
        };
        const url = `https://stppapiapp.azurewebsites.net/api/cities/${cityId}`;
        await backend.put(url, { description: cityDescription }, { headers });
        window.location.reload();
    }

    const removeCity = async (id) => {
        const a = AuthService.getCurrentUser();
        const headers = { 
            'Authorization': `Bearer ${a.accessToken}`
        };
        const url = `https://stppapiapp.azurewebsites.net/api/cities/${id}`;
        await backend.delete(url, { headers });
        console.log(id);
        window.location.reload();
    };

    useEffect(() => {
        const getAllCities = async() => {
          let response = await fetch('https://stppapiapp.azurewebsites.net/api/cities')
          let newcities = await response.json()
          setCities(newcities)
        };
        getAllCities();
      }, [])
    
	return(
        <div className="cities">
            <div className="container">
                <div className="section">
                    {renderCitiesTable()}
                </div>
                <br/><br/>
            </div>
        </div>
    );

    function renderCitiesTable() {
        return (
            <div className="row">
                {cities.map((city) => (
                    <div className="col s12 m4"key={city.id}>
                        <div className="card">
                            <div className="card-content">
                                <span className="card-title">{city.name}</span>
                                <p>{city.description}</p>
                            </div>
                            <div className="card-action">
                                <Link to={`/api/cities/${city.id}/ads`}>Peržiūrėti</Link>
                                {currentUser ? (
                                    <Fragment>
                                    <Modal
                                        actions={[
                                            <Button modal="close" node="button" onClick={(e)=> {onSubmitCityEdit(e, city.id)}}
                                            className='modal-close waves-effect blue btn' disabled={ errorDescription }>Išsaugoti
                                            <Icon right>
                                            save
                                            </Icon></Button>
                                        ]}
                                        bottomSheet={false}
                                        fixedFooter={false}
                                        header="Redaguoti miestą"
                                        id="ListingEditModal"
                                        open={false}
                                        options={{
                                            dismissible: true,
                                            endingTop: '10%',
                                            inDuration: 250,
                                            opacity: 0.5,
                                            outDuration: 250,
                                            preventScrolling: true,
                                            startingTop: '4%'
                                        }}
                                        trigger={<Button flat className="waves-effect waves-light" node="button">Redaguoti<Icon right>edit</Icon></Button>}
                                    >
                                        <TextInput 
                                            id="cityDescriptionInput"
                                            label="Aprašymas"
                                            onChange={(e => setCityDescription(e.target.value), e => descriptionError(e.target.value))}
                                        />
                                    </Modal>
                                    <button onClick={() => removeCity(city.id)} className="btn-flat waves-effect waves-light">
                                        šalinti
                                        <Icon right>delete</Icon>
                                    </button>
                                    <Link to={`/api/cities/${city.id}/adcreate`}>Pridėti skelbimą</Link>
                                    </Fragment>
                                ) : (
                                    <p></p>
                                )}
                                
                            </div>
                        </div>
                    </div>
                ))}
            </div>
        );
    }
}

export default Cities;