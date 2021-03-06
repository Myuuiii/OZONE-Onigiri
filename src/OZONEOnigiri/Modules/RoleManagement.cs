using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using OZONEOnigiri.Data;
using OZONEOnigiri.Models;

namespace OZONEOnigiri.Modules
{
	public class RoleManagement : BaseCommandModule
	{
		[Command("addrole")]
		[Aliases(new[] { "ar", "add" })]
		[Description("Add a role to the system")]
		[RequirePermissions(Permissions.ManageRoles)]
		public async Task AddRole(CommandContext ctx, int level, [RemainingText] DiscordRole role)
		{
			OnigiriDbContext db = new OnigiriDbContext();
			await ctx.TriggerTypingAsync();
			if (!db.Roles.Any(r => r.Id == role.Id))
			{
				DiscordEmbedBuilder embedBuilder = new DiscordEmbedBuilder();
				embedBuilder.WithTitle("New role registered!");
				embedBuilder.WithDescription($"**Role**: {role.Name}\n**Level**: {level}");
				embedBuilder.WithColor(new DiscordColor("#BED79D"));
				embedBuilder.WithFooter("OZONE Onigiri");

				db.Roles.Add(new OnigiriRole(role.Id, level));
				await db.SaveChangesAsync();
				await ctx.RespondAsync(embed: embedBuilder.Build());
			}
		}

		[Command("removerole")]
		[Aliases(new[] { "rr", "remove" })]
		[Description("Removes a a role from the system")]
		[RequirePermissions(Permissions.ManageRoles)]
		public async Task RemoveRole(CommandContext ctx, [RemainingText] DiscordRole role)
		{
			OnigiriDbContext db = new OnigiriDbContext();
			await ctx.TriggerTypingAsync();
			if (db.Roles.Any(r => r.Id == role.Id))
			{
				DiscordEmbedBuilder embedBuilder = new DiscordEmbedBuilder();
				embedBuilder.WithTitle("Role removed!");
				embedBuilder.WithDescription($"**Role**: {role.Name}\n**Level**: {db.Roles.Single(r => r.Id == role.Id).Level}");
				embedBuilder.WithColor(new DiscordColor("#BED79D"));
				embedBuilder.WithFooter("OZONE Onigiri");

				OnigiriRole onigiriRole = db.Roles.Single(r => r.Id == role.Id);
				db.Roles.Remove(onigiriRole);
				await db.SaveChangesAsync();
				await ctx.RespondAsync(embed: embedBuilder.Build());
			}
		}

		[Command("listroles")]
		[Aliases(new[] { "lr", "list" })]
		[Description("Lists all roles in the system")]
		public async Task ListRoles(CommandContext ctx)
		{
			OnigiriDbContext db = new OnigiriDbContext();
			await ctx.TriggerTypingAsync();
			if (db.Roles.Any())
			{
				DiscordEmbedBuilder embedBuilder = new DiscordEmbedBuilder();
				embedBuilder.WithTitle("Roles");
				embedBuilder.WithColor(new DiscordColor("#BED79D"));
				embedBuilder.WithFooter("OZONE Onigiri");
				string roles = "";
				foreach (OnigiriRole role in db.Roles.OrderBy(r => r.Level))
				{
					roles += $"**Level {role.Level}**: {ctx.Guild.GetRole(role.Id).Mention}\n";
				}
				embedBuilder.WithDescription(roles);
				await ctx.RespondAsync(embed: embedBuilder.Build());
			}
			else
			{
				await ctx.RespondAsync("No roles have been added");
			}
		}
	}
}