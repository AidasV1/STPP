import React, {Fragment} from 'react';
import ReactDOM from "react-dom";
import { NavLink } from "react-router-dom";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Navbar from './components/Navbar';
import Footer from './components/Footer';
import Listings from './components/Listings';

function App() {

  let html =
  <Fragment>
    <Navbar/>
    <div class="section no-pad-bot" id="index-banner">
      <div class="container">
        <br/><br/>
        <h1 class="header center blue-text">Trumpalaikės nuomos svetainė</h1>
        <div class="row center">
          <h5 class="header col s12 light">Tempor lacus aliquam class sapien per lacinia.</h5>
        </div>
        <div class="row center">
          <a href="Listings" id="download-button" class="btn-large waves-effect waves-light blue">Ieškoti</a>
        </div>
        <br/><br/>
      </div>
    </div>
    <div class="container">
      <div class="section">
        <div class="row">
          <div class="col s12 m4">
            <div class="icon-block">
              <h2 class="center light-blue-text"><i class="material-icons">euro_symbol</i></h2>
              <h5 class="center">Rutrum dolor malesuada</h5>
              <p class="light">Nullam risus nullam nibh suscipit. Pulvinar dolor ligula. Pulvinar urna litora a dapibus. Aliquet magna rutrum urna convallis. Sapien duis montes. Phasellus et magnis non venenatis risus consequat.</p>
            </div>
          </div>

          <div class="col s12 m4">
            <div class="icon-block">
              <h2 class="center light-blue-text"><i class="material-icons">location_city</i></h2>
              <h5 class="center">Hendrerit metus</h5>
              <p class="light">Hendrerit metus blandit non faucibus dui varius. Luctus leo consectetuer nulla auctor. Lobortis justo platea risus sociis dis varius. Cursus id molestie. Sapien et tristique.</p>
            </div>
          </div>

          <div class="col s12 m4">
            <div class="icon-block">
              <h2 class="center light-blue-text"><i class="material-icons">date_range</i></h2>
              <h5 class="center">Ligula eget litora</h5>
              <p class="light">Pulvinar cum rhoncus. Taciti eget pellentesque. Ornare per nonummy in nonummy nibh laoreet. Tempus dui consectetuer. Ultricies eros quisque. Taciti fusce ridiculus vitae sagittis quis congue. Gravida proin vulputate nulla hendrerit.</p>
            </div>
          </div>
        </div>
      </div>
      <br/><br/>
    </div>
    <Footer/>
    </Fragment>
  return html;
}

export default App;