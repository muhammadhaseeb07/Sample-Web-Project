# Sample-Web-Project


Server: HASEEB
DataBase: db

//Table
Create Table Candidate
(
	ID int primary key Identity(400,1),
	Username VARCHAR(20) unique,
	Password1 VARCHAR(20)
)

//Sample Data
Insert into Candidate values ('Haseeb','ABC')
Insert into Candidate values ('Moazan','ABC')
Insert into Candidate values ('Majid','ABC')
