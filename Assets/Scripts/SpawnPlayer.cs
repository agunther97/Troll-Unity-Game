using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {
	public GameObject Spawns;
	private Transform obj;
	private Vector3 offset;   
	void Start () {
		int random = Random.Range(0, Spawns.transform.childCount);
		print (random);
		obj = Spawns.transform.GetChild (random);
		obj.name = "Player";
		Spawns.transform.GetChild (random).gameObject.SetActive (true);
		offset = transform.position - obj.transform.position;
	}

	// Update is called once per frame
	void Update () {
		transform.position = obj.transform.position + offset;
	}
}