using DutyManager.Extensions;
using DutyManager.Models;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using Dapper;

namespace DutyManager.DB
{
    public static class DBService
    {
        public static IEnumerable<T> GetData<T>(string procedure)
        {
            using (var connect = Connection.Instance.GetNewConnection())
            {
                return connect.ExecuteProcedure<T>(procedure);
            }
        }

        public static void InsertData<T>(IEnumerable<T> data)
        {
            if (data.Any())
            {

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Connection.Instance.ConnectionString()))
                {
                    bulkCopy.DestinationTableName = SqlStr.Mapping;
                    foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(typeof(T)))
                    {
                        bulkCopy.ColumnMappings.Add(descriptor.Name, descriptor.Name);
                    }
                    try
                    {
                        DelAll(SqlStr.Mapping);
                        bulkCopy.WriteToServer(data.AsTable());
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        #region Holiday
        internal static void DelHoliday(int id)
        {
            using (var connect = Connection.Instance.GetNewConnection())
            {
                DynamicParameters dp = new DynamicParameters();
                dp.Add("HolidayId", id);
                connect.ExecuteProcedure<string>(SqlStr.DelHoliday, dp);
            }
        }

        internal static void EditHoliday(Holiday day)
        {
            using (var connect = Connection.Instance.GetNewConnection())
            {
                DynamicParameters dp = new DynamicParameters();
                dp.Add("HolidayId", day.HolidayId);
                dp.Add("EmployeeId", day.EmployeeId);
                dp.Add("DateStart", day.DateStart);
                dp.Add("DateFinish", day.DateFinish);

                connect.ExecuteProcedure<string>(SqlStr.EditHoliday, dp);
            }
        }

        internal static void DelAll(string table)
        {
            using (var connect = Connection.Instance.GetNewConnection())
            {
                connect.Execute($"truncate table {table}");
            }
        }

        internal static void AddHoliday(Holiday day)
        {
            using (var connect = Connection.Instance.GetNewConnection())
            {
                DynamicParameters dp = new DynamicParameters();
                dp.Add("EmployeeId", day.EmployeeId);
                dp.Add("DateStart", day.DateStart);
                dp.Add("DateFinish", day.DateFinish);

                connect.ExecuteProcedure<string>(SqlStr.AddHoliday, dp);
            }
        }
        #endregion

        #region Roster
        internal static void DelRoster(int id)
        {
            using (var connect = Connection.Instance.GetNewConnection())
            {
                DynamicParameters dp = new DynamicParameters();
                dp.Add("RosterId", id);
                connect.ExecuteProcedure<string>(SqlStr.DelRoster, dp);
            }
        }

        internal static void EditRoster(Roster ros)
        {
            using (var connect = Connection.Instance.GetNewConnection())
            {
                DynamicParameters dp = new DynamicParameters();
                dp.Add("RosterId", ros.RosterId);
                dp.Add("DayOfWeekId", ros.DayOfWeekId);
                dp.Add("StartTime", ros.StartTime);
                dp.Add("DurationOfDuty", ros.DurationOfDuty);

                connect.ExecuteProcedure<string>(SqlStr.EditRoster, dp);
            }
        }

        internal static void AddRoster(Roster ros)
        {
            using (var connect = Connection.Instance.GetNewConnection())
            {
                DynamicParameters dp = new DynamicParameters();
                dp.Add("DayOfWeekId", ros.DayOfWeekId);
                dp.Add("StartTime", ros.StartTime);
                dp.Add("DurationOfDuty", ros.DurationOfDuty);

                connect.ExecuteProcedure<string>(SqlStr.AddRoster, dp);
            }
        }
        #endregion

        #region Workday
        internal static void DelWorkday(int id)
        {
            using (var connect = Connection.Instance.GetNewConnection())
            {
                DynamicParameters dp = new DynamicParameters();
                dp.Add("WorkdayId", id);
                connect.ExecuteProcedure<string>(SqlStr.DelWorkday, dp);
            }
        }

        internal static void EditWorkday(Workday day)
        {
            using (var connect = Connection.Instance.GetNewConnection())
            {
                DynamicParameters dp = new DynamicParameters();
                dp.Add("WorkdayId", day.WorkdayId);
                dp.Add("EmployeeId", day.EmployeeId);
                dp.Add("RosterId", day.RosterId);
                dp.Add("IsAlways", day.IsAlways);
                dp.Add("DateWork", day.DateWork);

                connect.ExecuteProcedure<string>(SqlStr.EditWorkday, dp);
            }
        }

        internal static void AddWorkday(Workday day)
        {
            using (var connect = Connection.Instance.GetNewConnection())
            {
                DynamicParameters dp = new DynamicParameters();
                dp.Add("EmployeeId", day.EmployeeId);
                dp.Add("RosterId", day.RosterId);
                dp.Add("IsAlways", day.IsAlways);
                dp.Add("DateWork", day.DateWork);

                connect.ExecuteProcedure<string>(SqlStr.AddWorkday, dp);
            }
        }
        #endregion

        #region Employee
        internal static void EditEmployee(Employee emp)
        {
            using (var connect = Connection.Instance.GetNewConnection())
            {
                DynamicParameters dp = new DynamicParameters();
                dp.Add("EmployeeId", emp.EmployeeId);
                dp.Add("FullName", emp.FullName);
                dp.Add("LoginName", emp.LoginName);
                dp.Add("Phone", emp.Phone);
                
                connect.ExecuteProcedure<string>(SqlStr.EditEmployee, dp);
            }
        }

        internal static void AddEmployee(Employee emp)
        {
            using (var connect = Connection.Instance.GetNewConnection())
            {
                DynamicParameters dp = new DynamicParameters();
                dp.Add("FullName", emp.FullName);
                dp.Add("LoginName", emp.LoginName);
                dp.Add("Phone", emp.Phone);

                connect.ExecuteProcedure<string>(SqlStr.AddEmployee, dp);
            }
        }

        internal static void DelEmployee(int id)
        {
            using (var connect = Connection.Instance.GetNewConnection())
            {
                DynamicParameters dp = new DynamicParameters();
                dp.Add("EmployeeId", id);
                connect.ExecuteProcedure<string>(SqlStr.DelEmployee, dp);
            }
        }
        #endregion
    }
}
