import React from "react";
import ReactDOM from "react-dom";
import { QueryClientProvider, QueryClient } from "react-query";
import { ReactQueryDevtools } from "react-query/devtools";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Home from './components/Home';
import Navbar from './components/Navbar';
//import Footer from './components/Footer';
import Listings from './components/Listings';
import Listing from './components/Listing';
import 'materialize-css/dist/css/materialize.min.css'
import 'materialize-css/dist/css/materialize.css'
import Login from "./components/Login";
import Register from "./components/Register";
import Cities from "./components/Cities";
import CityCreate from "./components/CityCreate";
import ListingCreate from "./components/ListingCreate";
import './styles/main.css';

const queryClient = new QueryClient()

ReactDOM.render(
  <QueryClientProvider client={queryClient}>
  <Router>
    <Navbar />
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/api/login" element={<Login />} />
      <Route path="/api/register" element={<Register />} />
      <Route path="/api/citycreate" element={<CityCreate/>} />
      <Route path="/api/cities" element={<Cities />} />
      <Route path="/api/cities/:cityId/ads" element={<Listings />} />
      <Route path="/api/cities/:cityId/ads/:listingId" element={<Listing />} />
      <Route path="/api/cities/:cityId/adcreate" element={<ListingCreate />} />
    </Routes>
    {/* <Footer /> */}
  </Router>
  <ReactQueryDevtools initialIsOpen={false} position='bottom-right'/>
  </QueryClientProvider>,
  document.getElementById("root")
);