using docs_garage_api.Modal;

namespace docs_garage_api.Interface
{
    public interface IMergePdfService
    {
        Task<FileResponse> MergeAsync(List<IFormFile> files);
    }
}
