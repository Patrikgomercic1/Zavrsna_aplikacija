$(document).foundation()

$.ajax('https://localhost:7029/api/v1/Kupac',   // request url
    {
        success: function (data, status, xhr) {// success callback function
            //console.log(data);
            for(let i=0;i<data.length;i++){
                $('#podaci').append('<li>'+data[i].korisnickoIme+'</li>');
            }
    }
});    

$('#Dodaj').click(function(){

    let kupac = { korisnickoIme: $('#korisnickoIme').val(), ime: $('#ime').val(), prezime: $('#prezime').val(), lozinka: $('#lozinka').val(), telefon: $('#telefon').val, adresa: $('#adresa') };

    $.ajax('https://localhost:7029/api/v1/Kupac', {
        type: 'POST',  // http method
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify(kupac),  // data to submit
        success: function (data, status, xhr) {
            $('#podaci').append('<li>'+ $('#korisnickoIme') +'</li>');
        },
        error: function (jqXhr, textStatus, errorMessage) {
                //alert(errorMessage);
                console.log(errorMessage)
        }
    });

    return false;
});