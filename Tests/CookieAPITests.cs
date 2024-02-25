using DMA_FinalProject.API.Controllers;
using DMA_FinalProject.API.DTO;
using DMA_FinalProject.DAL.DAO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    /// <summary>
    /// This test method verifies the functionality of the CookieController by testing the Add and Get actions.
    /// </summary>
    public class CookieAPITests
    {
        /// <summary>
        /// Verifies the ability of the CookieController to add a new cookie and retrieve cookies by domain.
        /// </summary>
        /// <remarks>
        /// This test covers the following steps:
        /// - Arrange: Create instances of CookieDAO and CookieController, and prepare a test cookie.
        /// - Act: Add the test cookie using the Add action and retrieve cookies for a specific domain using the Get action.
        /// - Assert: Ensure that the actions return non-null results, the added cookie is present in the retrieved list,
        ///           and the Add action returns a true value indicating successful addition.
        /// - Cleanup: Remove the test cookie from the database to maintain a clean state.
        /// </remarks>
        [Fact]
        public void TestCookieControllerCreateRead()
        {
            // Arrange
            CookieDAO cookieDAO = new CookieDAO();
            CookieController cookieController = new CookieController(cookieDAO);
            CookieDTO testCookie = new CookieDTO("TestCookie", "TestValue", new DateTime(1999, 1, 1), "www.company1.com", "TestCategory");

            // Act
            ActionResult<bool> actionResultAdd = cookieController.Add(testCookie);
            ActionResult<IEnumerable<CookieDTO>> actionResultGet = cookieController.Get("www.company1.com");

            bool actionResultAddValue = (bool)((OkObjectResult)actionResultAdd.Result).Value;
            OkObjectResult okObjectResult = Assert.IsType<OkObjectResult>(actionResultGet.Result);
            IEnumerable<CookieDTO> cookiesList = (IEnumerable<CookieDTO>)okObjectResult.Value;

            // Assert
            Assert.NotNull(actionResultAdd);
            Assert.NotNull(actionResultGet);
            Assert.NotNull(cookiesList);
            Assert.Contains(cookiesList, cookie => cookie.Name == "TestCookie");
            Assert.True(actionResultAddValue);

            // Cleanup
            cookieDAO.Remove("TestCookie");
        }
    }

}
