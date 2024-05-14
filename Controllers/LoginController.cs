using AssesmentBE.Data;
using AssesmentBE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssesmentBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public LoginController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("employees")]
        public async Task<ActionResult<List<Employee>>> GetAllEmployees()
        {
            var employees = await _appDbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _appDbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound("Employee not found");
            }
            return Ok(employee);
        }

        /* [HttpPost("create")]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            if (employee != null)
            {
                var result = await _appDbContext.CreateEmployeeWithQualificationsSP(employee.Name, employee.Email, employee.Password, employee.DepartmentName, employee.Qualifications);
                // Assuming the stored procedure returns the ID of the newly created employee
                employee.Id = result;

                return Ok(employee);
            }
            return BadRequest("Invalid Request");
        }
        */

        [HttpPost("create")]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            if (employee != null)
            {
                // Assuming employee.Qualifications is a comma-separated string of qualification names
                var qualificationNames = employee.Qualifications?.Split(',');

                // Add employee to context
                var result = _appDbContext.Employees.Add(employee).Entity;

                // If there are qualification names, add them to the Qualifications table
                if (qualificationNames != null && qualificationNames.Length > 0)
                {
                    foreach (var qualificationName in qualificationNames)
                    {
                        var qualification = new Qualification { Name = qualificationName };
                        _appDbContext.Qualifications.Add(qualification);
                    }
                }

                await _appDbContext.SaveChangesAsync();
                return Ok(result);
            }
            return BadRequest("Invalid Request");
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee updatedEmployee)
        {
            var employee = await _appDbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            employee.Name = updatedEmployee.Name;
            employee.Email = updatedEmployee.Email;
            employee.Password = updatedEmployee.Password;
            // Update other properties as needed

            _appDbContext.Employees.Update(employee);
            await _appDbContext.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var employee = await _appDbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            _appDbContext.Employees.Remove(employee);
            await _appDbContext.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpPost("login")]
        public async Task<ActionResult<Employee>> Login(LoginRequest loginRequest)
        {
            if (loginRequest != null)
            {
                var user = await _appDbContext.Employees
                    .Where(x => x.Email!.ToLower() == loginRequest.Email.ToLower() && x.Password == loginRequest.Password)
                    .FirstOrDefaultAsync();

                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            return BadRequest("Invalid Request");
        }

        [HttpGet("qualifications")]
        public ActionResult<List<Qualification>> GetAllQualifications()
        {
            var qualifications = _appDbContext.Qualifications.ToList();
            return Ok(qualifications);
        }

        [HttpGet("departments")]
        public ActionResult<List<Department>> GetAllDepartments()
        {
            var departments = _appDbContext.Departments.ToList();
            return Ok(departments);
        }
    }
}
