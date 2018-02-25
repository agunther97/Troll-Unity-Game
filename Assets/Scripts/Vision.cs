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

	public void CalculatePlayerVisibility(Tile origin)
	{
		HideAllTiles();
		//Tile edgeTopRow1 = null, edgeTopRow2 = null, edgeBotRow1 = null, edgeBotRow2 = null;
		List<Tile> rowStartPoints = new List<Tile>();
		rowStartPoints = GetEastWestValidTilesInRange(3, origin);
		rowStartPoints.Add(origin);
		foreach (Tile rowStartPoint in rowStartPoints) {
			Reveal(rowStartPoint);
			List<Tile> validRowTiles = GetNorthSouthValidTilesInRange(5, rowStartPoint);
			foreach (Tile validRowTile in validRowTiles) {
				Reveal(validRowTile);
			}
		}
		if (origin.x + 4 < grid.GetRows()) {
			Tile edge = origin;
			for (int i = 0; i < 4; i++) {
				edge = grid.GetEastTile(edge);
			}
			Reveal(edge);
			List<Tile> validRowTiles = GetNorthSouthValidTilesInRange(4, edge);
			foreach (Tile validRowTile in validRowTiles) {
				Reveal(validRowTile);
			}
		}
		if (origin.x + 5 < grid.GetRows()) {
			Tile edge = origin;
			for (int i = 0; i < 5; i++) {
				edge = grid.GetEastTile(edge);
			}
			Reveal(edge);
			List<Tile> validRowTiles = GetNorthSouthValidTilesInRange(3, edge);
			foreach (Tile validRowTile in validRowTiles) {
				Reveal(validRowTile);
			}
		}
		if (origin.x - 4 >= 0) {
			Tile edge = origin;
			for (int i = 0; i < 4; i++) {
				edge = grid.GetWestTile(edge);
			}
			Reveal(edge);
			List<Tile> validRowTiles = GetNorthSouthValidTilesInRange(4, edge);
			foreach (Tile validRowTile in validRowTiles) {
				Reveal(validRowTile);
			}
		}
		if (origin.x - 5 >= 0) {
			Tile edge = origin;
			for (int i = 0; i < 5; i++) {
				edge = grid.GetWestTile(edge);
			}
			Reveal(edge);
			List<Tile> validRowTiles = GetNorthSouthValidTilesInRange(3, edge);
			foreach (Tile validRowTile in validRowTiles) {
				Reveal(validRowTile);
			}
		}
	}

	private List<Tile> GetNorthSouthValidTilesInRange(int range, Tile origin)
	{
		List<Tile> rowStartPoints = new List<Tile>();
		Tile current = origin;
		for (int i = 0; i < range; i++) {
			if (current.y + 1 < grid.GetCols()) {
				current = grid.GetNorthTile(current);
				rowStartPoints.Add(current);
			} else {
				break;
			}
		}
		current = origin;
		for (int i = 0; i < range; i++) {
			if (current.y - 1 >= 0) {
				current = grid.GetSouthTile(current);
				rowStartPoints.Add(current);
			} else {
				break;
			}
		}
		return rowStartPoints;
	}

	private List<Tile> GetEastWestValidTilesInRange(int range, Tile origin)
	{
		List<Tile> validRowTiles = new List<Tile>();
		Tile current = origin;
		for (int i = 0; i < range; i++) {
			if (current.x + 1 < grid.GetRows()) {
				current = grid.GetEastTile(current);
				validRowTiles.Add(current);
			} else {
				break;
			}
		}
		current = origin;
		for (int i = 0; i < range; i++) {
			if (current.x - 1 >= 0) {
				current = grid.GetWestTile(current);
				validRowTiles.Add(current);
			} else {
				break;
			}
		}
		return validRowTiles;
	}

	private void HideAllTiles()
	{
		foreach (List<Tile> row in grid.GetMap()) {
			foreach (Tile tile in row) {
				//if(!tile.isVisited)
					tile.obj.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 255);
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
		tile.obj.GetComponent<SpriteRenderer>().color = tile.originalColor;
		tile.isVisited = true;
	}
}
