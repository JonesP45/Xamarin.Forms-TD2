using System.Windows.Input;
using Storm.Mvvm;
using TD2.Models;
using Xamarin.Forms;

namespace TD2.ViewModels
{
    public class FormPasswordViewModel : ViewModelBase
    {
        public string EditButton { get; }
        
        private string _oldPassword;
        public string OldPassword
        {
            get => _oldPassword;
            set => SetProperty(ref _oldPassword, value);
        }
        
        private string _newPassword;
        public string NewPassword
        {
            get => _newPassword;
            set => SetProperty(ref _newPassword, value);
        }
        
        public ICommand EditCommand { get; set; }

        public FormPasswordViewModel()
        {
            EditButton = "Edit";
            EditCommand = new Command(Edit);
        }

        private void Edit()
        {
            ApiService.EditPassword(OldPassword, NewPassword);
        }
        
    }
}