using UnityEngine;
using System.Collections;

public class WorldScript : MonoBehaviour {

	// Use this for initialization
    Object droplet;
    System.Random random = new System.Random();

    public float leftPos;
    public float rightPos;
    public int scale;


	void Start () {
        droplet = Resources.Load("DropletPrefab");
        GameObject floor = GameObject.Find("Floor");
        scale = (int)floor.transform.localScale.x;

        leftPos = scale / 2;
        rightPos = scale / 2;
	}
	
	// Update is called once per frame
	void Update () {
        GameObject newthingy = Instantiate(droplet, new Vector3(0,15,0), new Quaternion()) as GameObject;
        newthingy.transform.position = new Vector2(random.Next(scale) - leftPos, 15);
	}
}
