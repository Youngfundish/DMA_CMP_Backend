using DMA_FinalProject.API.Controllers;
using DMA_FinalProject.API.DTO;
using DMA_FinalProject.DAL.DAO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    /// <summary>
    /// This test class contains xUnit tests for the DomainController, covering various CRUD operations
    /// </summary>
    [CollectionDefinition("SequentialTests", DisableParallelization = true)]

    [Collection("SequentialTests")]
    public class DomainAPITests
    {
        DomainController domainController = new DomainController(new DomainDAO());


        /// <summary>
        /// Verifies the ability of the DomainController to add a domain to the system.
        /// </summary>
        /// <remarks>
        /// This test covers the following steps:
        /// - Arrange: Create a DomainDTO representing a new domain.
        /// - Act: Call the Add method of DomainController with the DomainDTO.
        /// - Assert: Ensure that the action result is not null, is of type OkObjectResult, and the result value is true.
        /// - Cleanup: Remove the added domain from the system to maintain a clean state.
        /// </remarks>
        [Fact]
        public void TestDomainAdd()
        {
            // Arrange
            // Creating a DomainDTO representing a new domain
            DomainDTO domain = new DomainDTO("www.test1.dk", "Test 1", 1);

            // Act
            // Calling the Add method of DomainController with the DomainDTO
            ActionResult<bool> actionResult = domainController.Add(domain);

            // Assert
            // Verifying that the action result is not null
            Assert.NotNull(actionResult);

            // Verifying that the action result is of type OkObjectResult
            Assert.IsType<OkObjectResult>(actionResult.Result);

            // Extracting the boolean result value from the OkObjectResult
            bool resultValue = (bool)((OkObjectResult)actionResult.Result).Value;

            // Verifying that the result value is true
            Assert.True(resultValue);

            // Cleanup
            // Removing the added domain from the system
            domainController.Delete("www.test1.dk");
        }


        /// <summary>
        /// Verifies the ability of the DomainController to retrieve a list of domains based on a specified category.
        /// </summary>
        /// <remarks>
        /// This test covers the following steps:
        /// - Arrange: Insert a test domain into the system.
        /// - Act: Call the Get method of DomainController with a specified category.
        /// - Assert: Ensure that the action result is not null, is of type OkObjectResult, and the returned domain list contains the expected domain.
        /// - Cleanup: Remove the test domain from the system to maintain a clean state.
        /// </remarks>
        [Fact]
        public void TestDomainGet()
        {
            // Arrange
            // Inserting a test domain into the system
            DomainDTO insertedDomain = new DomainDTO("www.test1.dk", "Test 1", 1);
            domainController.Add(insertedDomain);

            // Act
            // Calling the Get method of DomainController with a specified category
            ActionResult<IEnumerable<DomainDTO>> actionResult = domainController.Get(1);

            // Extracting values from the OkObjectResult
            OkObjectResult okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            IEnumerable<DomainDTO> domainList = (IEnumerable<DomainDTO>)okObjectResult.Value;

            // Assert
            // Verifying that the action result is not null
            Assert.NotNull(actionResult);

            // Verifying that the returned domain list is not null
            Assert.NotNull(domainList);

            // Verifying that the returned domain list contains the expected domain
            Assert.Contains(domainList, domain => domain.Url == "www.test1.dk");

            // Cleanup
            // Removing the test domain from the system
            domainController.Delete("www.test1.dk");
        }

        /// <summary>
        /// Verifies the ability of the DomainController to update information for a specific domain.
        /// </summary>
        /// <remarks>
        /// This test covers the following steps:
        /// - Arrange: Insert a test domain into the system.
        /// - Act: Update the name of the domain and call the Update method of DomainController with the modified DomainDTO.
        /// - Assert: Ensure that the action result is not null, is of type OkObjectResult, and the returned value is true.
        /// - Cleanup: Remove the test domain from the system to maintain a clean state.
        /// </remarks>
        [Fact]
        public void TestDomainUpdate()
        {
            // Arrange
            // Inserting a test domain into the system
            DomainDTO domain = new DomainDTO("www.test1.dk", "Test 1", 1);
            domainController.Add(domain);

            // Act
            // Updating the name of the domain and calling the Update method of DomainController
            domain.Name = "Testing 1";
            ActionResult<bool> actionResult = domainController.Update(domain);

            // Assert
            // Verifying that the action result is not null
            Assert.NotNull(actionResult);

            // Verifying that the action result is of type OkObjectResult
            Assert.IsType<OkObjectResult>(actionResult.Result);

            // Extracting the boolean result value from the OkObjectResult
            bool resultValue = (bool)((OkObjectResult)actionResult.Result).Value;

            // Verifying that the result value is true
            Assert.True(resultValue);

            // Cleanup
            // Removing the test domain from the system
            domainController.Delete("www.test1.dk");
        }

        /// <summary>
        /// Verifies the ability of the DomainController to delete a domain from the system.
        /// </summary>
        /// <remarks>
        /// This test covers the following steps:
        /// - Arrange: Insert a test domain into the system.
        /// - Act: Call the Delete method of DomainController with the domain's URL.
        /// - Assert: Ensure that the action result is not null, is of type OkResult, and the HTTP status code is 200 OK.
        /// </remarks>
        [Fact]
        public void TestEmployeeDelete()
        {
            // Arrange
            // Inserting a test domain into the system
            DomainDTO domain = new DomainDTO("www.test1.dk", "Test 1", 1);
            domainController.Add(domain);

            // Act
            // Calling the Delete method of DomainController with the domain's URL
            ActionResult<bool> actionResult = domainController.Delete("www.test1.dk");

            // Assert
            // Verifying that the action result is not null
            Assert.NotNull(actionResult);

            // Verifying that the action result is of type OkResult
            Assert.IsType<OkResult>(actionResult.Result);

            // Verifying that the HTTP status code is 200 OK
            Assert.Equal(200, (actionResult.Result as OkResult)?.StatusCode);
        }
    }
}
