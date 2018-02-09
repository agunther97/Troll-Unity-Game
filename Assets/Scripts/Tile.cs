using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
	public int x { get; set; }
	public int y { get; set; }
	public bool isOccupied { get; set; }
	public bool isWall{ get; set; }
	public bool moveNorth { get; set; } = true;
	public bool moveSouth { get; set; } = true;
	public bool moveEast { get; set; } = true;
	public bool moveWest { get; set; } = true;
}
