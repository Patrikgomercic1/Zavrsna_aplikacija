import React, { Component } from "react";
import KosaricaDataService from "../../services/kosarica.service";
import KupacDataService from "../../services/kupac.service";
import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { Link } from "react-router-dom";
import moment from 'moment';



export default class DodajKosaricu extends Component {

  constructor(props) {
    super(props);
    
    this.dodajKosaricu = this.dodajKosaricu.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.dohvatiKupce = this.dohvatiKupce.bind(this);

    this.state = {
      kupci: [],
      sifraKupac:0
    };
  }

  componentDidMount() {
    //console.log("Dohvaćam kupce");
    this.dohvatiKupci();
  }

  async dodajKosaricu(kosarica) {
    const odgovor = await KosaricaDataService.post(kosarica);
    if(odgovor.ok){
      // routing na kupci
      window.location.href='/kosarice';
    }else{
      // pokaži grešku
      console.log(odgovor);
    }
  }


  async dohvatiKupce() {

    await KupacDataService.get()
      .then(response => {
        this.setState({
          kupci: response.data,
          sifraKupac: response.data[0].sifra
        });

       // console.log(response.data);
      })
      .catch(e => {
        console.log(e);
      });
  }


  handleSubmit(e) {
    e.preventDefault();
    const podaci = new FormData(e.target);
    console.log(podaci.get('kupac'));
    console.parseInt(podaci.get('kolicinaProizvod'));
    console.log(podaci.get('datumStvaranja'));
    console.log(podaci.get('vrijeme'));
    let datum = moment.utc(podaci.get('datumStvaranja') + ' ' + podaci.get('vrijeme'));
    console.log(datum);

    this.dodajKosaricu({
      kupac: podaci.get('korisnickoIme'),
      //kolicinaProizvod:
      datumStvaranja: datum,
      sifraKupac: this.state.sifraKupac
    });
    
  }


  render() { 
    const { kupci} = this.state;
    return (
    <Container>
        <Form onSubmit={this.handleSubmit}>


          <Form.Group className="mb-3" controlId="kolicinaProizvod">
            <Form.Label>Kolicina Proizvoda</Form.Label>
            <Form.Control type="text" name="kolicinaProizvod" placeholder="" maxLength={255} required/>
          </Form.Group>

          <Form.Group className="mb-3" controlId="kupac">
            <Form.Label>Kupac</Form.Label>
            <Form.Select onChange={e => {
              this.setState({ sifraKupac: e.target.value});
            }}>
            {kupci && kupci.map((kupac,index) => (
                  <option key={index} value={kupac.sifra}>{kupac.korisnickoIme}</option>

            ))}
            </Form.Select>
          </Form.Group>

          <Form.Group className="mb-3" controlId="datumPocetka">
            <Form.Label>Datum početka</Form.Label>
            <Form.Control type="date" name="datumPocetka" placeholder=""  />
          </Form.Group>

          <Form.Group className="mb-3" controlId="vrijeme">
            <Form.Label>Vrijeme</Form.Label>
            <Form.Control type="time" name="vrijeme" placeholder=""  />
          </Form.Group>

         



          <Row>
            <Col>
              <Link className="btn btn-danger gumb" to={`/kosarice`}>Odustani</Link>
            </Col>
            <Col>
            <Button variant="primary" className="gumb" type="submit">
              Dodaj košaricu
            </Button>
            </Col>
          </Row>
         
          
        </Form>


      
    </Container>
    );
  }
}

