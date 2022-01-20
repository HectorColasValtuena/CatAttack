using UnityEngine;

namespace CatAttack
{
	public class InventoryController : MonoBehaviour
	{
	//Sombrero
		//[TO-DO]==================================================================
		/*public bool Sombrero
		{
		}
		private bool _sombrero = false;
		*/
	//ENDOF Sombrero

	//Dynamite quest item
		[SerializeField]
		private GameObject dynamiteFX;
		public bool Dynamite 
		{
			get { return _dynamite; }
			set
			{
				_dynamite = value;
				if (dynamiteFX != null)
				{
					dynamiteFX.SetActive(value);
				}
			}
		}
		private bool _dynamite = false;
	//ENDOF Dynamite

		public void Awake ()
		{
			this.dynamiteFX.SetActive(this.Dynamite);
		}
	}
}