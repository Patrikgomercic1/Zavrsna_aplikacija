import React, { Component } from "react";
import KosaricaDataService from "../../services/kosarica.service";
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


export default class Kosarice extends Component {
  constructor(props) {
    super(props);
    const token = localStorage.getItem('Bearer');
    if(token==null || token===''){
      window.location.href='/';
    }
    this.dohvatiKosarice = this.dohvatiKosarice.bind(this);

    this.state = {
      kosarice: [],
      prikaziModal: false
    };
  }

  otvoriModal = () => this.setState({ prikaziModal: true });
  zatvoriModal = () => this.setState({ prikaziModal: false });


  componentDidMount() {
    this.dohvatiKosaricu();
  }
  dohvatiKosaricu() {
    KosaricaDataService.getAll()
      .then(response => {
        this.setState({
          kosarice: response.data
        });
      //  console.log(response);
      })
      .catch(e => {
        console.log(e);
      });
  }

  async obrisiKosaricu(sifra){
    
    const odgovor = await KosaricaDataService.delete(sifra);
    if(odgovor.ok){
     this.dohvatiKosaricu();
    }else{
     this.otvoriModal();
    }
    
   }

  render() {
    const { kosarice} = this.state;
    return (

    <Container>
      <a href="/kosarice/dodaj" className="btn btn-success gumb">Dodaj novu košaricu</a>
      <Table striped bordered hover responsive>
              <thead>
                <tr>
                  <th>Kupac</th>
                  <th>Količina Proizvoda</th>
                  <th>Datum Stvaranja</th>
                  <th>Akcija</th>
                </tr>
              </thead>
              <tbody>
              {kosarice&& kosarice.map((kk,index) => (
                
                <tr key={index}>
                  <td> 
                    <p className="nazivKupca">{kk.korisnickoIme} ({kk.brojProizvoda})</p>
                    {kk.kupac}
                  </td>
                  <td className="naslovSmjer">
                    {kk.datumStvaranja==null ? "Nije definirano" :
                    moment.utc(kk.datumStvaranja).format("DD. MM. YYYY. HH:mm")}
                  </td>
                  <td>
                    <Row>
                      <Col>
                        <Link className="btn btn-primary gumb" to={`/kosarice/${kk.sifra}`}><FaEdit /></Link>
                      </Col>
                      <Col>
                        { kk.brojProizvoda===0 &&
                             <Button variant="danger"  className="gumb" onClick={() => this.obrisiKosaricu(kk.sifra)}><FaTrash /></Button>
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
              <Modal.Body>Košarica sadrži proizvode.</Modal.Body>
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
