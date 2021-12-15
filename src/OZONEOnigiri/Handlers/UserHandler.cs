using System.Threading.Tasks;
using DSharpPlus.EventArgs;

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
				// 
				// Retrieve the user from the database and perform the following actions
				//
				// 1. Increase the Messages Sent count of the user
				// 2. Increase the Experience of the user
				// 3. Check if the user has leveled up
				//    - If they did grant them the appropriate role
				// 4. Update other user information like avatar, username, discriminator
				// 5. Save the user to the database
				//
			}
			catch
			{
				// Lol something didnt work
			}
		}
	}
}