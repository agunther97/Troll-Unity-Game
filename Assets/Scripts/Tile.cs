using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
	public int x { get; set; }
	public int y { get; set; }
	public Color originalColor { get; set; }
	public Sprite originalSprite { get; set; }
	public bool isOccupied { get; set; }
	public bool isWall { get; set; }
	public bool isVisited { get; set; }
	public bool moveNorth { get; set; }
	public bool moveSouth { get; set; }
	public bool moveEast { get; set; }
	public bool moveWest { get; set; }
	public GameObject obj { get; set;}

	public Tile(int p_x, int p_y)
	{
		x = p_x;
		y = p_y;
	}
}
