namespace CatalogService.Helpers
{
    public interface IFileHelper
    {
        void DeleteFile(string ImageUrl);
        string UploadFile(IFormFile file);
    }
}
