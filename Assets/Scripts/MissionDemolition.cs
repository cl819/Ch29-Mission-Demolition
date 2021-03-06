﻿using UnityEngine;
using System.Collections;

public enum GameMode{
	idle,
	playing,
	levelEnd
}

public class MissionDemolition : MonoBehaviour {

	static public MissionDemolition S;

	public GameObject[] castles;
	public GUIText gtLevel;
	public GUIText gtScore;
	public Vector3 castlePos;

	public bool _________________________;

	public int level;
	public int levelMax;
	public int shotsTaken;
	public GameObject castle;
	public GameMode mode=GameMode.idle;
	public string showing = "Slingshot";


	// Use this for initialization
	void Start () {
		S = this;

		level = 0;
		levelMax = castles.Length;
		StartLevel ();
	
	}

	void StartLevel(){
		//get rid of old castle if one exists
		if (castle != null) {
			Destroy (castle);
		}
		//Destroy old projectiles if they exist
		GameObject[]gos=GameObject.FindGameObjectsWithTag("Projectile");
		foreach (GameObject pTemp in gos) {
			Destroy (pTemp);
		}
		//Instantiate the new castle
		castle=Instantiate(castles[level]) as GameObject;
			castle.transform.position=castlePos;
			shotsTaken=0;

		//reset camera
		SwitchView("Both");
		ProjectileLine.S.Clear ();

		//Reset the goal
		Goal.goalMet=false;

		ShowGT ();

		mode = GameMode.playing;
	}

	void ShowGT(){
		gtLevel.text = "Level: " + (level + 1) + " of " + levelMax;
		gtScore.text = "Shots Taken: " + shotsTaken;
	}
	
	// Update is called once per frame
	void Update () {
		ShowGT ();

		//Check for level end
		if (mode == GameMode.playing && Goal.goalMet) {
			//change mode to stop checking for level end
			mode=GameMode.levelEnd;
			//zoom out
			SwitchView("Both");
			//Start the next level in 2 seconds
			Invoke("NextLevel", 2f);
		}
	
	}

	void NextLevel(){
		level++;
		if (level == levelMax) {
			level = 0;
		}
		StartLevel ();
	}

	void OnGUI(){
		//Draw the gui button for view switching at the top of the screen
		Rect buttonRect = new Rect((Screen.width/2)-50, 10 ,100,24);

		switch(showing){
		case"Slingshot":
			if(GUI.Button(buttonRect,"Show Castle")){
				SwitchView("Castle");
			}break;

		case "Castle":
			if(GUI.Button(buttonRect, "Show Both")){
				SwitchView("Both");
			}break;

		case "Both":
			if(GUI.Button(buttonRect,"Show Slingshot")){
				SwitchView("Slingshot");
			}break;
			
		}
	}

	static public void SwitchView(string eView){
		S.showing = eView;
		switch (S.showing) {
		case"Slingshot":
			FollowCam.S.poi = null;
			break;

		case "Castle":
			FollowCam.S.poi = S.castle;
			break;

		case "Both":
			FollowCam.S.poi = GameObject.Find ("ViewBoth");
			break;
		}
	}

	public static void ShotFired(){
		S.shotsTaken++;
	}
}
