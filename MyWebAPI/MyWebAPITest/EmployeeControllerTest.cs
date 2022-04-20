using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MyWebAPI.Controllers;
using MyWebAPI.Models;
using MyWebAPITest.MockData;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using Newtonsoft.Json;

namespace MyWebAPITest
{
    public class EmployeeControllerTest: IDisposable
    {
        private readonly EmployeeDBContext _dbcontext;

        public EmployeeControllerTest()
        {
            var options = new DbContextOptionsBuilder<EmployeeDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbcontext = new EmployeeDBContext(options);

            _dbcontext.Database.EnsureCreated();
        }

        [Fact]
        public async void TestGetEmployee()
        {
            _dbcontext.Employees.AddRange(EmployeeMockData.GetEmployees());
            _dbcontext.SaveChanges();

            var sut = new EmployeeController(_dbcontext);

            var result = await sut.GetEmployees();
            //result.Should().HaveCount(EmployeeMockData.GetEmployees().Count);
            result.Value.Count.Should().Be(EmployeeMockData.GetEmployees().Count);
            //result.Should().NotBeNull();
        }

        [Fact]
        public async void TestPutEmployee()
        {
            _dbcontext.Employees.AddRange(EmployeeMockData.GetEmployees());
            _dbcontext.SaveChanges();

            var newEmployee = EmployeeMockData.PostEmployee();
            var sut = new EmployeeController(_dbcontext);

            await sut.PostEmployee(newEmployee);

            int expectedResult = EmployeeMockData.GetEmployees().Count + 1;
            _dbcontext.Employees.Count().Should().Be(expectedResult);
        }

        public void Dispose()
        {
            _dbcontext.Database.EnsureDeleted();
            _dbcontext.Dispose();
        }
    }
}