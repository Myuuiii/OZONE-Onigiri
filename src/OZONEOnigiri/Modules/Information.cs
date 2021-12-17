using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using OZONEOnigiri.Data;
using OZONEOnigiri.Models;

namespace OZONEOnigiri.Modules
{
	public class Information : BaseCommandModule
	{
		[Command("leaderboard")]
		[Aliases(new[] { "lb", "scores", "rankings" })]
		public async Task Leaderboard(CommandContext ctx)
		{
			OnigiriDbContext db = new OnigiriDbContext();
			DiscordEmbedBuilder embedBuilder = new DiscordEmbedBuilder();
			embedBuilder.WithTitle("Leaderboard");
			embedBuilder.WithColor(new DiscordColor("#BED79D"));
			embedBuilder.WithFooter("OZONE Onigiri");

			StringBuilder sb = new StringBuilder();
			int currentUserRank = 1;
			foreach (var user in db.Users.OrderByDescending(u => u.Level).ThenByDescending(u => u.Experience).Take(10))
			{
				string medalIcon = "";
				switch (currentUserRank)
				{
					case 1:
						medalIcon = ":first_place:";
						break;
					case 2:
						medalIcon = ":second_place:";
						break;
					case 3:
						medalIcon = ":third_place:";
						break;
					default:
						medalIcon = "" + currentUserRank;
						break;
				}
				sb.AppendLine($"{medalIcon}: **<@{user.Id}>** - Level {user.Level}, {user.Experience} XP");
				currentUserRank++;
			}

			embedBuilder.WithDescription(sb.ToString());
			await ctx.RespondAsync(embed: embedBuilder.Build());
		}

		[Command("rank")]
		[Aliases(new[] { "r", "level", "xp" })]
		public async Task Rank(CommandContext ctx)
		{
			OnigiriDbContext db = new OnigiriDbContext();
			if (db.Users.Any(u => u.Id == ctx.User.Id))
			{
				DiscordEmbedBuilder embedBuilder = new DiscordEmbedBuilder();
				embedBuilder.WithTitle($"{ctx.User.Username}'s Rank");
				embedBuilder.WithColor(new DiscordColor("#BED79D"));
				embedBuilder.WithFooter("OZONE Onigiri");

				OnigiriUser user = db.Users.Single(u => u.Id == ctx.User.Id);
				embedBuilder.WithDescription($"**<@{user.Id}>** - Level {user.Level}, {user.Experience} XP");
				await ctx.RespondAsync(embed: embedBuilder.Build());
			}
		}
	}
}