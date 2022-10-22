using UnityEngine;

using static PHATASS.Utils.Extensions.LayerMaskExtensions;

namespace CatAttack
{
	public class BreakableBlock : MonoBehaviour
	{
	//Serialized fields
		/*
		[Tooltip("Will multiply the despawning animation's duration by this value")]
		[SerializeField]
		private float despawnAnimationTimeScale = 1.0f;
		*/

		[Tooltip("Time in seconds that respawning will be disabled when a character is detected in the disabled collider's space")]
		[SerializeField]
		private float respawnBlockTimeOnTrigger = 0.1f;

		[Tooltip("Time in seconds to wait before re-spawning the platform")]
		[SerializeField]
		private float respawnDelay = 3f;

		[Tooltip("These are the layers that are considered for the purposes of triggering and blocking this block's despawn/respawn. Generally the player mask")]
		[SerializeField]
		private LayerMask consideredLayers;

		[Tooltip("Controlling animator - Will fetch self's if not initialized")]
		[SerializeField]
		private Animator animator;

		[Tooltip("Animator variable name: Platform in cracking state bool")]
		[SerializeField]
		private string isCrackingAnimatorVarName = "IsCracking";

		[Tooltip("Animator variable name: Platform is broken bool")]
		[SerializeField]
		private string isBrokenAnimatorVarName = "IsBroken";

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
			if (this.isBroken)
			{
				this.respawnTimer -= Time.deltaTime;
				if (this.respawnTimer <= 0 && !this.respawnBlocked)
				{
					this.Respawn();
				}
			}
		}

		private void OnCollisionEnter2D (Collision2D collision)
		{ this.CollidedBy(collision.gameObject); }
		private void OnCollisionStay2D (Collision2D collision)
		{ this.CollidedBy(collision.gameObject); }

		private void OnTriggerEnter2D (Collider2D other)
		{ this.TriggeredBy(other.gameObject); }
		private void OnTriggerStay2D (Collider2D other)
		{ this.TriggeredBy(other.gameObject); }
	//ENDOF MonoBehaviour

	//Animator events
		//this MUST be called once the animator completes the cracking animator to actually pass to broken state
		public void OnCrackingAnimationCompleted ()
		{ this.SetBroken(); }
	//ENDOF Animator events

	//Private properties
		private bool respawnBlocked { get { return this.respawnBlockedTimer > 0f; }}

	  //animator variable setters
		private bool animatorIsCracking { set {this.animator.SetBool(isCrackingAnimatorVarName, value); }}
		private bool animatorIsBroken { set {this.animator.SetBool(isBrokenAnimatorVarName, value); }}
	  //ENDOF animator variable setters
	//ENDOF Private properties

	//Private fields
		private bool isBroken = false;

		private float respawnTimer = 0.0f;
		private float respawnBlockedTimer = 0.0f;
	//ENDOF Private fields

	//Private methods
		private void CollidedBy (GameObject other)
		{
			if (this.consideredLayers.EContainsLayer(other.layer))
			{ this.animatorIsCracking = true; }
		}

		private void SetBroken ()
		{
			this.animatorIsBroken = true;
			this.respawnTimer = respawnDelay;

			this.isBroken = true;
		}

		private void Respawn ()
		{
			this.animatorIsCracking = false;
			this.animatorIsBroken = false;

			this.isBroken = false;
		}

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