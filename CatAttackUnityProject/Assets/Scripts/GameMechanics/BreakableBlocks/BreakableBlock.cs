using UnityEngine;

using static PHATASS.Utils.Extensions.LayerMaskExtensions;

namespace CatAttack
{
	public class BreakableBlock : MonoBehaviour
	{
	//Serialized fields
		[Tooltip("Time in seconds that respawning will be disabled when a character is detected in the disabled collider's space")]
		[SerializeField]
		private float respawnBlockTimeOnTrigger = 0.1f;

		[Tooltip("These are the layers that are considered for the purposes of triggering and blocking this block's despawn/respawn. Generally the player mask")]
		[SerializeField]
		private LayerMask consideredLayers;

		[Tooltip("Will multiply the despawning animation's duration by this value")]
		[SerializeField]
		private float despawnAnimationTimeScale = 1.0f;

		[Tooltip("Controlling animator - Will fetch self's if not initialized")]
		[SerializeField]
		private Animator animator;

		[Tooltip("Animator variable name: Platform in breaking state bool")]
		[SerializeField]
		private string animatorVariablePlatformBreaking = "Breaking";

		[Tooltip("This block's collider - Will fetch self's if not initialized")]
		[SerializeField]
		private Collider2D blockCollider;
	//ENDOF Serialized

	//MonoBehaviour lifecycle
		private void Awake ()
		{
			if (this.blockCollider == null) { this.blockCollider = this.GetComponent<Collider2D>(); }
			if (this.animator == null) { this.animator = this.GetComponent<Animator>(); }
		}

		private void Update ()
		{
			if (this.respawnBlocked) { this.respawnBlockedTimer -= Time.deltaTime; }
		}

		private void OnTriggerEnter2D (Collider2D other)
		{ this.TriggeredBy(other.gameObject); }
		private void OnTriggerStay2D (Collider2D other)
		{ this.TriggeredBy(other.gameObject); }
	//ENDOF MonoBehaviour

	//Private properties
		private bool respawnBlocked { get { return this.respawnBlockedTimer > 0f; }}
	//ENDOF Private properties

	//Private fields
		private float respawnBlockedTimer = 0.0f;
	//ENDOF Private fields

	//Private methods

	  //these methods handle the temporary blockage of respawning while an object is occupying it's space
		private void TriggeredBy (GameObject triggerer)
		{
			//only go through if the object entering our trigger is of an appropriate layer
			if (this.consideredLayers.EContainsLayer(triggerer.layer))
			{ this.SetRespawnBlockedTimer(); }
		}

		private void SetRespawnBlockedTimer ()
		{ this.respawnBlockedTimer = this.respawnBlockTimeOnTrigger; }
	  //ENDOF respawn blockage methods
	//ENDOF Private methods
		
	}
}