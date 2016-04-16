using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	[Range(0, 10)]
	public int enemySpeed;
	[Range(0,5)]
	public int spawnDelay;
	public Rigidbody2D rb;
	public bool isAlive;

	private bool lerpVelocity;
	private float residualYVel;

	void Start () {

		rb = GetComponent<Rigidbody2D>();
		rb.velocity =  new Vector2 (0, - enemySpeed);

		isAlive = true;
	}
	
	void Update () {

		if(lerpVelocity) 
			DecelearateEnemy(1);

	}

	public void killEnemy()
	{
		if(Random.value >= .5)
			transform.Rotate(0, 0, 90); 
		else
			transform.Rotate(0, 0, -90);

		isAlive = false;
		residualYVel = rb.velocity.y;
		lerpVelocity = true;
	}

	private void DecelearateEnemy(float decTimeinSecs)
	{
		//Lerping is currently clamped to minimum enemy1 velocity(use global check variable???) 
		rb.velocity = new Vector2(0, rb.velocity.y - residualYVel * decTimeinSecs/60 );
		if(rb.velocity.y >= 0)
		{
			lerpVelocity = false;
			rb.velocity = new Vector2(0,0);
		}
	}

}
