using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

	/// <summary>
	/// Gets or sets the grid position.
	/// </summary>
	/// <value>The grid position.</value>
	public Point GridPosition { get; private set; }

	public Vector2 WorldPosition {
		get{
			Vector3 spriteSize = GetComponent<SpriteRenderer> ().bounds.size;
			return new Vector2 (transform.position.x + spriteSize.x / 2, transform.position.y - spriteSize.y / 2);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Setup(Point gridPosition, Vector3 worldPosition){
		GridPosition = gridPosition;
		transform.position = worldPosition;
	}
}
