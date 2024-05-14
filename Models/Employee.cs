namespace AssesmentBE.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public List<Qualification> Qualifications { get; set; }
    }
}
