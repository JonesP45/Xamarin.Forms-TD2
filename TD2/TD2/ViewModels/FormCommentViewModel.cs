using System.Collections.Generic;
using System.Windows.Input;
using Storm.Mvvm;
using TD2.Models;
using Xamarin.Forms;

namespace TD2.ViewModels
{
    public class FormCommentViewModel : ViewModelBase
    {
        private int Id { get; set; }
        
        private string _text;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }
        
        private string _textAddComment;
        public string TextAddComment
        {
            get => _textAddComment;
            set => SetProperty(ref _textAddComment, value);
        }
        
        public ICommand AddCommentCommand { get; set; }

        public FormCommentViewModel()
        {
            AddCommentCommand = new Command(AddComment);
            Text = "Post comment";
        }

        public override void Initialize(Dictionary<string, object> navigationParameters)
        {
            base.Initialize(navigationParameters);
            Id = GetNavigationParameter<int>("Id");
        }
        
        private void AddComment()
        {
            ApiService.AddComment(Id, TextAddComment);
        }
        
    }
}