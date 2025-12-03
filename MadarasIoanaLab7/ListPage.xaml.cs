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
        if (listView.SelectedItem is Product p)
        {
            var shopList = (ShopList)BindingContext;

            var lpList = await App.Database.GetListProductsLinksAsync(shopList.ID);
            var link = lpList.FirstOrDefault(x => x.ProductID == p.ID);

            if (link != null)
            {
                await App.Database.DeleteListProductAsync(link);
            }

            listView.ItemsSource = await App.Database.GetListProductsAsync(shopList.ID);
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