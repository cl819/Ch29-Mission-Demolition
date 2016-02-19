using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileLine : MonoBehaviour {
	static public ProjectileLine S;

	//fields set
	public float minDist=0.1f;
	public bool _______________;

	public LineRenderer line;
	public GameObject _poi;
	public List<Vector3> points;

	void Awake(){
		S = this;

		line = GetComponent<LineRenderer> ();
		line.enabled = false;

		points = new List<Vector3> ();
	}

	public GameObject poi {
		get{
			return (_poi);
		}
		set{
			_poi = value;
			if (_poi != null) {
				line.enabled = false;
				points = new List<Vector3> ();
				AddPoint ();
			}
		}
	}

	public void Clear(){

		_poi = null;
		line.enabled = false;
		points = new List<Vector3> ();
	}

	public void AddPoint(){
		//this is called to add a point to the line
		Vector3 pt=_poi.transform.position;

		if (points.Count > 0 && (pt - lastPoint).magnitude < minDist) {
			return;
		}

		if (points.Count == 0) {
			//if this is launch point
			Vector3 launchPos = Slingshot.S.launchPoint.transform.position;
			Vector3 launchPosDiff = pt - launchPos;

			points.Add (pt + launchPosDiff);
			points.Add (pt);
			line.SetVertexCount (2);
			line.SetPosition (0, points [0]);
			line.SetPosition (1, points [1]);

			line.enabled = true;
		} else {
			points.Add (pt);
			line.SetVertexCount (points.Count);
			line.SetPosition (points.Count - 1, lastPoint);
			line.enabled = true;
		}
	}

	public Vector3 lastPoint{
		get{
			if (points == null) {
				return(Vector3.zero);
			}
			return (points [points.Count - 1]);
		}
	}

	void FixedUpdate(){
		if (poi == null) {
			if (FollowCam.S.poi != null) {
				if (FollowCam.S.poi.tag == "Projectile") {
					poi = FollowCam.S.poi;
				} else {
					return;// return if we didn't find a poi
				}
			} else {
				return; //if we didn't find a poi
			}
			AddPoint ();
			if (poi.GetComponent<Rigidbody>().IsSleeping ()) {
				poi = null;
			}
		}

	}



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
