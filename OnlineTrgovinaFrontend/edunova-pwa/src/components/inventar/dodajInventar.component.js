import React, { Component } from "react";
import InventarDataService from "../../services/inventar.service";
import ProizvodDataService from "../../services/proizvod.service";
import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { Link } from "react-router-dom";




export default class DodajInventar extends Component {

  constructor(props) {
    super(props);
    
    this.dodajInventar = this.dodajInventar.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.dohvatiProizvode = this.dohvatiProizvode.bind(this);

    this.state = {
      proizvodi: [],
      sifraProizvod:0
    };
  }

  componentDidMount() {
    //console.log("Dohvaćam inventare");
    this.dohvatiInventare();
  }

  async dodajInvantar(inventar) {
    const odgovor = await InventarDataService.post(inventar);
    if(odgovor.ok){
      window.location.href='/proizvodi';
    }else{
      // pokaži grešku
      console.log(odgovor);
    }
  }


  async dohvatiProizvode() {

    await ProizvodDataService.get()
      .then(response => {
        this.setState({
          proizvodi: response.data,
          sifraProizvod: response.data[0].sifra
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
    console.log(podaci.get('proizvod'));
    console.log(podaci.get('kategorija'));
    console.log(podaci.get('kolicina'));
    console.log(podaci.get('dostupnost'));

    this.dodajInventar({
      proizvod: podaci.get('naziv'),
      Kategorija: podaci.get('kategorija'),
      Dostupnost: podaci.get('dostupnost'),
      sifraProizvod: this.state.sifraProizvod
    });
    
  }


  render() { 
    const { proizvodi} = this.state;
    return (
    <Container>
        <Form onSubmit={this.handleSubmit}>


          <Form.Group className="mb-3" controlId="proizvod">
            <Form.Label>Proizvod</Form.Label>
            <Form.Control type="text" name="proizvod" placeholder="Majica" maxLength={255} required/>
          </Form.Group>

          <Form.Group className="mb-3" controlId="kupac">
            <Form.Label>Kupac</Form.Label>
            <Form.Select onChange={e => {
              this.setState({ sifraKupac: e.target.value});
            }}>
            {proizvodi && proizvodi.map((proizvod,index) => (
                  <option key={index} value={proizvod.sifra}>{proizvod.Naziv}</option>

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
              Dodaj inventar
            </Button>
            </Col>
          </Row>
         
          
        </Form>


      
    </Container>
    );
  }
}

