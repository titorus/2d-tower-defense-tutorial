/// <summary>
/// A Point with x and y coordinate.
/// </summary>
public struct Point{
	
	/// <summary>
	/// Gets or sets the x.
	/// </summary>
	/// <value>The x.</value>
	public int X { get; set; }

	/// <summary>
	/// Gets or sets the y.
	/// </summary>
	/// <value>The y.</value>
	public int Y { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="Point"/> struct.
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public Point(int x, int y){
		this.X = x;
		this.Y = y;
	}
}
