using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;
using System.IO;
namespace projectDev.Model
{
    public class ProductModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public byte[] Image { get; set; }
    }
}