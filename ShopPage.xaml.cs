using Coblis_TudorCristian_Lab7.Models;
using Microsoft.Maui.Devices.Sensors;
using Plugin.LocalNotification;

namespace Coblis_TudorCristian_Lab7;

public partial class ShopPage : ContentPage
{
	public ShopPage()
	{
		InitializeComponent();
	}
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        await App.Database.SaveShopAsync(shop);
        await Navigation.PopAsync();
    }
    async void OnShowMapButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        var address = shop.Adress;
        var locations = await Geocoding.GetLocationsAsync(address);

        var options = new MapLaunchOptions
        {
            Name = "Magazinul meu preferat" };
        var location = locations?.FirstOrDefault();
        // var myLocation = await Geolocation.GetLocationAsync();
        var myLocation = new Location(46.7731796289, 23.6213886738);

        var distance = myLocation.CalculateDistance(location,
DistanceUnits.Kilometers);
        if (distance < 4)
        {
            var request = new NotificationRequest
            {
                Title = "Ai de facut cumparaturi in apropiere!",
                Description = address,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(1)
                }
            };
            LocalNotificationCenter.Current.Show(request);
        }

        await Map.OpenAsync(location, options);
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;

        if (shop != null)
        {
            await App.Database.DeleteShopAsync(shop);
            await DisplayAlert("Success", "Shop deleted successfully", "OK");
        }
        else
        {
            await DisplayAlert("Error", "Unable to delete shop", "OK");
        }

        await Navigation.PopAsync();
    }
}