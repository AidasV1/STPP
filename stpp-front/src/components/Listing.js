import React, {useState, Fragment} from "react";
import { useParams } from 'react-router-dom';
import sample from './sample.jpg';
import { Parallax, Modal, Button, TextInput, DatePicker, Icon } from 'react-materialize';
import { useListingData } from './hooks/useListingData'
import backend from "../components/backend/backend.tsx";
import AuthService from "../services/auth.service";

function Listing() {
    const { cityId, listingId } = useParams();
    const { data } = useListingData(cityId, listingId);
    const currentUser = AuthService.getCurrentUser();

    const [showReservations, setShowReservations] = useState(true);
    const [reservations, setReservations] = useState([]);

    const [reservationFrom, setReservationFrom] = useState(new Date());
    const [reservationTo, setReservationTo] = useState(new Date());
    const [reservationGuestCount, setReservationGuestCountCreate] = useState();
    const [reservationFromCreate, setReservationFromCreate] = useState(new Date());
    const [reservationToCreate, setReservationToCreate] = useState(new Date());

    const [errorGuestCount, setGuestCount] = useState("1");

    const guestCountError = (errdata) => {
        if (errdata.length < 1) {
            setGuestCount("Laukelis negali būti tuščias");
        } else {
            setGuestCount("");
           setReservationGuestCountCreate(errdata);
        }
    };

    function getReservations() {
        const url = `https://stppapiapp.azurewebsites.net/api/cities/${cityId}/ads/${listingId}/reservations`;
        backend.get(url).then((resp) => {
          setReservations(resp.data);
          console.log(resp.data);
        });
        setShowReservations(!showReservations);
    }

    const removeReservations = async (id) => {
        console.log(id);
        console.log(currentUser)
        const a = AuthService.getCurrentUser();
        const headers = { 
            'Authorization': `Bearer ${a.accessToken}`
        };
        const url = `https://stppapiapp.azurewebsites.net/api/cities/${cityId}/ads/${listingId}/reservations/${id}`;
        await backend.delete(url, { headers });
        console.log(id);
        const newReservations = reservations.filter((reservation) => {
          return reservation.id !== id;
        });
    
        setReservations(newReservations);
    };

    const onSubmitReservationEdit = async (reservationId) => {
        const a = AuthService.getCurrentUser();
        const headers = { 
            'Authorization': `Bearer ${a.accessToken}`
        };
        const url = `https://stppapiapp.azurewebsites.net/api/cities/${cityId}/ads/${listingId}/reservations/${reservationId}`;
        console.log("res")
        console.log(reservationFrom)
        console.log(reservationTo)
        await backend.put(url, { startDate: reservationFrom, endDate: reservationTo }, { headers });
        window.location.reload();
    }

    const onSubmitReservationCreate = async () => {
        const a = AuthService.getCurrentUser();
        const headers = { 
            'Authorization': `Bearer ${a.accessToken}`
        };
        const url = `https://stppapiapp.azurewebsites.net/api/cities/${cityId}/ads/${listingId}/reservations`;
        console.log("res")
        console.log(reservationFromCreate)
        console.log(reservationToCreate)
        await backend.post(url, { guestCount: reservationGuestCount, startDate: reservationFromCreate, endDate: reservationToCreate }, { headers });
        window.location.reload();
    }

	let html =
        <div>
            <Parallax
                image={<img alt="" src={sample}/>}
                options={{
                responsiveThreshold: 0
                }}
            />
            <div className="section white">
                <div className="row container">
                    <h2 className="header">
                        {data?.data.city}
                    </h2>
                    <p className="grey-text text-darken-3 lighten-3">
                    Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.
                     Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
                    </p>
                    <br></br>
                    <p className="grey-text text-darken-3 lighten-3">
                    Kaina: {data?.data.price} €
                    </p>
                    <p className="grey-text text-darken-3 lighten-3">
                    Lovų skaičius: {data?.data.bedCount}
                    </p>
                </div>
            </div>
            <div>
                <div className="container">
                    <div className="row min-vh-100">
                        <div>
                            <div>
                                {currentUser ? (
                                        <Fragment>
                                        <Modal
                                            actions={[
                                                <Button modal="close" node="button" onClick={()=> {onSubmitReservationCreate()}}
                                                className='modal-close waves-effect blue btn' disabled={ errorGuestCount }>Išsaugoti
                                                <Icon right>
                                                save
                                                </Icon></Button>
                                            ]}
                                            bottomSheet={false}
                                            fixedFooter={false}
                                            header="Rezervuoti"
                                            id="ReservationCreateModal"
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
                                            trigger={<Button className="btn-large waves-effect waves-light teal" node="button">Rezervuoti</Button>}
                                        >
                                            <TextInput 
                                                type="number"
                                                id="reservationCreateInput"
                                                label="Svečių skaičius"
                                                onChange={(e => setReservationGuestCountCreate(e.target.value), e => guestCountError(e.target.value))}
                                            />
                                            {errorGuestCount ? <p>Įveskite svečių kiekį</p> : <span></span>}
                                            <DatePicker
                                                label="Data nuo"
                                                id="DatePickerFrom"
                                                onChange={() => setReservationFromCreate(reservationFromCreate)}
                                                options={{
                                                    events: [],
                                                    firstDay: 1,
                                                    format: 'yyyy-mm-dd',
                                                    i18n: {
                                                    cancel: 'Cancel',
                                                    clear: 'Clear',
                                                    done: 'Ok',
                                                    nextMonth: '›',
                                                    previousMonth: '‹',
                                                    },
                                                    yearRange: 10
                                                }}
                                            />
                                            <DatePicker
                                                label="Data iki"
                                                id="DatePickerTo"
                                                onChange={(dateTo) => setReservationToCreate(dateTo)}
                                                options={{
                                                    events: [],
                                                    firstDay: 1,
                                                    format: 'yyyy-mm-dd',
                                                    i18n: {
                                                    cancel: 'Cancel',
                                                    clear: 'Clear',
                                                    done: 'Ok',
                                                    },
                                                    yearRange: 10
                                                }}
                                            />
                                            <br></br>
                                            <br></br>
                                            <br></br>
                                            <br></br>
                                            <br></br>
                                            <br></br>
                                            <br></br>
                                            <br></br>
                                            <br></br>
                                            <br></br>
                                        </Modal>
                                        </Fragment>
                                    ) : (
                                        <p></p>
                                )}
                            </div>
                            <br></br>
                            <div>
                                {showReservations ? (
                                    <button onClick={getReservations} className="btn-large waves-effect waves-light blue">
                                        Peržiūrėti rezervacijas
                                    </button>
                                ) : (
                                    <p></p>
                                )}
                            </div>
                            {reservations.length > 0 && renderReservationsTable()}
                        </div>
                    </div>
                </div>
            </div>
        </div>

	return html;

    function renderReservationsTable() {
        return (
            <table className="table table-bordered border-dark">
              <thead>
                <tr>
                  <th scope="col"></th>
                  <th scope="col">Rezervuota nuo</th>
                  <th scope="col">Rezervuota iki</th>
                </tr>
              </thead>
              <tbody>
                {reservations.map((reservation) => (
                  <tr key={reservation.id}>
                    <th scope="row">{reservation.id}</th>
                    <td>{reservation.startDate}</td>
                    <td>{reservation.endDate}</td>
                    <td>
                        {currentUser ? (
                            <Fragment>
                            <Modal
                                actions={[
                                    <Button modal="close" node="button" onClick={()=> {onSubmitReservationEdit(reservation.id)}}
                                    className='modal-close waves-effect blue btn'>Išsaugoti
                                    <Icon right>
                                    save
                                    </Icon></Button>
                                ]}
                                bottomSheet={false}
                                fixedFooter={false}
                                header="Redaguoti rezervaciją"
                                id="ReservationEditModal"
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
                                trigger={<Button className="waves-effect waves-light blue" node="button">Redaguoti<Icon right>edit</Icon></Button>}
                            >
                                <DatePicker
                                    label="Data nuo"
                                    id="DatePickerFrom"
                                    // onChange={e => setReservationFrom(e.target.value)}
                                    onChange={() => setReservationFrom(reservationFrom)}
                                    options={{
                                        events: [],
                                        firstDay: 1,
                                        format: 'yyyy-mm-dd',
                                        i18n: {
                                        cancel: 'Cancel',
                                        clear: 'Clear',
                                        done: 'Ok',
                                        },
                                        yearRange: 10
                                    }}
                                />
                                <DatePicker
                                    label="Data iki"
                                    id="DatePickerTo"
                                    onChange={(dateTo) => setReservationTo(dateTo)}
                                    options={{
                                        events: [],
                                        firstDay: 1,
                                        format: 'yyyy-mm-dd',
                                        i18n: {
                                        cancel: 'Cancel',
                                        clear: 'Clear',
                                        done: 'Ok',
                                        },
                                        yearRange: 10
                                    }}
                                />
                                <br></br>
                                <br></br>
                                <br></br>
                                <br></br>
                                <br></br>
                                <br></br>
                                <br></br>
                                <br></br>
                                <br></br>
                                <br></br>
                            </Modal>
                            </Fragment>
                        ) : (
                            <p></p>
                        )}
                    </td>
                    <td>
                        {currentUser ? (
                                    <button onClick={() => removeReservations(reservation.id)} className="btn waves-effect waves-light blue">
                                        Pašalinti
                                        <Icon right>delete</Icon>
                                    </button>
                                ) : (
                                    <p></p>
                                )}
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
        );
    }
}

export default Listing;