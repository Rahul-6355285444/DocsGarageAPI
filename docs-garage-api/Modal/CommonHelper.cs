namespace docs_garage_api.Modal
{
    public static class CommonHelper
    {
        public const string Nofilesuploaded = "No files uploaded.";
        public const string Success = "Success";
        public const string Failed = "Failed";
        public const int PdfMaxSize = 10;

        public static string EmptyFileMsg(string fileName) => $"{fileName} is empty.";
        public static int GetPdfMaxSize() => PdfMaxSize * 1024 * 1024;
        public static string MaxSizeExceedMsg(string fileName) => $"{fileName} exceeds max size.";
        public static string IsNotPdfMsg(string fileName) => $"{fileName} is not PDF.";
        public static string PdfInvExtMsg(string fileName) => $"{fileName} has invalid extension.";
        public static string GetFileName(string extension) =>
    $"docsgarage_{DateTime.Now:yyyyMMdd_HHmmss}_merge_pdf{extension}";


    }
}
