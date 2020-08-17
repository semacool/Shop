using ShopRESTAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace ShopRESTAPI.Services
{
    public interface IProductService 
    {
        List<Product> GetProducts();
        void AddProduct(Product productItem);
        void UpdateProduct(int id, Product productItem);
        void DeleteProduct(int id);
    }

    public class ProductService : IProductService 
    {
        string _tableName => "Products";
        string _connectionString = @"Data Source =(localdb)\MSSQLLocalDB;Initial Catalog=Shop;Integrated Security=True";

        public ProductService(){}
        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            string sql = $"SELECT * FROM {_tableName}";
            using(var connection = new SqlConnection(_connectionString)) 
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read()) 
                {
                    Product newProduct = new Product();
                    newProduct.ProductId = reader.GetInt32(0);
                    newProduct.Name = reader.GetString(1);
                    newProduct.Price = double.Parse(reader.GetDecimal(2).ToString());
                    newProduct.Date = reader.GetDateTime(3);

                    products.Add(newProduct);
                }
            }
            return products;
        }

        public void AddProduct(Product productItem)
        {
            string sql = @$"INSERT INTO {_tableName}(product_name,product_price) " +
                     $"VALUES('{productItem.Name}',{productItem.Price})";     
                
            using(var connection = new SqlConnection(_connectionString)) 
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateProduct(int id, Product productItem)
        {
            string sql = @$"UPDATE {_tableName} SET product_name = '{productItem.Name}', product_price = {productItem.Price},product_date = '{DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss")}' " +
                $"WHERE product_id = {id}";
             
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteProduct(int id)
        {
            string sql = $"DELETE FROM {_tableName} WHERE product_id = {id}";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
