using Coblis_TudorCristian_Lab7.Models;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using System;
using System.Collections.Generic;

namespace Coblis_TudorCristian_Lab7
{
    public partial class ListPage : ContentPage
    {
        private ShopList sl;

        public ListPage()
        {
            InitializeComponent();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var slist = (ShopList)BindingContext;
            slist.Date = DateTime.UtcNow;
            Shop selectedShop = (ShopPicker.SelectedItem as Shop);
            slist.ShopID = selectedShop.ID;
            await App.Database.SaveShopListAsync(slist);
            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var slist = (ShopList)BindingContext;
            await App.Database.DeleteShopListAsync(slist);
            await Navigation.PopAsync();
        }

        async void OnChooseButtonClicked(object sender, EventArgs e)
        {
            sl = (ShopList)this.BindingContext; // Assign 'sl' here
            await Navigation.PushAsync(new ProductPage(sl)
            {
                BindingContext = new Product()
            });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var items = await App.Database.GetShopsAsync();
            ShopPicker.ItemsSource = (System.Collections.IList)items;
            ShopPicker.ItemDisplayBinding = new Binding("ShopDetails");

            var shopl = (ShopList)BindingContext;
            listView.ItemsSource = await
           App.Database.GetListProductsAsync(shopl.ID);
        }

        async void OnDeleteProductButtonClicked(object sender, EventArgs e)
        {
            var product = (Product)listView.SelectedItem;

            if (product != null)
            {
                var listProduct = await App.Database.GetListProductAsync(sl.ID, product.ID);

                if (listProduct != null)
                {
                    await App.Database.DeleteListProductAsync(listProduct);
                    listView.ItemsSource = await App.Database.GetListProductsAsync(sl.ID);
                }
            }
        }
    }
}
