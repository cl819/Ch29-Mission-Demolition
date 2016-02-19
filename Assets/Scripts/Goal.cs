using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	static public bool goalMet = false;

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Projectile") {
			Goal.goalMet = true;
			Color c = GetComponent<Renderer>().material.color;
			c.a = 1;
			GetComponent<Renderer>().material.color = c;
			}
		}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
