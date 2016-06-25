using UnityEngine;
using System.Collections;

public class NpcSpawner : MonoBehaviour {

	public GameObject[] npcs;

	private GameObject npc;
	private float spawnDelay;
	private float randomX;

	void Start () {

		StartCoroutine("Spawn");	
	
	}
	
	void Update () {
	
	}

	private IEnumerator Spawn()
	{
		while(true)
		{     
			spawnDelay = Random.Range(3f, 5f);
			randomX = Random.Range(-2.5f, 2.5f);
			npc = npcs[Random.Range(0, npcs.Length)];
			 
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
