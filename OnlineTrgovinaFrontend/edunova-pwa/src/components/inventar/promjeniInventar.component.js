import React, { Component } from "react";
import InventarDataService from "../../services/kosarica.service";
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


export default class PromjeniKosarica extends Component {

  constructor(props) {
    super(props);
    

    this.kosarica = this.dohvatiKosarica();
    this.promjeniKosarica = this.promjeniKosarica.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.kupci = this.dohvatiKupci();
    this.proizvodi = this.dohvatiProizvode();
    this.obrisiProizvod = this.obrisiProizvod.bind(this);
    this.traziProizvod = this.traziProizvod.bind(this);
    this.dodajProizvod = this.dodajProizvod.bind(this);


    this.state = {
      kosarica: {},
      kupci: [],
      proizvodi: [],
      sifraKupac:0,
      pronadeniProizvodi: []
    };
  }




  async dohvatiInventar() {

    let href = window.location.href;
    let niz = href.split('/'); 
    await InventarDataService.getBySifra(niz[niz.length-1])
      .then(response => {
        let i = response.data;
        i.proizvod = moment.utc(i.naziv);

        
      
        this.setState({
          proizvod: {}
        });
       
      })
      .catch(e => {
        console.log(e);
      });
  }

  

  async promjeniKosarica(kosarica) {
    const odgovor = await InventarDataService.post(kosarica);
    if(odgovor.ok){
      // routing na kupce
      window.location.href='/kosarice';
    }else{
      // pokaži grešku
      console.log(odgovor);
    }
  }


  async dohvatiProizvodi() {
   // console.log('Dohvaćm kupce');
    await InventarDataService.get()
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

  async dohvatiProizvodi() {
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

   

   async traziProizvod( uvjet) {

    await ProizvodDataService.traziProizvod( uvjet)
       .then(response => {
         this.setState({
          pronadeniProizvodi: response.data
         });
 
        // console.log(response.data);
       })
       .catch(e => {
         console.log(e);
       });
   }

   async obrisiProizvod(inventar, proizvod){
    const odgovor = await InventarDataService.obrisiProizvod(inventar, proizvod);
    if(odgovor.ok){
     this.dohvatiProizvodi();
    }else{
     //this.otvoriModal();
    }
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
    console.log(podaci.get('datumStvaranja'));
    console.log(podaci.get('vrijeme'));
    let datum = moment.utc(podaci.get('datumStvaranja') + ' ' + podaci.get('vrijeme'));
    console.log(datum);

    this.promjeniInventar({
      proizvod: podaci.get('naziv'),
      kategorija: podaci.get('kategorija'),
      kolicina: podaci.get('kolicina'),
      dostupnost: podaci.get('dostupnost'),
      sifraProizvod: this.state.sifraProizvod
    });
    
  }


  render() { 
    const { inventar} = this.state;
    const { proizvodi} = this.state;
    const { pronadeniProizvodi} = this.state;


    const obradiTrazenje = (uvjet) => {
      this.traziProizvod( uvjet);
    };

    const odabraniProizvod = (proizvod) => {
      //console.log(kosarica.sifra + ' - ' + proizvod[0].sifra);
      if(proizvod.length>0){
        this.dodajProizvod(inventar.sifra, proizvod[0].sifra);
      }
     
    };

    return (
    <Container>
       
        <Form onSubmit={this.handleSubmit}>
          <Row>
          <Col key="1" sm={12} lg={6} md={6}>
              <Form.Group className="mb-3" controlId="proizvod">
                <Form.Label>Proizvod</Form.Label>
                <Form.Control type="text" name="proizvod" placeholder="" maxLength={255} defaultValue={inventar.proizvod}  required/>
              </Form.Group>

              <Form.Group className="mb-3" controlId="kategorija">
                <Form.Label>Kategorija</Form.Label>
                <Form.Select defaultValue={inventar.sifraProizvod}  onChange={e => {
                  this.setState({ sifraProizvod: e.target.value});
                }}>
                {proizvodi && proizvodi.map((proizvod,index) => (
                      <option key={index} value={proizvod.sifra}>{proizvod.naziv}</option>

                ))}
                </Form.Select>
              </Form.Group>

              <Form.Group className="mb-3" controlId="kolicina">
                <Form.Label>Kolicina</Form.Label>
                <Form.Control type="date" name="kolicina" placeholder="" defaultValue={inventar.kolicina}  />
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
                  <Link className="btn btn-danger gumb" to={`/inventari`}>Odustani</Link>
                </Col>
                <Col>
                <Button variant="primary" className="gumb" type="submit">
                  Promjeni inventar
                </Button>
                </Col>
              </Row>
          </Col>
          <Col key="2" sm={12} lg={6} md={6} className="proizvodiInventar">
          <Form.Group className="mb-3" controlId="uvjet">
                <Form.Label>Traži proizvod</Form.Label>
                
          <AsyncTypeahead
            className="autocomplete"
            id="uvjet"
            emptyLabel="Nema rezultata"
            searchText="Tražim..."
            labelKey={(proizvod) => `${proizvod.naziv}`}
            minLength={3}
            options={pronadeniProizvodi}
            onSearch={obradiTrazenje}
            placeholder="dio naziva"
            renderMenuItemChildren={(proizvod) => (
              <>
                <span>{proizvod.naziv}</span>
              </>
            )}
            onChange={odabraniProizvod}
          />
          </Form.Group>
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
                  <Button variant="danger"   onClick={() => this.obrisiPolaznika(inventar.sifra, proizvod.sifra)}><FaTrash /></Button>
                    
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

