using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {
	public GameObject Spawns;
	private Transform obj;
	void Start () {
		int random = Random.Range(0, Spawns.transform.childCount);
		print (random);
		obj = Spawns.transform.GetChild (random);
		obj.name = "Player";
		Spawns.transform.GetChild (random).gameObject.SetActive (true);
	}

	// Update is called once per frame
	void Update () {

	}
}
