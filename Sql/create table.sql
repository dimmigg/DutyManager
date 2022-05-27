drop table tool.tDutyManagerEmployees
drop table tool.tDutyManagerDaysOfWeek
drop table tool.tDutyManagerRoster
drop table tool.tDutyManagerMapping
drop table tool.tDutyManagerHolidays
drop table tool.tDutyManagerWorkdays

create table tool.tDutyManagerEmployees
(
    EmployeeId  int identity(1,1)   not null,
    FullName    varchar(200)        not null,
    LoginName   varchar(50)         not null,
    Phone       varchar(200)            null,
    OtherInfo   varchar(200)            null
)on [primary]

create table tool.tDutyManagerDaysOfWeek
(
    DayOfWeekId   int identity(1,1) not null,
    DayOfWeekName varchar(20)       not null,
)on [primary]

create table tool.tDutyManagerRoster
(
    RosterId        int identity(1,1)   not null,
    DayOfWeekId     int                 not null,
    StartTime       time(0)             not null,
    DurationOfDuty  int                 not null
)on [primary]

create table tool.tDutyManagerHolidays
(
    HolidayId   int identity(1,1)   not null,
    EmployeeId  int                 not null,
    DateStart   datetime2(0)        not null,
    DateFinish  datetime2(0)        not null
)on [primary]

create table tool.tDutyManagerWorkdays
(
    WorkdayId   int identity(1,1)   not null,
    EmployeeId  int                 not null,
    RosterId    int                 not null,
    DateWork    datetime2(0)        not null,
)on [primary]

create table tool.tDutyManagerMapping
(
    MappingId   int identity(1,1)   not null,
    RosterId    int                 not null,
    EmployeeId  int                 not null,
    DateStart   datetime2(0)        not null,
)on [primary]

insert into tool.tDutyManagerEmployees (FullName, LoginName, Phone, OtherInfo)values ('Иванов Иван Иванович', 'ivanov', '899999999', 'Всякая инфа' )
insert into tool.tDutyManagerEmployees (FullName, LoginName, Phone, OtherInfo)values ('Петров Петр Петрович', 'petrov', '899999999', 'Всякая инфа' )

insert into tool.tDutyManagerDaysOfWeek (DayOfWeekName) values ('Понедельник')
insert into tool.tDutyManagerDaysOfWeek (DayOfWeekName) values ('Вторник')
insert into tool.tDutyManagerDaysOfWeek (DayOfWeekName) values ('Среда')
insert into tool.tDutyManagerDaysOfWeek (DayOfWeekName) values ('Четврг')
insert into tool.tDutyManagerDaysOfWeek (DayOfWeekName) values ('Пятница')
insert into tool.tDutyManagerDaysOfWeek (DayOfWeekName) values ('Суббота')
insert into tool.tDutyManagerDaysOfWeek (DayOfWeekName) values ('Воскресенье')

insert into tool.tDutyManagerRoster (DayWeekId, StartTime, DurationOfDuty) values (1, '09:00:00', 12)
insert into tool.tDutyManagerRoster (DayWeekId, StartTime, DurationOfDuty) values (1, '21:00:00', 12)

insert into tool.tDutyManagerRoster (DayWeekId, StartTime, DurationOfDuty) values (2, '09:00:00', 12)
insert into tool.tDutyManagerRoster (DayWeekId, StartTime, DurationOfDuty) values (2, '21:00:00', 12)

insert into tool.tDutyManagerRoster (DayWeekId, StartTime, DurationOfDuty) values (3, '09:00:00', 12)
insert into tool.tDutyManagerRoster (DayWeekId, StartTime, DurationOfDuty) values (3, '21:00:00', 12)

insert into tool.tDutyManagerRoster (DayWeekId, StartTime, DurationOfDuty) values (4, '09:00:00', 12)
insert into tool.tDutyManagerRoster (DayWeekId, StartTime, DurationOfDuty) values (4, '21:00:00', 12)

insert into tool.tDutyManagerRoster (DayWeekId, StartTime, DurationOfDuty) values (5, '09:00:00', 12)
insert into tool.tDutyManagerRoster (DayWeekId, StartTime, DurationOfDuty) values (5, '21:00:00', 12)

insert into tool.tDutyManagerRoster (DayWeekId, StartTime, DurationOfDuty) values (6, '09:00:00', 12)
insert into tool.tDutyManagerRoster (DayWeekId, StartTime, DurationOfDuty) values (6, '21:00:00', 12)

insert into tool.tDutyManagerRoster (DayWeekId, StartTime, DurationOfDuty) values (7, '09:00:00', 12)
insert into tool.tDutyManagerRoster (DayWeekId, StartTime, DurationOfDuty) values (7, '21:00:00', 12)


insert into tool.tDutyManagerHolidays (EmployeeId, DateStart, DateFinish) values (1, getdate(), getdate()+1)

insert into tool.tDutyManagerWorkdays (EmployeeId, RosterId, DateWork) values (1, 11, '2022-05-21')
insert into tool.tDutyManagerWorkdays (EmployeeId, RosterId, DateWork) values (1, 11, '2022-05-22')
insert into tool.tDutyManagerWorkdays (EmployeeId, RosterId, DateWork) values (1, 11, '2022-05-23')
insert into tool.tDutyManagerWorkdays (EmployeeId, RosterId, DateWork) values (1, 11, '2022-05-24')
                                        
truncate table tool.tDutyManagerWorkdays
truncate table tool.tDutyManagerMapping

select * from 
    tool.tDutyManagerRoster as a
inner join
    tool.tDutyManagerDaysOfWeek as b
    on a.DayWeekId = b.DayOfWeekId

select * 
from 
    tool.tDutyManagerMapping as a
inner join
    tool.tDutyManagerEmployees as b
    on a.EmployeeId = b.EmployeeId

select 
    a.EmployeeId,
    count(a.DateStart)
from
    tool.tDutyManagerMapping as a
group by
    a.EmployeeId