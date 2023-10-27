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
    
    this.dodajProizvod= this.dodajProizvod.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }
  async dodajProizvod(proizvod) {
    const odgovor = await ProizvodDataService.post(proizvod);
    if(odgovor.ok){
      // routing na proizvodi
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
            <Form.Control type="text" name="naziv" placeholder="Naziv proizvoda" maxLength={255} required/>
          </Form.Group>

          <Form.Group className="mb-3" controlId="opis">
            <Form.Label>opis</Form.Label>
            <Form.Control type="text" name="opis" placeholder="Opis proizvoda" maxLength={255} required/>
          </Form.Group>

          <Form.Group className="mb-3" controlId="cijena">
            <Form.Label>Cijena</Form.Label>
            <Form.Control type="text" name="cijena" placeholder="500" required/>
            <Form.Text className="text-muted">
             Ne smije biti negativna
            </Form.Text>
          </Form.Group>

          <Row>
            <Col>
              <Link className="btn btn-danger gumb" to={`/proizvod`}>Odustani</Link>
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

