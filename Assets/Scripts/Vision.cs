using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision {
	private Sprite playerSprite;
	private Grid grid;

	public Vision(Grid p_grid, Sprite p_playerSprite)
	{
		grid = p_grid;
		playerSprite = p_playerSprite;
	}

	public void CalculateVisibility(Tile origin)
	{
		HideAllTiles();
		List<List<Tile>> visibleTiles = new List<List<Tile>>();
		origin.obj.GetComponent<SpriteRenderer>().sprite = playerSprite;
		origin.obj.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 255);
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
		foreach (List<Tile> row in grid.GetMap()) {
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
			current = grid.GetNorthTile(current);
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
			current = grid.GetSouthTile(current);
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
			current = grid.GetEastTile(current);
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
			current = grid.GetWestTile(current);
			if (current.isWall) {
				return visibleTiles;
			} else {
				visibleTiles.Add(current);
			}
		}
	}

	private void RevealCardinalWallsAroundVisibleTile(Tile tile)
	{
		List<Tile> neighbours = grid.GetTileNeighbours(tile);
		foreach (Tile neigh in neighbours) {
			if (neigh.isWall) {
				Reveal(neigh);
			}
		}
	}

	private void RevealAllSurroundingWallTiles(Tile tile)
	{
		Reveal(grid.GetMap()[tile.x - 1][tile.y + 1]);
		Reveal(grid.GetMap()[tile.x + 1][tile.y + 1]);
		Reveal(grid.GetMap()[tile.x - 1][tile.y - 1]);
		Reveal(grid.GetMap()[tile.x + 1][tile.y - 1]);

	}

	public void Reveal(Tile tile)
	{
		tile.obj.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 255);
		tile.isVisited = true;
	}
}
