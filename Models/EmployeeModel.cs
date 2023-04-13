namespace mvc_asp_dotnet_project2.Models
{
    public class EmployeeModel
    {

        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public DateTime dateOfBirth { get; set; }

        public string Technology { get; set; }
        public string BaseLocation { get; set; }    

        public float Salary { get; set; }
        public string Email { get; set; }

    }
}
