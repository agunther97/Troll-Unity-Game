using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid {
	private List<List<Tile>> map = new List<List<Tile>>();
	private int rows;
	private int cols;

	public Grid(int p_rows, int p_cols)
	{
		rows = p_rows;
		cols = p_cols;
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
}
