import React, { Component } from "react";
import InventarDataService from "../../services/inventar.service";
import ProizvodDataService from "../../services/proizvod.service";
import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { Link } from "react-router-dom";
import moment from 'moment';
import Table from 'react-bootstrap/Table';
import { FaTrash } from 'react-icons/fa';

import { AsyncTypeahead } from 'react-bootstrap-typeahead';


export default class PromjeniInventar extends Component {

  constructor(props) {
    

    this.inventar = this.dohvatiInventar();
    this.promjeniInventar = this.promjeniInventar.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.proizvodi = this.dohvatiProizvode();
    this.dodajProizvod = this.dodajProizvod.bind(this);


    this.state = {
      inventar: {},
      proizvodi: [],
      sifraProizvod:0,
    };
  }




  async dohvatiInventar() {
    InventarDataService.getAll()
      .then(response => {
        this.setState({
          inventari: response.data
        });
      //  console.log(response);
      })
      .catch(e => {
        console.log(e);
      });
  }

  

  async promjeniInventar(inventar) {
    const odgovor = await InventarDataService.post(inventar);
    if(odgovor.ok){
      // routing na kupce
      window.location.href='/inventari';
    }else{
      // pokaži grešku
      console.log(odgovor);
    }
  }


  async dohvatiInventar() {
   // console.log('Dohvaćm kupce');
    await InventarDataService.get()
      .then(response => {
        this.setState({
          inventar: response.data,
          sifraProizvod: response.data[0].sifra
        });

       // console.log(response.data);
      })
      .catch(e => {
        console.log(e);
      });
  }

  async dohvatiProizvode() {
    let href = window.location.href;
    let niz = href.split('/'); 
    await InventarDataService.getProizvodi(niz[niz.length-1])
       .then(response => {
         this.setState({
           proizvodi: response.data
         });
 
        // console.log(response.data);
       })
       .catch(e => {
         console.log(e);
       });
   }

   

   

   

   async dodajProizvod(inventar, proizvod){
    const odgovor = await InventarDataService.dodajProizvod(inventar, proizvod);
    if(odgovor.ok){
     this.dohvatiProizvodi();
    }else{
    //this.otvoriModal();
    }
   }
 

  handleSubmit(e) {
    e.preventDefault();
    const podaci = new FormData(e.target);
    console.log(podaci.get('proizvod'));
    console.log(podaci.get('kategorija'));
    console.log(podaci.get('kolicina'));
    console.log(podaci.get('dostupnost'));

    this.promjeniInventar({
      proizvod: podaci.get('naziv'),
      Kategorija: podaci.get('kategorija'),
      Kolicina: podaci.get('kolicina'),
      Dostupnost: podaci.get('dostupnost'),
      sifraProizvod: this.state.sifraProizvod
    });
    
  }


  render() { 
    
    const { inventar} = this.state;
    const { proizvodi} = this.state;
    


    

    

    return (
    <Container>
       
        <Form onSubmit={this.handleSubmit}>
          <Row>
          <Col key="1" sm={12} lg={6} md={6}>
          <Form.Group className="mb-3" controlId="proizvod">
            <Form.Label>Proizvod</Form.Label>
            <Form.Select onChange={e => {
              this.setState({ sifraProizvod: e.target.value});
            }}>
            {proizvodi && proizvodi.map((proizvod,index) => (
                  <option key={index} value={proizvod.sifra}>{proizvod.naziv}</option>

            ))}
            </Form.Select>
          </Form.Group>

          <Form.Group className="mb-3" controlId="kategorija">
            <Form.Label>Kategorija</Form.Label>
            <Form.Control type="text" name="kategorija" placeholder="Odjeća"  />
          </Form.Group>

          <Form.Group className="mb-3" controlId="dostupnost">
            <Form.Check
            inline
            label="Dostupnost"
            name="dostupnost"
          />
          </Form.Group>

            



              <Row>
                <Col>
                  <Link className="btn btn-danger gumb" to={`/kosarice`}>Odustani</Link>
                </Col>
                <Col>
                <Button variant="primary" className="gumb" type="submit">
                  Promjeni grupu
                </Button>
                </Col>
              </Row>
          </Col>
          <Col key="2" sm={12} lg={6} md={6} className="proizvodiKosarica">
          
          <Table striped bordered hover responsive>
              <thead>
                <tr>
                  <th>Proizvod</th>
                  <th>Akcija</th>
                </tr>
              </thead>
              <tbody>
              {proizvodi && proizvodi.map((proizvod,index) => (
                
                <tr key={index}>
                  <td > {proizvod.naziv} {proizvod.opis}</td>
                  <td>
                  <Button variant="danger"   onClick={() => this.obrisiProizvod(inventar.sifra, proizvod.sifra)}><FaTrash /></Button>
                    
                  </td>
                </tr>
                ))
              }
              </tbody>
            </Table>    
          </Col>
          </Row>

          
         
          
        </Form>


      
    </Container>
    );
  }
}

