namespace AssesmentBE.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DepartmentName { get; set; }
        public string Qualifications { get; set; } // Store qualifications as JSON string
       
    }
}
