using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using Newtonsoft.Json;
using OZONEOnigiri.Configuration;
using OZONEOnigiri.Data;
using OZONEOnigiri.Handlers;
using OZONEOnigiri.Modules;

namespace OZONEOnigiri
{
	public class Onigiri
	{
		private static DiscordClient _client;
		private static CommandsNextExtension _commands;
		public static OnigiriConfig _config;
		public static OnigiriDbContext _db;
		public static void Main(string[] args)
		{
			_config = OnigiriConfig.LoadIfExists();
			_db = new OnigiriDbContext();

			MainAsync().GetAwaiter().GetResult();
		}

		static async Task MainAsync()
		{
			_client = new DiscordClient(new DiscordConfiguration()
			{
				Token = _config.Token,
				TokenType = TokenType.Bot,
				Intents = DiscordIntents.All
			});

			_commands = _client.UseCommandsNext(new CommandsNextConfiguration()
			{
				UseDefaultCommandHandler = false,
				StringPrefixes = new[] { _config.Prefix, "onigiri!" }
			});

			_commands.RegisterCommands<RoleManagement>();
			_commands.RegisterCommands<Information>();
			_commands.RegisterCommands<Vanity>();

			_client.MessageCreated += CommandHandler.HandleMessageAsync;

			await _client.ConnectAsync();
			await Task.Delay(-1);
		}
	}
}