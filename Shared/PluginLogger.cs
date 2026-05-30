using MelonLoader;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Mugnum.PowerWashMods.Shared;

/// <summary>
/// Logger for plugins.
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class PluginLogger
{
	/// <summary>
	/// Prefix containing plugin name.
	/// </summary>
	private static string Prefix;

	/// <summary>
	/// Initializes logger.
	/// </summary>
	/// <param name="pluginName"> Plugin name. </param>
	public static void Initialize(string pluginName)
	{
		Prefix = $"[{pluginName}]";
	}

	/// <summary>
	/// Writes info log.
	/// </summary>
	/// <param name="message"> Message. </param>
	/// <param name="member"> Calling method name. </param>
	public static void Info(string message, [CallerMemberName] string member = "")
	{
		Write(MelonLogger.Msg, message, member);
	}

	/// <summary>
	/// Writes warning log.
	/// </summary>
	/// <param name="message"> Message. </param>
	/// <param name="member"> Calling method name. </param>
	public static void Warning(string message, [CallerMemberName] string member = "")
	{
		Write(MelonLogger.Warning, message, member);
	}

	/// <summary>
	/// Writes error log.
	/// </summary>
	/// <param name="message"> Message. </param>
	/// <param name="member"> Calling method name. </param>
	public static void Error(string message, [CallerMemberName] string member = "")
	{
		Write(MelonLogger.Error, message, member);
	}

	/// <summary>
	/// Writes log.
	/// </summary>
	/// <param name="logMethod"> Logging method delegate. </param>
	/// <param name="message"> Message. </param>
	/// <param name="member"> Calliong method name. </param>
	private static void Write(Action<string> logMethod, string message, [CallerMemberName] string member = "")
	{
		var text = string.IsNullOrEmpty(member)
			? $"{Prefix} {message}"
			: $"{Prefix} [{member}] {message}";

		logMethod(text);
	}
}
