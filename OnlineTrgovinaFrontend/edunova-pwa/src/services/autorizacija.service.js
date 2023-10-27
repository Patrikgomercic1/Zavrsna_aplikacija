import http from "../http-common";

class AutorizacijaDataService {


  async post(operater){
    const odgovor = await http.post('/Autorizacija/token',operater)
       .then(response => {
         return {ok:true, token: response.data}; 
       })
       .catch(error => {
         return {ok:false, poruka: 'Neispravna kombinacija korisničkog imena i lozinke'}; 
       });
 
       return odgovor;
}

   
    
}

export default new AutorizacijaDataService();