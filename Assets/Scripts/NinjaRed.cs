using UnityEngine;
using System.Collections;


public class NinjaRed : MonoBehaviour {

	private EnemyBehavior mainScr;

	// Use this for initialization
	void Start () {

		mainScr = GetComponent<EnemyBehavior>();

		StartCoroutine("RayCast5PerSec");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private IEnumerator RayCast5PerSec() 
	{
		while(true)
		{
			RaycastHit2D hit;
			float rayXPos = transform.position.x - mainScr.enemyWidth / 2;

			Debug.Log("Casting");


			for(int i=0; i<=2; i++)
			{
				Vector3 rayPos = new Vector3 (rayXPos, transform.position.y, 0); 
				hit = Physics2D.Raycast(rayPos, Vector2.down, 50f);

				if (hit && hit.collider.gameObject.GetComponent<PlayerBehavior>() && hit.distance >= 2.0f) 
		        {
					if(mainScr.dodgeSkill > 0 && mainScr.Choose(new float[]{100f - 10*mainScr.dodgeSkill, 10f*mainScr.dodgeSkill}) == 1)
					{
						StopCoroutine("RayCast5PerSec");
						StartCoroutine("NinjaLeap");
						yield break;
					}
			    }

				rayXPos += mainScr.enemyWidth / 2;
		    }

			yield return new WaitForSeconds(0.2f);
		}
	}

	private IEnumerator NinjaLeap()
    {
		mainScr.rb.velocity = Vector2.zero;

		while(mainScr.isAlive)
		{
			float tempOpacity = mainScr.enemysprite.color.a - 0.2f;
			mainScr.enemysprite.color = new Color(mainScr.enemysprite.color.r, mainScr.enemysprite.color.g, mainScr.enemysprite.color.b, tempOpacity);

			if(mainScr.enemysprite.color.a <= 0.0f)
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
			mainScr.rb.velocity =  new Vector2 (0, - mainScr.enemySpeed);
			float tempOpacity = mainScr.enemysprite.color.a + 0.2f;
			mainScr.enemysprite.color = new Color(mainScr.enemysprite.color.r, mainScr.enemysprite.color.g, mainScr.enemysprite.color.b, tempOpacity);

			if(mainScr.enemysprite.color.a >= 1.0f)
				StopCoroutine("NinjaLeap2");

			yield return new WaitForSeconds(0.1f); 
		}		
    }
}
