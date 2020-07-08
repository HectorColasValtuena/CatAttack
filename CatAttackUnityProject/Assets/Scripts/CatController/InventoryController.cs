using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
	//object containing carried dynamite sprite
	public GameObject dynamiteFX;

	public bool _dynamite = false;
	public bool hasDynamite 
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

}
