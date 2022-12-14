import React, {useState, useEffect, Fragment} from "react";
import { Modal, Button, TextInput, Icon } from 'react-materialize';
import { Link } from 'react-router-dom';
import { useParams } from 'react-router-dom';
//import { useCityData } from './hooks/useCityData'
import sample from './sample.jpg';
import backend from "../components/backend/backend.tsx";
import AuthService from "../services/auth.service";

function Listings() {
    const currentUser = AuthService.getCurrentUser();
    const { cityId } = useParams();
    //const { data } = useCityData(cityId);

    const [listings, setListings] = useState([]);
    const [listingPrice, setListingPrice] = useState('');

    const [errorPrice, setErrorPrice] = useState("1");
    // const [errorPriceOnCreate, setErrorPriceOnCreate] = useState("");

    const priceError = (data) => {
        if (data.length < 1) {
            setErrorPrice("Atsiliepimas turi bent 10 simbolių ilgio");
        } else {
            setErrorPrice("");
           // setErrorPriceOnCreate("");
            setListingPrice(data);
        }
    };


    const onSubmitListingEdit = async (e, listingId) => {
        const a = AuthService.getCurrentUser();
        const headers = { 
            'Authorization': `Bearer ${a.accessToken}`
        };
        const url = `https://stppapiapp.azurewebsites.net/api/cities/${cityId}/ads/${listingId}`;
        await backend.put(url, { price: listingPrice }, { headers });
        window.location.reload();
    }

    const removeListing = async (id) => {
        const a = AuthService.getCurrentUser();
        const headers = { 
            'Authorization': `Bearer ${a.accessToken}`
        };
        const url = `https://stppapiapp.azurewebsites.net/api/cities/${cityId}/ads/${id}`;
        await backend.delete(url, { headers });
        console.log(id);
        window.location.reload();
    };

    useEffect(() => {
        const getAllListings = async() => {
          let response = await fetch(`https://stppapiapp.azurewebsites.net/api/cities/${cityId}/ads`)
          let newlistings = await response.json()
          setListings(newlistings)
        };
        getAllListings();
      }, [])
    
	return(
        <div className="listings">
            <div className="container">
                <div className="section">
                    {renderListingTable()}
                </div>
                <br/><br/>
            </div>
        </div>
    );

    function renderListingTable() {
        return (
            <div className="row">
                {listings.map((listing) => (
                    <div className="col s12 m4"key={listing.id}>
                        <div className="card">
                            <div className="card-image">
                                <img src={sample} alt="Listing"/>
                                <span className="card-title">{listing.city}</span>
                            </div>
                            <div className="card-content">
                                <p>Kaina: {listing.price} €</p>
                                <p>Lovų skaičius: {listing.bedCount}</p>
                            </div>
                            <div className="card-action">
                                <Link to={`/api/cities/${cityId}/ads/${listing.id}`}>Peržiūrėti</Link>
                                {currentUser ? (
                                    <Fragment>
                                    <Modal
                                        actions={[
                                            <Button modal="close" node="button" onClick={(e)=> {onSubmitListingEdit(e, listing.id)}}
                                            className='modal-close waves-effect blue btn' disabled={ errorPrice }>Išsaugoti
                                            <Icon right>
                                            save
                                            </Icon></Button>
                                        ]}
                                        bottomSheet={false}
                                        fixedFooter={false}
                                        header="Redaguoti skelbimą"
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
                                            id="listingPriceInput"
                                            label="Kaina"
                                            type="number"
                                            onChange={(e => setListingPrice(e.target.value), e => priceError(e.target.value))}
                                        />
                                        {errorPrice ? <p>Įveskite kainą</p> : <span></span>}
                                    </Modal>
                                    <button onClick={() => removeListing(listing.id)} className="btn-flat waves-effect waves-light">
                                        šalinti
                                        <Icon right>delete</Icon>
                                    </button>
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

export default Listings;