import React, { Component } from "react";
import KupacDataService from "../../services/kupac.service";
import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { Link } from "react-router-dom";




export default class DodajKupca extends Component {

  constructor(props) {
    super(props);
   
    this.dodajKupca = this.dodajKupca.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  async dodajKupca(kupac) {
    const odgovor = await KupacDataService.post(kupac);
    if(odgovor.ok){
      // routing na smjerovi
      window.location.href='/kupci';
    }else{
      // pokaži grešku
     // console.log(odgovor.poruka.errors);
      let poruke = '';
      for (const key in odgovor.poruka.errors) {
        if (odgovor.poruka.errors.hasOwnProperty(key)) {
          poruke += `${odgovor.poruka.errors[key]}` + '\n';
         // console.log(`${key}: ${odgovor.poruka.errors[key]}`);
        }
      }

      alert(poruke);
    }
  }



  handleSubmit(e) {
    // Prevent the browser from reloading the page
    e.preventDefault();

    // Read the form data
    const podaci = new FormData(e.target);
    //Object.keys(formData).forEach(fieldName => {
    // console.log(fieldName, formData[fieldName]);
    //})
    
    //console.log(podaci.get('verificiran'));
    // You can pass formData as a service body directly:


    this.dodajKupca({
      korisnickoime: podaci.get('korisnickoime'),
      ime: podaci.get('ime'),
      prezime: podaci.get('prezime'),
      lozinka: podaci.get('lozinka'),
      telefon: parseInt(podaci.get('telefon')),
      adresa: podaci.get('adresa'),
    });
    
  }


  render() { 
    return (
    <Container>
        <Form onSubmit={this.handleSubmit}>


          <Form.Group className="mb-3" controlId="korisnickoime">
            <Form.Label>Korisničko ime</Form.Label>
            <Form.Control type="text" name="korisnickoime" placeholder="Korisnicko ime" maxLength={255} required/>
          </Form.Group>

          <Form.Group className="mb-3" controlId="ime">
            <Form.Label>Ime</Form.Label>
            <Form.Control type="text" name="ime" placeholder="Ime" maxLength={255} required/>
          </Form.Group>

          <Form.Group className="mb-3" controlId="prezime">
            <Form.Label>Prezime</Form.Label>
            <Form.Control type="text" name="prezime" placeholder="Prezime" maxLength={255} required/>
          </Form.Group>

          <Form.Group className="mb-3" controlId="lozinka">
            <Form.Label>Lozinka</Form.Label>
            <Form.Control type="text" name="lozinka" placeholder="Lozinka" maxLength={255} required/>
          </Form.Group>

          <Form.Group className="mb-3" controlId="telefon">
            <Form.Label>Telefon</Form.Label>
            <Form.Control type="text" name="telefon" placeholder="09523462152" />
            <Form.Text className="text-muted">
             Ne smije biti negativan
            </Form.Text>
          </Form.Group>

          <Form.Group className="mb-3" controlId="adresa">
            <Form.Label>Adresa</Form.Label>
            <Form.Control type="text" name="adresa" placeholder="Adresa" maxLength={255}/>
          </Form.Group>

          

          <Row>
            <Col>
              <Link className="btn btn-danger gumb" to={`/kupci`}>Odustani</Link>
            </Col>
            <Col>
            <Button variant="primary" className="gumb" type="submit">
              Dodaj kupca
            </Button>
            </Col>
          </Row>
         
          
        </Form>


      
    </Container>
    );
  }
}

