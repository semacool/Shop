using ShopDesktop.Models;
using ShopDesktop.Services;
using ShopDesktop.ViewModels.HelpMVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopDesktop.ViewModels
{
    class MainViewModel : ViewModel
    {

        private ShopApi _api;
        private List<Product> products;
        private Product selectedProduct;
        private string name;
        private double price;

        public List<Product> Products
        {
            get => products;
            set
            {
                products = value;
                OnChanged(nameof(Products));
            }
        }
        public Product SelectedProduct
        {
            get => selectedProduct;
            set
            {
                selectedProduct = value == null ? Products.Last() : value;
                Name = selectedProduct.Name;
                Price = selectedProduct.Price;

                OnChanged(nameof(SelectedProduct));
            }
        }


        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnChanged(nameof(Name));
            }
        }
        public double Price
        {
            get => price;
            set
            {
                price = value;
                OnChanged(nameof(Price));
            }
        }


        public DelegateCommand SaveCommand => new DelegateCommand(SaveProduct, CanSaveProduct);
        public DelegateCommand AddCommand => new DelegateCommand(AddProduct, CanAddProduct);
        public DelegateCommand DeleteCommand => new DelegateCommand(DeleteProduct, CanDeleteProduct);


        public MainViewModel()
        {
            _api = new ShopApi();
            Task.Run(InitProducts);
        }

        async void InitProducts()
        {
            try
            {
                Products = await _api.GetProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка");
            }
        }

        private async void SaveProduct(object param)
        {
            Product product = SelectedProduct;
            product.Name = Name;
            product.Price = Price;
            var answer = await _api.UpdateProduct(product);
            if (answer == "OK")
                InitProducts();
        }
        private bool CanSaveProduct(object param)
        {
            if (string.IsNullOrWhiteSpace(Name)) return false;
            if (Price < 0) return false;
            return true;

        }
        private async void AddProduct(object param)
        {
            try
            {
                Product product = new Product()
                {
                    Name = "Новый товар",
                    Price = 0
                };
                var answer = await _api.AddProduct(product);
                if (answer == "OK")
                    InitProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка");
            }
        }
        private bool CanAddProduct(object param)
        {
            return true;
        }

        private async void DeleteProduct(object param)
        {
            try
            {
                var answer = await _api.DeleteProduct(SelectedProduct);
                if (answer == "OK")
                    InitProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка");
            }
        }
        private bool CanDeleteProduct(object param)
        {
            if (SelectedProduct == null) return false;
            return true;
        }


    }
}
