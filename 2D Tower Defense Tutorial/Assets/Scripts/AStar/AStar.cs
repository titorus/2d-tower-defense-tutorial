using System.Collections.Generic;
using UnityEngine;

public class AStar {

	private static Dictionary<Point, AStarNode> nodes;

	private static AStarDebug debugger;

	/// <summary>
	/// Creates the nodes on which we will perform the A* Algorithm.
	/// </summary>
	private static void CreateNodes(){
		nodes = new Dictionary<Point, AStarNode> ();

		debugger = GameObject.Find ("Debug").GetComponent<AStarDebug> ();

		foreach (TileScript tile in LevelManager.Instance.Tiles.Values) {
			nodes.Add (tile.GridPosition, new AStarNode(tile));
		}
	}

	public static void GetPath(Point start, Point goal){
		if (nodes == null) CreateNodes ();

		HashSet<AStarNode> openList = new HashSet<AStarNode> ();
		HashSet<AStarNode> closedList = new HashSet<AStarNode> ();

		AStarNode currentNode = nodes [start];
		openList.Add (currentNode);

		//add neighbouring nodes from starting node to the openlist
		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				Point neighbourPos = new Point (currentNode.gridPosition.X - x, currentNode.gridPosition.Y - y);

				//same as the node we look at?
				if (neighbourPos == currentNode.gridPosition) continue;
				//is a tile at the calculated neighbour position? (boundary check)
				if (!LevelManager.Instance.Tiles.ContainsKey (neighbourPos)) continue; 
				//is the tile walkable?
				if (!LevelManager.Instance.Tiles [neighbourPos].IsWalkable) continue;

				AStarNode neighbour = nodes [neighbourPos];
				neighbour.CalcValues (currentNode);

				if (!openList.Contains (neighbour)) {
					openList.Add (neighbour);
				}
			}
		}

		//Debug the Neighbours of the Starting Node
		if (isDebugActive ()) {
			debugger.DebugPath (new HashSet<AStarNode>(openList), 1);
		}
	}

	private static bool isDebugActive(){
		return debugger != null;
	}
}