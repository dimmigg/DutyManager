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

        public static void InsertData<T>(IEnumerable<T> data, string table)
        {
            if (data.Any())
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Connection.Instance.ConnectionString()))
                {
                    bulkCopy.DestinationTableName = table;
                    foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(typeof(T)))
                    {
                        bulkCopy.ColumnMappings.Add(descriptor.Name, descriptor.Name);
                    }
                    try
                    {
                        bulkCopy.WriteToServer(data.AsTable());
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

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
    }
}
