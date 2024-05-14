namespace AssesmentBE.Models
{
    public class Qualification
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
