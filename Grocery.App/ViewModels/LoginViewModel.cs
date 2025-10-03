
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.App.ViewModels
{
    public partial class LoginViewModel(IAuthService authService, GlobalViewModel global) : BaseViewModel
    {
        private readonly IAuthService _authService = authService;
        private readonly GlobalViewModel _global = global;

        [ObservableProperty]
        private string email = "user3@mail.com";

        [ObservableProperty]
        private string password = "user3";

        [ObservableProperty]
        private string? loginMessage;

        [RelayCommand]
        private void Login()
        {
            Client? authenticatedClient = _authService.Login(Email, Password);
            if (authenticatedClient != null)
            {
                LoginMessage = $"Welkom {authenticatedClient.Name}!";
                _global.Client = authenticatedClient;
                if (Application.Current != null)
                {
                    Application.Current.MainPage = new AppShell();
                }
                else
                {
                    // Optionally handle the case where Application.Current is null
                    // For example, log an error or throw an exception
                }
            }
            else
            {
                LoginMessage = "Ongeldige inloggegevens.";
            }
        }
    }
}
