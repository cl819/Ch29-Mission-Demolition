using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {

	public float easing =0.05f;
	public Vector2 minXY;

	static public FollowCam S;

	public bool _______;

	public GameObject poi;

	public float camZ;

	void Awake(){
		S = this;
		camZ = this.transform.position.z;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Vector3 destination;
		//if there is no poi return to p:(0,0,0)
		if (poi == null) {
			destination = Vector3.zero;
		} else {
			//get position of poi
			destination = poi.transform.position;

			if (poi.tag == "Projectile") {
				if (poi.GetComponent<Rigidbody>().IsSleeping ()) {
					poi = null;
					return;
				}
			}
		}



		//limit the X & Y to minimum values
		destination.x=Mathf.Max(minXY.x, destination.x);
		destination.y = Mathf.Max (minXY.y, destination.y);

		destination = Vector3.Lerp (transform.position, destination, easing);
		destination.z = camZ;
		transform.position = destination;

		//keep ground veiw
		this.GetComponent<Camera>().orthographicSize=destination.y+10;
	}
}
