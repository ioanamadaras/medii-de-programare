namespace MadarasIoanaLab7;
using MadarasIoanaLab7.Models;

public partial class ListPage : ContentPage
{
    ShopList sl;
    public ListPage()
    {
        InitializeComponent();
        sl = (ShopList)this.BindingContext;
    }

    async void OnDeleteItemButtonClicked(object sender, EventArgs e)
    {
        Product p;

        if (listView.SelectedItem != null)
        {
            p = listView.SelectedItem as Product;
            await App.Database.DeleteProductAsync(p);
        }
    }

    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(
            new ProductPage((ShopList)this.BindingContext)
            {
                BindingContext = new Product()
            }
        );
    }


    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;

        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;

        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var shopl = (ShopList)BindingContext;

        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
    }

}