using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using MyWebAPI.Repositories.Interfaces;
using MyWebAPI.Controllers;
using MyWebAPI.Helpers;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MyWebAPITest
{
    public class AuthControllerTest
    {
        [Fact]
        public async Task TestLogin_ShouldReturnOk()
        {
            var userRepository = new Mock<IUserRepository>();
            var jwtService = new Mock<JwtService>();
            var passwordService = new Mock<PasswordService>();
            var mapper = new Mock<IMapper>();

            var sut = new AuthController(userRepository.Object, jwtService.Object, passwordService.Object, mapper.Object);

            var result = await sut.Login("ian.bacolod","password");

            result.Should().Be(typeof(OkObjectResult));
            (result as OkObjectResult).StatusCode.Should().Be(200);
        }
    }
}
