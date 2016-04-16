using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

	[Range(0, 50)]	
	public float playerSpeed;
	public EnemyBehavior hitEnemy;

	private Transform playerTransform;
	private float playerWidth;
	private float playerMovement;

	void Start () {

		playerTransform = GetComponent<Transform>();
		playerWidth = playerTransform.GetComponent<BoxCollider2D>().bounds.size.x;

	}
	
	void Update () {

		//change to input.GetAxisRaw("Horizontal") 
		//to capture just left/right w/out smoothing effect....
		MovePlayer(Input.GetAxis("Horizontal"));	
	}

	void FixedUpdate() {

		RaycastHit2D hit;
		float rayXPos = transform.position.x - playerWidth / 2;

		for(int i=0; i<=2; i++)
		{
			Vector3 rayPos = new Vector3 (rayXPos, transform.position.y, 0); 
			hit = Physics2D.Raycast(rayPos, Vector2.up, .3f);

			if (hit && hit.collider.gameObject.GetComponent<EnemyBehavior>()) 
	        {
				hitEnemy = hit.collider.gameObject.GetComponent<EnemyBehavior>();

				if(hitEnemy.isAlive)
					hitEnemy.killEnemy();
				//some alternate ways of killing the enemy: 
				//Destroy(hitEnemy.transform.root.gameObject);
				//hitEnemy.rb.velocity = Vector2.zero;
		    }

		    rayXPos += playerWidth / 2;
		}
    }

	public void MovePlayer(float horizontalInput)
	{
		//Debug.Log(horizontalInput);

		playerMovement += horizontalInput * playerSpeed * Time.deltaTime;
		playerTransform.position = new Vector2(Mathf.Clamp( playerMovement, -5f, 5f ), 0);
	}

}
