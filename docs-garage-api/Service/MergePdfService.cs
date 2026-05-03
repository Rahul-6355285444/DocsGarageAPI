using docs_garage_api.Interface;
using docs_garage_api.Modal;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace docs_garage_api.Service
{
    public class MergePdfService : IMergePdfService
    {
        public async Task<FileResponse> MergeAsync(List<IFormFile> files)
        {
            try
            {
                if (files == null || files.Count == 0)
                    return new FileResponse { IsSuccess = false };

                var outputdocument = new PdfDocument();

                foreach (var file in files)
                {
                    using var stream = file.OpenReadStream();

                    var inputDocument = PdfReader.Open(stream, PdfDocumentOpenMode.Import);

                    for (int i = 0; i < inputDocument.PageCount; i++)
                    {
                        outputdocument.AddPage(inputDocument.Pages[i]);
                    }
                }

                using var memoryStream = new MemoryStream();
                outputdocument.Save(memoryStream, false);

                byte[] bytes = memoryStream.ToArray();

                if (bytes != null && bytes.Length > 0)
                    return new FileResponse { IsSuccess = true, Base64String = Convert.ToBase64String(bytes), FileName = CommonHelper.GetFileName(FileExtensions.Pdf) };
            }
            catch //(Exception ex)
            {
            }
            return new FileResponse { IsSuccess = false };
        }
    }
}
