﻿using System;
using UnityEngine;
using System.Collections;
using InfimaGames.LowPolyShooterPack;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour {

	[Range(5, 100)]
	[Tooltip("After how long time should the bullet prefab be destroyed?")]
	public float destroyAfter;
	[Tooltip("If enabled the bullet destroys on impact")]
	public bool destroyOnImpact = false;
	[Tooltip("Minimum time after impact that the bullet is destroyed")]
	public float minDestroyTime;
	[Tooltip("Maximum time after impact that the bullet is destroyed")]
	public float maxDestroyTime;

	[Tooltip("The amount damage the bullet deals upon hitting a target.")]
	public int damage=15;
	
	[Header("Impact Effect Prefabs")]
	public Transform [] bloodImpactPrefabs;
	public Transform [] metalImpactPrefabs;
	public Transform [] dirtImpactPrefabs;
	public Transform []	concreteImpactPrefabs;

	private void Start ()
	{
		//Grab the game mode service, we need it to access the player character!
		var gameModeService = ServiceLocator.Current.Get<IGameModeService>();
		//Ignore the main player character's collision. A little hacky, but it should work.
		Physics.IgnoreCollision(gameModeService.GetPlayerCharacter().GetComponent<Collider>(), GetComponent<Collider>());
		
		//Start destroy timer
		StartCoroutine (DestroyAfter ());
	}

	//If the bullet collides with anything
	private void OnCollisionEnter (Collision collision)
	{
		//Ignore collisions with other projectiles.
		if (collision.gameObject.GetComponent<Projectile>() != null)
			return;

		if (collision.gameObject.GetComponent<Damageable>())
		{
			Debug.Log("Dealing damage...");
			collision.gameObject.GetComponent<Damageable>().TakeDamage(damage);
		}
		else
		{
			Debug.Log("Hit object is not damageable.");
		}
		
		Destroy(gameObject);
	}

	private IEnumerator DestroyTimer () 
	{
		//Wait random time based on min and max values
		yield return new WaitForSeconds
			(Random.Range(minDestroyTime, maxDestroyTime));
		//Destroy bullet object
		Destroy(gameObject);
	}

	private IEnumerator DestroyAfter () 
	{
		//Wait for set amount of time
		yield return new WaitForSeconds (destroyAfter);
		//Destroy bullet object
		Destroy (gameObject);
	}
}