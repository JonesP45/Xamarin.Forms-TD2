using Newtonsoft.Json;

namespace TD2.Ressources
{
	public class CreateCommentRequest
	{
		[JsonProperty("text")]
		public string Text { get; set; }
	}
}