CREATE DATABASE HackatonDb2
GO

use HackatonDb2;

CREATE TABLE SensorData
(
    SensorName varchar(255) NOT NULL,
    SensorValue varchar(255) NULL,
	EventProcessedUtcTime datetimeoffset -- this column must exist!
)
GO

CREATE TABLE Sensors
(
	Id int not null identity(1,1) primary key,
    Name varchar(255) NOT NULL,
    Location varchar(255) NULL
)
GO
