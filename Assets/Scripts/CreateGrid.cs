using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour 
{
	List<List<Tile>> map = new List<List<Tile>>();
	Stack<Tile> visitedTiles = new Stack<Tile>();
	[Tooltip("The number of rows or cols in our maze, must be odd numbers, will scale up if an even # is entered")]
	public int rows;
	public int cols;
	//the rows and cols of our grid

	struct Point {
		public int x { get; set;}
		public int y { get; set; }
		public Point(int p_x, int p_y)
		{
			x = p_x;
			y = p_y;
		}
	}

	public void MazeDriver()
	{
		EnsureOddSize(); // ensure that the grid has an odd number for rows and cols
		PopulateMapWithWalls();
		GenerateMaze();
	}

	private void EnsureOddSize()
	{
		if (rows % 2 == 0)
			rows++;
		if (cols % 2 == 0)
			cols++;
	}

	private void PopulateMapWithWalls()
	{
		foreach (Tile tile in map) {
			tile.isWall = true;
			if (tile.x == 0 || tile.x == 1 )
				tile.moveWest = false;
			if (tile.x == rows - 1 || tile.x == rows - 2)
				tile.moveEast = false;
			if (tile.y == 0 || tile.y == 1)
				tile.moveSouth = false;
			if (tile.y == cols - 1 || tile.y == cols - 2)
				tile.moveNorth = false;
		}
	}

	private void GenerateMaze()
	{
		Tile startTile = GrabRandomTile();
		visitedTiles.Push(startTile); // put our first tile on the stack
		bool running = true;
		while (running) {
			startTile = GetRandomPath(startTile);
			if (startTile == null) {
				return;
			}
		}
	}

	private Tile GrabRandomTile()
	{
		int randomX = Random.Range(0, rows-1);
		int randomY = Random.Range(0, cols-1);
		return map[randomX][randomY];
	}

	private Tile GetRandomPath(Tile startTile)
	{
		List<int> validDirections = GetValidDirections(startTile);
		if (validDirections.Count == 0) {
			if (visitedTiles.Count == 0)
				return null;
			visitedTiles.Pop();
			return visitedTiles.Peek();
		}
		int randomDirectionIndex = Random.Range(0, validDirections - 1);
		int direction = validDirections[randomDirectionIndex];
		switch (direction) {
			case 0:
				startTile.moveNorth = false;
				Tile northTile = map.GetNorthTile(startTile);
				northTile.isWall = false;
				northTile.moveNorth = false;
				map.GetNorthTile(northTile).isWall = false;
				visitedTiles.Push(map.GetNorthTile(northTile));
				return map.GetNorthTile(northTile);
				break;
			case 1:
				startTile.moveSouth = false;
				Tile southTile = map.GetSouthTile(startTile);
				southTile.isWall = false;
				southTile.moveSouth = false;
				map.GetSouthTile(southTile).isWall = false;
				visitedTiles.Push(map.GetSouthTile(southTile));
				return map.GetSouthTile(southTile);
				break;
			case 2:
				startTile.moveEast = false;
				Tile eastTile = map.GetEastTile(startTile);
				eastTile.isWall = false;
				eastTile.moveEast = false;
				map.GetEastTile(eastTile).isWall = false;
				visitedTiles.Push(map.GetEastTile(eastTile));
				return map.GetEastTile(eastTile);
				break;
			case 3:
				startTile.moveWest = false;
				Tile westTile = map.GetWestTile(startTile);
				westTile.isWall = false;
				westTile.moveWest = false;
				map.GetWestTile(westTile).isWall = false;
				visitedTiles.Push(map.GetWestTile(westTile));
				return map.GetWestTile(westTile);
				break;
		}
	}

	private List<int> GetValidDirections(Tile startTile)
	{
		List<int> directions = new List<int>();
		if (startTile.moveNorth)
			directions.Add(0);
		if (startTile.moveSouth)
			directions.Add(1);
		if (startTile.moveEast)
			directions.Add(2);
		if (startTile.moveWest)
			directions.Add(3);
		return directions;
	}
}