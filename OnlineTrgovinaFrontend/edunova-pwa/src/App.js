import React from 'react';
import './App.css';
import { BrowserRouter as Router, Routes, Route  } from 'react-router-dom';
import Izbornik from './components/izbornik.component';
import Pocetna from './components/pocetna.component';
import NadzornaPloca from './components/nadzornaploca.component';
import Kupci from './components/kupac/kupci.component';
import DodajKupca from './components/kupac/dodajKupca.component';
import PromjeniKupca from './components/kupac/promjeniKupca.component';
import Proizvodi from './components/proizvod/proizvodi.component';
import DodajProizvod from './components/proizvod/dodajProizvod.component';
import PromjeniProizvod from './components/proizvod/promjeniProizvod.component';
import Kosarice from './components/kosarica/kosarice.component';
import DodajKosaricu from './components/kosarica/dodajKosaricu.component';
import PromjeniKosaricu from './components/kosarica/promjeniKosaricu.component';
import Inventari from './components/inventar/inventari.component';
import DodajInventar from './components/inventar/dodajInventar.component';
import PromjeniInventar from './components/inventar/promjeniInventar.component';
import Email from './components/grupa/email.component';
import Login from './components/login.component';
import Odjava from './components/odjava.component';

export default function App() {
  return (
    <Router>
      <Izbornik />
      <Routes>
        <Route path='/' element={<Pocetna />} />
        <Route path='/nadzornaploca' element={<NadzornaPloca />} />
        <Route path='/kupci' element={<Smjerovi />} />
        <Route path="/kupci/dodaj" element={<DodajSmjer />} />
        <Route path="/kupci/:sifra" element={<PromjeniSmjer />} />
        <Route path="/proizvodi" element={<Polaznici />} />
        <Route path="/proizvodi/dodaj" element={<DodajPolaznik />} />
        <Route path="/proizvodi/:sifra" element={<PromjeniPolaznik />} />
        <Route path="/kosarice" element={<Grupe />} />
        <Route path="/kosarice/dodaj" element={<DodajGrupa />} />
        <Route path="/kosarice/:sifra" element={<PromjeniGrupa />} />
        <Route path="/kupci/email/:sifra" element={<Email />} />
        <Route path="/inventari" element={<Grupe />} />
        <Route path="/inventari/dodaj" element={<DodajGrupa />} />
        <Route path="/inventari/:sifra" element={<PromjeniGrupa />} />
        <Route path="/login" element={<Login />} />
        <Route path="/odjava" element={<Odjava />} />
      </Routes>
     
    </Router>
  );
}
