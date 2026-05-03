namespace docs_garage_api.Modal
{
    public class FileResponse
    {
        public string Base64String { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
    }
}
