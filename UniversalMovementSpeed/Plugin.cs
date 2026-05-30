using MelonLoader;
using Mugnum.PowerWashMods.UniversalMovementSpeed;
using Mugnum.PowerWashMods.UniversalMovementSpeed.Patches;
using Mugnum.PowerWashMods.Shared;
using UnityEngine;

[assembly: MelonInfo(typeof(Plugin), Plugin.PluginName, "1.0.0", "Mugnum")]
[assembly: MelonGame("FuturLab", "PowerWash Simulator")]

namespace Mugnum.PowerWashMods.UniversalMovementSpeed;

/// <summary>
/// Universal Movement Speed mod.
/// </summary>
public class Plugin : MelonMod
{
	/// <summary>
	/// Mod name.
	/// </summary>
	public const string PluginName = "UniversalMovementSpeed";

	/// <summary>
	/// Vanilla minimum speed curve value.
	/// </summary>
	internal const float VanillaMinSpeed = 0.2f;

	/// <summary>
	/// Preferences category.
	/// </summary>
	private static MelonPreferences_Category PreferencesCategory;

	/// <summary>
	/// Minimum speed curve value (when looking all the way down).
	/// </summary>
	internal static MelonPreferences_Entry<float> MinSpeed;

	/// <summary>
	/// Keybind for toggling mod.
	/// </summary>
	internal static MelonPreferences_Entry<KeyCode> ToggleKey;

	/// <summary>
	/// Mod toggle.
	/// </summary>
	internal static bool IsModEnabled = true;

	/// <summary>
	/// Initializes plugin.
	/// </summary>
	public override void OnInitializeMelon()
	{
		PluginLogger.Initialize(PluginName);
		InitConfig();
		HarmonyInstance.PatchAll();
		PluginLogger.Info("Plugin initialized.");
	}

	/// <summary>
	/// "Update" processing every frame.
	/// </summary>
	public override void OnUpdate()
	{
		base.OnUpdate();

		if (!Input.GetKeyDown(ToggleKey.Value))
		{
			return;
		}

		IsModEnabled = !IsModEnabled;
		PhysicalCharacterControllerStartPatch.ApplyCurve();
	}

	/// <summary>
	/// Initializes config.
	/// </summary>
	private static void InitConfig()
	{
		PreferencesCategory = MelonPreferences.CreateCategory("Universal movement speed");
		MinSpeed = PreferencesCategory.CreateEntry("Minimum movement speed", 1f,
			description: "Movement speed multiplier when looking all the way down.");

		ToggleKey = PreferencesCategory.CreateEntry("Toggle mod keybind", KeyCode.BackQuote,
			description: "Keybind for toggling universal movement speed.");
	}
}
