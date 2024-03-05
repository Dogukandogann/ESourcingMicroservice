namespace ESourcing.Products.Settings
{
    public class ProductDbSettings : IProductDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
