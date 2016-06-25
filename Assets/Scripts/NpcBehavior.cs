using UnityEngine;
using System.Collections;

public class NpcBehavior : MonoBehaviour {

	public Rigidbody2D rb;
	public bool isAlive;

	[Range(0, 10)]
	public int npcSpeed;


	void Start () {

		rb = GetComponent<Rigidbody2D>();
		rb.velocity =  new Vector2 (0, - npcSpeed);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
