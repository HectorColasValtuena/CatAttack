using UnityEngine;

namespace CatAttack
{		
	public class LethalDropoffHeightSetter : MonoBehaviour
	{
		void Awake ()
		{
			LevelManager.instance.lethalDropoffHeight = transform.position.y;
			Destroy(gameObject);
		}
	}
}