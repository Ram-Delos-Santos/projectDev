﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using projectDev.Model;
using System.IO;

namespace projectDev
{
    public class SQLiteHelper
    {
        private readonly SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<ProductModel>();
        }
        public Task<int> CreateProduct(ProductModel product)
        {
            return db.InsertAsync(product);
        }
        public Task<List<ProductModel>> ReadProducts()
        {
            return db.Table<ProductModel>().ToListAsync();
        }
        public Task<int> UpdateProduct(ProductModel product)
        {
            return db.UpdateAsync(product);
        }
        public Task<int> DeleteProduct(ProductModel product)
        {
            return db.DeleteAsync(product);
        }
        public Task<List<ProductModel>> Search(string search)
        {
            return db.Table<ProductModel>().Where(p => p.Name.StartsWith(search)).ToListAsync();
        }
    }
}