using UnityEngine;

namespace CatAttack
{
	public static class RecordVault
	{
		private const string RECORD_KEY = "RunRecordTime";

	//public properties
		public static bool HasRecord { get { return PlayerPrefs.HasKey(RECORD_KEY); }}

		public static float Record
		{
			get { return PlayerPrefs.GetFloat(RECORD_KEY); }
			private set { PlayerPrefs.SetFloat(RECORD_KEY, value); }
		}
	//ENDOF public properties

	//public methods
		public static bool IsRecord (float seconds)
		{
			if (!HasRecord) { return true; }
			return (seconds < Record);
		}

		public static void RegisterTime (float seconds)
		{
			if (IsRecord(seconds))
			{
				Record = seconds;
			}
		}
	//ENDOF public methods
	}
}