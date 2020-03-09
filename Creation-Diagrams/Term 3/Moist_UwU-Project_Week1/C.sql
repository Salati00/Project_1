Create table [student](
	student_id int primary key identity(1,1) not null,
	first_name varchar(max) not null,
	last_name varchar(max) not null,
	phone_number varchar(max) null
);
go
Create table [teacher](
	teacher_id int primary key identity(1,1) not null,
	first_name varchar(max) not null,
	last_name varchar(max) not null,
	phone_number varchar(max) null
);
go
Create table [activities](
	activity_id int primary key identity(1,1) not null,
	[name] varchar(max) not null,
	[location] varchar(max) not null,
	[time] time not null,
	[date] date not null,
	[description] varchar(max) null,
);
go
Create table [drinks](
	drink_id int primary key identity(1,1) not null,
	cost int not null,
	[name] varchar(max) not null
);
go
Create table [room_types](
	room_type varchar(2) primary key not null,
	room_description varchar(max) not null
);
go
Create table [rooms](
	room_id int primary key identity(1,1) not null,
	room_type varchar(2) foreign key references room_types(room_type),
	room_capacity int not null
);
go
Create table [purchases](
	purchase_id int primary key identity(1,1) not null,
	student_id int foreign key references student(student_id),
	drink_id int foreign key references drinks(drink_id)
);
go
Create table [teacher_bookings](
	teacher_bookings_id int primary key identity(1,1) not null,
	teacher_id int foreign key references teacher(teacher_id),
	room_id int foreign key references rooms(room_id)
);
go
Create table [student_bookings](
	student_bookings_id int primary key identity(1,1) not null,
	student_id int foreign key references student(student_id),
	room_id int foreign key references rooms(room_id)
);
go
Create table [participates](
	participation_id int primary key identity(1,1) not null,
	student_id int foreign key references student(student_id),
	activity_id int foreign key references activities(activity_id)
);
go
Create table [supervises](
	supervise_id int primary key identity(1,1) not null,
	teacher_id int foreign key references teacher(teacher_id),
	activity_id int foreign key references activities(activity_id)
);
go

/*
drop table student;
go
drop table teacher;
go
drop table activities;
go
drop table drinks;
go
drop table room_types;
go
drop table rooms;
go
drop table purchases;
go
drop table teacher_bookings;
go
drop table student_bookings;
go
drop table participates;
go
drop table supervises;
*/