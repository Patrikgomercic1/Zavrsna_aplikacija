using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Početak.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloWorldController:ControllerBase
    {
        [HttpGet]
        public string Hello()
        {
            return "Hello world";
        }

        [HttpGet]
        [Route("pozdrav")]
        public string Hello2() 
        {
            return "Pozdrav svijetu";
        }

        [HttpGet]
        [Route("pozdravParametar")]
        public string Hello3(string s)
        {
            return "Hello " + s;
        }

        [HttpGet]
        [Route("pozdravViseParametar")]
        public string Hello3(string s = "",int i = 0)
        {
            return "Hello " + s + " " + i;
        }

        //Z1
        [HttpGet]
        [Route("HelloWorld/Z1")]
        public string HelloZ1()
        {
            return "Patrik";
        }

        //Z2
        [HttpGet]
        [Route("HelloWorld/Z2")]
        public int HelloZ2(int pb, int db)
        {
            int zbroj=pb+db;
            return zbroj;
        }

        //Z3
        [HttpGet]
        [Route("HelloWorld/Z3")]
        public string[] HelloZ3(int brojPonavljanja)
        {
            var nizOsijek = new string[brojPonavljanja];
            for(int i = 0; i < brojPonavljanja; i++)
            {
                nizOsijek[i] = "Osijek";
            }
            return nizOsijek;
        }



        [HttpGet]
        [Route("{sifra:int}")]
        public string PozdravRuta(int sifra)
        {
            return "Hello " + sifra;
        }

        [HttpGet]
        [Route("{sifra:int}/{kategorija}")]
        public string PozdravRuta2(int sifra, string kategorija)
        {
            return "Hello " + sifra + " " + kategorija;
        }


        [HttpPost]
        public string DodavanjeNovog(string ime)
        {
            return "Dodao " + ime;
        }

        [HttpPut]
        public string Promjena(int sifra, string naziv)
        {
            return "Na šifri " + sifra + " postavljam " + naziv;
        }

        [HttpDelete]
        public bool Obrisao(int sifra)
        {
            return true;
        }

        [HttpGet]
        [Route("matrica")]
        public IActionResult Matrica(int redovi, int stupci)
        {
            //vlastiti kod za cikličnu matricu

            //Stvaranje nizova
        
            int[,] matrica = new int[redovi, stupci];

            //Brojač 
            int brojac = 1;


            //Dodatni brojač kako bi se tablica mogla ispravno vrtiti/ispisivati više puta
            var dodbrojac = 0;


            //Petlja sa uputama za ispisivanje obrnutim redoslijedom
            while (brojac < redovi * stupci)
            {
                //Desna strana na lijevu
                for (int i = dodbrojac + 1; i <= stupci - dodbrojac; i++)
                {
                    if (brojac <= stupci * redovi)
                    {
                        matrica[redovi - dodbrojac - 1, stupci - i] = brojac++;
                    }
                    else
                    {
                        break;
                    }

                }

                //Prema gore
                for (int i = redovi - dodbrojac - 2; i >= dodbrojac; i--)
                {
                    if (brojac <= stupci * redovi)
                    {
                        matrica[i, dodbrojac] = brojac++;
                    }
                    else
                    {
                        break;
                    }
                }

                //Lijeva strana na desnu
                for (int i = dodbrojac + 1; i <= stupci - dodbrojac - 1; i++)
                {
                    if (brojac <= stupci * redovi)
                    {
                        matrica[dodbrojac, i] = brojac++;
                    }
                    else
                    {
                        break;
                    }
                }

                //Prema dolje
                for (int i = dodbrojac + 1; i <= redovi - dodbrojac - 2; i++)
                {
                    if (brojac <= stupci * redovi)
                    {
                        matrica[i, stupci - dodbrojac - 1] = brojac++;
                    }
                    else
                    {
                        break;
                    }
                }


                //Ispis tablice
                Console.WriteLine(" ");
                Console.WriteLine("::   :: CIKLIČNA TABLICA v2 ::  ::");
                //Console.WriteLine(" ");

                Console.WriteLine("*---------- | {0}. krug | ----------*", dodbrojac + 1);
                Console.WriteLine(" ");

                string IspisTablice;
                for (int i = 0; i < redovi; i++)
                {
                    for (int j = 0; j < stupci; j++)
                    {
                        IspisTablice = "    " + matrica[i, j];
                        Console.Write(IspisTablice[^4..]);
                    }
                    Console.WriteLine();
                }
                dodbrojac++;
            }

            return new JsonResult(JsonConvert.SerializeObject(matrica));
        }



    }
}
