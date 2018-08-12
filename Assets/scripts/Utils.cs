using UnityEngine;

public static class Utils {

	public static T RandomItem<T>(this T[] options)
    {
        return options[Random.Range(0, options.Length)];
    }
}