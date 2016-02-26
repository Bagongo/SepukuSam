using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	[Range(0, 10)]
	public int enemySpeed;

	[Range(0,5)]
	public int spawnDelay;

	private Rigidbody2D rb;

	void Start () {

		rb = GetComponent<Rigidbody2D>();
		rb.velocity =  new Vector2 (0, - enemySpeed);
	}
	
	void Update () {
			
	}
}
