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
	naziv varchar(20) not null,
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
insert into kupac(sifra,korisnickoime,ime,prezime,lozinka,telefon,adresa)
values (1,'DarkLord34','Matej','Knežević','Alsmhtj563nb','095 284 2371','Opatijska 12, Osijek');

insert into kupac(sifra,korisnickoime,ime,prezime,lozinka,telefon,adresa)
values (2,'Avante_marauder2','Eugen','Novak','770#kB7RGsJV','031/150-620','Rapska 37, Zagreb');

insert into kupac(sifra,korisnickoime,ime,prezime,lozinka,telefon,adresa)
values (3,'OniKyu','Tihana','Babić','1h8h9zYx@Ii@','092 358 1548','Put Ravnih Njiva 30, Split');


--TABLICA PROIZVOD
--String or binary data would be truncated in table 'online_trgovina.dbo.proizvod', column 'naziv'. Truncated value: 'NINTENDO Switch Lite'.
insert into proizvod(sifra,naziv,opis,cijena)
values (1,'NINTENDO Switch Lite-crven','Nintendo Switch Lite je mali i lagan Nintendo Switch sustav po odličnoj cijeni.','259.99');

insert into proizvod(sifra,naziv,opis,cijena)
values (2,'Battlefield 2042 PS5','Sljedeća generacija sveobuhvatnog rata je ovdje.','79.99');

--tring or binary data would be truncated in table 'online_trgovina.dbo.proizvod', column 'naziv'. Truncated value: 'LED fleksibilna trak'.
insert into proizvod(sifra,naziv,opis,cijena)
values (3,'LED fleksibilna traka','Proširite mogućnosti osvjetljenja vašeg doma. 1m, 2.1W, 24V','11.59');


insert into inventar(sifra,proizvod,kolicina,dostupnost)
values (1,'2','250','dostupno');

insert into inventar(sifra,proizvod,kolicina,dostupnost)
values (2,'3','100','nedostupno');

insert into inventar(sifra,proizvod,kolicina,dostupnost)
values (3,'1','50','dostupno');


insert into kosarica(sifra,kolicina,kupac,proizvod)
values (1,'20','1','3');

insert into kosarica(sifra,kolicina,kupac,proizvod)
values (2,'1','2','2');

insert into kosarica(sifra,kolicina,kupac,proizvod)
values (3,'2','3','1');