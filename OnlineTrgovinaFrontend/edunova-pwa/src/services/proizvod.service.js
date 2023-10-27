import http from '../http-common';

class ProizvodDataService {
  async getAll() {
    return await http.get('/proizvod');
  }



  async getBySifra(sifra) {
    return await http.get('/proizvod/' + sifra);
  }

  async traziProizvod(uvjet) {
    console.log('Tražim s: ' + uvjet);
    return await http.get('/proizvod/trazi/'+uvjet);
  }

  async post(proizvod){
    //console.log(smjer);
    const odgovor = await http.post('/proizvod',proizvod)
       .then(response => {
         return {ok:true, poruka: 'Unio proizvod'}; // return u odgovor
       })
       .catch(error => {
        console.log(error.response);
         return {ok:false, poruka: error.response.data}; // return u odgovor
       });
 
       return odgovor;
  }

  async put(sifra,proizvod){
    const odgovor = await http.put('/proizvod/' + sifra,proizvod)
       .then(response => {
         return {ok:true, poruka: 'Promjenio proizvod'}; // return u odgovor
       })
       .catch(error => {
        console.log(error.response);
         return {ok:false, poruka: error.response.data}; // return u odgovor
       });
 
       return odgovor;
     }


  async delete(sifra){
    
    const odgovor = await http.delete('/proizvod/' + sifra)
       .then(response => {
         return {ok:true, poruka: 'Uspješno obrisao '};
       })
       .catch(error => {
         console.log(error);
         return {ok:false, poruka: error.response.data};
       });
 
       return odgovor;
     }

     async postaviSliku(sifra,slika){
    
      const odgovor = await http.put('/proizvod/postaviSliku/' + sifra,slika)
         .then(response => {
           return {ok:true, poruka: 'Postavio sliku'};
         })
         .catch(error => {
           console.log(error);
           return {ok:false, poruka: error.response.data};
         });
   
         return odgovor;
       }
     
 
}

export default new ProizvodDataService();