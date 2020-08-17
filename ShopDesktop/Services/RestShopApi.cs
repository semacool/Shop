using ShopDesktop.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace ShopDesktop.Services
{
    class ShopApi
    {
        HttpClient _client;

        public RestShopApi() 
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            _client = new HttpClient(clientHandler);
            _client.BaseAddress = new Uri("http://localhost:5000");
        }

        public async Task<List<Product>> GetProducts() 
        {

            List<Product> products = new List<Product>();
            HttpResponseMessage responce = await _client.GetAsync("/api/products");
            string json = await responce.Content.ReadAsStringAsync();

            JArray Jproducts = JArray.Parse(json);

            foreach(JObject Jprod in Jproducts) 
            {
                products.Add(ParseFromObjToProd(Jprod));
            }
            return products;
        }

        public async Task<string> AddProduct(Product product) 
        {
            JObject jObject = new JObject();
            jObject["name"] = product.Name;
            jObject["price"] = product.Price;

            var content = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json");

            var answer = await _client.PostAsync("/api/products",content);
            return answer.StatusCode.ToString();
        }

        public async Task<string> UpdateProduct(Product product)
        {
            JObject jObject = new JObject();
            jObject["name"] = product.Name;
            jObject["price"] = product.Price;

            var content = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json");

            var answer = await _client.PutAsync($"/api/products/{product.Id}", content);
            return answer.StatusCode.ToString();
        }

        public async Task<string> DeleteProduct(Product product)
        {
            var answer = await _client.DeleteAsync($"/api/products/{product.Id}");
            return answer.StatusCode.ToString();
        }

        private Product ParseFromObjToProd(JObject Jprod) 
        {
            Product prod = new Product();
            prod.Id = int.Parse(Jprod["productId"].ToString());
            prod.Name = Jprod["name"].ToString();
            prod.Price = double.Parse(Jprod["price"].ToString());
            prod.DateUpdate = DateTime.Parse(Jprod["date"].ToString());
            return prod;
        }
    }
}
