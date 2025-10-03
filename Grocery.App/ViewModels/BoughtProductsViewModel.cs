using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;


namespace Grocery.App.ViewModels
{
    public partial class BoughtProductsViewModel(
            IBoughtProductsService boughtProductsService,
            IProductService productService
        ) : BaseViewModel
    {
        private readonly IBoughtProductsService _boughtProductsService = boughtProductsService;

        [ObservableProperty]
        Product? selectedProduct;
        public ObservableCollection<BoughtProducts> BoughtProductsList { get; set; } = [];
        public ObservableCollection<Product> Products { get; set; } = new(productService.GetAll());

        partial void OnSelectedProductChanged(Product? oldValue, Product? newValue)
        {
            BoughtProductsList.Clear();
            if (newValue == null) return;

            foreach (var row in _boughtProductsService.Get(newValue.Id))
                BoughtProductsList.Add(row);
        }

        [RelayCommand]
        public void NewSelectedProduct(Product product)
        {
            SelectedProduct = product;
        }
    }
}
