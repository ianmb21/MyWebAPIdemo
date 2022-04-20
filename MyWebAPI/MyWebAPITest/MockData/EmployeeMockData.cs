using MyWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebAPITest.MockData
{
    public class EmployeeMockData
    {
        public static List<Employee> GetEmployees()
        {
            return new List<Employee>
            {
                new Employee {
                    id = 1,
                    fullName = "test",
                    mobile = "123",
                    email = "test@email.com",
                    age = 12,
                    position = "test",
                    address = "test" 
                }
            };
        }

        public static Employee PostEmployee()
        {
            return new Employee {
                fullName = "test",
                mobile = "123",
                email = "test@email.com",
                age = 12,
                position = "test",
                address = "test"
            };
        }
    }
}
