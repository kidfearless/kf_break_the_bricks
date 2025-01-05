using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox;
public static class Extensions
{
	public static T As<T>(this Component component) where T : Component
	{
		return component.GetComponent<T>();
	}

	public static T As<T>(this GameObject go) where T : Component
	{
		return go.GetComponent<T>();
	}

	public static float Scale(this Vector3 scale)
	{
		return (scale.x + scale.y + scale.z) / 3;
	}
}
