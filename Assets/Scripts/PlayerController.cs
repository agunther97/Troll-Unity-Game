using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private Tile playerTile;
	private Grid grid;
	private Vision vision;
	public Sprite playerSprite;

	public void StartUp(Tile p_playerTile, Grid p_grid)
	{
		playerTile = p_playerTile;
		grid = p_grid;
		playerTile.obj.GetComponent<SpriteRenderer>().sprite = playerSprite;
		vision = new Vision(grid, playerTile.obj.GetComponent<SpriteRenderer>().sprite);
		PlayerSetup();
	}

	private void PlayerSetup()
	{
		vision.CalculateVisibility(playerTile);
		List<Tile> playerNeigh = grid.GetTileNeighbours(playerTile);
		Tile behind;
		foreach (Tile tile in playerNeigh) {
			if (!tile.isWall) {
				if (grid.GetNorthTile(playerTile) == tile) {
					behind = grid.GetSouthTile(playerTile);
					vision.Reveal(grid.GetWestTile(behind));
					vision.Reveal(grid.GetEastTile(behind));
					break;
				} else if (grid.GetSouthTile(playerTile) == tile) {
					playerTile.obj.GetComponent<Transform>().Rotate(0.0f, 0.0f, 180.0f);
					behind = grid.GetNorthTile(playerTile);
					vision.Reveal(grid.GetWestTile(behind));
					vision.Reveal(grid.GetEastTile(behind));
					break;
				} else if (grid.GetEastTile(playerTile) == tile) {
					playerTile.obj.GetComponent<Transform>().Rotate(0.0f, 0.0f, 90.0f);
					behind = grid.GetWestTile(playerTile);
					vision.Reveal(grid.GetNorthTile(behind));
					vision.Reveal(grid.GetSouthTile(behind));
					break;
				} else if (grid.GetWestTile(playerTile) == tile) {
					playerTile.obj.GetComponent<Transform>().Rotate(0.0f, 0.0f, -90.0f);
					behind = grid.GetEastTile(playerTile);
					vision.Reveal(grid.GetNorthTile(behind));
					vision.Reveal(grid.GetSouthTile(behind));
					break;
				}
			}
		}

	}


	// Update is called once per frame
	void Update () {
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
				if (!grid.GetNorthTile(playerTile).isWall) {
					playerTile.obj.GetComponent<Transform>().rotation = Quaternion.identity;
					playerTile.obj.GetComponent<SpriteRenderer>().sprite = null;
					playerTile = grid.GetNorthTile(playerTile);
					playerTile.obj.GetComponent<Transform>().Rotate(0.0f, 0.0f, 0.0f);
				}
				break;
			case 's':
				if (!grid.GetSouthTile(playerTile).isWall) {
					playerTile.obj.GetComponent<Transform>().rotation = Quaternion.identity;
					playerTile.obj.GetComponent<SpriteRenderer>().sprite = null;
					playerTile = grid.GetSouthTile(playerTile);
					playerTile.obj.GetComponent<Transform>().Rotate(0.0f, 0.0f, 180.0f);
				}
				break;
			case 'e':
				if (!grid.GetEastTile(playerTile).isWall) {
					playerTile.obj.GetComponent<Transform>().rotation = Quaternion.identity;
					playerTile.obj.GetComponent<SpriteRenderer>().sprite = null;
					playerTile = grid.GetEastTile(playerTile);
					playerTile.obj.GetComponent<Transform>().Rotate(0.0f, 0.0f, 90.0f);
				}
				break;
			case 'w':
				if(!grid.GetWestTile(playerTile).isWall) {
					playerTile.obj.GetComponent<Transform>().rotation = Quaternion.identity;
					playerTile.obj.GetComponent<SpriteRenderer>().sprite = null;
					playerTile = grid.GetWestTile(playerTile);
					playerTile.obj.GetComponent<Transform>().Rotate(0.0f, 0.0f, -90.0f);
				}
				break;
			default:
				Debug.Log("Error, invalid direction supplied");
				break;
		}
		vision.CalculateVisibility(playerTile);
	}
}

