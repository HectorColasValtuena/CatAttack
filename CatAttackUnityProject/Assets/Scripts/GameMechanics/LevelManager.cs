using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatAttack
{		
	public class LevelManager : MonoBehaviour
	{
	//public values
		public static LevelManager instance;
		public static PlatformerCharacter2D playerGameObject;	//reference to the player
		public static Transform cameraFocusTarget = null;		//object to be focused by the camera

		public float lethalDropoffHeight = -100f;	//dropping below this vertical position means death

	//ENDOF public values

	//In-Class management

		//Save a public reference to this instance on creation
		public void Awake ()
		{
			LevelManager.instance = this;
			cameraFocusTarget = null;
			checkpointList = new List<RespawnCheckpoint>();
			if (activeCheckpoint == null) { Debug.LogError("LevelManager.activeCheckpoint initial value has not been set. Set it up within the inspector."); }
		}
	//ENDOF In-Class management

	//Checkpoint management
		//Checkpoint currently in use. Getting the active checkpoint coordinates will return this checkpoints spawn spot
		public RespawnCheckpoint activeCheckpoint;
		//get the coordinates for re-spawning
		public Vector3 respawnSpot { get { return activeCheckpoint.spawnPosition; } }
		//list of checkpoints available in the world
		private List<RespawnCheckpoint> checkpointList;

		//set the target checkpoint as the active checkpoint. Every other checkpoint will be deactivated.
		//every subsequent request for the active checkpoint or its coordinates will return target checkpoint.
		public void SetActiveCheckpoint (RespawnCheckpoint targetCheckpoint)
		{
			activeCheckpoint = targetCheckpoint;
			foreach (RespawnCheckpoint checkpoint in checkpointList)
			{
				//if checkpoint is our target, set active to true. false otherwise.
				checkpoint.active = checkpoint == targetCheckpoint;
			}
		}

		//add a new reference to a world checkpoint
		public void AddCheckpoint (RespawnCheckpoint targetCheckpoint)
		{
			checkpointList.Add(targetCheckpoint);
		}
	//ENDOF Checkpoint management

	//Reset management
		public void ResetPlayer ()
		{
			LevelManager.playerGameObject.ResetPlayer();
		}

	//ENDOF Reset management

	//Level Beaten management
		public GameObject endLevelDialog;

		public void ShowEndLevelDialog ()
		{
			if (endLevelDialog != null)	{ endLevelDialog.SetActive(true); }
		}

		public void AdvanceLevel ()
		{
			ProgressionManager.AdvanceLevel ();
		}
	//ENDOF Level Beaten management
	}
}