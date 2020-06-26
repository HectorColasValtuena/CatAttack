﻿using UnityEngine;

public class RespawnCheckpoint : MonoBehaviour
{
	public Transform _spawnSpot;
	private Animator animator;

	//spawnPosition returns the world coordinates for respawning
	public Vector3 spawnSpot
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
	public void Awake()
	{
		LevelManager.instance.AddCheckpoint(this);
		animator = GetComponent<Animator>();
	}
}
