
drop database if exists online_trgovina;
go
create database online_trgovina;
go
use online_trgovina;



create table kupac(
	sifra int not null primary key identity(1,1),
	korisnickoime varchar(30) not null,
	ime varchar(30) not null,
	prezime varchar(30) not null,
	lozinka varchar(50) not null,
	telefon varchar(15),
	adresa varchar(50)
);

create table kosarica(
	sifra int not null primary key identity(1,1),
	kupac int,
	kolicinaProizvod int not null,
	datumStvaranja datetime
);

create table proizvod(
	sifra int not null primary key identity(1,1),
	naziv varchar(50) not null,
	opis varchar(750) not null,
	cijena decimal(6,2) not null	
);

create table inventar(
	sifra int not null primary key identity(1,1),
	proizvod int,
	kategorija varchar(30),
	kolicina int not null,
	dostupnost bit not null
);

create table kosaricaProizvod
(
	sifra int not null primary key identity (1,1),
	kosarica int,
	proizvod int
);

--Veze između tablica

alter table kosarica add foreign key (kupac) references kupac(sifra);
alter table kosaricaProizvod add foreign key (kosarica) references kosarica(sifra);
alter table kosaricaProizvod add foreign key (proizvod) references proizvod(sifra);
alter table inventar add foreign key (proizvod) references proizvod(sifra);



--TABLICA KUPAC

insert into kupac(korisnickoime,ime,prezime,lozinka,telefon,adresa)
values ('DarkLord34','Matej','Knežević','Alsmhtj563nb','095 284 2371','Opatijska 12, Osijek');

insert into kupac(korisnickoime,ime,prezime,lozinka,telefon,adresa)
values ('Avante_marauder2','Eugen','Novak','770#kB7RGsJV','031/150-620','Rapska 37, Zagreb');

insert into kupac(korisnickoime,ime,prezime,lozinka,telefon,adresa)
values ('OniKyu','Tihana','Babić','1h8h9zYx@Ii@','092 358 1548','Put Ravnih Njiva 30, Split');



--TABLICA PROIZVOD

insert into proizvod(naziv,opis,cijena)
values ('NINTENDO Switch Lite-crven','Nintendo Switch Lite je mali i lagan Nintendo Switch sustav po odličnoj cijeni.',259.99);

insert into proizvod(naziv,opis,cijena)
values ('Battlefield 2042 PS5','Sljedeća generacija sveobuhvatnog rata je ovdje.',79.99);

insert into proizvod(naziv,opis,cijena)
values ('LED fleksibilna traka','Proširite mogućnosti osvjetljenja vašeg doma. 1m, 2.1W, 24V',11.59);



--TABLICA INVENTAR

insert into inventar(proizvod,kategorija,kolicina,dostupnost)
values (2,'Informatika',10,0);

insert into inventar(proizvod,kategorija,kolicina,dostupnost)
values (3,'Osvjetljenje',50,1);

insert into inventar(proizvod,kategorija,kolicina,dostupnost)
values (1,'Informatika',50,1);



--TABLICA KOSARICA

insert into kosarica(kupac,kolicinaProizvod,datumStvaranja)
values (2,15,'2020-02-20 17:25:01');

insert into kosarica(kupac,kolicinaProizvod,datumStvaranja)
values (1,5,'2022-05-02 10:58:32');

insert into kosarica(kupac,kolicinaProizvod,datumStvaranja)
values (3,20,'2023-12-05 08:26:41');



--TABLICA KOSARICAPROIZVOD

insert into kosaricaProizvod(kosarica,proizvod)
values(1,2),(2,3),(3,1);



--Isprobavanje inner join-a
/*
select k.ime, kk.datumStvaranja, p.naziv, i.dostupnost
from kupac k inner join kosarica kk on k.sifra=kk.kupac
inner join kosaricaProizvod kP on kk.sifra=kP.kosarica
inner join proizvod p on kP.proizvod=p.sifra
inner join inventar i on p.sifra=i.proizvod
*/

--Dohvaćanje podataka iz tablica
/*select korisnickoime,ime from kupac order by des;*/
/*select kupac,kolicinaProizvod from kosarica;*/
/*select * from proizvod;/*

--Promjena podataka
/*
update kupac set 
korisnickoime = 'TihBab',
lozinka = '213svat3Iyfg2i@',
telefon = '095 418 1748'
where sifra = 3;
*/

--Brisanje podataka
/*
delete from proizvod where sifra=3;
*/