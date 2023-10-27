import React, { Component } from "react";
import ProizvodDataService from "../../services/proizvod.service";
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Card from 'react-bootstrap/Card';
import Button from 'react-bootstrap/Button';
import { Link } from "react-router-dom";
import { FaEdit } from 'react-icons/fa';
import { FaTrash } from 'react-icons/fa';
import { Modal } from 'react-bootstrap';


export default class Proizvodi extends Component {
  constructor(props) {
    super(props);
    
    this.dohvatiProizvode = this.dohvatiProizvode.bind(this);

    this.state = {
      proizvodi: [],
      prikaziModal: false
    };
  }



  otvoriModal = () => this.setState({ prikaziModal: true });
  zatvoriModal = () => this.setState({ prikaziModal: false });

  componentDidMount() {
    this.dohvatiProizvode();
  }
  dohvatiProizvode() {
    ProizvodDataService.getAll()
      .then(response => {
        this.setState({
          proizvodi: response.data
        });
      })
      .catch(e => {
        console.log(e);
      });
  }

  async obrisiProizvod(sifra){
    
    const odgovor = await ProizvodDataService.delete(sifra);
    if(odgovor.ok){
     this.dohvatiProizvode();
    }else{
      this.otvoriModal();
    }
    
   }

  render() {
    const { proizvodi} = this.state;
    return (

    <Container>
      <a href="/proizvodi/dodaj" className="btn btn-success gumb">Dodaj novi proizvod</a>
    <Row>
      { proizvodi && proizvodi.map((p) => (
           
           <Col key={p.sifra} sm={12} lg={3} md={3}>

              <Card style={{ width: '18rem' }}>
              <Card.Img variant="top" src={p.slika} />
                <Card.Body>
                  <Card.Title>{p.naziv} {p.cijena}</Card.Title>
                  <Card.Text>
                   
                  </Card.Text>
                  <Row>
                      <Col>
                      <Link className="btn btn-primary gumb" to={`/proizvodi/${p.sifra}`}><FaEdit /></Link>
                      </Col>
                      <Col>
                      <Button variant="danger" className="gumb"  onClick={() => this.obrisiProizvod(p.sifra)}><FaTrash /></Button>
                      </Col>
                    </Row>
                </Card.Body>
              </Card>
            </Col>
          ))
      }
      </Row>


      <Modal show={this.state.prikaziModal} onHide={this.zatvoriModal}>
              <Modal.Header closeButton>
                <Modal.Title>Greška prilikom brisanja</Modal.Title>
              </Modal.Header>
              <Modal.Body>Proizvod se nalazi na jednoj ili više inventara ili košarica i ne može se obrisati.</Modal.Body>
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
