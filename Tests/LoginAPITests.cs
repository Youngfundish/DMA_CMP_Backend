using DMA_FinalProject.API.Controllers;
using DMA_FinalProject.API.DTO;
using DMA_FinalProject.DAL.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    /// <summary>
    /// This test class contains xUnit tests for the LoginController, specifically focusing on the login functionality.
    /// </summary>
    [CollectionDefinition("SequentialTests", DisableParallelization = true)]

    [Collection("SequentialTests")]
    public class LoginAPITests
    {
        // Configuration instance for test setup
        private IConfiguration configuration;

        // LoginController instance for testing
        private LoginController loginController;

        // EmployeeController instance for additional setup
        private EmployeeController employeeController = new EmployeeController(new EmployeeDAO());

        /// <summary>
        /// Initializes a new instance of the LoginAPITests class.
        /// </summary>
        public LoginAPITests()
        {
            InitializeConfiguration();
            InitializeLoginController();
        }

        /// <summary>
        /// Initializes the configuration for the test class.
        /// </summary>
        private void InitializeConfiguration()
        {
            configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
        }

        /// <summary>
        /// Initializes the LoginController instance for testing.
        /// </summary>
        private void InitializeLoginController()
        {
            LoginDAO loginDAO = new LoginDAO();
            loginController = new LoginController(loginDAO, configuration);
        }

        /// <summary>
        /// Verifies the ability of the LoginController to handle the login process.
        /// </summary>
        /// <remarks>
        /// This test covers the following steps:
        /// - Arrange: Insert a test employee into the database and create a LoginDTO with login credentials.
        /// - Act: Call the Login method of LoginController with the LoginDTO.
        /// - Assert: Ensure that the login action returns a non-null result, and the returned employee's email matches the input email.
        ///           Additionally, check the token validity using the CheckToken method.
        /// - Cleanup: Remove the test employee from the database to maintain a clean state.
        /// </remarks>
        [Fact]
        public void TestLoginControllerLoginMethod()
        {
            // Arrange
            EmployeeDTO insertedEmployee = new EmployeeDTO("Bob", "88888888", "Bob@test.dk", "test123", 1);
            employeeController.Add(insertedEmployee);
            LoginDTO loginDTO = new LoginDTO("Bob@test.dk", "test123");

            // Act
            ActionResult<EmployeeDTO> actionResult = loginController.Login(loginDTO);

            // Assert
            Assert.NotNull(actionResult);
            OkObjectResult okResult = (OkObjectResult)actionResult.Result;
            EmployeeDTO employee = (EmployeeDTO)okResult.Value;
            Assert.Equal("Bob@test.dk", employee.Email);

            OkObjectResult result = loginController.CheckToken(employee.Token) as OkObjectResult;
            Assert.NotNull(result);
            Assert.Equal(true, result.Value);

            // Cleanup
            employeeController.Delete("Bob@test.dk");
        }
    }

}
