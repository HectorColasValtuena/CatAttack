namespace CatAttack.Input
{
	public class InputControllerCache : UnityEngine.MonoBehaviour
	{
	//serialized fields
		[UnityEngine.SerializeField]
		private UnityEngine.GameObject _touchInputPrefab;
		private static UnityEngine.GameObject touchInputPrefab;
	//ENDOF serialized static fields

	//MonoBehaviour lifecycle
		private void Awake ()
		{
			touchInputPrefab = this._touchInputPrefab;
			touchController = null;
		}
	//ENDOF MonoBehaviour		

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
			TouchInputController[] foundControllers = UnityEngine.Object.FindObjectsOfType<TouchInputController>();
			if (foundControllers.Length >= 1) { touchController = foundControllers[0]; }
			
			if (touchController == null)
			{
				UnityEngine.GameObject instance = UnityEngine.Object.Instantiate(touchInputPrefab);
				touchController = instance.GetComponent<TouchInputController>();
			}
		}
	//ENDOF private methods
	}
}