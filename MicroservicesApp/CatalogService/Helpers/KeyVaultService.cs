
using Azure.Security.KeyVault.Secrets;

namespace CatalogService.Helpers
{
    public class KeyVaultService : IKeyVaultService
    {
        private readonly SecretClient _secretClient;

        public KeyVaultService(SecretClient secretClient)
        {
            _secretClient = secretClient;
        }

        public async Task<string> GetSecret(string key)
        {
            try
            {
                KeyVaultSecret secret = await  _secretClient.GetSecretAsync(key);
                return secret.Value;
            }
            catch (Exception)
            {

                throw;
            }    
        }

        public async Task<string> SetSecret(string secreatName, string secreatValue)
        {
            var secret= await _secretClient.SetSecretAsync(secreatName, secreatValue);
            return secret.Value.Value;
        }
    }
}
