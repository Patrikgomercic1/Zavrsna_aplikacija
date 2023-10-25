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


export default function App() {
  return (
    <Router>
      <Izbornik />
      <Routes>
        <Route path='/' element={<Pocetna />} />
        <Route path='/nadzornaploca' element={<NadzornaPloca />} />
        <Route path='/kupci' element={<Kupci />} />
        <Route path="/kupci/dodaj" element={<DodajKupca />} />
        <Route path="/kupci/:sifra" element={<PromjeniKupca />} />
        <Route path="/proizvodi" element={<Proizvodi />} />
        <Route path="/proizvodi/dodaj" element={<DodajProizvod />} />
        <Route path="/proizvodi/:sifra" element={<PromjeniProizvod />} />
        <Route path="/kosarice" element={<Kosarice />} />
        <Route path="/kosarice/dodaj" element={<DodajKosaricu />} />
        <Route path="/kosarice/:sifra" element={<PromjeniKosaricu />} />
        <Route path="/inventari" element={<Inventari />} />
        <Route path="/inventari/dodaj" element={<DodajInventar />} />
        <Route path="/inventari/:sifra" element={<PromjeniInventar />} />
      </Routes>
     
    </Router>
  );
}
