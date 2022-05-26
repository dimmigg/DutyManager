create procedure tool.uspDutyManagerGetEmployees
as
begin
    select
        a.EmployeeId,
        a.FullName,
        a.LoginName,
        a.Phone,
        a.OtherInfo
    from
        tool.tDutyManagerEmployees as a
end;


create procedure tool.uspDutyManagerGetRoster
as
begin
    select
        a.RosterId,
        a.DayWeekId,
        a.StartTime,
        a.DurationOfDuty
    from
        tool.tDutyManagerRoster as a
end;


create procedure tool.uspDutyManagerGetHolidays
as
begin
    select
        a.EmployeeId,
        a.DateStart,
        a.DateFinish
    from
        tool.tDutyManagerHolidays as a
end;

create procedure tool.uspDutyManagerGetWorkdays
as
begin
    select
        a.EmployeeId,
        a.RosterId,
        a.DateWork
    from
        tool.tDutyManagerWorkdays as a
end;


create procedure tool.uspDutyManagerGetMainTable
as
begin
    select
        a.MappingId,
        d.DayOfWeekName,
        a.DateStart, 
        dateadd(hour, c.DurationOfDuty, a.DateStart) as DateFinish,
        b.LoginName,
        b.FullName,
        b.Phone,
        b.OtherInfo
    from
        tool.tDutyManagerMapping as a
    inner join
        tool.tDutyManagerEmployees as b
        on a.EmployeeId = b.EmployeeId
    inner join
        tool.tDutyManagerRoster as c
        on a.RosterId = c.RosterId
    inner join
        tool.tDutyManagerDaysOfWeek as d
        on c.DayWeekId = d.DayOfWeekId
end;



create procedure tool.uspDutyManagerEditEmployee
(
    @EmployeeId int,
    @FullName varchar(200),
    @LoginName varchar(50),
    @Phone varchar(200)
)
as
begin
    update a
    set
        a.FullName = @FullName,
        a.Loginname = @LoginName,
        a.Phone = @Phone
    from
        tool.tDutyManagerEmployees as a
    where
        a.EmployeeId = @EmployeeId;
end;


create procedure tool.uspDutyManagerDelEmployee
(
    @EmployeeId int
)
as
begin
    delete a
    from
        tool.tDutyManagerEmployees as a
    where
        a.EmployeeId = @EmployeeId;
end;

create procedure tool.uspDutyManagerAddEmployee
(
    @FullName varchar(200),
    @LoginName varchar(50),
    @Phone varchar(200)
)
as
begin
    insert into tool.tDutyManagerEmployees
    (
        FullName,
        LoginName,
        Phone
    )
    values
    (
        @FullName,
        @LoginName,
        @Phone
    )
end;