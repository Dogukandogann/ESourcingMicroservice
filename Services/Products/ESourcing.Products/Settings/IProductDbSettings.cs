namespace ESourcing.Products.Settings
{
    public interface IProductDbSettings
    {
        public string ConnectionStrings { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
