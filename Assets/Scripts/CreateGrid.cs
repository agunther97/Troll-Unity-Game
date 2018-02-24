using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour 
{
	Grid grid;
	List<List<Tile>> map;
	Stack<Tile> visitedTiles = new Stack<Tile>();
	Tile playerTile;
	[Tooltip("The number of rows or cols in our maze, must be odd numbers, will scale up if an even # is entered")]
	public int rows;
	public int cols;
	public GameObject tileObject;
	public Transform container;
	public GameObject playerController;
	public Sprite wallSprite;
	public Sprite floorSprite;
	//the rows and cols of our grid

	public void Awake()
	{
		grid = new Grid(rows, cols, floorSprite, new Color32(86, 86, 86, 255), wallSprite, new Color32(144, 144, 144, 255));
		map = grid.GetMap();
		MazeDriver();
		playerController.GetComponent<PlayerController>().StartUp(playerTile, grid);
	}

	private void MazeDriver()
	{
		EnsureOddSize(); // ensure that the grid has an odd number for rows and cols
		FillMap();
		PopulateMapWithWalls();
		GenerateMaze();
		ChangeSprites();
		SpawnTrolls();
	}

	private void EnsureOddSize()
	{
		if (rows % 2 == 0)
			rows++;
		if (cols % 2 == 0)
			cols++;
	}

	private void FillMap()
	{
		float xPadding = 0.065f, yPadding = 0.075f, UnityX = 0.0f, UnityY = 0.0f;
		for (int x = 0; x < rows; x++) {
			List<Tile> row = new List<Tile>();
			for (int y = 0; y < cols; y++) {
				Tile tile = new Tile(x, y);
				tile.obj = Instantiate(tileObject, new Vector3(UnityX, UnityY, 0f), Quaternion.identity, container);
				tile.obj.name = "(" + x + ", " + y + ")";
				tile.obj.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 255); // start invisible
				row.Add(tile);
				UnityX += xPadding;
			}
			map.Add(row);
			UnityY += yPadding;
			UnityX = 0.0f;
		}
	}

	private void PopulateMapWithWalls()
	{
		foreach (List<Tile> row in map) {
			foreach (Tile tile in row) {
				tile.isWall = true;
				if (tile.x <= 2) {
					tile.moveWest = false;
				} else {
					tile.moveWest = true;
				}
				if (tile.x >= rows - 3) {
					tile.moveEast = false;
				} else {
					tile.moveEast = true;
				}
				if (tile.y <= 2) {
					tile.moveSouth = false; 
				} else {
					tile.moveSouth = true;
				}
				if (tile.y >= cols - 3) {
					tile.moveNorth = false;
				} else {
					tile.moveNorth = true;
				}
			}
		}
	}

	private void GenerateMaze()
	{
		Tile startTile = GrabRandomStartTile();
		startTile.isWall = false;
		visitedTiles.Push(startTile); // put our first tile on the stack
		bool running = true;
		while (running) {
			startTile = GetRandomPath(startTile);
			if (startTile == null) {
				break;
			}
		}
	}
		
	private Tile GrabRandomStartTile()
	{
		int randomX, randomY;
		do {
			randomX = Random.Range(1, cols - 1);
			randomY = Random.Range(1, rows - 1);
		} while(randomX % 2 == 0 || randomY % 2 == 0);
		Debug.Log("Start X: " + randomX + " Start Y: " + randomY);
		map[randomX][randomY].obj.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 255);
		playerTile = map[randomX][randomY];
		return map[randomX][randomY];
	}

	private Tile GetRandomPath(Tile startTile)
	{
		List<int> validDirections = GetValidDirections(startTile);
		if (validDirections.Count == 0) {
			if (visitedTiles.Count == 1)
				return null;
			visitedTiles.Pop();
			return visitedTiles.Peek();
		}
		int randomDirectionIndex = Random.Range(0, validDirections.Count);
		int direction = validDirections[randomDirectionIndex];
		switch (direction) {
			case 0:
				startTile.moveNorth = false;
				Tile northTile = grid.GetNorthTile(startTile);
				northTile.isWall = false;
				grid.GetNorthTile(northTile).isWall = false;
				visitedTiles.Push(grid.GetNorthTile(northTile));
				return grid.GetNorthTile(northTile);
			case 1:
				startTile.moveSouth = false;
				Tile southTile = grid.GetSouthTile(startTile);
				southTile.isWall = false;
				grid.GetSouthTile(southTile).isWall = false;
				visitedTiles.Push(grid.GetSouthTile(southTile));
				return grid.GetSouthTile(southTile);
			case 2:
				startTile.moveEast = false;
				Tile eastTile = grid.GetEastTile(startTile);
				eastTile.isWall = false;
				grid.GetEastTile(eastTile).isWall = false;
				visitedTiles.Push(grid.GetEastTile(eastTile));
				return grid.GetEastTile(eastTile);
			case 3:
				startTile.moveWest = false;
				Tile westTile = grid.GetWestTile(startTile);
				westTile.isWall = false;
				grid.GetWestTile(westTile).isWall = false;
				visitedTiles.Push(grid.GetWestTile(westTile));
				return grid.GetWestTile(westTile);
			default:
				return null;
		}
	}

	private List<int> GetValidDirections(Tile startTile)
	{
		List<int> directions = new List<int>();
		if (startTile.moveNorth) {
			if(grid.GetNorthTile(startTile).isWall && grid.GetNorthTile(grid.GetNorthTile(startTile)).isWall)
				directions.Add(0);
		}
		if (startTile.moveSouth) {
			if(grid.GetSouthTile(startTile).isWall && grid.GetSouthTile(grid.GetSouthTile(startTile)).isWall)
				directions.Add(1);
		}
		if (startTile.moveEast) {
			if(grid.GetEastTile(startTile).isWall && grid.GetEastTile(grid.GetEastTile(startTile)).isWall)
				directions.Add(2);
		}
		if (startTile.moveWest) {
			if(grid.GetWestTile(startTile).isWall && grid.GetWestTile(grid.GetWestTile(startTile)).isWall)
				directions.Add(3);
		}
		return directions;
	}

	private void ChangeSprites()
	{
		foreach (List<Tile> row in map) {
			foreach (Tile tile in row) {
				if (!tile.isWall) {
					tile.obj.GetComponent<SpriteRenderer>().sprite = floorSprite;
					tile.obj.GetComponent<SpriteRenderer>().color = new Color32(86, 86, 86, 255);
					tile.originalColor = new Color32(86, 86, 86, 255);
				} else {
					tile.obj.GetComponent<SpriteRenderer>().sprite = wallSprite;
					tile.obj.GetComponent<SpriteRenderer>().color = new Color32(144, 144, 144, 255);
					tile.originalColor = new Color32(144, 144, 144, 255);
				}
				tile.originalSprite = tile.obj.GetComponent<SpriteRenderer>().sprite;
			}
		}
	}
		
	private void SpawnTrolls()
	{
		int totalTiles = rows * cols;
		int numberOfTrolls = totalTiles / 100;
		Debug.Log("Spawning " + numberOfTrolls + " trolls");
	}
}