using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

// ReSharper disable All

namespace AnotherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PackingListPage : ContentPage
    {
        private User _user;
        private List<string> _plList = new List<string>();
        private List<DatabaseClasses.PackingList> _otherlist;

        public PackingListPage(User user)
        {
            _user = user;
            InitializeComponent();

            PopulateList();
        }

        public async void PopulateList()
        {
            await GetPackingLists();
        }

        public async Task<List<DatabaseClasses.PackingList>> GetPackingLists()
        {
            try
            {
                var url = "http://192.168.1.61:8081/packinglist";

                var response = await _user.Client.GetAsync(url + ".json");
                if (!response.IsSuccessStatusCode) return null;

                var content = response.Content.ReadAsStringAsync().Result;
                _otherlist = JsonConvert.DeserializeObject<List<DatabaseClasses.PackingList>>(content);

                foreach (var p in _otherlist)
                {
                    var pk = p.PackingNo + ": \n" + p.Status;
                    _plList.Add(pk);
                }

                ListView.ItemsSource = _plList;
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
                DatabaseClasses.PackingList item = _otherlist.FirstOrDefault(i => i.PackingNo.ToString() == esplit[0]);
                if (item == null)
                {
                    EditorWarning.Text = "Packinglist could not be found.";
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