using HarmonyLib;
using Mugnum.PowerWashMods.Shared;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using UnityEngine;

namespace Mugnum.PowerWashMods.UniversalMovementSpeed.Patches;

/// <summary>
/// Patch for PhysicalCharacterController.Start method.
/// </summary>
[HarmonyPatch]
public static class PhysicalCharacterControllerStartPatch
{
	/// <summary>
	/// Current "PhysicalCharacterController" instance.
	/// Used for applying value after changing it using keyboard shortcut.
	/// </summary>
	private static Object CurrentController;

	/// <summary>
	/// Vanilla value for curve "m_lookDownMoveSpeed".
	/// </summary>
	private static readonly AnimationCurve VanillaCurve = new(new Keyframe(0f, Plugin.VanillaMinSpeed), new Keyframe(1f, 1f));

	/// <summary>
	/// Modded value for curve "m_lookDownMoveSpeed".
	/// </summary>
	private static AnimationCurve ModdedCurve => new(new Keyframe(0f, Plugin.MinSpeed.Value), new Keyframe(1f, 1f));

	/// <summary>
	/// Returns patched method.
	/// </summary>
	/// <returns> Method. </returns>
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	internal static MethodBase TargetMethod() => PatchHelper.FindMethod("PhysicalCharacterController", "Start");

	/// <summary>
	/// Patch postfix.
	/// </summary>
	/// <param name="__instance"> PhysicalCharacterController instance. </param>
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	internal static void Postfix(object __instance)
	{
		if (__instance is not Object unityObject || !unityObject)
		{
			return;
		}

		CurrentController = unityObject;
		ApplyCurve();
	}

	/// <summary>
	/// Applies a new curve for "m_lookDownMoveSpeed" field.
	/// </summary>
	internal static void ApplyCurve()
	{
		if (!CurrentController)
		{
			return;
		}

		const string PropertyName = "m_lookDownMoveSpeed";
		var curve = Plugin.IsModEnabled
			? ModdedCurve
			: VanillaCurve;

		PatchHelper.SetProperty(CurrentController, PropertyName, curve);
		PluginLogger.Info($"Is enabled: {Plugin.IsModEnabled}. Value patched: {PropertyName}.");
	}
}
