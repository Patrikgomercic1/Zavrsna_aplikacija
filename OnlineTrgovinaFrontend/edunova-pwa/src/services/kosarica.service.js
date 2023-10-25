import http from "../http-common";

class KosaricaDataService {
  getAll() {
    return http.get("/kosarica");
  }

  async getBySifra(sifra) {
   // console.log(sifra);
    return await http.get('/kosarica/' + sifra);
  }

  async getProizvodi(sifra) {
    // console.log(sifra);
     return await http.get('/kosarica/' + sifra + '/proizvodi');
   }
 


  async post(kosarica){
    //console.log(kosarica);
    const odgovor = await http.post('/kosarica',kosarica)
       .then(response => {
         return {ok:true, poruka: 'Unio košaricu'}; // return u odgovor
       })
       .catch(error => {
        console.log(error.response);
         return {ok:false, poruka: error.response.data}; // return u odgovor
       });
 
       return odgovor;
}


  async delete(sifra){
    
    const odgovor = await http.delete('/kosarica/' + sifra)
       .then(response => {
         return {ok:true, poruka: 'Uspješno Obrisao'};
       })
       .catch(error => {
         console.log(error);
         return {ok:false, poruka: error.response.data};
       });
 
       return odgovor;
     }

     async obrisiProizvod(kosarica, proizvod){
    
      const odgovor = await http.delete('/kosarica/' + kosarica + '/obrisi/' + proizvod)
         .then(response => {
           return {ok:true, poruka: 'Uspješno Obrisao'};
         })
         .catch(error => {
           console.log(error);
           return {ok:false, poruka: error.response.data};
         });
   
         return odgovor;
       }

       async dodajPolaznika(kosarica, proizvod){
    
        const odgovor = await http.post('/kosarica/' + kosarica + '/dodaj/' + proizvod)
           .then(response => {
             return {ok:true, poruka: 'Uspješno Dodao'};
           })
           .catch(error => {
             console.log(error);
             return {ok:false, poruka: error.response.data};
           });
     
           return odgovor;
         }

}

export default new KosaricaDataService();