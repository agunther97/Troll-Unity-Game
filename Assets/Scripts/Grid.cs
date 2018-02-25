using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid {
	private List<List<Tile>> map = new List<List<Tile>>();
	private int rows;
	private int cols;
	private Sprite floorSprite;
	private Sprite wallSprite;
	private Color floorColor;
	private Color wallColor;

	public Grid(int p_rows, int p_cols, Sprite p_floorSprite, Color p_floorColor, Sprite p_wallSprite, Color p_wallColor)
	{
		rows = p_rows;
		cols = p_cols;
		floorSprite = p_floorSprite;
		wallSprite = p_wallSprite;
		wallColor = p_wallColor;
		floorColor = p_floorColor;
	}

	public int GetRows()
	{
		return rows;
	}

	public int GetCols()
	{
		return cols;
	}
	public List<List<Tile>> GetMap()
	{
		return map;
	}

	public Tile GetNorthTile(Tile origin)
	{
		return map[origin.x][origin.y + 1];
	}

	public Tile GetSouthTile(Tile origin)
	{
		return map[origin.x][origin.y - 1];
	}

	public Tile GetEastTile(Tile origin)
	{
		return map[origin.x + 1][origin.y];
	}

	public Tile GetWestTile(Tile origin)
	{
		return map[origin.x - 1][origin.y];
	}

	public List<Tile> GetTileNeighbours(Tile tile)
	{
		List<Tile> neighbours = new List<Tile>();
		neighbours.Add(GetNorthTile(tile));
		neighbours.Add(GetSouthTile(tile));
		neighbours.Add(GetEastTile(tile));
		neighbours.Add(GetWestTile(tile));
		return neighbours;
	}

	public void ChangeToWall(Tile tile)
	{
		tile.obj.GetComponent<SpriteRenderer>().sprite = wallSprite;
		tile.isWall = true;
		tile.obj.GetComponent<SpriteRenderer>().color = wallColor;
		tile.originalColor = wallColor;
		tile.originalSprite = wallSprite;
	}

	public void ChangeToFloor(Tile tile)
	{
		tile.obj.GetComponent<SpriteRenderer>().sprite = floorSprite;
		tile.isWall = false;
		tile.obj.GetComponent<SpriteRenderer>().color = floorColor;
		tile.originalColor = floorColor;
		tile.originalSprite = floorSprite;
	}

	public void CollectLaser(Tile tile)
	{
		tile.isLaser = false;
		tile.originalColor = floorColor;
		tile.originalSprite = floorSprite;
	}
}
