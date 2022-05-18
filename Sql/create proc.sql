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