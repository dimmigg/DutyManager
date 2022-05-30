namespace DutyManager.DB
{ 
    public static class SqlStr
    {
        internal static string GetDaysOfWeek => "tool.uspDutyManagerGetDaysOfWeek";

        public static string GetMainTable => "tool.uspDutyManagerGetMainTable";
        public static string Mapping => "tool.tDutyManagerMapping";

        // Сотрудники
        public static string GetEmployees => "tool.uspDutyManagerGetEmployees";
        public static string EditEmployee => "tool.uspDutyManagerEditEmployee";
        public static string AddEmployee => "tool.uspDutyManagerAddEmployee";
        public static string DelEmployee => "tool.uspDutyManagerDelEmployee";

        //Рабочие дни
        public static string Workday => "tool.tDutyManagerWorkdays";
        public static string GetWorkdays => "tool.uspDutyManagerGetWorkdays";
        public static string AddWorkday => "tool.uspDutyManagerAddWorkday";
        public static string EditWorkday => "tool.uspDutyManagerEditWorkday";
        public static string DelWorkday => "tool.uspDutyManagerDelWorkday";

        //Дежурства
        public static string GetRoster => "tool.uspDutyManagerGetRoster";
        public static string DelRoster => "tool.uspDutyManagerDelRoster";
        public static string EditRoster => "tool.uspDutyManagerEditRoster";
        public static string AddRoster => "tool.uspDutyManagerAddRoster";

        //Отпуска
        public static string Holiday => "tool.tDutyManagerHolidays";
        public static string GetHolidays => "tool.uspDutyManagerGetHolidays";
        public static string DelHoliday => "tool.uspDutyManagerDelHoliday";
        public static string EditHoliday => "tool.uspDutyManagerEditHoliday";
        public static string AddHoliday => "tool.uspDutyManagerAddHoliday";

    }
}
