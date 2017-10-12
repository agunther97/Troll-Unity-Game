using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {
	public GameObject Spawns;
	private Transform obj; //player
	private Transform Troll;
	private Transform[] Trolls = new Transform[7];

	private Vector3 current;
	private Vector3 offset;
	public Camera maincam;

	private SpriteRenderer s;//player sprite
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
		obj = Spawns.transform.GetChild (random);
		obj.name = "Player";
		s = obj.GetComponent<SpriteRenderer>();
		obj.gameObject.AddComponent<Rigidbody2D> ();
		r = obj.gameObject.GetComponent<Rigidbody2D> ();
		r.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
		r.gravityScale = 0;
		r.interpolation = RigidbodyInterpolation2D.Extrapolate;
		Spawns.transform.GetChild (random).gameObject.SetActive (true);

		//camera setup
		offset = new Vector3 (0.0f, 0.0f, -1.0f);
		current = obj.transform.position;
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
		for (int i = 0; i < 7; i++) {
			int moverand = Random.Range (1, 4);
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
		}
	}

	// Update is called once per frame
	void Update () {
		transform.position = obj.transform.position + offset;
		if (Input.GetKeyDown ("d")) {
			TrollMove ();
			s.flipX = false;
			obj.transform.position = new Vector3 (obj.position.x + 4.0f, obj.position.y, obj.position.z);
			obj.transform.rotation = Quaternion.Euler(obj.transform.rotation.x, obj.transform.rotation.y, 0.0f);
		}
		if (Input.GetKeyDown ("w")) {
			TrollMove ();
			obj.transform.position = new Vector3 (obj.position.x, obj.position.y+4.0f, obj.position.z); 
			if(s.flipX == false)
				obj.transform.rotation = Quaternion.Euler(obj.rotation.x, obj.rotation.y, 90.0f);
			else
				obj.transform.rotation = Quaternion.Euler(obj.rotation.x, obj.rotation.y, -90.0f);
		}
		if (Input.GetKeyDown ("a")) {
			TrollMove ();
			s = obj.GetComponent<SpriteRenderer>();
			s.flipX = true;
			obj.transform.position = new Vector3 (obj.position.x - 4.0f, obj.position.y, obj.position.z);
			obj.transform.rotation = Quaternion.Euler(obj.rotation.x, obj.rotation.y, 0.0f);
		}
		if (Input.GetKeyDown ("s")) {
			TrollMove ();
			obj.transform.position = new Vector3 (obj.position.x, obj.position.y - 4.0f, obj.position.z);
			if(s.flipX == false)
				obj.transform.rotation = Quaternion.Euler(obj.rotation.x, obj.rotation.y, -90.0f);
			else
				obj.transform.rotation = Quaternion.Euler(obj.rotation.x, obj.rotation.y, 90.0f);
		}
	}
}