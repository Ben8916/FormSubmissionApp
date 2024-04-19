namespace FormSubmissionApp.Services
{
    public interface IFileSaveService
    {
        Task SaveToFileAsync<T>(string filePath, T data);
    }
}
