using DMA_FinalProject.API;
using DMA_FinalProject.API.Controllers;
using DMA_FinalProject.API.DTO;
using DMA_FinalProject.DAL.DAO;
using DMA_FinalProject.DAL.Model;
using Microsoft.AspNetCore.Mvc;

namespace Tests
{
    /// <summary>
    /// This test class contains xUnit tests for the EmployeeController, covering various CRUD operations.
    /// </summary>
    public class EmployeeAPITests
    {
        // Create an instance of EmployeeController for testing
        EmployeeController employeeController = new EmployeeController(new EmployeeDAO());

        /// <summary>
        /// Verifies the ability of the EmployeeController to add a new employee.
        /// </summary>
        /// <remarks>
        /// This test covers the following steps:
        /// - Arrange: Create an instance of EmployeeDTO with test data.
        /// - Act: Add the test employee using the Add action.
        /// - Assert: Ensure that the action returns a non-null result, the HTTP status code is 200 OK,
        ///           and the extracted result value is true indicating successful addition.
        /// - Cleanup: Remove the test employee from the database to maintain a clean state.
        /// </remarks>
        [Fact]
        public void TestEmployeeAdd()
        {
            // Arrange
            EmployeeDTO employee = new EmployeeDTO("Bob", "88888888", "Bob@test.dk", "password123", 1);

            // Act
            ActionResult<bool> actionResult = employeeController.Add(employee);

            // Assert
            Assert.NotNull(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            bool resultValue = (bool)((OkObjectResult)actionResult.Result).Value;
            Assert.True(resultValue);

            // Cleanup
            employeeController.Delete("Bob@test.dk");
        }

        /// <summary>
        /// Verifies the ability of the EmployeeController to retrieve an employee by email.
        /// </summary>
        /// <remarks>
        /// This test covers the following steps:
        /// - Arrange: Insert a test employee into the database.
        /// - Act: Retrieve the employee by email using the Get action.
        /// - Assert: Ensure that the action returns a non-null result, and the retrieved employee's email matches the input email.
        /// - Cleanup: Remove the test employee from the database to maintain a clean state.
        /// </remarks>
        [Fact]
        public void TestEmployeeGet()
        {
            // Arrange
            // Inserting a user to get
            EmployeeDTO insertedEmployee = new EmployeeDTO("Bob", "88888888", "Bob@test.dk", "password123", 1);
            employeeController.Add(insertedEmployee);

            // Act
            ActionResult<EmployeeDTO> actionResult = employeeController.Get("Bob@test.dk");

            // Assert
            Assert.NotNull(actionResult);
            OkObjectResult okResult = (OkObjectResult)actionResult.Result;
            EmployeeDTO employee = (EmployeeDTO)okResult.Value;
            Assert.Equal("Bob@test.dk", employee.Email);

            // Cleanup
            employeeController.Delete("Bob@test.dk");
        }

        /// <summary>
        /// Verifies the ability of the EmployeeController to retrieve all employees.
        /// </summary>
        /// <remarks>
        /// This test covers the following steps:
        /// - Arrange: Insert some test data, including multiple employees, into the database.
        /// - Act: Retrieve all employees using the GetAll action.
        /// - Assert: Ensure that the action returns a non-null result, the HTTP status code is 200 OK,
        ///           and the result value is of type List<EmployeeDTO>.
        /// - Cleanup: Remove the test employees from the database to maintain a clean state.
        /// </remarks>
        [Fact]
        public void TestEmployeeGetAll()
        {
            // Arrange
            // Inserting some test data
            EmployeeDTO employee = new EmployeeDTO("Bob", "88888888", "Bob@test.dk", "password123", 1);
            employeeController.Add(employee);
            EmployeeDTO employee2 = new EmployeeDTO("Bobby", "77777777", "Bobby@test.dk", "password456", 1);
            employeeController.Add(employee2);
            EmployeeDTO employee3 = new EmployeeDTO("Bobber", "99999999", "Bobber@test.dk", "password789", 1);
            employeeController.Add(employee3);

            // Act
            ActionResult<IEnumerable<EmployeeDTO>> actionResult = employeeController.GetAll();

            // Assert
            Assert.NotNull(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            OkObjectResult result = (OkObjectResult)actionResult.Result;
            Assert.IsType<List<EmployeeDTO>>(((IEnumerable<EmployeeDTO>)result.Value).ToList());

            // Cleanup
            employeeController.Delete("Bob@test.dk");
            employeeController.Delete("Bobby@test.dk");
            employeeController.Delete("Bobber@test.dk");
        }


        /// <summary>
        /// Verifies the ability of the EmployeeController to update an existing employee.
        /// </summary>
        /// <remarks>
        /// This test covers the following steps:
        /// - Arrange: Insert a test employee into the database.
        /// - Act: Update the employee's name and phone number using the Update action.
        /// - Assert: Ensure that the action returns a non-null result, the HTTP status code is 200 OK,
        ///           and the extracted result value is true indicating successful update.
        /// - Cleanup: Remove the test employee from the database to maintain a clean state.
        /// </remarks>
        [Fact]
        public void TestEmployeeUpdate()
        {
            // Arrange
            // Insert an employee to be updated
            EmployeeDTO employee = new EmployeeDTO("Bob", "88888888", "Bob@test.dk", "password123", 1);
            employeeController.Add(employee);

            // Act
            employee.Name = "Bobster";
            employee.Phone = "12345678";
            ActionResult<bool> actionResult = employeeController.Update(employee);

            // Assert
            Assert.NotNull(actionResult);

            // Check if the HTTP status code is 200 OK
            Assert.IsType<OkObjectResult>(actionResult.Result);

            // Extract the value from OkObjectResult and check if it's true
            bool resultValue = (bool)((OkObjectResult)actionResult.Result).Value;
            Assert.True(resultValue);

            // Cleanup
            employeeController.Delete("Bob@test.dk");
        }


        /// <summary>
        /// Verifies the ability of the EmployeeController to delete an existing employee.
        /// </summary>
        /// <remarks>
        /// This test covers the following steps:
        /// - Arrange: Insert a test employee into the database.
        /// - Act: Delete the test employee using the Delete action.
        /// - Assert: Ensure that the action returns a non-null result, and the HTTP status code is 200 OK.
        /// </remarks>
        [Fact]
        public void TestEmployeeDelete()
        {
            // Arrange
            EmployeeDTO employee = new EmployeeDTO("Bob", "88888888", "Bob@test.dk", "password123", 1);
            employeeController.Add(employee);

            // Act
            ActionResult<bool> actionResult = employeeController.Delete("Bob@test.dk");

            // Assert
            Assert.NotNull(actionResult);

            // Check if the HTTP status code is 200 OK
            Assert.IsType<OkResult>(actionResult.Result);
            Assert.Equal(200, (actionResult.Result as OkResult)?.StatusCode);
        }
    }
}