import http from "../http-common";

class InventarDataService {
  getAll() {
    return http.get("/inventar");
  }

  async getBySifra(sifra) {
   // console.log(sifra);
    return await http.get('/inventar/' + sifra);
  }

  async getProizvodi(sifra) {
    // console.log(sifra);
     return await http.get('/inventar/' + sifra + '/proizvodi');
   }
 


  async post(inventar){
    //console.log(inventar);
    const odgovor = await http.post('/inventar',inventar)
       .then(response => {
         return {ok:true, poruka: 'Unio inventar'}; // return u odgovor
       })
       .catch(error => {
        console.log(error.response);
         return {ok:false, poruka: error.response.data}; // return u odgovor
       });
 
       return odgovor;
}


  async delete(sifra){
    
    const odgovor = await http.delete('/inventar/' + sifra)
       .then(response => {
         return {ok:true, poruka: 'Uspješno obrisao'};
       })
       .catch(error => {
         console.log(error);
         return {ok:false, poruka: error.response.data};
       });
 
       return odgovor;
     }

     async obrisiProizvod(inventar, proizvod){
    
      const odgovor = await http.delete('/inventar/' + inventar + '/obrisi/' + proizvod)
         .then(response => {
           return {ok:true, poruka: 'Uspješno obrisao'};
         })
         .catch(error => {
           console.log(error);
           return {ok:false, poruka: error.response.data};
         });
   
         return odgovor;
       }

       async dodajProizvod(inventar, proizvod){
    
        const odgovor = await http.post('/inventar/' + inventar + '/dodaj/' + proizvod)
           .then(response => {
             return {ok:true, poruka: 'Uspješno dodao'};
           })
           .catch(error => {
             console.log(error);
             return {ok:false, poruka: error.response.data};
           });
     
           return odgovor;
         }

}

export default new InventarDataService();