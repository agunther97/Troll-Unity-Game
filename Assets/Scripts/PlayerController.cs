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
	private bool moveNorth = false, moveEast = false, moveWest = false, moveSouth = false;
	private bool hasLaser;

	public void StartUp(Tile p_playerTile, Grid p_grid)
	{
		playerTile = p_playerTile;
		grid = p_grid;
		playerTile.obj.GetComponent<SpriteRenderer>().sprite = playerSprite;
		vision = new Vision(grid, playerTile.obj.GetComponent<SpriteRenderer>().sprite);
		playerCam.GetComponent<CameraFollow>().SetTarget(playerTile.obj.GetComponent<Transform>());
		vision.CalculatePlayerVisibility(playerTile);
		playerTile.obj.GetComponent<SpriteRenderer>().color = Color.yellow;
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
			if (PlayerMovementCheck('n')) {
				playerCam.GetComponent<CameraFollow>().SetTarget(playerTile.obj.GetComponent<Transform>());
			}
			moveNorth = false;
		} else if (moveEast) {
			if (PlayerMovementCheck('e')) {
				playerCam.GetComponent<CameraFollow>().SetTarget(playerTile.obj.GetComponent<Transform>());
			}
			moveEast = false;
		} else if (moveWest) {
			if (PlayerMovementCheck('w')) {
				playerCam.GetComponent<CameraFollow>().SetTarget(playerTile.obj.GetComponent<Transform>());
			}
			moveWest = false;
		} else if (moveSouth) {
			if (PlayerMovementCheck('s')) {
				playerCam.GetComponent<CameraFollow>().SetTarget(playerTile.obj.GetComponent<Transform>());
			}
			moveSouth = false;
		}
	}

	private Tile PlayerMovement(Tile playerTile, Tile newPlayerTile)
	{
		playerTile.obj.GetComponent<Transform> ().rotation = Quaternion.identity;
		playerTile.obj.GetComponent<SpriteRenderer> ().sprite = playerTile.originalSprite;
		newPlayerTile.obj.GetComponent<SpriteRenderer> ().sprite = playerSprite;
		return newPlayerTile;
	}

	private bool pushedNorthBefore = false;
	private bool pushedSouthBefore = false;
	private bool pushedWestBefore = false;
	private bool pushedEastBefore = false;

	private bool PlayerMovementCheck(char direction)
	{
		Tile possiblePushRecieveTile;
		bool pass = false;
		switch (direction) {
			case 'n':
				pass = NorthMovement();
				break;
			case 's':
				pass = SouthMovement();
				break;
			case 'e':
				pass = EastMovement();
				break;
			case 'w':
				pass = WestMovement();
				break;
			default:
				Debug.Log("Error, invalid direction supplied");
				break;
		}
		if (pass) {
			vision.CalculatePlayerVisibility(playerTile);
			if (playerTile.isLaser) {
				grid.CollectLaser(playerTile);
				hasLaser = true;
			}
			if (hasLaser) {
				playerTile.obj.GetComponent<SpriteRenderer>().color = Color.blue;
			} else {
				playerTile.obj.GetComponent<SpriteRenderer>().color = Color.yellow;
			}
		}
		return pass;
	}

	private bool NorthMovement()
	{
		bool pass = false;
		Tile possiblePushRecieveTile;

		pushedEastBefore = false;
		pushedWestBefore = false;
		pushedSouthBefore = false;

		if (!grid.GetNorthTile(playerTile).isWall) {
			pass = true;
			playerTile = PlayerMovement(playerTile, grid.GetNorthTile(playerTile));	
		} else {
			if (pushedNorthBefore) {
				if (!grid.GetNorthTile(playerTile).isEdge) {
					possiblePushRecieveTile = grid.GetNorthTile(grid.GetNorthTile(playerTile)); 
					if (!possiblePushRecieveTile.isWall) {
						pass = true;
						PushWall(possiblePushRecieveTile, grid.GetNorthTile(playerTile));
						playerTile = PlayerMovement(playerTile, grid.GetNorthTile(playerTile));
					}
				}
			} else {
				pushedNorthBefore = true;
			}
		}
		playerTile.obj.GetComponent<Transform>().rotation = Quaternion.identity;
		playerTile.obj.GetComponent<Transform>().Rotate(0.0f, 0.0f, 0.0f);
		return pass;
	}

	private bool SouthMovement()
	{
		bool pass = false;
		Tile possiblePushRecieveTile;

		pushedEastBefore = false;
		pushedWestBefore = false;
		pushedNorthBefore = false;

		if (!grid.GetSouthTile(playerTile).isWall) {
			pass = true;
			playerTile = PlayerMovement(playerTile, grid.GetSouthTile(playerTile));
		} else {
			if (pushedSouthBefore) {
				if (!grid.GetSouthTile(playerTile).isEdge) {
					possiblePushRecieveTile = grid.GetSouthTile(grid.GetSouthTile(playerTile));
					if (!possiblePushRecieveTile.isWall) {
						pass = true;
						PushWall(possiblePushRecieveTile, grid.GetSouthTile(playerTile));
						playerTile = PlayerMovement(playerTile, grid.GetSouthTile(playerTile));
					}
				}
			} else {
				pushedSouthBefore = true;
			}
		}
		playerTile.obj.GetComponent<Transform>().rotation = Quaternion.identity;
		playerTile.obj.GetComponent<Transform>().Rotate(0.0f, 0.0f, 180.0f);
		return pass;
	}

	private bool EastMovement()
	{
		bool pass = false;
		Tile possiblePushRecieveTile;

		pushedSouthBefore = false;
		pushedNorthBefore = false;
		pushedWestBefore = false;

		if (!grid.GetEastTile(playerTile).isWall) {
			pass = true;
			playerTile = PlayerMovement(playerTile, grid.GetEastTile(playerTile));
		} else {
			if (pushedEastBefore) {
				if (!grid.GetEastTile(playerTile).isEdge) {
					possiblePushRecieveTile = grid.GetEastTile(grid.GetEastTile(playerTile));
					if (!possiblePushRecieveTile.isWall) {
						pass = true;
						PushWall(possiblePushRecieveTile, grid.GetEastTile(playerTile));
						playerTile = PlayerMovement(playerTile, grid.GetEastTile(playerTile));
					}
				}
			} else {
				pushedEastBefore = true;
			}
		}
		playerTile.obj.GetComponent<Transform>().rotation = Quaternion.identity;
		playerTile.obj.GetComponent<Transform>().Rotate(0.0f, 0.0f, 90.0f);
		return pass;
	}

	private bool WestMovement()
	{
		bool pass = false;
		Tile possiblePushRecieveTile;

		pushedSouthBefore = false;
		pushedNorthBefore = false;
		pushedEastBefore = false;

		if (!grid.GetWestTile(playerTile).isWall) {
			pass = true;
			playerTile = PlayerMovement(playerTile, grid.GetWestTile(playerTile));
		} else {
			if (pushedWestBefore) {
				if (!grid.GetWestTile(playerTile).isEdge) {
					possiblePushRecieveTile = grid.GetWestTile(grid.GetWestTile(playerTile));
					if (!possiblePushRecieveTile.isWall) {
						pass = true;
						PushWall(possiblePushRecieveTile, grid.GetWestTile(playerTile));
						playerTile = PlayerMovement(playerTile, grid.GetWestTile(playerTile));
					}
				}
			} else {
				pushedWestBefore = true;
			}
		}
		playerTile.obj.GetComponent<Transform>().rotation = Quaternion.identity;
		playerTile.obj.GetComponent<Transform>().Rotate(0.0f, 0.0f, -90.0f);
		return pass;
	}

	void PushWall(Tile possiblePushRecieveTile, Tile newFloorTile)
	{
		grid.ChangeToWall(possiblePushRecieveTile);
		grid.ChangeToFloor(newFloorTile);
	}
}

