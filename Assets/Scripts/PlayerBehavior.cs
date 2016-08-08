using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

	[Range(0, 20)]	
	public float playerSpeed;
	public EnemyBehavior hitEnemy;
	public float movementRange = 3f;
	public Text gameOver; 

	private Transform playerTransform;

	private float nextPosX;
	private float pointer_x;
	private Animator anim;
	private Vector2 touchStartPos = Vector2.zero;
	private Animator goverAnim;


	void Start () {

		playerTransform = GetComponent<Transform>();
		anim = GetComponent<Animator>();

		goverAnim = gameOver.GetComponent<Animator>();
	}
	
	void Update () {

		if(Input.GetAxis("Horizontal") != 0)
			MovePlayer(Input.GetAxis("Horizontal"));

		if (Input.touchCount > 0)
			MovePlayer(TouchInput());
		
	}

	void OnTriggerEnter2D(Collider2D coll)
	{

		if(coll.gameObject.GetComponent<EnemyBehavior>())
		{
			hitEnemy = coll.gameObject.GetComponent<EnemyBehavior>();

			if(hitEnemy.isAlive)
				hitEnemy.killEnemy();
		}
		else if(coll.gameObject.GetComponent<NpcBehavior>())
		{
			Debug.Log("Game Over....!");
			goverAnim.SetTrigger("gameOver");
		}									
	}

	public void MovePlayer(float horizontalInput)
	{

		//Debug.Log(horizontalInput);

		bool onEdge = playerTransform.position.x <= -movementRange || playerTransform.position.x >= movementRange;
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

		nextPosX = Mathf.Clamp(playerTransform.position.x + (horizontalInput * playerSpeed * Time.deltaTime), -movementRange, movementRange);

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
