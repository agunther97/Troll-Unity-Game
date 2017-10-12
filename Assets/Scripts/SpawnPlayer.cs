using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {
	public GameObject Spawns;
	private Transform obj;
	private Vector3 current;
	private Vector3 offset;
	private Vector3 oldpos;
	private Quaternion oldrotation;
	public Camera maincam;
	private SpriteRenderer s;
	private Rigidbody2D r;
	void Start () {
		int random = Random.Range(0, Spawns.transform.childCount);
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
		offset = new Vector3 (0.0f, 0.0f, -1.0f);
		current = obj.transform.position;
		current = new Vector3 (current.x, current.y, -1.0f);
		transform.position = current;
		maincam.orthographicSize = 20.0f;
	}

	// Update is called once per frame
	void FixedUpdate () {
		transform.position = obj.transform.position + offset;
		if (Input.GetKeyDown ("d")) {
			s.flipX = false;
			oldpos = obj.transform.position;
			oldrotation = obj.transform.rotation;
			//r.AddForce(new Vector2(1000, 0));
			obj.transform.position = new Vector3 (obj.position.x + 4f, obj.position.y, obj.position.z);
			obj.transform.rotation = Quaternion.Euler(obj.transform.rotation.x, obj.transform.rotation.y, 0.0f);
		}
		if (Input.GetKeyDown ("w")) {
			oldpos = obj.transform.position;
			oldrotation = obj.transform.rotation;
			//r.AddForce(new Vector2 (0, 1000));
			obj.transform.position = new Vector3 (obj.position.x, obj.position.y+4.0f, obj.position.z); 
			if(s.flipX == false)
				obj.transform.rotation = Quaternion.Euler(obj.rotation.x, obj.rotation.y, 90.0f);
			else
				obj.transform.rotation = Quaternion.Euler(obj.rotation.x, obj.rotation.y, -90.0f);
		}
		if (Input.GetKeyDown ("a")) {
			s = obj.GetComponent<SpriteRenderer>();
			s.flipX = true;
			oldpos = obj.transform.position;
			oldrotation = obj.transform.rotation;
			//r.AddForce(new Vector2 (-1000, 0.0f));
			obj.transform.position = new Vector3 (obj.position.x - 4.4f, obj.position.y, obj.position.z);
			obj.transform.rotation = Quaternion.Euler(obj.rotation.x, obj.rotation.y, 0.0f);
		}
		if (Input.GetKeyDown ("s")) {
			oldpos = obj.transform.position;
			oldrotation = obj.transform.rotation;
			//r.AddForce(new Vector2 (0, -1000));
			obj.transform.position = new Vector3 (obj.position.x, obj.position.y - 4.0f, obj.position.z);
			if(s.flipX == false)
				obj.transform.rotation = Quaternion.Euler(obj.rotation.x, obj.rotation.y, -90.0f);
			else
				obj.transform.rotation = Quaternion.Euler(obj.rotation.x, obj.rotation.y, 90.0f);
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		print ("ran");
		if (col.gameObject.tag == "Wall") {
			obj.transform.position = oldpos;
			obj.transform.rotation = oldrotation;
		}
	}
}