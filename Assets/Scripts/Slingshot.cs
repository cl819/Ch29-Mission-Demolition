using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {

	static public Slingshot S;

	public GameObject prefabProjectile;
	public float velocityMult = 4f;
	public bool _____________;

	public GameObject launchPoint;
	public Vector3 launchPos;
	public GameObject projectile;
	public bool aimingMode;

	void Awake(){
		//Set the slingshot singleton S
		S=this;

		Transform launchPointTrans = transform.Find ("LaunchPoint");
		launchPoint = launchPointTrans.gameObject;
		launchPoint.SetActive (false);
		launchPos = launchPointTrans.position;
	}

	void OnMouseEnter() {
		print ("Slingshot:OnMouseEnter()");
		launchPoint.SetActive (true);
	}

	void OnMouseExit(){
		print ("Slingshot:OnMouseExit()");
		launchPoint.SetActive (false);
	}

	void OnMouseDown(){
		//the player has pressed the mouse button while over slingshot
		aimingMode=true;

		//instantiate a projectile
		projectile = Instantiate(prefabProjectile) as GameObject;

		//start at launch point
		projectile.transform.position=launchPos;

		//set it to iskinematic
		//projectile.rigidbody.isKinematic=true;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	//slingshot not aiming don't run this code
		if(!aimingMode)return;

		Vector3 mousePos2D = Input.mousePosition;
		mousePos2D.z = -Camera.main.transform.position.z;

		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint (mousePos2D);

		Vector3 mouseDelta = mousePos3D - launchPos;
		float maxMagnitude = this.GetComponent<SphereCollider> ().radius;
		if (mouseDelta.magnitude > maxMagnitude) {
			mouseDelta.Normalize ();
			mouseDelta *= maxMagnitude;
		}

		//move projectile to new position
		Vector3 projPos = launchPos+mouseDelta;
		projectile.transform.position = projPos;

		if (Input.GetMouseButtonUp (0)) {
			//mouse released
			aimingMode=false;
			projectile.GetComponent<Rigidbody>().isKinematic = false;
			projectile.GetComponent<Rigidbody>().velocity = -mouseDelta * velocityMult;
			FollowCam.S.poi = projectile;
			projectile = null;
		}
	}
}
