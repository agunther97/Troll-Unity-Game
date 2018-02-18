using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private Tile playerTile;
	private Grid grid;
	private Vision vision;
	private char movementDirection = 'x';
	public Camera playerCam;
	public Sprite playerSprite;
	bool moveNorth = false, moveEast = false, moveWest = false, moveSouth = false;

	public void StartUp(Tile p_playerTile, Grid p_grid)
	{
		playerTile = p_playerTile;
		grid = p_grid;
		playerTile.obj.GetComponent<SpriteRenderer>().sprite = playerSprite;
		vision = new Vision(grid, playerTile.obj.GetComponent<SpriteRenderer>().sprite);
		playerCam.GetComponent<CameraFollow>().SetTarget(playerTile.obj.GetComponent<Transform>());
		vision.CalculatePlayerVisibility(playerTile);
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
			moveEast = true;
		else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
			moveWest = true;
		else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
			moveSouth = true;
		else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
			moveNorth = true;
	}

	void FixedUpdate() {
		if (moveNorth) {
			if (PlayerMovement('n')) {
				playerCam.GetComponent<CameraFollow>().SetTarget(playerTile.obj.GetComponent<Transform>());
			}
			moveNorth = false;
		} else if (moveEast) {
			if (PlayerMovement('e')) {
				playerCam.GetComponent<CameraFollow>().SetTarget(playerTile.obj.GetComponent<Transform>());
			}
			moveEast = false;
		} else if (moveWest) {
			if (PlayerMovement('w')) {
				playerCam.GetComponent<CameraFollow>().SetTarget(playerTile.obj.GetComponent<Transform>());
			}
			moveWest = false;
		} else if (moveSouth) {
			if (PlayerMovement('s')) {
				playerCam.GetComponent<CameraFollow>().SetTarget(playerTile.obj.GetComponent<Transform>());
			}
			moveSouth = false;
		}
	}

	private bool PlayerMovement(char direction)
	{
		bool pass = false;
		switch (direction) {
			case 'n':
				if (!grid.GetNorthTile(playerTile).isWall) {
					playerTile.obj.GetComponent<Transform>().rotation = Quaternion.identity;
					pass = true;
					playerTile.obj.GetComponent<SpriteRenderer>().sprite = playerTile.originalSprite;
					playerTile = grid.GetNorthTile(playerTile);
					playerTile.obj.GetComponent<SpriteRenderer>().sprite = playerSprite;
				}
				playerTile.obj.GetComponent<Transform>().rotation = Quaternion.identity;
				playerTile.obj.GetComponent<Transform>().Rotate(0.0f, 0.0f, 0.0f);
				break;
			case 's':
				if (!grid.GetSouthTile(playerTile).isWall) {
					playerTile.obj.GetComponent<Transform>().rotation = Quaternion.identity;
					pass = true;
					playerTile.obj.GetComponent<SpriteRenderer>().sprite = playerTile.originalSprite;
					playerTile = grid.GetSouthTile(playerTile);
					playerTile.obj.GetComponent<SpriteRenderer>().sprite = playerSprite;
				}
				playerTile.obj.GetComponent<Transform>().rotation = Quaternion.identity;
				playerTile.obj.GetComponent<Transform>().Rotate(0.0f, 0.0f, 180.0f);
				break;
			case 'e':
				if (!grid.GetEastTile(playerTile).isWall) {
					playerTile.obj.GetComponent<Transform>().rotation = Quaternion.identity;
					pass = true;
					playerTile.obj.GetComponent<SpriteRenderer>().sprite = playerTile.originalSprite;
					playerTile = grid.GetEastTile(playerTile);
					playerTile.obj.GetComponent<SpriteRenderer>().sprite = playerSprite;
				}
				playerTile.obj.GetComponent<Transform>().rotation = Quaternion.identity;
				playerTile.obj.GetComponent<Transform>().Rotate(0.0f, 0.0f, 90.0f);
				break;
			case 'w':
				if(!grid.GetWestTile(playerTile).isWall) {
					playerTile.obj.GetComponent<Transform>().rotation = Quaternion.identity;
					pass = true;
					playerTile.obj.GetComponent<SpriteRenderer>().sprite = playerTile.originalSprite;
					playerTile = grid.GetWestTile(playerTile);
					playerTile.obj.GetComponent<SpriteRenderer>().sprite = playerSprite;
				}
				playerTile.obj.GetComponent<Transform>().rotation = Quaternion.identity;
				playerTile.obj.GetComponent<Transform>().Rotate(0.0f, 0.0f, -90.0f);
				break;
			default:
				Debug.Log("Error, invalid direction supplied");
				break;
		}
		vision.CalculatePlayerVisibility(playerTile);
		return pass;
	}
}

