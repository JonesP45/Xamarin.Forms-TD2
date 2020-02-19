using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Storm.Mvvm;
using TD2.Models;
using TD2.Ressources;
using Xamarin.Forms;

namespace TD2.ViewModels
{
    public class CommentsViewModel : ViewModelBase
    {
        private int Id { get; set; }
        
        private ObservableCollection<CommentItem> _comments;
        public ObservableCollection<CommentItem> Comments
        {
            get => _comments;
            set => SetProperty(ref _comments, value);
        }

        public ICommand GoToFormCommentCommand { get; set; }

        public CommentsViewModel()
        {
            GoToFormCommentCommand = new Command(GoToFormComment);
        }
        
        public override void Initialize(Dictionary<string, object> navigationParameters)
        {
            base.Initialize(navigationParameters);
            Id = GetNavigationParameter<int>("Id");
        }
        
        public override async Task OnResume()
        {
            await base.OnResume();
            Comments = await ApiService.GetComments(Id);
        }

        private void GoToFormComment()
        {
            if (UserService.IsConnected())
            {
                var parameters = new Dictionary<string, object> {{"Id", Id}};
                new GoToService().GoToFormComment(parameters);
            }
            else
            {
                AlertService.UserNotConnected();
            }
        }

    }
}