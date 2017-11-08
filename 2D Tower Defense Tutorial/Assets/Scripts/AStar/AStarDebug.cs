﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarDebug : MonoBehaviour {
	private TileScript startTile;
	private TileScript goalTile;

	[SerializeField]
	private GameObject arrowPrefab;

	[SerializeField]
	private GameObject debugTilePrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ClickTile ();

		if (Input.GetKeyDown (KeyCode.Space)) {
			if (startTile != null && goalTile != null) {
				AStar.GetPath (startTile.GridPosition, goalTile.GridPosition);
			}
		}
	}

	private void ClickTile(){
		if (Input.GetKeyDown (KeyCode.O)) {
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);

			if (hit.collider != null) {
				//try to get a TileScript from the click
				TileScript tmp = hit.collider.GetComponent<TileScript> ();

				//we hit a Tile!
				if (tmp != null) {
					//first we want to set the start tile
					if (startTile == null) {
						startTile = tmp;
						CreateDebugTile(tmp, new Color32(255, 132, 0, 255));
					} 
					//then we want to set the goal tile
					else if (goalTile == null) {
						goalTile = tmp;
						CreateDebugTile(tmp, new Color32(255, 0, 0, 255));
					}
				}
			}
		}
	}

	public void DebugPath(HashSet<AStarNode> openList, HashSet<AStarNode> closedList, int idx){
		StartCoroutine (DebugPathTimed (openList, closedList, idx));
	}

	private IEnumerator DebugPathTimed(HashSet<AStarNode> openList, HashSet<AStarNode> closedList, int idx){
		int secondsToWait = idx * 1;
		yield return new WaitForSeconds (secondsToWait);

		//change color of openList nodes
		foreach(AStarNode node in openList){
			if (node.TileReference != startTile) {
				CreateDebugTile(node.TileReference, new Color32(0, 0, 215, 255));
				PointToParent (node, node.TileReference.WorldPosition);
			}
		}

		//display closed list nodes
		foreach (AStarNode node in closedList) {
			CreateDebugTile(node.TileReference, new Color32(0, 0, 150, 255));
			PointToParent (node, node.TileReference.WorldPosition);
		}
	}

	private void PointToParent(AStarNode node, Vector2 position){
		if (node.Parent == null) return;

		GameObject arrow = (GameObject) Instantiate (arrowPrefab, position, Quaternion.identity);

		//right
		if (node.gridPosition.X < node.Parent.gridPosition.X && node.gridPosition.Y == node.Parent.gridPosition.Y) {
			arrow.transform.eulerAngles = new Vector3(0, 0, 0);
		}
		//top right
		else if (node.gridPosition.X < node.Parent.gridPosition.X && node.gridPosition.Y > node.Parent.gridPosition.Y) {
			arrow.transform.eulerAngles = new Vector3(0, 0, 45);
		}
		//up
		else if (node.gridPosition.X == node.Parent.gridPosition.X && node.gridPosition.Y > node.Parent.gridPosition.Y) {
			arrow.transform.eulerAngles = new Vector3(0, 0, 90);
		}
		//top left
		else if (node.gridPosition.X > node.Parent.gridPosition.X && node.gridPosition.Y > node.Parent.gridPosition.Y) {
			arrow.transform.eulerAngles = new Vector3(0, 0, 135);
		}
		//left
		else if (node.gridPosition.X > node.Parent.gridPosition.X && node.gridPosition.Y == node.Parent.gridPosition.Y) {
			arrow.transform.eulerAngles = new Vector3(0, 0, 180);
		}
		//bottom left
		else if (node.gridPosition.X > node.Parent.gridPosition.X && node.gridPosition.Y < node.Parent.gridPosition.Y) {
			arrow.transform.eulerAngles = new Vector3(0, 0, 225);
		}
		//bottom
		else if (node.gridPosition.X == node.Parent.gridPosition.X && node.gridPosition.Y < node.Parent.gridPosition.Y) {
			arrow.transform.eulerAngles = new Vector3(0, 0, 270);
		}
		//bottom right
		else if (node.gridPosition.X < node.Parent.gridPosition.X && node.gridPosition.Y < node.Parent.gridPosition.Y) {
			arrow.transform.eulerAngles = new Vector3(0, 0, 315);
		}
	}

	private GameObject CreateDebugTile(TileScript tile, Color32 color){
		GameObject debugTile = (GameObject) Instantiate (debugTilePrefab, tile.WorldPosition, Quaternion.identity);
		debugTile.GetComponent<SpriteRenderer> ().color = color;
		debugTile.GetComponent<SpriteRenderer> ().sortingOrder = tile.GetComponent<SpriteRenderer> ().sortingOrder + 1;

		return debugTile;
	}
}
