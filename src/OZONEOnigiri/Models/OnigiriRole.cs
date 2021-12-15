using System.ComponentModel.DataAnnotations;

namespace OZONEOnigiri.Models
{
	public class OnigiriRole
	{
		[Key]
		public ulong Id { get; set; }
		public int Level { get; set; }

		public string GetMention()
		{
			return $"<@&{Id}>";
		}
	}
}