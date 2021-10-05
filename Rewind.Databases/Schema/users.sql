drop table if exists users;
create table users
(
	id int identity(1,1) primary key,
	firstName varchar(500) not null,
	lastName varchar(500) not null,
	userName varchar(15),
	email varchar(500),
	passwordHash varchar(500),
	phone varchar(50),
	createdTimestamp datetime default GETUTCDATE(),
	modifiedTimestamp datetime default GETUTCDATE(),
	lastLoginTimestamp datetime default GETUTCDATE(),
	userTimezone varchar(500) default 'Greenwich Standard Time'
);


select * from users