using ESourcing.Products.Data.Interfaces;
using ESourcing.Products.Entities;
using ESourcing.Products.Settings;
using MongoDB.Driver;

namespace ESourcing.Products.Data
{
    public class ProductContext : IProductContext
    {
        public ProductContext(IProductDbSettings _productDbSettings)
        {
            var client = new MongoClient(_productDbSettings.ConnectionStrings);
            var database = client.GetDatabase(_productDbSettings.DatabaseName);
            var products = database.GetCollection<Product>(_productDbSettings.CollectionName);
            //ProductContextSeed.Seeddata
        }
        public IMongoCollection<Product> Products { get;}
    }
}
