using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SnappedChat.Models;
using SnappedChat.Views;
using SnappedChat.ViewModels;
using System.ComponentModel;

namespace SnappedChat.Views
{
    [DesignTimeVisible(true)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }

        async void OnItemSelected(object sender, SelectionChangedEventArgs args)
        {
            var item = args.CurrentSelection.FirstOrDefault() as Item;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            // The Thanos Algorithm
            await DisplayAlert("Thanos", "You should have gone for the head!", "SNAP!");
            for (int i=0; i < viewModel.Items.Count; i++)
            {
                viewModel.Items.RemoveAt(viewModel.Items.Count - 1);
            }
        }
    }
}