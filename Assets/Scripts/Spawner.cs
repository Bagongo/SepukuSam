using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject enemy;

	void Start () {

	}
	
	void Update () {

		if (isTimeToSpawn(enemy))
			SpawnEnemy();
	}


	public void SpawnEnemy()
	{

		float randomX = Mathf.Round(Random.Range(-5, 5));

		Instantiate(enemy, new Vector3(randomX, this.transform.position.y), Quaternion.identity);
	}

	bool isTimeToSpawn(GameObject enemy)
	{
		EnemyBehavior ninja = enemy.GetComponent<EnemyBehavior>();
		
		float meantSpawnDelay = ninja.spawnFrequency;
		float spawnsPerSeconds = 1 / ninja.spawnFrequency;
		
		if (Time.deltaTime > meantSpawnDelay)
		{
			Debug.LogWarning("Spawn rate capped by frame-rate!!!!");
			return false;
		}
		
		float threshold = spawnsPerSeconds * Time.deltaTime / 4;

		print(threshold + " " + spawnsPerSeconds);
		
		return (Random.value < threshold);
			
	}

}
