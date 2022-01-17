using UnityEngine;

namespace CatAttack
{
	public static class UnlockablesManager
	{
	//constants
		private const string stopwatchKey = "stopwatchState";
		private const string sombreroKey = "sombreroState";
	//ENDOF constants

	//stopwatch
		public static EUnlockableState Stopwatch
		{
			get { return GetState(stopwatchKey); }
			private set { SetState(stopwatchKey, value); }
		}

		public static void UnlockStopwatch ()
		{ Unlock(stopwatchKey); }

		public static EUnlockableState ToggleStopwatch ()
		{ return Toggle(stopwatchKey); }
	//ENDOF stopwatch

	//sombrero
		public static EUnlockableState Sombrero
		{
			get { return GetState(sombreroKey); }
			private set { SetState(sombreroKey, value); }
		}

		public static void UnlockSombrero ()
		{ Unlock(sombreroKey); }

		public static EUnlockableState ToggleSombrero ()
		{ return Toggle(sombreroKey); }
	//ENDOF sombrero

	//private static methods
		private static EUnlockableState GetState (string key)
		{	
			if (!PlayerPrefs.HasKey(key))
			{ return EUnlockableState.Locked; }
			return (EUnlockableState) PlayerPrefs.GetInt(key);
		}

		private static void SetState (string key, EUnlockableState value)
		{ PlayerPrefs.SetInt(key, (int) value); }

		//unlocks an unlockable to enabled
		private static void Unlock (string key)
		{
			if (GetState(key) == EUnlockableState.Locked)
			{ SetState(key, EUnlockableState.Enabled); }
		}

		//toggles the state of an unlocked unlockable and returns its state - no change if locked
		private static EUnlockableState Toggle (string key)
		{
			EUnlockableState state = GetState(key);
			if (state == EUnlockableState.Locked)
			{ return state; }

			EUnlockableState newState;
			if (state == EUnlockableState.Enabled)
			{
				newState = EUnlockableState.Disabled;
			}
			else
			{
				newState = EUnlockableState.Enabled;
			}
			SetState(key, newState);
			return newState;
		}
	//ENDOF private static methods
	}

	//enum defining the state of an unlockable
	public enum EUnlockableState
	{
		Locked = 0,
		Enabled = 1,
		Disabled = 2
	}
}