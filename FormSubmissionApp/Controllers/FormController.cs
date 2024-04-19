using FormSubmissionApp.Models;
using FormSubmissionApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FormSubmissionApp.Controllers
{
    public class FormController : Controller
    {
        private const string FileName = "formdata.json";
        private readonly IFileSaveService _fileSaveService;

        public FormController(IFileSaveService fileSaveService)
        {
            _fileSaveService = fileSaveService;
        }

        [HttpPost]
        public async Task<IActionResult> Submit(FormData formData)
        {
            // Read existing data from the file
            List<FormData> existingFormData = ReadFormDataFromFile();

            existingFormData.Add(formData);

            // Save the form data asynchronously
            await _fileSaveService.SaveToFileAsync(FileName, existingFormData);

            return RedirectToAction("Index", "Home");
        }

        private List<FormData> ReadFormDataFromFile()
        {
            List<FormData> existingFormData = new List<FormData>();

            // Check if the file exists
            if (System.IO.File.Exists(FileName))
            {
                string existingJsonData = System.IO.File.ReadAllText(FileName);

                if (!string.IsNullOrEmpty(existingJsonData))
                {
                    // Deserialize the JSON data into a list of FormData objects
                    existingFormData = JsonSerializer.Deserialize<List<FormData>>(existingJsonData);
                }
            }

            return existingFormData;
        }
    }
}
