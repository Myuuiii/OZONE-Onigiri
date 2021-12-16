using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;

namespace OZONEOnigiri.Handlers
{
	public class CommandHandler
	{
		public static async Task HandleMessageAsync(DiscordClient client, MessageCreateEventArgs eventArgs)
		{
			if (eventArgs.Author.IsBot) return;
			var cnext = client.GetCommandsNext();
			var message = eventArgs.Message;

			var cmdStart = message.GetStringPrefixLength(Onigiri._config.Prefix);

			if (cmdStart == -1)
			{
				// Regular message
				_ = Task.Run(async () => await UserHandler.HandleUser(eventArgs));
				return;
			}


			var prefix = message.Content.Substring(0, cmdStart);
			var cmdString = message.Content.Substring(cmdStart);

			var command = cnext.FindCommand(cmdString, out var args);
			var ctx = cnext.CreateContext(message, prefix, command, args);

			_ = Task.Run(async () => await cnext.ExecuteCommandAsync(ctx));

			return;
		}
	}
}