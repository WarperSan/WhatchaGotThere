using System.Text;
using BepInEx.Logging;

namespace WhatchaGotThere.Helpers;

/// <summary>
///     Class helping for logging stuff
/// </summary>
internal static class Log
{
	private static readonly ManualLogSource? _logger = Logger.CreateLogSource(MyPluginInfo.PLUGIN_GUID);

	private static void LogSelf(object?[] data, LogLevel level)
	{
		if (_logger == null)
			return;

		var message = new StringBuilder();

		for (var i = 0; i < data.Length; i++)
		{
			var content = data[i] ?? "null";

			message.Append(content);

			if (i < data.Length - 1)
				message.Append(' ');
		}

		_logger.Log(level, message.ToString());
	}

	/// <summary>
	///     Logs information for developers that helps to debug the mod
	/// </summary>
	public static void Debug(params object?[] data)
	{
		LogSelf(data, LogLevel.Debug);
	}

	/// <summary>
	///     Logs information for players to know important steps of the mod
	/// </summary>
	public static void Info(params object?[] data)
	{
		LogSelf(data, LogLevel.Message);
	}

	/// <summary>
	///     Logs information for players to warn them about an unwanted state
	/// </summary>
	public static void Warning(params object?[] data)
	{
		LogSelf(data, LogLevel.Warning);
	}

	/// <summary>
	///     Logs information for players to notify them of an error
	/// </summary>
	public static void Error(params object?[] data)
	{
		LogSelf(data, LogLevel.Error);
	}
}