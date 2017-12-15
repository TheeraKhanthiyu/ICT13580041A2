using ICT13580041A2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ICT13580041A2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            newButton.Clicked += NewButton_Clicked;
        }

        protected override void OnAppearing()
        {
            LoadData();
        }

        void LoadData()
        {

            productListView.ItemsSource = App.DBHelper.GetProducts();
        }

         void NewButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ProductNewPage());
        }

         void Edit_Clicked(object sender, EventArgs e)
        {
            var button = sender as MenuItem;
            var product = button.CommandParameter as Product;
            Navigation.PushModalAsync(new ProductNewPage(product));
        }

        async void Delete_Clicked_1(object sender, EventArgs e)
        {
            var isOk = await DisplayAlert("ยืนยัน", "คุณต้องการลบใช่หรือไม่", "ใช่", "ไม่ใช่");

            if (isOk)
            {
                var button = sender as MenuItem;
                var product = button.CommandParameter as Product;
                App.DBHelper.DeleteProduct(product);

                DisplayAlert("ลบสำเร็จ", "ลบข้อมูลสินค้าเรียบร้อย", "ตกลง");
                LoadData();

            }
        }
    }
}