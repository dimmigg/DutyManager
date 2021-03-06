using DutyManager.DB;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DutyManager.Models
{
    public class Employee
    {
        [HiddenInput(DisplayValue = false)]
        public int EmployeeId { get; set; }
        [Display(Name = "Полное имя")]
        [Required(ErrorMessage = "Необходимо ввести имя")]
        [StringLength(50, ErrorMessage = "Имя не должно превышать 50 симовлов")]
        public string FullName { get; set; }
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Необходимо ввести логин")]
        [StringLength(20, ErrorMessage = "Логин не должен превышать 20 симовлов")]
        public string LoginName { get; set; }
        [Display(Name = "Телефон")]
        [Required(ErrorMessage = "Необходимо ввести телефон")]
        [StringLength(15, ErrorMessage = "Телефон не должен превышать 15 симовлов")]
        public string Phone { get; set; }
        [Display(Name = "Другая информация")]
        [StringLength(200, ErrorMessage = "Поле не должно превышать 200 симовлов")]
        public string OtherInfo { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int CountDuty { get; set; } = 0;
        public static IEnumerable<Employee> GetAllEmployees() => DBService.GetData<Employee>(SqlStr.GetEmployees);
        public static Employee GetEmployeeById(int id) => id == -1 ? new Employee() : GetAllEmployees().FirstOrDefault(x => x.EmployeeId == id);
        public static void EditEmployee(Employee emp)
        {
            if (emp.EmployeeId == 0)
                DBService.AddEmployee(emp);
            else
                DBService.EditEmployee(emp);
        }
        public static void AddEmployee(Employee emp) => DBService.AddEmployee(emp);
        public static void DelEmployee(int id) => DBService.DelEmployee(id);
    }
}
