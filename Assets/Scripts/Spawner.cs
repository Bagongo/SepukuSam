using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject[] npcs;
	public GameObject[] enemies;
	public float Lboundary;
	public float Rboundary;

	private float gameFieldX;
	private GameObject npc;
	private GameObject enemy;

	void Start () {

		gameFieldX = Mathf.Abs(Lboundary) + Mathf.Abs(Rboundary);

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
			float randomX = Random.Range(Lboundary, Rboundary); 

			yield return new WaitForSeconds(spawnDelay);

			Instantiate(enemy, new Vector2(randomX, transform.position.y), Quaternion.identity);
		}
	}

	private IEnumerator SpawnNpcs()
	{
		while(true)
		{ 
			int npcsQuant = 8; //HowManyNPcs();
			npc = npcs[Random.Range(0, npcs.Length)]; 
			float spawnDelay = Random.Range(3f, 5f);
			float randomX = Random.Range(Lboundary, Rboundary);
			 
			yield return new WaitForSeconds(spawnDelay);

			if(npcsQuant > 1)
				ChainNpcs(npcsQuant);
			else
				Instantiate(npc, new Vector2(randomX, transform.position.y), Quaternion.identity);
		}
	}

	//takes score, time played or other var to evaluate how many npcs...?
	private int HowManyNPcs()
	{
		float[] probabilities = new float[]{50f, 45f, 40f, 35f, 30f, 25f, 20f, 15f, 15f, 5f};
		return (int) Choose(probabilities) + 1;
	}

	float Choose (float[] probs) 
	{
        float total = 0;

        foreach (float elem in probs) {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i= 0; i < probs.Length; i++) {
            if (randomPoint < probs[i]) {
                return i;
            }
            else {
                randomPoint -= probs[i];
            }
        }
        return probs.Length - 1;
    }

	private void ChainNpcs(int quantity)
	{
		float randomX;
		float fixedY = transform.position.y;
		int spawnDirection;
		float npcWidth = npc.GetComponent<BoxCollider2D>().size.x;
		float npcHeight = npc.GetComponent<BoxCollider2D>().size.y;
		float gutterSize = 0.2f;
		float totalWidth = (npcWidth + gutterSize) * quantity;
		float spawnSpanX = gameFieldX - totalWidth;
		bool skipSingleSpawn = false;

		if(Random.value < 0.5)
		{
			randomX = Random.Range(Lboundary, Lboundary + spawnSpanX);
			spawnDirection = 1;
		}
		else
		{
			randomX = Random.Range(Rboundary - spawnSpanX, Rboundary);
			spawnDirection = -1;
		}

		for(int i=1; i <= quantity; i++)
		{
			
			npc = npcs[Random.Range(0, npcs.Length)];

			if(!skipSingleSpawn)
				Instantiate(npc, new Vector2(randomX, fixedY), Quaternion.identity);

			if(quantity >= 3)
			{
				skipSingleSpawn = Random.value < 0.2f;
				float changeY = Choose(new float[]{50f, 25f, 25f});

				if(changeY == 0)
					fixedY = transform.position.y;
				else if (changeY == 1)
					fixedY += npcHeight;
				else if (changeY == 2)
					fixedY -= npcHeight;				
			}
				
			randomX += (npcWidth + gutterSize) * spawnDirection;
		}
	}

}
