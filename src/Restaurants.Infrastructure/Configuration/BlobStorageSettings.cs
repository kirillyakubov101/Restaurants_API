namespace Restaurants.Infrastructure.Configuration
{
    public class BlobStorageSettings
    {
        public string ConnectionStrings { get; set; }
        public string LogosContainerName { get; set; }
        public string AccountKey { get; set; }
    }
}
