using System.IO;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Storm.Mvvm.Services;
using Xamarin.Forms;

namespace TD2.Models
{
    public static class ImageService
    {
        private static byte[] ImageData { get; set; }
        
        public static async Task<(string APpath, string id)> SetImage()
        {
            var actionCamera = "Camera";
            var actionGallery = "Gallery";
            var dialogService = DependencyService.Resolve<IDialogService>();
            var result = await dialogService.DisplayActionSheet("Add an image", "Cancel", null, actionCamera, actionGallery);
            if (result.Equals(actionCamera))
            {
                var res = await ViaCamera();
                return res;
            }
            else if (result.Equals(actionGallery))
            {
                var res = await ViaGallery();
                return res;
            }
            return (null, null);
        }

        private static async Task<(string APpath, string id)> ViaCamera()
        {
            await CrossMedia.Current.Initialize();
    
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                AlertService.Error("No camera available", false);
                return (null, null);
            }

            var file = await CrossMedia.Current.TakePhotoAsync(options: new StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Small,
                SaveToAlbum = true,
                Directory = "Sample",
                Name = "test.jpg"
            });
            if (file == null)
                return (null, null);
            
            ImageData = GetImageStreamAsBytes(file);
            var id = await PostImage(ImageData);
            return (file.AlbumPath, id);
        }

        private static byte[] GetImageStreamAsBytes(MediaFile input)
        {
            using (var ms = new MemoryStream())
            {
                input.GetStream().CopyTo(ms);
                return ms.ToArray();
            }
        }

        private static async Task<(string APpath, string id)> ViaGallery()
        {
            await CrossMedia.Current.Initialize();
    
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                AlertService.Error("No camera available", false);
                return (null, null);
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Small
            });

            if (file == null)
                return (null, null);

            ImageData = GetImageStreamAsBytes(file);
            var id = await PostImage(ImageData);
            return (file.Path, id);
        }
        
        private static async Task<string> PostImage(byte[] imageData)
        {
            var id = await ApiService.PostImage(imageData);
            return id;
        }
        
    }
}