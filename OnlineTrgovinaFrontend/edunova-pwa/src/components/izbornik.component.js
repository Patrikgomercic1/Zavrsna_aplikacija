import React, { Component } from "react";
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import logo from '../logo.svg';


export default class Izbornik extends Component{
  render(){
   
      
    
    return (
        <Navbar expand="lg" className="bg-body-tertiary">
        <Container>
          <Navbar.Brand href="/"> <img className="App-logo" src={logo} alt="" /> Online Trgovina</Navbar.Brand>
          <Navbar.Toggle aria-controls="basic-navbar-nav" />
           
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="me-auto">
            <Nav.Link href="/nadzornaploca">Nadzorna ploča</Nav.Link>
            <NavDropdown title="Programi" id="basic-nav-dropdown">
              <NavDropdown.Item href="/kupci">Kupci</NavDropdown.Item>
              <NavDropdown.Item href="/proizvodi">Proizvodi</NavDropdown.Item>
              <NavDropdown.Item href="/kosarice">Košarice</NavDropdown.Item>
              <NavDropdown.Item href="/inventari">Inventari</NavDropdown.Item>
            </NavDropdown>
            
            <Nav.Link target="_blank" href="/swagger/index.html">API dokumentacija</Nav.Link>
          </Nav>   
        </Navbar.Collapse>
      </Container>
    </Navbar>
      );
  }
}