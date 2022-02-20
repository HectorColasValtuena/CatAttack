namespace CatAttack.Input
{
	public static class InputControllerCache
	{
	//serialized static fields
		[UnityEngine.SerializeField]
		private static UnityEngine.GameObject touchInputPrefab;
	//ENDOF serialized static fields

	//public static properties
		//is touch input enabled
		public static bool touchInputEnabled
		{ get { return UnityEngine.Input.touchSupported; }}

		//controllers per input type
		public static IInputController analogInputController
		{
			get
			{
				if (analogController == null)
				{ PopulateAnalogController(); }
				return analogController;
			}
		}
		public static IInputController touchInputController
		{
			get
			{
				if (touchController == null && touchInputEnabled)
				{ PopulateTouchController(); }
				return touchController;
			}
		}
	//ENDOF public static properties

	//private static properties
	//ENDOF private static properties

	//private static fields
		private static IInputController analogController;
		private static IInputController touchController;
	//ENDOF private static fields

	//private methods
		private static void PopulateAnalogController ()
		{ analogController = new AnalogInputController(); }

		private static void PopulateTouchController ()
		{
			touchController = UnityEngine.Object.FindObjectsOfType<TouchInputController>()[0];
			if (touchController == null)
			{
				UnityEngine.Object.Instantiate(touchInputPrefab);
				touchController = UnityEngine.Object.FindObjectsOfType<TouchInputController>()[0];
			}
		}
	//ENDOF private methods
	}
}