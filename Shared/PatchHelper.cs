using HarmonyLib;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Mugnum.PowerWashMods.Shared;

/// <summary>
/// Helper class for applying harmony patches.
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class PatchHelper
{
	/// <summary>
	/// Finds method to patch.
	/// </summary>
	/// <param name="typeName"> Type name for containing class. </param>
	/// <param name="methodName"> Method name. </param>
	/// <returns> Method. </returns>
	public static MethodBase FindMethod(string typeName, string methodName)
	{
		if (string.IsNullOrEmpty(typeName))
		{
			throw new ArgumentNullException(nameof(typeName));
		}
		if (string.IsNullOrEmpty(methodName))
		{
			throw new ArgumentNullException(nameof(methodName));
		}

		var type = AccessTools.TypeByName(typeName);

		if (type == null)
		{
			PluginLogger.Error($"Couldn't find type: {typeName}");
			return null;
		}

		var method = AccessTools.Method(type, methodName);

		if (method != null)
		{
			return method;
		}

		PluginLogger.Error($"Couldn't find method: {methodName}");
		return null;
	}

	/// <summary>
	/// Finds getter for property.
	/// </summary>
	/// <param name="target"> Target type containing property. </param>
	/// <param name="propertyName"> Property name. </param>
	/// <returns> Getter method. </returns>
	public static MethodInfo FindGetter(UnityEngine.Object target, string propertyName) => FindAccessor(target, propertyName, false);

	/// <summary>
	/// Finds setter for property.
	/// </summary>
	/// <param name="target"> Target type containing property. </param>
	/// <param name="propertyName"> Property name. </param>
	/// <returns> Setter method. </returns>
	public static MethodInfo FindSetter(UnityEngine.Object target, string propertyName) => FindAccessor(target, propertyName, true);

	/// <summary>
	/// Gets value from property.
	/// </summary>
	/// <typeparam name="T"> Value type. </typeparam>
	/// <param name="target"> Target type containing property. </param>
	/// <param name="propertyName"> Property name. </param>
	/// <returns> Value. </returns>
	/// <exception cref="MissingMethodException"> Thrown if the getter for the property cannot be found. </exception>
	public static T GetProperty<T>(UnityEngine.Object target, string propertyName)
	{
		return (T)FindGetter(target, propertyName).Invoke(target, []);
	}

	/// <summary>
	/// Sets value to property.
	/// </summary>
	/// <typeparam name="T"> Value type. </typeparam>
	/// <param name="target"> Target type containing property. </param>
	/// <param name="propertyName"> Property name. </param>
	/// <param name="value"> Value. </param>
	/// <exception cref="MissingMethodException"> Thrown if the setter for the property cannot be found. </exception>
	public static void SetProperty<T>(UnityEngine.Object target, string propertyName, T value)
	{
		FindSetter(target, propertyName).Invoke(target, [value]);
	}

	/// <summary>
	/// Finds propety accessor.
	/// </summary>
	/// <param name="target"> Target type containing property. </param>
	/// <param name="propertyName"> Property name. </param>
	/// <param name="isSetter"> Is looking for setter. </param>
	/// <returns> Property accessor. </returns>
	private static MethodInfo FindAccessor(UnityEngine.Object target, string propertyName, bool isSetter)
	{
		if (string.IsNullOrEmpty(propertyName))
		{
			throw new ArgumentNullException(nameof(propertyName));
		}
		if (!target)
		{
			return null;
		}

		var type = target.GetType();
		var setterName = isSetter
			? $"set_{propertyName}"
			: $"get_{propertyName}";

		var accessor = AccessTools.Method(type, setterName);

		if (accessor == null)
		{
			PluginLogger.Error($"Accessor not found: {setterName}");
		}

		return accessor;
	}
}
