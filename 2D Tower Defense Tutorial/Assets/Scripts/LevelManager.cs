using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	[SerializeField]
	private GameObject[] tilePrefabs;
	[SerializeField]
	private GameObject portalPrefab;

	[SerializeField]
	private CameraMovement cameraMovement;

	private string[] mapData;

	private Point startPortal;
	private Point endPortal;

	public Dictionary<Point, TileScript> Tiles { get; set; }

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
		Tiles = new Dictionary<Point, TileScript> ();

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

		int mapGridX = mapData [0].ToCharArray ().Length;
		int mapGridY = mapData.Length;

		//place the tiles
		for (int y = 0; y < mapGridY; y++) {
			for (int x = 0; x < mapGridX; x++) {
				int tileType = int.Parse(mapData[y].ToCharArray()[x].ToString());
				placeTile (x, y, tileType, worldOrigin);
			}
		}

		Vector3 maxTilePositon = Tiles [new Point (mapGridX - 1, mapGridY - 1)].transform.position;
		cameraMovement.setLimits (new Vector3(maxTilePositon.x + tileWidth,
			maxTilePositon.y - tileHeight));

		createPortals ();
	}

	/// <summary>
	/// Places the tile.
	/// </summary>
	/// <param name="x">The x coordinate of the tile.</param>
	/// <param name="y">The y coordinate of the tile.</param>
	/// <param name="worldOrigin">The origin of the world.</param>
	private void placeTile(int x, int y, int tileType, Vector3 worldOrigin){
		//get the TileScript of the tile to place
		TileScript newTileScript = Instantiate (tilePrefabs[tileType]).GetComponent<TileScript>();
		//calculate the world position of the tile
		Vector3 worldPosition = new Vector3 (worldOrigin.x + (tileWidth * x), worldOrigin.y - (tileHeight * y), 0);

		//coordinate of the Tile in the grid
		Point gridPosition = new Point (x, y);

		//setup the new tile
		newTileScript.Setup (gridPosition, worldPosition);
		Tiles.Add (gridPosition, newTileScript);
	}

	private void createPortals(){
		startPortal = new Point (0, 1);
		endPortal = new Point (19, 9);

		Instantiate (portalPrefab, Tiles[startPortal].WorldPosition, Quaternion.identity);
		Instantiate (portalPrefab, Tiles[endPortal].WorldPosition, Quaternion.identity);
	}
}