using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	[Range(0, 10)]
	public int enemySpeed;
	[Range(0,5)]
	public int spawnDelay;
	[Range(0,5)]
	public int dodgeSkill;
	public Sprite[] enmDisguise;
	public Rigidbody2D rb;
	public bool isAlive;
	public GameObject blooddrip;

	private bool lerpVelocity;
	private Vector2 residualVel;
	private float enemyWidth;
	private SpriteRenderer enemysprite;
	private IEnumerator coroutine;
	private ScoreMan scoreMan;
	private Animator anim;
	private PlayerBehavior player;
	private GameObject blood;

	void Start () {

		rb = GetComponent<Rigidbody2D>();
		enemyWidth = GetComponent<BoxCollider2D>().size.x;
		enemysprite = GetComponent<SpriteRenderer>();
		scoreMan = FindObjectOfType<ScoreMan>();
		anim = GetComponent<Animator>();
		player = FindObjectOfType<PlayerBehavior>();

		if(enmDisguise.Length > 0 || !enemysprite.sprite)
		{
			enemysprite.sprite = enmDisguise[Random.Range(1, enmDisguise.Length - 1)];
			coroutine = NinjaMutation(1);
			StartCoroutine(coroutine);
		}

		isAlive = true;
		rb.velocity =  new Vector2 (0, - enemySpeed);

		StartCoroutine("RayCast5PerSec");
	}
	
	void Update () {

		if(lerpVelocity)
			DecelerateEnemy(Random.Range(1f, 2f));
	}

	private IEnumerator RayCast5PerSec() 
	{
		while(isAlive)
		{
			RaycastHit2D hit;
			float rayXPos = transform.position.x - enemyWidth / 2;

			for(int i=0; i<=2; i++)
			{
				Vector3 rayPos = new Vector3 (rayXPos, transform.position.y, 0); 
				hit = Physics2D.Raycast(rayPos, Vector2.down, 50f);

				if (hit && hit.collider.gameObject.GetComponent<PlayerBehavior>() && hit.distance >= 2.0f) 
		        {
					if(dodgeSkill > 0 && Choose(new float[]{100f - 10*dodgeSkill, 10f*dodgeSkill}) == 1)
					{
						StopCoroutine("RayCast5PerSec");
						StartCoroutine("NinjaLeap");
						yield break;
					}
			    }

			    rayXPos += enemyWidth / 2;
		    }

			yield return new WaitForSeconds(0.2f);
		}
	}

	public void killEnemy()
	{

		blood = (GameObject)Instantiate(blooddrip, transform.position, Quaternion.identity);
		blood.transform.parent = transform;

		if(transform.position.x < player.transform.position.x)
			transform.Rotate(0, 0, 90);
		else
			transform.Rotate(0, 0, -90);

		Vector2 newVel = transform.position - player.transform.position;
		rb.velocity = new Vector2(newVel.x * 2.5f, -enemySpeed * 1.4f); // tweak values to make closer to player / properly spread heap........?
		residualVel = rb.velocity;

		isAlive = false;
		anim.Stop();
		anim.SetBool("isAlive", isAlive);
		lerpVelocity = true;

		scoreMan.UpdateScore(1); //add enemyValue public var for possible multi-type scoring system.....
	}

	private void DecelerateEnemy(float decTimeinSecs)
	{
		//Lerping is currently clamped to minimum enemy1 velocity(use global check variable???) 
		rb.velocity = new Vector2(rb.velocity.x - residualVel.x * decTimeinSecs/60, rb.velocity.y - residualVel.y * decTimeinSecs/60 );
		//transform.position = new Vector2(transform.position.x, transform.position.y);

		if(rb.velocity.y >= 0)
		{
			lerpVelocity = false;
			rb.velocity = new Vector2(0,0);
		}
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
            else 
            {
                randomPoint -= probs[i];
            }
        }

        return probs.Length - 1;
    }

	private IEnumerator NinjaLeap()
    {
		rb.velocity = Vector2.zero;

		while(true)
		{
			float tempOpacity = enemysprite.color.a - 0.2f;
			enemysprite.color = new Color(enemysprite.color.r, enemysprite.color.g, enemysprite.color.b, tempOpacity);

			if(enemysprite.color.a <= 0.0f)
			{
				if(transform.position.x < 0)
					transform.position = new Vector2(transform.position.x + Random.Range(1f, 2f), transform.position.y);
				else
					transform.position = new Vector2(transform.position.x - Random.Range(1f, 2f), transform.position.y);
																					
				StopCoroutine("NinjaLeap");
				StartCoroutine("NinjaLeap2");
			}

			yield return new WaitForSeconds(0.1f); 
		} 
	}		 

	private IEnumerator NinjaLeap2()
    {
		while(true)
		{
			rb.velocity =  new Vector2 (0, - enemySpeed);
			float tempOpacity = enemysprite.color.a + 0.2f;
			enemysprite.color = new Color(enemysprite.color.r, enemysprite.color.g, enemysprite.color.b, tempOpacity);

			if(enemysprite.color.a >= 1.0f)
				StopCoroutine("NinjaLeap2");

			yield return new WaitForSeconds(0.1f); 
		}		
    }

	private IEnumerator NinjaMutation(int delay)
    {
		yield return new WaitForSeconds(delay);

		rb.velocity = Vector2.zero;

		while(true)
		{
			float tempOpacity = enemysprite.color.a - 0.2f;
			enemysprite.color = new Color(enemysprite.color.r, enemysprite.color.g, enemysprite.color.b, tempOpacity);

			if(enemysprite.color.a <= 0.0f)
			{
				enemysprite.sprite = enmDisguise[0];																
				StopCoroutine(coroutine);
				StartCoroutine("NinjaMutation2");
			}

			yield return new WaitForSeconds(0.1f); 
		} 
	}

	private IEnumerator NinjaMutation2()
    {
		while(true)
		{
			float tempOpacity = enemysprite.color.a + 0.2f;
			enemysprite.color = new Color(enemysprite.color.r, enemysprite.color.g, enemysprite.color.b, tempOpacity);

			if(enemysprite.color.a >= 1.0f)
			{
				StopCoroutine("NinjaLeap2");
				rb.velocity =  new Vector2 (0, - enemySpeed);
			}

			yield return new WaitForSeconds(0.1f); 
		}
	}			  

}
