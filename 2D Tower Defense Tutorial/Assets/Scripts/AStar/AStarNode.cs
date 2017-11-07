public class AStarNode{
	public Point gridPosition { get; private set; }
	public TileScript TileReference { get; private set; }
	public AStarNode Parent { get; private set;	}

	public AStarNode(TileScript tileReference){
		TileReference = tileReference;
		gridPosition = tileReference.GridPosition;
	}

	public void CalcValues(AStarNode parent){
		Parent = parent;
	}
}