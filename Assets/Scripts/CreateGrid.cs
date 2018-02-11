using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour 
{
	List<List<Tile>> map = new List<List<Tile>>();
	Stack<Tile> visitedTiles = new Stack<Tile>();
	Tile playerTile;
	[Tooltip("The number of rows or cols in our maze, must be odd numbers, will scale up if an even # is entered")]
	public int rows;
	public int cols;
	public GameObject tileObject;
	public Transform container;
	public Sprite blankSprite;
	//the rows and cols of our grid

	public void Awake()
	{
		MazeDriver();
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
			PlayerMovement('e');
		else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
			PlayerMovement('w');
		else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
			PlayerMovement('s');
		else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
			PlayerMovement('n');
	}

	private void PlayerMovement(char direction)
	{
		switch (direction) {
			case 'n':
				if (!GetNorthTile(playerTile).isWall) {
					playerTile.obj.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 255);
					playerTile = GetNorthTile(playerTile);
				}
				break;
			case 's':
				if (!GetSouthTile(playerTile).isWall) {
					playerTile.obj.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 255);
					playerTile = GetSouthTile(playerTile);
				}
				break;
			case 'e':
				if (!GetEastTile(playerTile).isWall) {
					playerTile.obj.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 255);
					playerTile = GetEastTile(playerTile);
				}
				break;
			case 'w':
				if(!GetWestTile(playerTile).isWall) {
					playerTile.obj.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 255);
					playerTile = GetWestTile(playerTile);
				}
				break;
			default:
				Debug.Log("Error, invalid direction supplied");
				break;
		}
		CalculateVisibility(playerTile);
	}

	public void MazeDriver()
	{
		EnsureOddSize(); // ensure that the grid has an odd number for rows and cols
		FillMap();
		PopulateMapWithWalls();
		GenerateMaze();
		ChangeSprites();
		CalculateVisibility(playerTile);
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
		float xPadding = 0.61f, yPadding = 0.61f, UnityX = 0.0f, UnityY = 0.0f;
		for (int x = 0; x < rows; x++) {
			List<Tile> row = new List<Tile>();
			for (int y = 0; y < cols; y++) {
				Tile tile = new Tile(x, y);
				tile.obj = Instantiate(tileObject, new Vector3(UnityX, UnityY, 0f), Quaternion.identity, container);
				tile.obj.name = "(" + x + ", " + y + ")";
				tile.obj.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0);
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
		map[randomX][randomY].obj.GetComponent<SpriteRenderer>().color = Color.red; //test code
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
				Tile northTile = GetNorthTile(startTile);
				northTile.isWall = false;
				GetNorthTile(northTile).isWall = false;
				visitedTiles.Push(GetNorthTile(northTile));
				return GetNorthTile(northTile);
			case 1:
				startTile.moveSouth = false;
				Tile southTile = GetSouthTile(startTile);
				southTile.isWall = false;
				GetSouthTile(southTile).isWall = false;
				visitedTiles.Push(GetSouthTile(southTile));
				return GetSouthTile(southTile);
			case 2:
				startTile.moveEast = false;
				Tile eastTile = GetEastTile(startTile);
				eastTile.isWall = false;
				GetEastTile(eastTile).isWall = false;
				visitedTiles.Push(GetEastTile(eastTile));
				return GetEastTile(eastTile);
			case 3:
				startTile.moveWest = false;
				Tile westTile = GetWestTile(startTile);
				westTile.isWall = false;
				GetWestTile(westTile).isWall = false;
				visitedTiles.Push(GetWestTile(westTile));
				return GetWestTile(westTile);
			default:
				return null;
		}
	}

	private Tile GetNorthTile(Tile origin)
	{
		return map[origin.x][origin.y + 1];
	}

	private Tile GetSouthTile(Tile origin)
	{
		return map[origin.x][origin.y - 1];
	}

	private Tile GetEastTile(Tile origin)
	{
		return map[origin.x + 1][origin.y];
	}

	private Tile GetWestTile(Tile origin)
	{
		return map[origin.x - 1][origin.y];
	}

	private List<int> GetValidDirections(Tile startTile)
	{
		List<int> directions = new List<int>();
		if (startTile.moveNorth) {
			if(GetNorthTile(startTile).isWall && GetNorthTile(GetNorthTile(startTile)).isWall)
				directions.Add(0);
		}
		if (startTile.moveSouth) {
			if(GetSouthTile(startTile).isWall && GetSouthTile(GetSouthTile(startTile)).isWall)
				directions.Add(1);
		}
		if (startTile.moveEast) {
			if(GetEastTile(startTile).isWall && GetEastTile(GetEastTile(startTile)).isWall)
				directions.Add(2);
		}
		if (startTile.moveWest) {
			if(GetWestTile(startTile).isWall && GetWestTile(GetWestTile(startTile)).isWall)
				directions.Add(3);
		}
		return directions;
	}

	private void ChangeSprites()
	{
		foreach (List<Tile> row in map) {
			foreach (Tile tile in row) {
				if (tile.obj == null)
					Debug.Log("Tile has no object");
				if(!tile.isWall)
					tile.obj.GetComponent<SpriteRenderer>().sprite = blankSprite;
			}
		}
	}

	private void CalculateVisibility(Tile origin)
	{
		HideAllTiles();
		List<List<Tile>> visibleTiles = new List<List<Tile>>();
		origin.obj.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
		origin.isVisited = true;
		RevealCardinalWallsAroundVisibleTile(origin);
		visibleTiles.Add(GetVisibleNorthTiles(origin));
		visibleTiles.Add(GetVisibleSouthTiles(origin));
		visibleTiles.Add(GetVisibleEastTiles(origin));
		visibleTiles.Add(GetVisibleWestTiles(origin));
		foreach (List<Tile> direction in visibleTiles) {
			foreach (Tile tile in direction) {
				Reveal(tile);
				RevealCardinalWallsAroundVisibleTile(tile);
			}
		}
		foreach (List<Tile> direction in visibleTiles) {
			if (direction.Count != 0) {
				Tile rowEndTile = direction[direction.Count - 1];
				RevealAllSurroundingWallTiles(rowEndTile);
			}
		}
	}

	private void HideAllTiles()
	{
		foreach (List<Tile> row in map) {
			foreach (Tile tile in row) {
				if(!tile.isVisited)
					tile.obj.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0);
			}
		}
	}

	private List<Tile> GetVisibleNorthTiles(Tile current)
	{
		List<Tile> visibleTiles = new List<Tile>();
		while (true) {
			current = GetNorthTile(current);
			if (current.isWall) {
				return visibleTiles;
			} else {
				visibleTiles.Add(current);
			}
		}
	}

	private List<Tile> GetVisibleSouthTiles(Tile current)
	{
		List<Tile> visibleTiles = new List<Tile>();
		while (true) {
			current = GetSouthTile(current);
			if (current.isWall) {
				return visibleTiles;
			} else {
				visibleTiles.Add(current);
			}
		}
	}

	private List<Tile> GetVisibleEastTiles(Tile current)
	{
		List<Tile> visibleTiles = new List<Tile>();
		while (true) {
			current = GetEastTile(current);
			if (current.isWall) {
				return visibleTiles;
			} else {
				visibleTiles.Add(current);
			}
		}
	}

	private List<Tile> GetVisibleWestTiles(Tile current)
	{
		List<Tile> visibleTiles = new List<Tile>();
		while (true) {
			current = GetWestTile(current);
			if (current.isWall) {
				return visibleTiles;
			} else {
				visibleTiles.Add(current);
			}
		}
	}

	private void RevealCardinalWallsAroundVisibleTile(Tile tile)
	{
		List<Tile> neighbours = GetTileNeighbours(tile);
		foreach (Tile neigh in neighbours) {
			if (neigh.isWall) {
				Reveal(neigh);
			}
		}
	}

	private List<Tile> GetTileNeighbours(Tile tile)
	{
		List<Tile> neighbours = new List<Tile>();
		neighbours.Add(GetNorthTile(tile));
		neighbours.Add(GetSouthTile(tile));
		neighbours.Add(GetEastTile(tile));
		neighbours.Add(GetWestTile(tile));
		return neighbours;
	}

	private void RevealAllSurroundingWallTiles(Tile tile)
	{
		Reveal(map[tile.x - 1][tile.y + 1]);
		Reveal(map[tile.x + 1][tile.y + 1]);
		Reveal(map[tile.x - 1][tile.y - 1]);
		Reveal(map[tile.x + 1][tile.y - 1]);

	}

	private void Reveal(Tile tile)
	{
		tile.obj.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 255);
		tile.isVisited = true;
	}

	private void SpawnTrolls()
	{
		int totalTiles = rows * cols;
		int numberOfTrolls = totalTiles / 100;
		Debug.Log("Spawning " + numberOfTrolls + " trolls");
	}
}