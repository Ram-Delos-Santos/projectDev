using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using Plugin.Media.Abstractions;
using projectDev.Model;

namespace projectDev
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetail : Rg.Plugins.Popup.Pages.PopupPage
    {
        byte[] imageData;

        ProductModel _product;

        public ProductDetail()
        {
            InitializeComponent();
        }

        public ProductDetail(ProductModel product) : this()
        {
            Title = "Edit Product";
            _product = product;
            nameEntry.Text = product.Name;
            descEntry.Text = product.Description;
            priceEntry.Text = product.Price;
            imageEntry.Source = ImageSource.FromStream(() => new MemoryStream(product.Image));
            nameEntry.Focus();
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameEntry.Text) || string.IsNullOrWhiteSpace(descEntry.Text) || string.IsNullOrWhiteSpace(priceEntry.Text))
            {
                await DisplayAlert("Invalid", "Blank is Invalid!", "OK");
                return;
            }

            byte[] imageArray = imageData ?? (_product?.Image ?? Array.Empty<byte>());

            if (_product != null)
            {
                _product.Name = nameEntry.Text;
                _product.Description = descEntry.Text;
                _product.Price = priceEntry.Text;
                _product.Image = imageArray;
                await App.MyDatabase.UpdateProduct(_product);
            }
            else
            {
                var newProduct = new ProductModel
                {
                    Name = nameEntry.Text,
                    Description = descEntry.Text,
                    Price = priceEntry.Text,
                    Image = imageArray
                };
                await App.MyDatabase.CreateProduct(newProduct);
            }

            await Navigation.PopAsync();
        }

        async void Button_Clicked_1(object sender, EventArgs e)
        {
            await UploadPhoto();
        }

        async Task UploadPhoto()
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Photos Not Supported", "Permission not granted to photos.", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Full,
                CompressionQuality = 40
            });

            if (file == null)
                return;

            imageData = File.ReadAllBytes(file.Path);
            imageEntry.Source = ImageSource.FromStream(() => file.GetStream());
        }
    }
}
