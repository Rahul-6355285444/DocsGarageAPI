using docs_garage_api.Interface;
using docs_garage_api.Modal;

namespace docs_garage_api.Service
{
    public class FileValidationService : IFileValidationService
    {
        public ValidationResult ValidatePdfFiles(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Message = CommonHelper.Nofilesuploaded
                };
            }

            foreach (var file in files)
            {
                if (file.Length == 0)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        Message = CommonHelper.EmptyFileMsg(file.FileName)
                    };
                }

                if (file.Length > CommonHelper.GetPdfMaxSize())
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        Message = CommonHelper.MaxSizeExceedMsg(file.FileName)
                    };
                }

                if (file.ContentType != ContentTypes.Pdf)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        Message = CommonHelper.IsNotPdfMsg(file.FileName)
                    };
                }

                if (!Path.GetExtension(file.FileName).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        Message = CommonHelper.PdfInvExtMsg(file.FileName)
                    };
                }
            }
            return new ValidationResult
            {
                IsValid = true,
                Message = "Validation successful."
            };
        }
    }
}
