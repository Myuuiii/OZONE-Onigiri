﻿using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using Newtonsoft.Json;
using OZONEOnigiri.Configuration;
using OZONEOnigiri.Handlers;

namespace OZONEOnigiri
{
	public class Onigiri
	{
		private static DiscordClient _client;
		private static CommandsNextExtension _commands;
		public static OnigiriConfig _config;
		public static void Main(string[] args)
		{

			MainAsync().GetAwaiter().GetResult();
		}

		static async Task MainAsync()
		{
			_client = new DiscordClient(new DiscordConfiguration()
			{
				Token = "",
				TokenType = TokenType.Bot,
				Intents = DiscordIntents.All
			});

			_commands = _client.UseCommandsNext(new CommandsNextConfiguration()
			{
				UseDefaultCommandHandler = false
			});

			_client.MessageCreated += CommandHandler.HandleMessageAsync;

			await _client.ConnectAsync();
			await Task.Delay(-1);
		}
	}
}