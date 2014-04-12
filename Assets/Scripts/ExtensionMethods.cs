
/// <summary>
/// Unity is VERY extendable. Once you know how to make custom components and extend existing ones,
/// you can make the editor your own.
/// 
/// https://unity3d.com/learn/tutorials/modules/intermediate/scripting/extension-methods
/// </summary>
public static class ExtensionMethods {

	/// <summary>
	/// Remaps a value from one range to another. Adaptation of http://processing.org/reference/map_.html
	/// </summary>
	/// <param name="value">Value to remap.</param>
	/// <param name="low1">Lower range 1.</param>
	/// <param name="high1">Upper range 1.</param>
	/// <param name="low2">Lower range 2.</param>
	/// <param name="high2">Upper range 2.</param>
	public static float Remap (this float value, float low1, float high1, float low2, float high2) {
		return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
	}
	/*Here's what this means:
	 * 
	 * We say "public static X", where X is the existing part of Unity we want to extend.
	 * 
	 * We can then set our arguments however we wish as if we had access to the original "float" code.
	 * 
	 * Then we can call it simply with:
	 * myFloat.Remap(
	*/

}
