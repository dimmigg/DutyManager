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
        on c.DayOfWeekId = d.DayOfWeekId
    order by
        a.DateStart
end;


-- —Œ“–”ƒÕ» »
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


-- –‡·Ó˜ËÂ ‰ÌË
create procedure tool.uspDutyManagerGetWorkdays
as
begin
    select
        a.WorkdayId,
        a.EmployeeId,
        a.RosterId,
        a.IsAlways,
        a.DateWork,
        b.FullName,
        c.StartTime,
        c.DurationOfDuty,
        d.DayOfWeekName
    from
        tool.tDutyManagerWorkdays as a
    inner join
        tool.tDutyManagerEmployees as b
        on a.EmployeeId = b.EmployeeId
    inner join
        tool.tDutyManagerRoster as c
        on a.RosterId = c.RosterId
    inner join
        tool.tDutyManagerDaysOfWeek as d
        on c.DayOfWeekId = d.DayOfWeekId
end;


create procedure tool.uspDutyManagerAddWorkday
(
    @EmployeeId int,
    @RosterId int,
    @IsAlways bit,
    @DateWork datetime2(0)
)
as
begin
    if @IsAlways = 1
    begin
        set @DateWork = null;
    end;

    insert into tool.tDutyManagerWorkdays
    (
        EmployeeId,
        RosterId,
        IsAlways,
        DateWork
    )
    values
    (
        @EmployeeId,
        @RosterId,
        @IsAlways,
        @DateWork
    )
end;

create procedure tool.uspDutyManagerEditWorkday
(
    @WorkdayId int,
    @EmployeeId int,
    @RosterId int,
    @IsAlways bit,
    @DateWork datetime2(0)
)
as
begin
    if @IsAlways = 1
    begin
        set @DateWork = null;
    end;
    update a
    set
        a.EmployeeId = @EmployeeId,
        a.RosterId = @RosterId,
        a.IsAlways = @IsAlways,
        a.DateWork = @DateWork
    from
        tool.tDutyManagerWorkdays as a
    where
        a.WorkdayId = @WorkdayId;
end;

create procedure tool.uspDutyManagerDelWorkday
(
    @WorkdayId int
)
as
begin
    delete a
    from
        tool.tDutyManagerWorkdays as a
    where
        a.WorkdayId = @WorkdayId;
end;


-- ‰ÂÊÛÒÚ‚‡
create procedure tool.uspDutyManagerGetRoster
as
begin
    select
        a.RosterId,
        a.DayOfWeekId,
        a.StartTime,
        a.DurationOfDuty,
        b.DayOfWeekName
    from
        tool.tDutyManagerRoster as a
    inner join
        tool.tDutyManagerDaysOfWeek as b
        on a.DayOfWeekId = b.DayOfWeekId
    order by
        a.DayOfWeekId,
        a.StartTime
end;


create procedure tool.uspDutyManagerAddRoster
(
    @DayOfWeekId int,
    @StartTime time(0),
    @DurationOfDuty int
)
as
begin
    insert into tool.tDutyManagerRoster
    (
        DayOfWeekId,
        StartTime,
        DurationOfDuty
    )
    values
    (
        @DayOfWeekId,
        @StartTime,
        @DurationOfDuty
    )
end;

create procedure tool.uspDutyManagerEditRoster
(
    @RosterId int,
    @DayOfWeekId int,
    @StartTime time(0),
    @DurationOfDuty int
)
as
begin
    update a
    set
        a.DayOfWeekId = @DayOfWeekId,
        a.StartTime = @StartTime,
        a.DurationOfDuty = @DurationOfDuty
    from
        tool.tDutyManagerRoster as a
    where
        a.RosterId = @RosterId;
end;

create procedure tool.uspDutyManagerDelRoster
(
    @RosterId int
)
as
begin
    delete a
    from
        tool.tDutyManagerRoster as a
    where
        a.RosterId = @RosterId;
end;


-- ƒÌË ÌÂ‰ÂÎË
create procedure tool.uspDutyManagerGetDaysOfWeek
as
begin
    select
        a.DayOfWeekId,
        a.DayOfWeekName
    from
        tool.tDutyManagerDaysOfWeek as a;
end;

-- Œ“œ”— ¿
create procedure tool.uspDutyManagerGetHolidays
as
begin
    select
        a.HolidayId,
        a.EmployeeId,
        b.FullName as EmployeeName,
        a.DateStart,
        a.DateFinish
    from
        tool.tDutyManagerHolidays as a
    inner join
        tool.tDutyManagerEmployees as b
        on a.EmployeeId = b.EmployeeId;
end;

create procedure tool.uspDutyManagerAddHoliday
(
    @EmployeeId int,
    @DateStart datetime2(0),
    @DateFinish datetime2(0)
)
as
begin
    insert into tool.tDutyManagerHolidays
    (
        EmployeeId,
        DateStart,
        DateFinish
    )
    values
    (
        @EmployeeId,
        @DateStart,
        @DateFinish
    )
end;

create procedure tool.uspDutyManagerEditHoliday
(
    @HolidayId int,
    @EmployeeId int,
    @DateStart datetime2(0),
    @DateFinish datetime2(0)
)
as
begin
    update a
    set
        a.EmployeeId = @EmployeeId,
        a.DateStart = @DateStart,
        a.DateFinish = @DateFinish
    from
        tool.tDutyManagerHolidays as a
    where
        a.HolidayId = @HolidayId;
end;

create procedure tool.uspDutyManagerDelHoliday
(
    @HolidayId int
)
as
begin
    delete a
    from
        tool.tDutyManagerHolidays as a
    where
        a.HolidayId = @HolidayId;
end;
