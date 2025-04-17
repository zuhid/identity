
if exists (select 1 from sys.databases where name = 'Log')
  drop database Log;
go
create database Log;
go
use Log;
go
create table dbo.Log (
    Id uniqueidentifier NOT NULL
  , Updated datetime2 null
  , LogLevel nvarchar(100) null
  , Category nvarchar(100) null
  , EventId nvarchar(100) null
  , EventName nvarchar(100) null
  , State nvarchar(max) null
  , Exception nvarchar(max) null,
  CONSTRAINT [PK_User] PRIMARY KEY ([Id])
)
