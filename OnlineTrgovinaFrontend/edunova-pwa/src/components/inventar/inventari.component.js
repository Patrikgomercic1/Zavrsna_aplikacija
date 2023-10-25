import React, { Component } from "react";
import InventarDataService from "../../services/inventar.service";
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Table from 'react-bootstrap/Table';
import Button from 'react-bootstrap/Button';
import { Link } from "react-router-dom";
import { FaEdit } from 'react-icons/fa';
import { FaTrash } from 'react-icons/fa';
import moment from 'moment';
import { Modal } from 'react-bootstrap';


export default class Inventari extends Component {
  constructor(props) {
    super(props);
    const token = localStorage.getItem('Bearer');
    if(token==null || token===''){
      window.location.href='/';
    }
    this.dohvatiInventare = this.dohvatiInventare.bind(this);

    this.state = {
      inventari: [],
      prikaziModal: false
    };
  }

  otvoriModal = () => this.setState({ prikaziModal: true });
  zatvoriModal = () => this.setState({ prikaziModal: false });


  componentDidMount() {
    this.dohvatiInventare();
  }
  dohvatiInventare() {
    KosaricaDataService.getAll()
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

  async obrisiInventar(sifra){
    
    const odgovor = await InventarDataService.delete(sifra);
    if(odgovor.ok){
     this.dohvatiInventare();
    }else{
     this.otvoriModal();
    }
    
   }

  render() {
    const { inventari} = this.state;
    return (

    <Container>
      <a href="/inventari/dodaj" className="btn btn-success gumb">Dodaj novi Inventar</a>
      <Table striped bordered hover responsive>
              <thead>
                <tr>
                  <th>Proizvod</th>
                  <th>Kategorija</th>
                  <th>Količina</th>
                  <th>Dostupnost</th>
                  <th>Akcija</th>
                </tr>
              </thead>
              <tbody>
              {inventari && inventari.map((i,index) => (
                
                <tr key={index}>
                  <td> 
                    <p className="nazivProizvoda">{i.naziv} ({i.kolicina})</p>
                    {i.proizvod}
                  </td>
                  <td>
                    <Row>
                      <Col>
                        <Link className="btn btn-primary gumb" to={`/inventari/${i.sifra}`}><FaEdit /></Link>
                      </Col>
                      <Col>
                        { i.kolicina===0 &&
                             <Button variant="danger"  className="gumb" onClick={() => this.obrisiInventar(i.sifra)}><FaTrash /></Button>
                        }
                      </Col>
                    </Row>
                    
                  </td>
                </tr>
                ))
              }
              </tbody>
            </Table>     

             <Modal show={this.state.prikaziModal} onHide={this.zatvoriModal}>
              <Modal.Header closeButton>
                <Modal.Title>Greška prilikom brisanja</Modal.Title>
              </Modal.Header>
              <Modal.Body>Inventar sadrži proizvode.</Modal.Body>
              <Modal.Footer>
                <Button variant="secondary" onClick={this.zatvoriModal}>
                  Zatvori
                </Button>
              </Modal.Footer>
            </Modal>

    </Container>


    );
    
        }
}
