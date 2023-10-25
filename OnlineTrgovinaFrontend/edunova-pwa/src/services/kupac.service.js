import http from "../http-common";


class KupacDataService{

    async get(){
        return await http.get('/Kupac');
    }

    async getBySifra(sifra) {
        return await http.get('/kupac/' + sifra);
      }

    async delete(sifra){
        const odgovor = await http.delete('/Kupac/' + sifra)
        .then(response => {
            return {ok: true, poruka: 'Uspješno obrisao'};
        })
        .catch(e=>{
            return {ok: false, poruka: e.response.data};
        });

        return odgovor;
    }


    async post(kupac){
        //console.log(smjer);
        const odgovor = await http.post('/kupac',kupac)
           .then(response => {
             return {ok:true, poruka: 'Unio kupca'}; // return u odgovor
           })
           .catch(error => {
            //console.log(error.response);
             return {ok:false, poruka: error.response.data}; // return u odgovor
           });
     
           return odgovor;
    }

    async put(sifra,kupac){
        //console.log(kupac);
        const odgovor = await http.put('/kupac/' + sifra,kupac)
           .then(response => {
             return {ok:true, poruka: 'Promjenio kupca'}; // return u odgovor
           })
           .catch(error => {
            //console.log(error.response);
             return {ok:false, poruka: error.response.data}; // return u odgovor
           });
     
           return odgovor;
         }

}

export default new KupacDataService();