import React, { Component } from "react";
import ProizvodDataService from "../../services/proizvod.service";
import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { Link } from "react-router-dom";


export default class DodajProizvod extends Component {

  constructor(props) {
    super(props);
    const token = localStorage.getItem('Bearer');
    if(token==null || token===''){
      window.location.href='/';
    }
    this.dodajProizvod = this.dodajProizvod.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }
  async dodajProizvod(inventar) {
    const odgovor = await ProizvodDataService.post(inventar);
    if(odgovor.ok){
      // routing na inventar
      window.location.href='/proizvodi';
    }else{
      // pokaži grešku
      console.log(odgovor);
    }
  }



  handleSubmit(e) {
    e.preventDefault();
    const podaci = new FormData(e.target);

    this.dodajProizvod({
      naziv: podaci.get('naziv'),
      opis: podaci.get('opis'),
      cijena: parseFloat(podaci.get('cijena'))
    });
    
  }


  render() { 
    return (
    <Container>
        <Form onSubmit={this.handleSubmit}>


          <Form.Group className="mb-3" controlId="naziv">
            <Form.Label>Naziv</Form.Label>
            <Form.Control type="text" name="ime" placeholder="Majica" maxLength={255} required/>
          </Form.Group>


          <Form.Group className="mb-3" controlId="opis">
            <Form.Label>Opis</Form.Label>
            <Form.Control type="text" name="opis" placeholder="Crvena vunena majica" required />
          </Form.Group>


          <Form.Group className="mb-3" controlId="cijena">
            <Form.Label>Cijena</Form.Label>
            <Form.Control type="text" name="cijena" placeholder="25" />
            <Form.Text className="text-muted">
             Ne smije biti negativan
            </Form.Text>
          </Form.Group>

          <Row>
            <Col>
              <Link className="btn btn-danger gumb" to={`/proizvodi`}>Odustani</Link>
            </Col>
            <Col>
            <Button variant="primary" className="gumb" type="submit">
              Dodaj proizvod
            </Button>
            </Col>
          </Row>
         
          
        </Form>


      
    </Container>
    );
  }
}

