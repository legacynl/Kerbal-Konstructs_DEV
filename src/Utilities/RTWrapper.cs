using System;
using System.Reflection;
using UnityEngine;

namespace KerbalKonstructs.Utilities
{
	public class RTWrapper
	{
		public static bool RTLoaded { get; private set;}

		static RTWrapper()
		{
			RTLoaded = false;
			for (int i = 0; i < AssemblyLoader.loadedAssemblies.Count; i++)
			{
				if (AssemblyLoader.loadedAssemblies[i].name == "RemoteTech")
				{
					RTLoaded = true;
					break;
				}
			}
		}

		public static Guid AddBaseStation(string name, double latitutde, double longitude, double height, int body)
		{
			Type RTType = Type.GetType ("RemoteTech.API.API, RemoteTech");
			return (Guid)RTType.InvokeMember ("AddGroundStation",
								BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.Public,
								null,
								null,
								new System.Object[] {name,
													latitutde,
													longitude,
													height,
													body});
		}

		public static bool RemoveBaseStation(Guid baseguid)
		{
			Type RTType = Type.GetType ("RemoteTech.API.API, RemoteTech");
			return (bool)RTType.InvokeMember ("RemoveGroundStation",
								BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.Public,
								null,
								null,
								new System.Object[] { baseguid });
		}

		public void getRTProps()
		{
			if (RTLoaded)
			{
				Debug.Log ("RT is loaded!");
				Type RTType = Type.GetType ("RemoteTech.API.API, RemoteTech");
				if (RTType == null)
					Debug.Log ("RTTYPE is null");
				MethodInfo[] methods = RTType.GetMethods();
				foreach (MethodInfo method in methods)
				{
					Debug.Log ("blaat?");
					Debug.Log (method.Name);
					Debug.Log (method.ReturnType);
				}
			} else
				Debug.Log ("Cannot find RT, RT is not loaded?");
		}
	}
}

