using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemy;

	void Start () {

		StartCoroutine("Spawn");
	
	}
	
	void Update () {

//		if (isTimeToSpawn(enemy))
//			SpawnEnemy();
	}


	private IEnumerator Spawn()
	{
		
		EnemyBehavior ninja = enemy.GetComponent<EnemyBehavior>();

		float spawnDelay;
		float randomX;

		while(true)
		{     
			spawnDelay = Random.Range(1f,3f) + ninja.spawnDelay;
			randomX = Random.Range(-2.5f, 2.5f); 

			yield return new WaitForSeconds(spawnDelay);

			Instantiate(enemy, new Vector2(randomX, transform.position.y), Quaternion.identity);
		}

	}

	/*public void SpawnEnemy()
	{

		float randomX = Mathf.Round(Random.Range(-5, 5));

		Instantiate(enemy, new Vector3(randomX, this.transform.position.y), Quaternion.identity);
	}


	bool isTimeToSpawn(GameObject enemy)
	{
		EnemyBehavior ninja = enemy.GetComponent<EnemyBehavior>();
		
		float meantSpawnDelay = ninja.spawnDelay;

		if (Time.deltaTime > meantSpawnDelay)
		{
			Debug.LogWarning("Spawn rate capped by frame-rate!!!!");
			return false;
		}
		
		float threshold = meantSpawnDelay * Time.deltaTime /10;

		return (Random.value < threshold);
			
	}*/

}
