using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour {
	string line1 = "#########################################################################";
	string line2 = "#   #               #               #           #                   #   #";
	string line3 = "#   #   #########   #   #####   #########   #####   #####   #####   #   #";
	string line4 = "#               #       #   #           #           #   #   #       #   #";
	string line5 = "#########   #   #########   #########   #####   #   #   #   #########   #";
	string line6 = "#       #   #               #           #   #   #   #   #           #   #";
	string line7 = "#   #   #############   #   #   #########   #####   #   #########   #   #";
	string line8 = "#   #               #   #   #       #           #           #       #   #";
	string line9 = "#   #############   #####   #####   #   #####   #########   #   #####   #";
	string line10 ="#           #       #   #       #   #       #           #   #           #";
	string line11 ="#   #####   #####   #   #####   #   #########   #   #   #   #############";
	string line12 ="#       #       #   #   #       #       #       #   #   #       #       #";
	string line13 ="#############   #   #   #   #########   #   #####   #   #####   #####   #";
	string line14 ="#           #   #           #       #   #       #   #       #           #";
	string line15 ="#   #####   #   #########   #####   #   #####   #####   #############   #";
	string line16 ="#   #       #           #           #       #   #   #               #   #";
	string line17 ="#   #   #########   #   #####   #########   #   #   #############   #   #";
	string line18 ="#   #           #   #   #   #   #           #               #   #       #";
	string line19 ="#   #########   #   #   #   #####   #########   #########   #   #########";
	string line20 ="#   #       #   #   #           #           #   #       #               #";
	string line21 ="#   #   #####   #####   #####   #########   #####   #   #########   #   #";
	string line22 ="#   #                   #           #               #               #   #";
	string line23 ="# X #####################################################################";
	public GameObject firstwall;
	public GameObject wallparent;
	private GameObject obj;
	private float temp = 0;
	private float temp2 = 16f;
	int j = 83;
	int count = 0;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < line3.Length; i++) {
			if (line3 [i] == '#') {
				count++;
				j = j + count;
				obj = Instantiate (firstwall, wallparent.transform);
				Vector3 newPos = new Vector3 (temp, temp2, firstwall.transform.position.z);
				obj.transform.position = newPos;
				obj.name = "Wall (" + j + ")";
				count = 0;
				firstwall = obj;
				temp = temp + 6.2f;
				continue;
			}
			temp = temp + 6.2f;
		}
		temp2 = temp2 + 8f;
		temp = 0;
		for (int i = 0; i < line4.Length; i++) {
			if (line4 [i] == '#') {
				count++;
				j = j + count;
				obj = Instantiate (firstwall, wallparent.transform);
				Vector3 newPos = new Vector3 (temp, temp2, firstwall.transform.position.z);
				obj.transform.position = newPos;
				obj.name = "Wall (" + j + ")";
				count = 0;
				firstwall = obj;
				temp = temp + 6.2f;
				continue;
			}
			temp = temp + 6.2f;
		}
		temp2 = temp2 + 8f;
		temp = 0;
		for (int i = 0; i < line5.Length; i++) {
			if (line5 [i] == '#') {
				count++;
				j = j + count;
				obj = Instantiate (firstwall, wallparent.transform);
				Vector3 newPos = new Vector3 (temp, temp2, firstwall.transform.position.z);
				obj.transform.position = newPos;
				obj.name = "Wall (" + j + ")";
				count = 0;
				firstwall = obj;
				temp = temp + 6.2f;
				continue;
			}
			temp = temp + 6.2f;
		}
		temp2 = temp2 + 8f;
		temp = 0;
		for (int i = 0; i < line6.Length; i++) {
			if (line6 [i] == '#') {
				count++;
				j = j + count;
				obj = Instantiate (firstwall, wallparent.transform);
				Vector3 newPos = new Vector3 (temp, temp2, firstwall.transform.position.z);
				obj.transform.position = newPos;
				obj.name = "Wall (" + j + ")";
				count = 0;
				firstwall = obj;
				temp = temp + 6.2f;
				continue;
			}
			temp = temp + 6.2f;
		}
		temp2 = temp2 + 8f;
		temp = 0;
		for (int i = 0; i < line7.Length; i++) {
			if (line7 [i] == '#') {
				count++;
				j = j + count;
				obj = Instantiate (firstwall, wallparent.transform);
				Vector3 newPos = new Vector3 (temp, temp2, firstwall.transform.position.z);
				obj.transform.position = newPos;
				obj.name = "Wall (" + j + ")";
				count = 0;
				firstwall = obj;
				temp = temp + 6.2f;
				continue;
			}
			temp = temp + 6.2f;
		}
		temp2 = temp2 + 8f;
		temp = 0;
		for (int i = 0; i < line8.Length; i++) {
			if (line8 [i] == '#') {
				count++;
				j = j + count;
				obj = Instantiate (firstwall, wallparent.transform);
				Vector3 newPos = new Vector3 (temp, temp2, firstwall.transform.position.z);
				obj.transform.position = newPos;
				obj.name = "Wall (" + j + ")";
				count = 0;
				firstwall = obj;
				temp = temp + 6.2f;
				continue;
			}
			temp = temp + 6.2f;
		}
		temp2 = temp2 + 8f;
		temp = 0;
		for (int i = 0; i < line9.Length; i++) {
			if (line9 [i] == '#') {
				count++;
				j = j + count;
				obj = Instantiate (firstwall, wallparent.transform);
				Vector3 newPos = new Vector3 (temp, temp2, firstwall.transform.position.z);
				obj.transform.position = newPos;
				obj.name = "Wall (" + j + ")";
				count = 0;
				firstwall = obj;
				temp = temp + 6.2f;
				continue;
			}
			temp = temp + 6.2f;
		}
		temp2 = temp2 + 8f;
		temp = 0;
		for (int i = 0; i < line10.Length; i++) {
			if (line10 [i] == '#') {
				count++;
				j = j + count;
				obj = Instantiate (firstwall, wallparent.transform);
				Vector3 newPos = new Vector3 (temp, temp2, firstwall.transform.position.z);
				obj.transform.position = newPos;
				obj.name = "Wall (" + j + ")";
				count = 0;
				firstwall = obj;
				temp = temp + 6.2f;
				continue;
			}
			temp = temp + 6.2f;
		}
		temp2 = temp2 + 8f;
		temp = 0;
		for (int i = 0; i < line11.Length; i++) {
			if (line11 [i] == '#') {
				count++;
				j = j + count;
				obj = Instantiate (firstwall, wallparent.transform);
				Vector3 newPos = new Vector3 (temp, temp2, firstwall.transform.position.z);
				obj.transform.position = newPos;
				obj.name = "Wall (" + j + ")";
				count = 0;
				firstwall = obj;
				temp = temp + 6.2f;
				continue;
			}
			temp = temp + 6.2f;
		}
		temp2 = temp2 + 8f;
		temp = 0;
		for (int i = 0; i < line12.Length; i++) {
			if (line12 [i] == '#') {
				count++;
				j = j + count;
				obj = Instantiate (firstwall, wallparent.transform);
				Vector3 newPos = new Vector3 (temp, temp2, firstwall.transform.position.z);
				obj.transform.position = newPos;
				obj.name = "Wall (" + j + ")";
				count = 0;
				firstwall = obj;
				temp = temp + 6.2f;
				continue;
			}
			temp = temp + 6.2f;
		}
		temp2 = temp2 + 8f;
		temp = 0;
		for (int i = 0; i < line13.Length; i++) {
			if (line13 [i] == '#') {
				count++;
				j = j + count;
				obj = Instantiate (firstwall, wallparent.transform);
				Vector3 newPos = new Vector3 (temp, temp2, firstwall.transform.position.z);
				obj.transform.position = newPos;
				obj.name = "Wall (" + j + ")";
				count = 0;
				firstwall = obj;
				temp = temp + 6.2f;
				continue;
			}
			temp = temp + 6.2f;
		}
		temp2 = temp2 + 8f;
		temp = 0;
		for (int i = 0; i < line14.Length; i++) {
			if (line14 [i] == '#') {
				count++;
				j = j + count;
				obj = Instantiate (firstwall, wallparent.transform);
				Vector3 newPos = new Vector3 (temp, temp2, firstwall.transform.position.z);
				obj.transform.position = newPos;
				obj.name = "Wall (" + j + ")";
				count = 0;
				firstwall = obj;
				temp = temp + 6.2f;
				continue;
			}
			temp = temp + 6.2f;
		}
		temp2 = temp2 + 8f;
		temp = 0;
		for (int i = 0; i < line15.Length; i++) {
			if (line15 [i] == '#') {
				count++;
				j = j + count;
				obj = Instantiate (firstwall, wallparent.transform);
				Vector3 newPos = new Vector3 (temp, temp2, firstwall.transform.position.z);
				obj.transform.position = newPos;
				obj.name = "Wall (" + j + ")";
				count = 0;
				firstwall = obj;
				temp = temp + 6.2f;
				continue;
			}
			temp = temp + 6.2f;
		}
		temp2 = temp2 + 8f;
		temp = 0;
		for (int i = 0; i < line16.Length; i++) {
			if (line16 [i] == '#') {
				count++;
				j = j + count;
				obj = Instantiate (firstwall, wallparent.transform);
				Vector3 newPos = new Vector3 (temp, temp2, firstwall.transform.position.z);
				obj.transform.position = newPos;
				obj.name = "Wall (" + j + ")";
				count = 0;
				firstwall = obj;
				temp = temp + 6.2f;
				continue;
			}
			temp = temp + 6.2f;
		}
		temp2 = temp2 + 8f;
		temp = 0;
		for (int i = 0; i < line17.Length; i++) {
			if (line17 [i] == '#') {
				count++;
				j = j + count;
				obj = Instantiate (firstwall, wallparent.transform);
				Vector3 newPos = new Vector3 (temp, temp2, firstwall.transform.position.z);
				obj.transform.position = newPos;
				obj.name = "Wall (" + j + ")";
				count = 0;
				firstwall = obj;
				temp = temp + 6.2f;
				continue;
			}
			temp = temp + 6.2f;
		}
		temp2 = temp2 + 8f;
		temp = 0;
		for (int i = 0; i < line18.Length; i++) {
			if (line18 [i] == '#') {
				count++;
				j = j + count;
				obj = Instantiate (firstwall, wallparent.transform);
				Vector3 newPos = new Vector3 (temp, temp2, firstwall.transform.position.z);
				obj.transform.position = newPos;
				obj.name = "Wall (" + j + ")";
				count = 0;
				firstwall = obj;
				temp = temp + 6.2f;
				continue;
			}
			temp = temp + 6.2f;
		}
		temp2 = temp2 + 8f;
		temp = 0;
		for (int i = 0; i < line19.Length; i++) {
			if (line19 [i] == '#') {
				count++;
				j = j + count;
				obj = Instantiate (firstwall, wallparent.transform);
				Vector3 newPos = new Vector3 (temp, temp2, firstwall.transform.position.z);
				obj.transform.position = newPos;
				obj.name = "Wall (" + j + ")";
				count = 0;
				firstwall = obj;
				temp = temp + 6.2f;
				continue;
			}
			temp = temp + 6.2f;
		}
		temp2 = temp2 + 8f;
		temp = 0;
		for (int i = 0; i < line20.Length; i++) {
			if (line20 [i] == '#') {
				count++;
				j = j + count;
				obj = Instantiate (firstwall, wallparent.transform);
				Vector3 newPos = new Vector3 (temp, temp2, firstwall.transform.position.z);
				obj.transform.position = newPos;
				obj.name = "Wall (" + j + ")";
				count = 0;
				firstwall = obj;
				temp = temp + 6.2f;
				continue;
			}
			temp = temp + 6.2f;
		}
		temp2 = temp2 + 8f;
		temp = 0;
		for (int i = 0; i < line21.Length; i++) {
			if (line21 [i] == '#') {
				count++;
				j = j + count;
				obj = Instantiate (firstwall, wallparent.transform);
				Vector3 newPos = new Vector3 (temp, temp2, firstwall.transform.position.z);
				obj.transform.position = newPos;
				obj.name = "Wall (" + j + ")";
				count = 0;
				firstwall = obj;
				temp = temp + 6.2f;
				continue;
			}
			temp = temp + 6.2f;
		}
		temp2 = temp2 + 8f;
		temp = 0;
		for (int i = 0; i < line22.Length; i++) {
			if (line22 [i] == '#') {
				count++;
				j = j + count;
				obj = Instantiate (firstwall, wallparent.transform);
				Vector3 newPos = new Vector3 (temp, temp2, firstwall.transform.position.z);
				obj.transform.position = newPos;
				obj.name = "Wall (" + j + ")";
				count = 0;
				firstwall = obj;
				temp = temp + 6.2f;
				continue;
			}
			temp = temp + 6.2f;
		}
		temp2 = temp2 + 8f;
		temp = 0;
		for (int i = 0; i < line23.Length; i++) {
			if (line23 [i] == '#') {
				count++;
				j = j + count;
				obj = Instantiate (firstwall, wallparent.transform);
				Vector3 newPos = new Vector3 (temp, temp2, firstwall.transform.position.z);
				obj.transform.position = newPos;
				obj.name = "Wall (" + j + ")";
				count = 0;
				firstwall = obj;
				temp = temp + 6.2f;
				continue;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}