import React, { Component } from "react";
import KosaricaDataService from "../../services/kosarica.service";
import KupacDataService from "../../services/kupac.service";
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
    const token = localStorage.getItem('Bearer');
    if(token==null || token===''){
      window.location.href='/';
    }

    this.kosarica = this.dohvatiKosarica();
    this.promjeniKosarica = this.promjeniKosarica.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.kupci = this.dohvatiKupci();
    this.proizvodi = this.dohvatiProizvodi();
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




  async dohvatiKosarica() {

    let href = window.location.href;
    let niz = href.split('/'); 
    await KosaricaDataService.getBySifra(niz[niz.length-1])
      .then(response => {
        let kk = response.data;
        kk.vrijemeStvaranja = moment.utc(kk.datumStvaranja).format("HH:mm");
        kk.datumStvaranja = moment.utc(kk.datumStvaranja).format("yyyy-MM-DD");
        
      
        this.setState({
          grupa: g
        });
       
      })
      .catch(e => {
        console.log(e);
      });
  }

  

  async promjeniKosarica(kosarica) {
    const odgovor = await kosaricaDataService.post(kosarica);
    if(odgovor.ok){
      // routing na kupce
      window.location.href='/kosarice';
    }else{
      // pokaži grešku
      console.log(odgovor);
    }
  }


  async dohvatiKupci() {
   // console.log('Dohvaćm kupce');
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

  async dohvatiProizvodi() {
    let href = window.location.href;
    let niz = href.split('/'); 
    await KosaricaDataService.getProizvodi(niz[niz.length-1])
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

   async obrisiProizvod(kosarica, proizvod){
    const odgovor = await KosaricaDataService.obrisiProizvod(kosarica, proizvod);
    if(odgovor.ok){
     this.dohvatiProizvodi();
    }else{
     //this.otvoriModal();
    }
   }

   async dodajProizvod(kosarica, proizvod){
    const odgovor = await KosaricaDataService.dodajProizvod(kosarica, proizvod);
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

    this.promjeniKosarica({
      kupac: podaci.get('korisnickoIme'),
      //kolicinaProizvod:
      datumStvaranja: datum,
      sifraKupac: this.state.sifraKupac
    });
    
  }


  render() { 
    const { kupci} = this.state;
    const { kosarica} = this.state;
    const { proizvodi} = this.state;
    const { pronadeniProizvodi} = this.state;


    const obradiTrazenje = (uvjet) => {
      this.traziProizvod( uvjet);
    };

    const odabraniProizvod = (proizvod) => {
      //console.log(kosarica.sifra + ' - ' + proizvod[0].sifra);
      if(proizvod.length>0){
        this.dodajProizvod(kosarica.sifra, proizvod[0].sifra);
      }
     
    };

    return (
    <Container>
       
        <Form onSubmit={this.handleSubmit}>
          <Row>
          <Col key="1" sm={12} lg={6} md={6}>
              <Form.Group className="mb-3" controlId="kolicinaProizvod">
                <Form.Label>Naziv</Form.Label>
                <Form.Control type="text" name="kolicinaProizvod" placeholder="" maxLength={255} defaultValue={kosarica.kolicinaProizvod}  required/>
              </Form.Group>

              <Form.Group className="mb-3" controlId="kupac">
                <Form.Label>Smjer</Form.Label>
                <Form.Select defaultValue={grupa.sifraKupac}  onChange={e => {
                  this.setState({ sifraKupac: e.target.value});
                }}>
                {kupci && kupci.map((kupac,index) => (
                      <option key={index} value={kupac.sifra}>{kupac.korisnickoIme}</option>

                ))}
                </Form.Select>
              </Form.Group>

              <Form.Group className="mb-3" controlId="datumStvaranja">
                <Form.Label>Datum Stvaranja</Form.Label>
                <Form.Control type="date" name="datumStvaranja" placeholder="" defaultValue={kosarica.datumStvaranja}  />
              </Form.Group>

              <Form.Group className="mb-3" controlId="vrijeme">
                <Form.Label>Vrijeme</Form.Label>
                <Form.Control type="time" name="vrijeme" placeholder="" defaultValue={kosarica.vrijemeStvaranja}  />
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
          <Form.Group className="mb-3" controlId="uvjet">
                <Form.Label>Traži proizvod</Form.Label>
                
          <AsyncTypeahead
            className="autocomplete"
            id="uvjet"
            emptyLabel="Nema rezultata"
            searchText="Tražim..."
            labelKey={(polaznik) => `${polaznik.prezime} ${polaznik.ime}`}
            minLength={3}
            options={pronadeniProizvodi}
            onSearch={obradiTrazenje}
            placeholder="dio naziva"
            renderMenuItemChildren={(proizvod) => (
              <>
                <span>{proizvod.naziv} {polaznik.opis}</span>
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
                  <Button variant="danger"   onClick={() => this.obrisiPolaznika(kosarica.sifra, proizvod.sifra)}><FaTrash /></Button>
                    
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

