using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnotherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomersPage : ContentPage
    {
        private User _user;
        private List<string> _customerList = new List<string>();
        List<DatabaseClasses.Customer> _otherlist;
        public CustomersPage(User user)
        {
                _user = user;
                InitializeComponent();

                PopulateList();
        }

        public async void PopulateList()
        {
            await GetCustomers();
        }

        public async Task<List<DatabaseClasses.Customer>> GetCustomers()
        {
            try
            {
                var url = "http://192.168.1.61:8081/customer";

                var response = await _user.Client.GetAsync(url + ".json");
                if (!response.IsSuccessStatusCode) return null;
                var content = response.Content.ReadAsStringAsync().Result;
                _otherlist = JsonConvert.DeserializeObject<List<DatabaseClasses.Customer>>(content);
                foreach (var c in _otherlist)
                {
                    var cus = c.Code + ": \n" + c.Name;
                    _customerList.Add(cus);
                }
                ListView.ItemsSource = _customerList;
                Content = StackLayout;
                return null;
            }
            catch (Exception ex)
            {
                EditorWarning.Text = ex.ToString();
                EditorWarning.IsVisible = true;
                Content = StackLayout;
                return null;
            }
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                string[] esplit = e.Item.ToString().Split(':');
                DatabaseClasses.Customer item = _otherlist.FirstOrDefault(i => i.Code.ToString() == esplit[0]);
                if (item == null)
                {
                    EditorWarning.Text = "Customer could not be found.";
                    return;
                }
                Navigation.PushModalAsync(new DisplayItemPage(item));
            }
            catch (Exception ex)
            {
                EditorWarning.Text = ex.ToString();
            }
        }
    }
}