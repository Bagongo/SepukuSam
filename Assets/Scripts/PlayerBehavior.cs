using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

	[Range(0, 20)]	
	public float playerSpeed;
	public EnemyBehavior hitEnemy;

	private Transform playerTransform;
	private float playerWidth;
	private float nextPosX;
	private float pointer_x;
	private Animator anim;
	private Vector2 touchStartPos = Vector2.zero;


	void Start () {

		playerTransform = GetComponent<Transform>();
		playerWidth = playerTransform.GetComponent<BoxCollider2D>().bounds.size.x;
		anim = GetComponent<Animator>();

		StartCoroutine("RayCast5PerSec");
	}
	
	void Update () {

		if(Input.GetAxis("Horizontal") != 0)
			MovePlayer(Input.GetAxis("Horizontal"));

		if (Input.touchCount > 0)
			MovePlayer(TouchInput());
		
	}

	private IEnumerator RayCast5PerSec() 
	{
		while(true)
		{
			RaycastHit2D hit;
			float rayXPos = transform.position.x - playerWidth / 2;

			for(int i=0; i<=2; i++)
			{
				Vector3 rayPos = new Vector3 (rayXPos, transform.position.y, 0); 
				hit = Physics2D.Raycast(rayPos, Vector2.up, .3f);
				if(hit) 
		        {
					if(hit.collider.gameObject.GetComponent<EnemyBehavior>())
					{
						hitEnemy = hit.collider.gameObject.GetComponent<EnemyBehavior>();

						if(hitEnemy.isAlive)
							hitEnemy.killEnemy();
					}
					else if(hit.collider.gameObject.GetComponent<NpcBehavior>())
					{
						Debug.Log("Game Over....!");	
						//StopCoroutine("RayCast5PerSec");
						//yield return new WaitForSeconds(2.0f);
					}									
			    }

			    rayXPos += playerWidth / 2;
			}

			yield return new WaitForSeconds(0.2f);
		}
	}

	void FixedUpdate() {

		RaycastHit2D hit;
		float rayXPos = transform.position.x - playerWidth / 2;

		for(int i=0; i<=2; i++)
		{
			Vector3 rayPos = new Vector3 (rayXPos, transform.position.y, 0); 
			hit = Physics2D.Raycast(rayPos, Vector2.up, .3f);
			if(hit) 
	        {
				if(hit.collider.gameObject.GetComponent<EnemyBehavior>())
				{
					hitEnemy = hit.collider.gameObject.GetComponent<EnemyBehavior>();

					if(hitEnemy.isAlive)
						hitEnemy.killEnemy();
				}
				else if(hit.collider.gameObject.GetComponent<NpcBehavior>())
					Debug.Log("Game Over....!");				
		    }

		    rayXPos += playerWidth / 2;
		}
    }

	public void MovePlayer(float horizontalInput)
	{

		Debug.Log(horizontalInput);

		bool onEdge = playerTransform.position.x <= -2.5f || playerTransform.position.x >= 2.5f;
		float animSpeed = Mathf.Clamp(Mathf.Abs(horizontalInput) * 1f, 0.5f, 1f);
		anim.SetFloat("movingSpeed", animSpeed);

		if(horizontalInput < -0.2f && !onEdge)
			anim.SetBool("movingL", true);
		else if (horizontalInput > 0.2f && !onEdge)
			anim.SetBool("movingR", true);
		else
		{
			anim.SetBool("movingL", false);
			anim.SetBool("movingR", false);
		}

		nextPosX = Mathf.Clamp(playerTransform.position.x + (horizontalInput * playerSpeed * Time.deltaTime), -2.5f, 2.5f);

		playerTransform.position = new Vector2(nextPosX, 0);													
	}

	private float TouchInput()
	{
	    foreach(Touch currentTouch in Input.touches)
	    {
	        switch(currentTouch.phase)
	        {
	            case TouchPhase.Began:
	                touchStartPos = Camera.main.ScreenToWorldPoint(currentTouch.position);
	                break;
	            case TouchPhase.Moved:
	                 Vector2 touchToWorld = (Vector2)Camera.main.ScreenToWorldPoint(currentTouch.position);
	                 return Mathf.Clamp((touchToWorld.x - touchStartPos.x) / 2, -1f, 1f);

	        }
	    }

	    return new Vector2(0,0).x;
	}

}
