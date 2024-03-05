using ESourcing.Products.Data.Interfaces;
using ESourcing.Products.Entities;
using ESourcing.Products.Settings;
using MongoDB.Driver;
using System;

namespace ESourcing.Products.Data
{
    public class ProductContext : IProductContext
    {
        public ProductContext(IProductDbSettings _productDbSettings)
        {
            var client = new MongoClient(_productDbSettings.ConnectionString);
            var database = client.GetDatabase(_productDbSettings.DatabaseName);

            Products = database.GetCollection<Product>(_productDbSettings.CollectionName);
            ProductContextSeed.SeedData(Products);
            
        }
        
        public IMongoCollection<Product> Products { get;}
    }
}
