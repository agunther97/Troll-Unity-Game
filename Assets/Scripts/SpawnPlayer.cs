using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {
	public GameObject Spawns;
	private Transform obj;
	private Vector3 current;
	private Vector3 offset;
	public Camera maincam;
	private SpriteRenderer s;
	void Start () {
		int random = Random.Range(0, Spawns.transform.childCount);
		print (random);
		obj = Spawns.transform.GetChild (random);
		obj.name = "Player";
		Spawns.transform.GetChild (random).gameObject.SetActive (true);
		offset = new Vector3 (0.0f, 0.0f, -1.0f);
		current = obj.transform.position;
		current = new Vector3 (current.x, current.y, -1.0f);
		transform.position = current;
		maincam.orthographicSize = 20.0f;
	}

	// Update is called once per frame
	void Update () {
		transform.position = obj.transform.position + offset;
		if (Input.GetKeyDown ("d")) {
			s = obj.GetComponent<SpriteRenderer>();
			s.flipX = false;
			obj.transform.position = new Vector3 (obj.position.x + 1.0f, obj.position.y, obj.position.z); 
		}
		if (Input.GetKeyDown ("w")) {
			obj.transform.position = new Vector3 (obj.position.x, obj.position.y+1.0f, obj.position.z); 
		}
		if (Input.GetKeyDown ("a")) {
			s = obj.GetComponent<SpriteRenderer>();
			s.flipX = true;
			obj.transform.position = new Vector3 (obj.position.x - 1.0f, obj.position.y, obj.position.z); 
		}
		if (Input.GetKeyDown ("s")) {
			obj.transform.position = new Vector3 (obj.position.x, obj.position.y - 1.0f, obj.position.z); 
		}
	}
}