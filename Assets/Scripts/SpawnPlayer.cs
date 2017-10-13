using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {
	public GameObject Spawns;
	private Transform PlayerTransform; //player
	private Transform Troll;
	private Transform[] Trolls = new Transform[7];

	private Vector3 current;
	private Vector3 offset;
	public Camera maincam;

	private SpriteRenderer PlayerSprite;//player sprite
	public Sprite Tsprite;
	private SpriteRenderer TrollSprite;

	private Rigidbody2D r;//player rigidbody
	private Rigidbody2D TrollBody;

	bool duplicate = false;
	int[] pastrand = new int[7];

	void Start () {
		//setup storage for already used spawn points
		for (int i = 0; i < 7; i++) {
			pastrand [i] = 999;
		}

		//spawn player
		int random = 0;
		random = RandomNumber(random);
		pastrand [0] = random;
		print (random);
		PlayerTransform = Spawns.transform.GetChild (random);
		PlayerTransform.name = "Player";
		PlayerTransform.tag = "Player";
		PlayerSprite = PlayerTransform.GetComponent<SpriteRenderer>();
		PlayerTransform.gameObject.AddComponent<Rigidbody2D> ();
		r = PlayerTransform.gameObject.GetComponent<Rigidbody2D> ();
		r.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
		r.gravityScale = 0;
		r.interpolation = RigidbodyInterpolation2D.Extrapolate;
		Spawns.transform.GetChild (random).gameObject.SetActive (true);

		//camera setup
		offset = new Vector3 (0.0f, 0.0f, -1.0f);
		current = PlayerTransform.transform.position;
		current = new Vector3 (current.x, current.y, -1.0f);
		transform.position = current;
		maincam.orthographicSize = 20.0f;

		//spawn trolls
		for(int i = 0; i < 7; i++){
			random = RandomNumber (random);
			print (random);
			Troll = Spawns.transform.GetChild (random);
			Troll.name = "Troll " + i;
			Troll.gameObject.AddComponent<Rigidbody2D> ();
			TrollBody = Troll.gameObject.GetComponent<Rigidbody2D> ();
			TrollBody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
			TrollBody.gravityScale = 0;
			TrollBody.interpolation = RigidbodyInterpolation2D.Extrapolate;
			Spawns.transform.GetChild (random).gameObject.SetActive (true);
			TrollSprite = Troll.GetComponent<SpriteRenderer> ();
			TrollSprite.sprite = Tsprite;
			Trolls [i] = Troll;

		}
	}

	int RandomNumber(int random){
		random = Random.Range(0, Spawns.transform.childCount);
		do {
			for (int i = 0; i < 7; i++) {
				if (pastrand [i] == random) {
					duplicate = true;
					break;
				}
				duplicate = false;
			}

		} while(duplicate == true);
		for (int i = 0; i < 7; i++) {
			if (pastrand [i] == 999) {
				pastrand [i] = random;
			}
		}
		return random;
	}

	void TrollMove(){
		//raycasting
		for (int i = 0; i < 7; i++) {
			int moverand = Random.Range (1, 5);
			TrollBody = Trolls [i].gameObject.GetComponent<Rigidbody2D> ();
			if (moverand == 1) {
				TrollBody.transform.position = new Vector3 (Trolls[i].position.x + 4.0f, Trolls[i].position.y, Trolls[i].position.z);
			}
			if(moverand == 2){
				TrollBody.transform.position = new Vector3 (Trolls[i].position.x, Trolls[i].position.y + 4.0f, Trolls[i].position.z);
			}
			if (moverand == 3) {
				TrollBody.transform.position = new Vector3 (Trolls[i].position.x - 4.0f, Trolls[i].position.y, Trolls[i].position.z);
			}
			if (moverand == 4) {
				TrollBody.transform.position = new Vector3 (Trolls[i].position.x, Trolls[i].position.y - 4.0f, Trolls[i].position.z);
			}

			Vector3 RightUpDirection = Quaternion.AngleAxis (20.0f, Vector3.forward) * -Trolls [i].right;
			Vector3 RightDownDirection = Quaternion.AngleAxis (-20.0f, Vector3.forward) * -Trolls [i].right;
			Vector3 LeftUpDirection = Quaternion.AngleAxis (20.0f, Vector3.back) * Trolls [i].right;
			Vector3 LeftDownDirection = Quaternion.AngleAxis (-20.0f, Vector3.back) * Trolls [i].right;

			RaycastHit2D RightUpHit = Physics2D.Raycast (new Vector2(Trolls[i].position.x + 2.5f, Trolls[i].position.y), RightUpDirection, 20.0f);
			RaycastHit2D RightDownHit = Physics2D.Raycast (new Vector2(Trolls[i].position.x + 2.5f, Trolls[i].position.y), RightDownDirection, 20.0f);
			RaycastHit2D LeftUpHit = Physics2D.Raycast (new Vector2(Trolls[i].position.x - 2.5f, Trolls[i].position.y), LeftUpDirection, 20.0f);
			RaycastHit2D LeftDownHit = Physics2D.Raycast (new Vector2(Trolls[i].position.x - 2.5f, Trolls[i].position.y), LeftDownDirection, 20.0f);

			RaycastHit2D DownHit = Physics2D.Raycast (new Vector2(Trolls[i].position.x, Trolls[i].position.y - 2.5f), new Vector2(0, -500), 20.0f);
			RaycastHit2D UpHit = Physics2D.Raycast (new Vector2(Trolls[i].position.x, Trolls[i].position.y + 2.5f), new Vector2(0, 500), 20.0f);
			RaycastHit2D LeftHit = Physics2D.Raycast (new Vector2(Trolls[i].position.x - 2.5f, Trolls[i].position.y - 2.5f), new Vector2(-500, 0), 20.0f);
			RaycastHit2D RightHit = Physics2D.Raycast (new Vector2(Trolls[i].position.x + 2.5f, Trolls[i].position.y), new Vector2(500, 0), 20.0f);


//			Debug.DrawRay (new Vector3(Trolls [i].position.x, Trolls[i].position.y - 2.5f, 0.0f), new Vector3(0, -500, 0), Color.black, 1.0f, false);
//			Debug.DrawRay (new Vector3(Trolls [i].position.x, Trolls[i].position.y + 2.5f, 0.0f), new Vector3(0, 500, 0), Color.black, 1.0f, false);
//			Debug.DrawRay (new Vector3(Trolls [i].position.x - 2.5f, Trolls[i].position.y, 0.0f), new Vector3(-500, 0, 0), Color.black, 1.0f, false);
//			Debug.DrawRay (new Vector3(Trolls [i].position.x + 2.5f, Trolls[i].position.y, 0.0f), new Vector3(500, 0, 0), Color.black, 1.0f, false);
//
//			Debug.DrawRay (new Vector3(Trolls[i].position.x + 2.5f, Trolls[i].position.y, 0.0f), RightUpDirection*500.0f, Color.black, 1.0f, false);
//			Debug.DrawRay (new Vector3(Trolls[i].position.x + 2.5f, Trolls[i].position.y, 0.0f), RightDownDirection*500.0f, Color.black, 1.0f, false);
//			Debug.DrawRay (new Vector3(Trolls[i].position.x - 2.5f, Trolls[i].position.y, 0.0f), LeftUpDirection*500.0f, Color.black, 1.0f, false);
//			Debug.DrawRay (new Vector3(Trolls[i].position.x - 2.5f, Trolls[i].position.y, 0.0f), LeftDownDirection*500.0f, Color.black, 1.0f, false);


			if (DownHit) {
				if(DownHit.collider.name == "Player"){
					TrollCharge (i);
					continue;
				}
			}
			if (UpHit) {
				if(UpHit.collider.name == "Player"){
					TrollCharge (i);
					continue;
				}
			}
			if (LeftHit) {
				if(LeftHit.collider.name == "Player"){
					TrollCharge (i);
					continue;
				}
			}
			if (RightHit) {
				if(RightHit.collider.name == "Player"){
					TrollCharge (i);
					continue;
				}
			}
			if (RightUpHit) {
				if(RightUpHit.collider.name == "Player"){
					TrollCharge (i);
					continue;
				}
			}
			if (RightDownHit) {
				if(RightDownHit.collider.name == "Player"){
					TrollCharge (i);
					continue;
				}
			}
			if (LeftUpHit) {
				if(LeftUpHit.collider.name == "Player"){
					TrollCharge (i);
					continue;
				}
			}
			if (LeftDownHit) {
				if(LeftDownHit.collider.name == "Player"){
					TrollCharge (i);
					continue;
				}
			}
		}
	}

	void TrollCharge(int i){
		print ("Troll Charge!");
	}

	// Update is called once per frame
	void FixedUpdate () {
		//player movement
		transform.position = PlayerTransform.transform.position + offset;
		if (Input.GetKeyDown ("d")) {
			TrollMove ();
			PlayerSprite.flipX = false;
			PlayerTransform.transform.position = new Vector3 (PlayerTransform.position.x + 4.0f, PlayerTransform.position.y, PlayerTransform.position.z);
			PlayerTransform.transform.rotation = Quaternion.Euler(PlayerTransform.transform.rotation.x, PlayerTransform.transform.rotation.y, 0.0f);
		}
		if (Input.GetKeyDown ("w")) {
			TrollMove ();
			PlayerTransform.transform.position = new Vector3 (PlayerTransform.position.x, PlayerTransform.position.y+4.0f, PlayerTransform.position.z); 
			if(PlayerSprite.flipX == false)
				PlayerTransform.transform.rotation = Quaternion.Euler(PlayerTransform.rotation.x, PlayerTransform.rotation.y, 90.0f);
			else
				PlayerTransform.transform.rotation = Quaternion.Euler(PlayerTransform.rotation.x, PlayerTransform.rotation.y, -90.0f);
		}
		if (Input.GetKeyDown ("a")) {
			TrollMove ();
			PlayerSprite = PlayerTransform.GetComponent<SpriteRenderer>();
			PlayerSprite.flipX = true;
			PlayerTransform.transform.position = new Vector3 (PlayerTransform.position.x - 4.0f, PlayerTransform.position.y, PlayerTransform.position.z);
			PlayerTransform.transform.rotation = Quaternion.Euler(PlayerTransform.rotation.x, PlayerTransform.rotation.y, 0.0f);
		}
		if (Input.GetKeyDown ("s")) {
			TrollMove ();
			PlayerTransform.transform.position = new Vector3 (PlayerTransform.position.x, PlayerTransform.position.y - 4.0f, PlayerTransform.position.z);
			if(PlayerSprite.flipX == false)
				PlayerTransform.transform.rotation = Quaternion.Euler(PlayerTransform.rotation.x, PlayerTransform.rotation.y, -90.0f);
			else
				PlayerTransform.transform.rotation = Quaternion.Euler(PlayerTransform.rotation.x, PlayerTransform.rotation.y, 90.0f);
		}
	}
}