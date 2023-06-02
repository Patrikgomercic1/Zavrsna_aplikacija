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
	opis varchar(100) not null,
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



insert into kupac(sifra,korisnickoime,ime,prezime,lozinka,telefon,adresa)
values (1,'DarkLord34','Matej','Knežević','Alsmhtj563nb','095 284 2371','Opatijska 12, Osijek');

insert into kupac(sifra,korisnickoime,ime,prezime,lozinka,telefon,adresa)
values (2,'Avante_marauder2','Eugen','Novak','770#kB7RGsJV','031/150-620','Rapska 37, Zagreb');

insert into kupac(sifra,korisnickoime,ime,prezime,lozinka,telefon,adresa)
values (3,'OniKyu','Tihana','Babić','1h8h9zYx@Ii@','092 358 1548','Put Ravnih Njiva 30, Split');