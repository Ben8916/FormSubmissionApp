using FormSubmissionApp.Controllers;
using FormSubmissionApp.Models;
using FormSubmissionApp.Services;
using Moq;

namespace FormTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public async Task SubmitForm_WithValidData_SuccessfullyProcesses()
        {
            // Arrange
            var formData = new FormData
            {
                FirstName = "John",
                LastName = "Doe"
            };

            var mockFileSaveService = new Mock<IFileSaveService>();
            mockFileSaveService.Setup(service => service.SaveToFileAsync(It.IsAny<string>(), formData))
                               .Returns(Task.CompletedTask);

            var formController = new FormController(mockFileSaveService.Object);

            // Act
            var result = formController.Submit(formData);

            // Assert
            Assert.IsNotNull(result); 
        }

        [Test]
        public async Task SubmitForm_WithInvalidData_FailsValidation()
        {
            // Arrange
            var formData = new FormData // Invalid data with empty first name
            {
                FirstName = "",
                LastName = "Doe"
            };

            var mockFileSaveService = new Mock<IFileSaveService>();
            var formController = new FormController(mockFileSaveService.Object);

            // Act
            var result = await formController.Submit(formData);

            // Assert
            mockFileSaveService.Verify(service => service.SaveToFileAsync("formdata.json", formData), Times.Never);
        }

    }
}