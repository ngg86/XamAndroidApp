using System;
using Xamarin.Forms;

namespace AnotherApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var user = LoginActivity.LoadUser();
            if (user.Name == null || user.Pass == null) return;
            LoginActivity.AttemptLogin(user);
            GoToMenu(user);
        }

        private void GoToMenu(User user)
        {
            try
            {
                Navigation.PushModalAsync(new MenuPage(user));
            }
            catch (Exception ex)
            {
                DisplayAlert("Exception", ex.ToString(), "Uh-oh!");
            }
        }

        private void ButtonLogin_OnClicked(object sender, EventArgs e)
        {
            User user;
            try
            {
                user = new User(EntryUsername.Text, EntryPassword.Text) {Save = RememberSwitch.On};
                user = LoginActivity.AttemptLogin(user);
            }
            catch (Exception ex)
            {
                DisplayAlert("Exception", ex.ToString(), "Uh-oh!");
                return;
            }
            if (RememberSwitch.On)
            {
                LoginActivity.SaveUser(user);
            }
            GoToMenu(user);
        }
    }
}
