using docs_garage_api.Interface;
using docs_garage_api.Modal;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace docs_garage_api.Service
{
    public class MergePdfService : IMergePdfService
    {
        public async Task<FileResponse> MergeAsync(List<IFormFile> files, List<int> rotations)
        {
            try
            {
                if (files == null || files.Count == 0)
                    return new FileResponse { IsSuccess = false };

                var outputdocument = new PdfDocument();

                //foreach (var file in files)
                //{
                //    using var stream = file.OpenReadStream();

                //    var inputDocument = PdfReader.Open(stream, PdfDocumentOpenMode.Import);

                //    for (int i = 0; i < inputDocument.PageCount; i++)
                //    {
                //        outputdocument.AddPage(inputDocument.Pages[i]);
                //    }
                //}

                for (int f = 0; f < files.Count; f++)
                {
                    var file = files[f];

                    int rotation = (rotations != null && rotations.Count > 0) ? rotations[f] : 0;

                    using var stream = file.OpenReadStream();
                    var inputDocument = PdfReader.Open(stream, PdfDocumentOpenMode.Import);

                    for (int i = 0; i < inputDocument.PageCount; i++)
                    {
                        var page = outputdocument.AddPage(inputDocument.Pages[i]);

                        if (rotation != 0)
                            page.Rotate = (page.Rotate + rotation) % 360;
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
