import React, { Component } from "react";
import { Button, Container, Table } from "react-bootstrap";
import KupacDataService from "../../services/kupac.service";
import { NumericFormat } from "react-number-format";
import { Link } from "react-router-dom";
import {FaEdit, FaTrash} from "react-icons/fa"


export default class Kupci extends Component{

    constructor(props){
        super(props);
        const token = localStorage.getItem('Bearer');
        if(token==null || token===''){
          window.location.href='/';
        }

        this.state = {
            kupci: []
        };

    }

    componentDidMount(){
        this.dohvatiKupce();
    }

    async dohvatiKupce(){

        await KupacDataService.get()
        .then(response => {
            this.setState({
                kupci: response.data
            });
            console.log(response.data);
        })
        .catch(e =>{
            console.log(e);
        });
    }

    async obrisiKupca(sifra){
        const odgovor = await KupacDataService.delete(sifra);
        if(odgovor.ok){
            this.dohvatiSmjerovi();
        }else{
            alert(odgovor.poruka);
        }
    }


    render(){

        const { kupci } = this.state;

        return (
            <Container>
               <a href="/smjerovi/dodaj" className="btn btn-success gumb">
                Dodaj novi smjer
               </a>
                
               <Table striped bordered hover responsive>
                <thead>
                    <tr>
                        <th>Korisnicko Ime</th>
                        <th>Ime</th>
                        <th>Prezime</th>
                        <th>Lozinka</th>
                        <th>Telefon</th>
                        <th>Adresa</th>
                        <th>Akcija</th>
                    </tr>
                </thead>
                <tbody>
                   { kupci && kupci.map((kupac,index) => (

                    <tr key={index}>
                        <td>{kupac.korisnickoIme}</td>
                        <td>{kupac.ime}</td>
                        <td>{kupac.prezime}</td>
                        <td>{kupac.telefon}</td>
                        <td>{kupac.adresa}</td>

                        <td>
                            <Link className="btn btn-primary gumb"
                            to={`/kupci/${Kupac.sifra}`}>
                                <FaEdit />
                            </Link>

                            <Button variant="danger" className="gumb"
                            onClick={()=>this.obrisiKupca(kupac.sifra)}>
                                <FaTrash />
                            </Button>
                        </td>
                    </tr>

                   ))}
                </tbody>
               </Table>



            </Container>


        );
    }
}