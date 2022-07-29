using DutyManager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DutyManager.Extensions
{
    public static class IEnumerableExtensions
    {
        public static DataTable AsTable<T>(this IEnumerable<T> data)
        {
            DataTable result = new DataTable();
            List<TableProperty> properties = new List<TableProperty>();

            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(typeof(T)))
            {
                string name = descriptor.Name;
                var property = new TableProperty(descriptor, name);
                result.Columns.Add(name, property.PropertyType);
                properties.Add(property);
            }

            foreach (T item in data)
            {
                DataRow row = result.NewRow();

                foreach (TableProperty property in properties)
                {
                    row[property.Name] = property.GetValue(item) ?? DBNull.Value;
                }
                result.Rows.Add(row);
            }
            return result;
        }

        public static IEnumerable ToSelectListItems(this IEnumerable<DutyManager.Models.Employee> employees, int selectedId)
        {
            return employees.OrderBy(emp => emp.FullName)
                      .Select(emp =>
                          new SelectListItem
                          {
                              Selected = (emp.EmployeeId == selectedId),
                              Text = emp.FullName,
                              Value = emp.EmployeeId.ToString()
                          });
        }

        public static T Random<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            // note: creating a Random instance each call may not be correct for you,
            // consider a thread-safe static instance
            var r = new Random();
            var list = enumerable as IList<T> ?? enumerable.ToList();
            return list.Count == 0 ? default(T) : list[r.Next(0, list.Count)];
        }
    }
}
