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
        int dropletsPerFrame = 4;
        for (int i = 0; i < dropletsPerFrame; i++)
        {
            GameObject newthingy = Instantiate(droplet, new Vector3(0, 15, 0), new Quaternion()) as GameObject;
            int rnd = random.Next();
            float withDecimals = rnd / 100.0f;
            float max = scale;
            float clamped = withDecimals % max;

            newthingy.transform.position = new Vector2(clamped - leftPos, 20);
        }
	}
}
