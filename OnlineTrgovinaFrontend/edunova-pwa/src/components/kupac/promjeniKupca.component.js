import React, { Component } from "react";
import KupacDataService from "../../services/kupac.service";
import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { Link } from "react-router-dom";



export default class PromjeniSmjer extends Component {

  constructor(props) {
    super(props);
    

   
    this.kupac = this.dohvatiKupca();
    this.promjeniKupca = this.promjeniKupca.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    

    this.state = {
      kupac: {}
    };

  }



  async dohvatiKupca() {
    let href = window.location.href;
    let niz = href.split('/'); 
    await KupacDataService.getBySifra(niz[niz.length-1])
      .then(response => {
        this.setState({
          kupac: response.data
        });
       // console.log(response.data);
      })
      .catch(e => {
        console.log(e);
      });
    
   
  }

  async promjeniKupca(kupac) {
    let href = window.location.href;
    let niz = href.split('/'); 
    const odgovor = await KupacDataService.put(niz[niz.length-1],kupac);
    if(odgovor.ok){
      // routing na kupce
      window.location.href='/kupci';
    }else{
      // pokaži grešku
      console.log(odgovor);
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

    this.promjeniKupca({
      korisnickoIme: podaci.get('korisnickoIme'),
      ime: podaci.get('ime'),
      prezime: podaci.get('prezime'),
      lozinka: podaci.get('lozinka'),
      telefon: podaci.get('telefon'),
      adresa: podaci.get('adresa')
    });
    
  }


  render() {
    
   const { kupac} = this.state;


    return (
    <Container>
        <Form onSubmit={this.handleSubmit}>


        <Form.Group className="mb-3" controlId="korisnickoIme">
            <Form.Label>Korisničko Ime</Form.Label>
            <Form.Control type="text" name="korisnickoIme" placeholder="Korisnicko Ime" maxLength={255} required/>
          </Form.Group>


          <Form.Group className="mb-3" controlId="ime">
            <Form.Label>Ime</Form.Label>
            <Form.Control type="text" name="ime" placeholder="Marko" />
          </Form.Group>


          <Form.Group className="mb-3" controlId="prezime">
            <Form.Label>Prezime</Form.Label>
            <Form.Control type="text" name="prezime" placeholder="Horvat" />
          </Form.Group>

          <Form.Group className="mb-3" controlId="lozinka">
            <Form.Label>Lozinka</Form.Label>
            <Form.Control type="text" name="lozinka" placeholder="5asfafg32saxFD0" />
          </Form.Group>

          <Form.Group className="mb-3" controlId="telefon">
            <Form.Label>Telefon</Form.Label>
            <Form.Control type="text" name="telefon" placeholder="0985812437" />
            <Form.Text className="text-muted">
             Ne smije biti negativan
            </Form.Text>
          </Form.Group>

          <Form.Group className="mb-3" controlId="adresa">
            <Form.Label>Adresa</Form.Label>
            <Form.Control type="text" name="adresa" placeholder="Osječka 68" />
          </Form.Group>

        
         
          <Row>
            <Col>
              <Link className="btn btn-danger gumb" to={`/kupci`}>Odustani</Link>
            </Col>
            <Col>
            <Button variant="primary" className="gumb" type="submit">
              Promjeni kupca
            </Button>
            </Col>
          </Row>
        </Form>


      
    </Container>
    );
  }
}

