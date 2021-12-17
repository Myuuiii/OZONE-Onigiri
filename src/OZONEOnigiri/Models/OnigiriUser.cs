using System.ComponentModel.DataAnnotations;
using DSharpPlus.Entities;

namespace OZONEOnigiri.Models
{
	public class OnigiriUser
	{
		[Key]
		public ulong Id { get; set; }
		public string Avatar { get; set; }
		public int MessagesSent { get; set; }
		public int Experience { get; set; }
		public int Level { get; set; } = 1;

		public string GetMention()
		{
			return $"<@{Id}>";
		}

		public void UpdateInformation(DiscordUser user)
		{
			this.Avatar = user.GetAvatarUrl(DSharpPlus.ImageFormat.Auto, 2048);
		}
	}
}