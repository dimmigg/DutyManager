using DutyManager.Extensions;
using DutyManager.Models;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

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
            if(data.Any())
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
    }
}
