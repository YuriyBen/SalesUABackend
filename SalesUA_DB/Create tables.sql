create database SalesProject;

create table Shop
(
Id			INT				Primary key		Identity(1,1),
Title		Varchar(30)		NOT NULL,
ImagePath	Varchar(max)	NOT NULL,
CONSTRAINT UQ_Title UNIQUE (Title)
);

create table Product
(
Id					INT					Primary key		Identity(1,1),
Title				Varchar(30)			NOT NULL,
OldPrice			Smallmoney			NOT NULL,
NewPrice			Smallmoney			NOT NULL,
DiscountPercent		tinyint				NOT NULL,
Description			Varchar(1000)		NOT NULL,
ImagePath			Varchar(max)		NOT NULL,
ShopId				INT					NOT NULL,
Foreign key(ShopId) references Shop(Id) on update cascade on delete cascade
);




