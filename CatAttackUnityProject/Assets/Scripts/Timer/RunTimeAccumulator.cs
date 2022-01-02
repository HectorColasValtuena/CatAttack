using System.Collections.Generic;

using Debug = UnityEngine.Debug;

namespace CatAttack
{
	public static class RunTimeAccumulator
	{
	//const	
	//ENDOF const

	//public static members
		public static bool FullRunTimeAvailable
		{ get {/*[TO-DO]*/ return false; } }

		public static void RegisterLevelTime (int level, float seconds)
		{
			ValidateDictionary();

			Debug.Log(string.Format("Registered level {0} time: {1}", level, seconds));
			if (levelTimes.ContainsKey(level))
			{ levelTimes.Remove(level); }

			levelTimes.Add(level, seconds);
		}

		//returns level # time, or null if not registered
		public static float? GetLevelTime (int level, float seconds)
		{
			ValidateDictionary();

			if (levelTimes.ContainsKey(level))
			{ return levelTimes[level]; }
			return null;
		}

		public static void ResetTimeAccumulator ()
		{
			ResetTimeDictionary();
		}
	//ENDOF public static

	//private static fields
		//a dictionary matching level # with its stored time
		private static IDictionary<int, float> levelTimes;
		private static void ResetTimeDictionary ()
		{ levelTimes = new Dictionary<int,float>(); }
		private static void ValidateDictionary()
		{ if (levelTimes == null) { ResetTimeDictionary(); }}
	//ENDOF private static
	}
}