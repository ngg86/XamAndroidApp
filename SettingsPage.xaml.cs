using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnotherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private User _user;
        private SwitchCell _sc;
        private EntryCell _ec;
        public SettingsPage(User user)
        {
            _user = user;
            InitializeComponent();

            DisplaySettings();
            Content = StackLayout;
        }

        private void DisplaySettings()
        {
            _sc = new SwitchCell() { Text = "Remember user?", On = _user.Save };
            _ec = new EntryCell() { Label = "Connect to:", Text = _user.Url };

            HeaderLabel.Text = _user.Name + " Settings";
            Section.Add(_sc);
            Section.Add(_ec);
        }


        private void Submit_OnClicked(object sender, EventArgs e)
        {
            bool sw = _sc.On;
            string url = _ec.Text;

            if (string.IsNullOrEmpty(url))
            {
                DisplayAlert("Warning", "Url must not be empty.", "OK");
                return;
            }
            if (sw == false)
            {
                LoginActivity.DeleteUser();
                DisplayAlert("Success", "Login settings removed.", "OK");
                return;
            }
            LoginActivity.SaveUser(_user);
            DisplayAlert("Success", "Login settings saved.", "OK");
        }
    }
}