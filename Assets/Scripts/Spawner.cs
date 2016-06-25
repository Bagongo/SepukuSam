using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject[] npcs;
	public GameObject[] enemies;

	private GameObject npc;
	private GameObject enemy;

	void Start () {

		StartCoroutine("SpawnEnemies");
		StartCoroutine("SpawnNpcs");	
	
	}
	
	void Update () {
	
	}

	private IEnumerator SpawnEnemies()
	{
		while(true)
		{ 
			enemy = enemies[Random.Range(0, enemies.Length)];
			EnemyBehavior enemyScript = enemy.GetComponent<EnemyBehavior>();		    
			float spawnDelay = Random.Range(1f,3f) + enemyScript.spawnDelay;
			float randomX = Random.Range(-2.5f, 2.5f); 

			yield return new WaitForSeconds(spawnDelay);

			Instantiate(enemy, new Vector2(randomX, transform.position.y), Quaternion.identity);
		}
	}

	private IEnumerator SpawnNpcs()
	{
		while(true)
		{ 
			npc = npcs[Random.Range(0, npcs.Length)]; 
			float spawnDelay = Random.Range(3f, 5f);
			float randomX = Random.Range(-2.5f, 2.5f);
			 
			yield return new WaitForSeconds(spawnDelay);

			Instantiate(npc, new Vector2(randomX, transform.position.y), Quaternion.identity);
		}
	}

	//takes score, time played or other var to evaluate how many npcs...?
	private int howManyNpcs()
	{
		int npcNum = 0;


		return npcNum;	
	}

}
