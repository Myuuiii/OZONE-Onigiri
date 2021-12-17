create table Users (

	Id bigint not null primary key,
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