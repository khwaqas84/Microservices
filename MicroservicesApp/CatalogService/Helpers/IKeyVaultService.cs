namespace CatalogService.Helpers
{
    public interface IKeyVaultService
    {
        Task<string> GetSecret(string key);
        Task<string> SetSecret(string secreatName,string secreatValue);

    }
}
