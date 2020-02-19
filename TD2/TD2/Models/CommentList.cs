using System.Collections.ObjectModel;
using TD2.Ressources;

namespace TD2.Models
{
    public class CommentList
    {
        public static ObservableCollection<CommentItem> Comments { get; }

        static CommentList()
        {
            Comments = new ObservableCollection<CommentItem>();
        }

        public static void AddComment(CommentItem commentItem)
        {
            Comments.Add(commentItem);
        }

        public static void ClearAll()
        {
            Comments.Clear();
        }
    }
}