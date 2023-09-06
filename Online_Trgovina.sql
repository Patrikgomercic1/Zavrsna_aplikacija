use master
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
	adresa varchar(50) not null
);

create table kosarica(
	sifra int not null primary key identity(1,1),
	kolicina int not null,
	kupac int,
	proizvod int,
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
	kolicina int not null,
	dostupnost varchar(13) not null
);



alter table kosarica add foreign key (kupac) references  kupac(sifra);
alter table kosarica add foreign key (proizvod) references  proizvod(sifra);
alter table inventar add foreign key (proizvod) references  proizvod(sifra);



--TABLICA KUPAC
SET IDENTITY_INSERT kupac ON
insert into kupac(korisnickoime,ime,prezime,lozinka,telefon,adresa)
values ('DarkLord34','Matej','Knežević','Alsmhtj563nb','095 284 2371','Opatijska 12, Osijek');

insert into kupac(korisnickoime,ime,prezime,lozinka,telefon,adresa)
values ('Avante_marauder2','Eugen','Novak','770#kB7RGsJV','031/150-620','Rapska 37, Zagreb');

insert into kupac(korisnickoime,ime,prezime,lozinka,telefon,adresa)
values ('OniKyu','Tihana','Babić','1h8h9zYx@Ii@','092 358 1548','Put Ravnih Njiva 30, Split');

SET IDENTITY_INSERT kupac OFF


--TABLICA PROIZVOD
SET IDENTITY_INSERT proizvod ON

insert into proizvod(naziv,opis,cijena)
values ('NINTENDO Switch Lite-crven','Nintendo Switch Lite je mali i lagan Nintendo Switch sustav po odličnoj cijeni.','259.99');

insert into proizvod(naziv,opis,cijena)
values ('Battlefield 2042 PS5','Sljedeća generacija sveobuhvatnog rata je ovdje.','79.99');

insert into proizvod(naziv,opis,cijena)
values ('LED fleksibilna traka','Proširite mogućnosti osvjetljenja vašeg doma. 1m, 2.1W, 24V','11.59');

SET IDENTITY_INSERT proizvod OFF


--TABLICA INVENTAR
SET IDENTITY_INSERT inventar ON

insert into inventar(proizvod,kolicina,dostupnost)
values ('2','250','dostupno');

insert into inventar(proizvod,kolicina,dostupnost)
values ('3','100','nedostupno');

insert into inventar(proizvod,kolicina,dostupnost)
values ('1','50','dostupno');

SET IDENTITY_INSERT inventar OFF


--TABLICA KOSARICA
SET IDENTITY_INSERT kosarica ON

insert into kosarica(kolicina,kupac,proizvod)
values ('20','1','3');

insert into kosarica(kolicina,kupac,proizvod)
values ('1','2','2');

insert into kosarica(kolicina,kupac,proizvod)
values ('2','3','1');

SET IDENTITY_INSERT kosarica OFF

--isprobavanje inner join-a
select i.dostupnost, p.naziv, kk.korisnickoime, k.kolicina
from proizvod p inner join inventar i on p.sifra=i.proizvod 
inner join kosarica k on i.sifra=k.proizvod
inner join kupac kk on k.kupac=kk.sifra