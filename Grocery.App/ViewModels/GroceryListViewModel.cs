using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;
using Grocery.App.Views;

namespace Grocery.App.ViewModels
{
    public partial class GroceryListViewModel : BaseViewModel
    {
        public ObservableCollection<GroceryList> GroceryLists { get; } = new();
        private readonly IGroceryListService _groceryListService;
        private readonly GlobalViewModel _global;

        public GroceryListViewModel(IGroceryListService groceryListService, GlobalViewModel global) 
        {
            Title = "Boodschappenlijst";
            _groceryListService = groceryListService;
            _global = global;
            LoadLists();
        }

        public Client? CurrentClient => _global.Client;

        [RelayCommand]
        public async Task SelectGroceryList(GroceryList groceryList)
        {
            var parameter = new Dictionary<string, object> { { nameof(GroceryList), groceryList } };
            await Shell.Current.GoToAsync($"{nameof(GroceryListItemsView)}?Titel={groceryList.Name}", true, parameter);
        }
        public override void OnAppearing()
        {
            base.OnAppearing();
            LoadLists();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            GroceryLists.Clear();
        }

        [RelayCommand]
        private async Task ShowBoughtProducts()
        {
            if (_global.Client?.Role == Role.Admin)
                await Shell.Current.GoToAsync($"{nameof(BoughtProductsView)}");
        }

        private void LoadLists()
        {
            GroceryLists.Clear();
            var lists = _groceryListService.GetAll();
            foreach (var list in lists)
            {
                GroceryLists.Add(list);
            }
        }
    }
}
