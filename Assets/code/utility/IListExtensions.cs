using UnityEngine;
using System.Collections.Generic;
using System.Text;

static class IListExtensions {
	public static void Swap<T>(this IList<T> list, int firstIndex, int secondIndex) {
		T tmp = list[firstIndex];
		list[firstIndex] = list[secondIndex];
		list[secondIndex] = tmp;
	}

	/// <summary>
	/// Shuffle the array. Implements the Fisher-Yates/Knuth shuffle
	/// </summary>
	/// <typeparam name="T">Array element type.</typeparam>
	/// <param name="array">Array to shuffle.</param>
	public static void Shuffle<T>(this IList<T> list)
	{
		int n = list.Count;
		for (int i = 0; i < n; i++)
		{
			int r = i + (int)(Random.value * (n - i));
			T t = list[r];
			list[r] = list[i];
			list[i] = t;
		}
	}

	public static string toString<T>(this IEnumerable<T> elements)
	{
		StringBuilder sb = new StringBuilder("[");
		foreach (var element in elements)
		{
			sb.AppendFormat("{0},", element);
		}
		sb.Remove(sb.Length - 1, 1);
		sb.Append("]");
		return sb.ToString();
	}

}