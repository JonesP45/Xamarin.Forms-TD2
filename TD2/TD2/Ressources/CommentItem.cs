using System;
using Newtonsoft.Json;
using TD.Api.Dtos;

namespace TD2.Ressources
{
	public class CommentItem
	{
		[JsonProperty("date")]
		public DateTime Date { get; set; }
		
		[JsonProperty("author")]
		public UserItem Author { get; set; }
		
		[JsonProperty("text")]
		public string Text { get; set; }
	}
}