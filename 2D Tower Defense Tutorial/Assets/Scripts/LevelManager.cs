using System;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	[SerializeField]
	private GameObject[] tilePrefabs;

	private string[] mapData;

	/// <summary>
	/// Gets the width of the tile.
	/// </summary>
	/// <value>The width of the tile.</value>
	public float tileWidth {
		get{
			return tilePrefabs[0].GetComponent<SpriteRenderer> ().sprite.bounds.size.x;	
		}
	}

	/// <summary>
	/// Gets the height of the tile.
	/// </summary>
	/// <value>The height of the tile.</value>
	public float tileHeight {
		get{
			return tilePrefabs[0].GetComponent<SpriteRenderer> ().sprite.bounds.size.y;	
		}
	}

	// Use this for initialization
	void Start () {
		loadLevel ("level1");
		createLevel ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	/// <summary>
	/// Loads the level.
	/// </summary>
	private void loadLevel(string levelName){
		TextAsset levelRes = Resources.Load ("Levels/" + levelName) as TextAsset;

		string levelString = levelRes.text.Replace (Environment.NewLine, string.Empty);
		mapData = levelString.Split ('-');
	}

	/// <summary>
	/// Creates the level.
	/// </summary>
	private void createLevel(){
		//calculates the world origin to be the top left corner of the camera
		Vector3 worldOrigin = Camera.main.ScreenToWorldPoint (new Vector3 (0, Screen.height));

		//place the tiles
		for (int y = 0; y < mapData.Length; y++) {
			for (int x = 0; x < mapData[0].ToCharArray().Length; x++) {
				int tileType = int.Parse(mapData[y].ToCharArray()[x].ToString());
				placeTile (x, y, tileType, worldOrigin);
			}
		}
	}

	/// <summary>
	/// Places the tile.
	/// </summary>
	/// <param name="x">The x coordinate of the tile.</param>
	/// <param name="y">The y coordinate of the tile.</param>
	/// <param name="worldOrigin">The origin of the world.</param>
	private void placeTile(int x, int y, int tileType, Vector3 worldOrigin){
		GameObject newTile = Instantiate (tilePrefabs[tileType]);
		newTile.transform.position = new Vector3 (worldOrigin.x + (tileWidth * x), 
			worldOrigin.y - (tileHeight * y), 0);
	}
}
