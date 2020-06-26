using UnityEngine;

namespace CatAttack
{		
	public class RespawnCheckpoint : MonoBehaviour
	{
		public Transform _spawnSpot;
		private Animator animator;

		//spawnPosition returns the world coordinates for respawning
		public Vector3 spawnPosition
		{
			get { return _spawnSpot.position; }
		}

		//sets the animation to active or inactive state or gets its state
		public bool active
		{
			get { return animator.GetBool("IsActive"); }
			set { animator.SetBool("IsActive", value); }
		}

		//on creation report ourselves to the level manager and cache our animator
		public void Awake ()
		{
			if (_spawnSpot == null) { _spawnSpot = transform; }
			LevelManager.instance.AddCheckpoint(this);
			animator = GetComponent<Animator>();
		}

		public void OnTriggerEnter2D (Collider2D other)
		{
			//if (other.tag == "Player")
			//{
				LevelManager.instance.SetActiveCheckpoint(this);
			//}
		}
	}
}