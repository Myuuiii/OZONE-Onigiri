using System;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using OZONEOnigiri.Data;
using OZONEOnigiri.Models;

namespace OZONEOnigiri.Handlers
{
	public class UserHandler
	{
		/// <summary>
		/// Grant the user the level roles that they should have
		/// </summary>
		/// <param name="args">Message Create Event Arguments</param>
		/// <returns></returns>
		public static async Task HandleUser(MessageCreateEventArgs args)
		{
			try
			{
				OnigiriDbContext db = new OnigiriDbContext();
				if (db.Users.Any(u => u.Id == args.Author.Id))
				{
					// User was found
					OnigiriUser user = db.Users.Single(u => u.Id == args.Author.Id);
					user.MessagesSent++;
					user.UpdateInformation(args.Author);

					Random rand = new Random();
					int randomXpValue = rand.Next(1, 4);

					user.Experience += randomXpValue;

					if (user.Experience >= (user.Level * 100))
					{
						user.Level++;
						user.Experience = 0;
						await args.Message.RespondAsync($"Level up! You have reached level {user.Level}!");
					}

					if (db.Roles.Any(r => r.Level <= user.Level))
					{
						OnigiriRole[] roles = db.Roles.Where(r => r.Level <= user.Level).ToArray();
						foreach (OnigiriRole role in roles)
						{
							if (args.Guild.Roles.Any(r => r.Key == role.Id))
							{
								if ((args.Author as DiscordMember).Roles.Any(r => r.Id == role.Id))
									continue;

								await (args.Author as DiscordMember).GrantRoleAsync(args.Guild.GetRole(role.Id));
							}
						}
					}

					await db.SaveChangesAsync();
				}
				else
				{
					OnigiriUser user = new OnigiriUser();
					user.Id = args.Author.Id;
					user.UpdateInformation(args.Author);

					db.Users.Add(user);
					await db.SaveChangesAsync();
				}
			}
			catch
			{
				// Lol something didnt work
			}
		}
	}
}