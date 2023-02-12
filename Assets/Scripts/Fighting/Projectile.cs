using System;
using UnityEngine;
using System.Collections;
using InfimaGames.LowPolyShooterPack;
using UnityEngine.Pool;
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

	private IObjectPool<Projectile> objectPool;
	private bool releasedToPool = false;

	[SerializeField] private ParticleSystem impactParticles;
	private void OnEnable ()
	{
		//Start destroy timer
		StartCoroutine (DestroyAfter ());
		releasedToPool = false;
	}

	public void SetPool(IObjectPool<Projectile> pOjectPool)
	{
		
		objectPool = pOjectPool;
	}
	
	//If the bullet collides with anything
	private void OnCollisionEnter (Collision collision)
	{
		//Ignore collisions with other projectiles.
		if (collision.gameObject.GetComponent<Projectile>() != null)
			return;
		//Instantiate(impactParticles, collision.transform.position, collision.transform.rotation);
		//impactParticles.Play();
		Damageable damageable = collision.gameObject.transform.root.GetComponentInChildren<Damageable>();
		
		if (damageable && damageable.enabled)
		{

			Debug.Log("Dealing damage to " + collision.gameObject);
				collision.gameObject.transform.root.GetComponentInChildren<Damageable>().TakeDamage(damage);
			

		}
		else
		{
			Debug.Log("Hit object "+collision.gameObject+" is not damageable.");
		}

		//Instantiate(metalImpactPrefabs[0], collision.transform.position, collision.transform.rotation);
		
		DestroyBullet();
	}

	private void ReleaseGameObject()
	{
		if (releasedToPool) return;
		releasedToPool = true;
		objectPool.Release(this);
	}

	private IEnumerator DestroyTimer () 
	{
		//Wait random time based on min and max values
		yield return new WaitForSeconds
			(Random.Range(minDestroyTime, maxDestroyTime));
		//Destroy bullet object
		DestroyBullet();
	}

	private IEnumerator DestroyAfter () 
	{
		//Wait for set amount of time
		yield return new WaitForSeconds (destroyAfter);
		//Destroy bullet object
		DestroyBullet();
	}

	private void DestroyBullet()
	{
		if(objectPool==null) Destroy(gameObject);
		else ReleaseGameObject();
	}
}