import React from 'react';
import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Izbornik from './components/izbornik.component';
import Pocetna from './components/pocetna.component';
import NadzornaPloca from './components/nadzornaploca.component';
import Kupci from './components/kupac/kupci.component';
import DodajKupca from './components/kupac/dodajKupca.component';
import PromjeniKupca from './components/kupac/promjeniKupca.component';
import Proizvodi from './components/proizvod/proizvodi.component';
import PromjeniProizvod from './components/proizvod/promjeniProizvod.component';
import DodajProizvod from './components/proizvod/dodajProizvod.component';
import Inventari from './components/inventar/inventari.component';
import DodajInventar from './components/inventar/dodajInventar.component';
import PromjeniInventar from './components/inventar/promjeniInventar.component';
import Kosarice from './components/kosarica/kosarice.component';
import DodajKosaricu from './components/kosarica/dodajKosaricu.component';
import PromjeniKosarica from './components/inventar/promjeniInventar.component';



export default function App() {
  return (
    <Router>
      <Izbornik />
      <Routes>
        <Route path='/' element={<Pocetna />} />
        <Route path='/nadzornaploca' element={<NadzornaPloca />} />
        <Route path='/kupci' element={<Kupci/>} />
        <Route path='/kupci/dodaj' element={<DodajKupca/>} />
        <Route path='/kupci/promjeni' element={<PromjeniKupca/>} />
        <Route path='/kupci/:sifra' element={<PromjeniKupca/>} />
        <Route path='/proizvodi' element={<Proizvodi/>} />
        <Route path='/proizvodi/promjeni' element={<DodajProizvod/>} />
        <Route path='/proizvodi/:sifra' element={<PromjeniProizvod/>} />
        <Route path='/inventari' element={<Inventari/>} />
        <Route path='/inventari/dodaj' element={<DodajInventar/>} />
        <Route path='/inventari/promjeni' element={<PromjeniInventar/>} />
        <Route path='/kosarice' element={<Kosarice/>} />
        <Route path='/kosarice/dodaj' element={<DodajKosaricu/>} />
        <Route path='/kosarice/promjeni' element={<PromjeniKosarica/>} />
      </Routes>
    </Router>
  );
}
