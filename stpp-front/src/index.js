import React from "react";
import ReactDOM from "react-dom";
import { QueryClientProvider, QueryClient } from "react-query";
import { ReactQueryDevtools } from "react-query/devtools";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Home from './components/Home';
import Navbar from './components/Navbar';
import Footer from './components/Footer';
import Listings from './components/Listings';
import Listing from './components/Listing';
import 'materialize-css/dist/css/materialize.min.css'
import 'materialize-css/dist/css/materialize.css'
import Login from "./components/Login";
import Register from "./components/Register";
import ListingCreate from "./components/ListingCreate";

const queryClient = new QueryClient()

ReactDOM.render(
  <QueryClientProvider client={queryClient}>
  <Router>
    <Navbar />
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/listings" element={<Listings />} />
      <Route path="/listing/:listingId" element={<Listing />} />
      <Route path="/api/login" element={<Login />} />
      <Route path="/api/register" element={<Register />} />
      <Route path="/listingCreate" element={<ListingCreate />} />
    </Routes>
    {/* <Footer /> */}
  </Router>
  <ReactQueryDevtools initialIsOpen={false} position='bottom-right'/>
  </QueryClientProvider>,
  document.getElementById("root")
);