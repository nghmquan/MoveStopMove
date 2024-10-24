using UnityEngine;
using System.Collections;

namespace VoxelArsenal
{

public class VoxelTarget : MonoBehaviour
{
    [Header("Effect shown on player hit")]
	public GameObject hitParticle;
    [Header("Effect shown on player respawn")]
	public GameObject respawnParticle;
	private Renderer targetRenderer;
	private Collider targetCollider;

    void Start()
    {
		targetRenderer = GetComponent<Renderer>();
		targetCollider = GetComponent<Collider>();
    }

    void SpawnTarget()
    {
        targetRenderer.enabled = true; //Shows the player
		targetCollider.enabled = true; //Enables the collider
		GameObject respawnEffect = Instantiate(respawnParticle, transform.position, transform.rotation) as GameObject; //Spawns attached respawn effect
		Destroy(respawnEffect, 3.5f); //Removes attached respawn effect after x seconds
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Missile") // If collider is tagged as missile
        {
            if (hitParticle)
            {
				//Debug.Log("Target hit!");
				GameObject destructibleEffect = Instantiate(hitParticle, transform.position, transform.rotation) as GameObject; // Spawns attached hit effect
				Destroy(destructibleEffect, 2f); // Removes hit effect after x seconds
				targetRenderer.enabled = false; // Hides the player
				targetCollider.enabled = false; // Disables player collider
				StartCoroutine(Respawn()); // Sets timer for respawning the player
            }
        }
    }
	
	IEnumerator Respawn()
    {
        yield return new WaitForSeconds(3);
		SpawnTarget();
    }
}
}