using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnotherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        private readonly User _user;
        public MenuPage(User user)
        {
            _user = user;
            InitializeComponent();
        }

        private void CustomerButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new CustomersPage(_user));
        }

        private void PackingLabelButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PackingListPage(_user));
        }

        private void SettingsButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SettingsPage(_user));
        }
    }
}