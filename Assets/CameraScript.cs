using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    GameObject player;
	float lastPos;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
		lastPos = player.transform.position.x;
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
	}

	void Update () {
        if (player == null) return;

		float currentPos = player.transform.position.x;

		if (currentPos <= lastPos)
			return;

        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
		lastPos = currentPos;
	}
}
