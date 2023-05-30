use master
drop database if exists online_trgovina
go
create database online_trgovina
go
use online_trgovina



create table kupac(
	sifra int not null primary key identity(1,1),
	korisnickoime varchar(30) not null,
	ime varchar(30) not null,
	prezime varchar(30) not null,
	lozinka varchar(50) not null,
	telefon int not null,
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