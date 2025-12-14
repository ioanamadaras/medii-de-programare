using Microsoft.Maui.Devices.Sensors;
using MadarasIoanaLab7.Models;
using Plugin.LocalNotification;
using System.Threading.Tasks;

namespace MadarasIoanaLab7;

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
        var address = shop.Address;

        if (string.IsNullOrWhiteSpace(address))
        {
            await DisplayAlert("Error", "Please enter an address for the shop.", "OK");
            return;
        }

        var locations = await Geocoding.GetLocationsAsync(address);

        var options = new MapLaunchOptions
        {
            Name = "Magazinul meu preferat"
        };

        var shoplocation = locations?.FirstOrDefault();
        var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

        if (status != PermissionStatus.Granted)
        {
            await DisplayAlert("Permission required",
                "Location permission is needed to calculate distance.",
                "OK");
            return;
        }

        var myLocation = await Geolocation.GetLocationAsync();
        var distance = myLocation.CalculateDistance(shoplocation,
       DistanceUnits.Kilometers);
        if (distance < 5)
        {
            var notifStatus = await Permissions.RequestAsync<Permissions.PostNotifications>();

            if (notifStatus != PermissionStatus.Granted)
            {
                return;
            }

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
        await Map.OpenAsync(shoplocation, options);
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;

        bool answer = await DisplayAlert(
            "Confirm Delete",
            "Are you sure you want to delete this shop?",
            "Yes",
            "No");

        if(!answer)
            return;

        await App.Database.DeleteShopAsync(shop);
        await Navigation.PopAsync();
    }
}