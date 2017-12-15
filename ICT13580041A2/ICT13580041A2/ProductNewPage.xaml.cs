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
    public partial class ProductNewPage : ContentPage
    {
        Product product;
        public ProductNewPage(Product product=null)
        {

            InitializeComponent();

            this.product = product;
            titleLabel.Text = product == null ? "เพิ่มสินค้าใหม่" : "แก้ไขข้อมูลสินค้า";

            saveButton.Clicked += SaveButton_Clicked;
            cancelButton.Clicked += CancelButton_Clicked;

            categoryPicker.Items.Add("เสื้อ");
            categoryPicker.Items.Add("กางเกง");
            categoryPicker.Items.Add("ถุงเท้า");

            if (product != null)
            {
                nameEntry.Text = product.Name;
                descriptionEditor.Text = product.Description;
                categoryPicker.SelectedItem = product.Category;
                productPriceEntry.Text = product.ProductPrice.ToString();
                sellPriceEntry.Text = product.SellPrice.ToString();
                stockEntry.Text = product.Stock.ToString();

            }


        }

        

        async   void SaveButton_Clicked(object sender, EventArgs e)
        {
            var isOk = await DisplayAlert("ยืนยัน", "คุณต้องการบันทึกใช่หรือไม่", "ใช่", "ไม่ใช่");
            if (isOk)
            {
                
                if (product == null)
                {
                product = new Product();
                product.Name = nameEntry.Text;
                product.Description = descriptionEditor.Text;
                product.Category = categoryPicker.SelectedItem.ToString();
                product.ProductPrice = decimal.Parse(productPriceEntry.Text);
                product.SellPrice = decimal.Parse(sellPriceEntry.Text);
                product.Stock = int.Parse(stockEntry.Text);
                var id = App.DBHelper.AddProduct(product);
                await DisplayAlert("บันทึกสำเร็จ", "รหัสสินค้าของท่านคือ #" + id , "ตกลง");
                }
                else
                {
                product.Name = nameEntry.Text;
                product.Description = descriptionEditor.Text;
                product.Category = categoryPicker.SelectedItem.ToString();
                product.ProductPrice = decimal.Parse(productPriceEntry.Text);
                product.SellPrice = decimal.Parse(sellPriceEntry.Text);
                product.Stock = int.Parse(stockEntry.Text);
                var id = App.DBHelper.UpdateProduct(product);
                await DisplayAlert("บันทึกสำเร็จ", "แก้ไขข้อมูลสินค้าเรียบร้อย" , "ตกลง");

                }
              await Navigation.PopModalAsync();

            }
        }
        async    void CancelButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}