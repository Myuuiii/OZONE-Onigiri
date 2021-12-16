using System.ComponentModel.DataAnnotations;

namespace OZONEOnigiri.Models
{
	public class OnigiriRole
	{
		public OnigiriRole(ulong id, int level)
		{
			Id = id;
			Level = level;
		}

		[Key]
		public ulong Id { get; set; }
		public int Level { get; set; }

		public string GetMention()
		{
			return $"<@&{Id}>";
		}
	}
}