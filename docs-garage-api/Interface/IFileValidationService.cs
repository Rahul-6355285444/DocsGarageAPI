using docs_garage_api.Modal;

namespace docs_garage_api.Interface
{
    public interface IFileValidationService
    {
        ValidationResult ValidatePdfFiles(List<IFormFile> files);
    }
}
