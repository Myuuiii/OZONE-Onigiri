create table Users (

	Id bigint not null primary key,
	Username char(128) not null,
	Discriminator char(5) not null,
	Avatar text,
	MessagesSent int not null default 0,
	Experience int not null default 0,
	level int not null default 1

)

create table Roles
(

	Id bigint not null primary key,
	Level int

)