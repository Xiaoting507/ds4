create database Productos;

use Productos;

create table Laptops (
	id int not null identity,
	nombre varchar(50) not null,
	precio decimal(6,2),
	stock float,
	constraint pk_laptops primary key(id)
);