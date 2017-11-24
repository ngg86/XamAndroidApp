using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace AnotherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DisplayItemPage : ContentPage
    {
        public DisplayItemPage(DatabaseClasses.PackingList packinglist)
        {
            InitializeComponent();

            FillTable(packinglist);
        }

        public DisplayItemPage(DatabaseClasses.Customer customer)
        {
            InitializeComponent();

            FillTable(customer);
        }

        private void FillTable(DatabaseClasses.PackingList packinglist)
        {
            try
            {
                foreach (var property in packinglist.GetType().GetProperties())
                {
                    TextCell tc = new TextCell() { TextColor = Color.WhiteSmoke, DetailColor = Color.Peru, };
                    var id = property.Name;
                    var val = property.GetValue(packinglist);

                    tc.Text = id;
                    tc.Detail = val.ToString();
                    Section.Add(tc);
                }
                HeaderLabel.Text = "Packing Number: " + packinglist.PackingNo;
                Content = StackLayout;
            }
            catch (Exception ex)
            {
                HeaderLabel.Text = ex.ToString();
            }
        }

        private void FillTable(DatabaseClasses.Customer customer)
        {
            try
            {
                foreach (var property in customer.GetType().GetProperties())
                {
                    TextCell tc = new TextCell() { TextColor = Color.WhiteSmoke, DetailColor = Color.Peru, };
                    var id = property.Name;
                    var val = property.GetValue(customer);

                    tc.Text = id;
                    tc.Detail = val.ToString();
                    Section.Add(tc);
                }
                HeaderLabel.Text = "Customer ID: " + customer.Code;
                Content = StackLayout;
            }
            catch (Exception ex)
            {
                HeaderLabel.Text = ex.ToString();
            }
        }
    }
}