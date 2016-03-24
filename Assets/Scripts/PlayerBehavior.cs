using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

	[Range(0, 50)]	
	public float playerSpeed;
	public EnemyBehavior hitEnemy;

	private Transform playerTransform;
	private float playerMovement;

	void Start () {

		playerTransform = GetComponent<Transform>();	
	}
	
	void Update () {

		//change to input.GetAxisRaw("Horizontal") 
		//to capture just left/right w/out smoothing effect....
		MovePlayer(Input.GetAxis("Horizontal"));	
	}

	void FixedUpdate() {

		Debug.Log("raycasting...");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1f);

		if (hit.collider.gameObject.GetComponent<EnemyBehavior>()) 
        {
			hitEnemy = hit.collider.gameObject.GetComponent<EnemyBehavior>();
			hitEnemy.rb.velocity = Vector2.zero;
	    }
    }

	public void MovePlayer(float horizontalInput)
	{
		//Debug.Log(horizontalInput);

		playerMovement += horizontalInput * playerSpeed * Time.deltaTime;
		playerTransform.position = new Vector2(Mathf.Clamp( playerMovement, -5f, 5f ), 0);
	}

	/*
	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.tag == "Enemy")
		{
			 hitEnemy = coll.GetComponent<EnemyBehavior>();
			 hitEnemy.rb.velocity = Vector2.zero;
		}
	}*/


}
