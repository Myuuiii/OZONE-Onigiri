using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace OZONEOnigiri.Modules
{
	public class Vanity : BaseCommandModule
	{
		[Command("o-zone")]
		public async Task Ozone(CommandContext ctx)
		{
			await ctx.TriggerTypingAsync();
			await ctx.RespondAsync("https://youtu.be/YnopHCL1Jk8");
		}
	}
}