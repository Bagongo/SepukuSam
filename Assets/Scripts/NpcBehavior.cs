using UnityEngine;
using System.Collections;

public class NpcBehavior : MonoBehaviour {

	public Rigidbody2D rb;
	public bool isAlive;

	private SpriteRenderer sprRndr;


	[Range(0, 10)]
	public int npcSpeed;


	void Start () {

		rb = GetComponent<Rigidbody2D>();
		rb.velocity =  new Vector2 (0, - npcSpeed);

		sprRndr = GetComponent<SpriteRenderer>();
		sprRndr.flipX = Random.value <= 0.5f;	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
