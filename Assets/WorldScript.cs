using UnityEngine;
using System.Collections;

public class WorldScript : MonoBehaviour {

    static int dropletsPerSecond;
	// Use this for initialization
    Object droplet;
    System.Random random = new System.Random();

    public float leftPos;
    public float rightPos;
    public int scale;
    GameObject umbrella;
    Object umbrellaPrefab;


	void Start () {
        dropletsPerSecond = 15;
		//dropletsPerSecond = 1;
        droplet = Resources.Load("DropletPrefab");
        //umbrellaPrefab = Resources.Load("umbrella");

        leftPos = scale / 2;
        rightPos = scale / 2;
	}

    float dropletsFraction = 0;
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.LoadLevel(0);

        dropletsFraction += Time.deltaTime * dropletsPerSecond;


        int droplets = (int)dropletsFraction; 

        if(droplets > 0)
        {
            dropletsFraction -= droplets;
        }

        for (int i = 0; i < droplets; i++)
        {
            GameObject newthingy = Instantiate(droplet, new Vector3(0, 20, 0), new Quaternion()) as GameObject;
            int rnd = random.Next();
            float withDecimals = rnd / 100.0f;
            float max = scale;
            float clamped = withDecimals % max;

            newthingy.transform.position = new Vector2(clamped - leftPos, 22);
        }
        //int r = random.Next(100);

        //if (umbrella == null && r > 98)
        //{
        //    //umbrella = Instantiate(umbrellaPrefab) as GameObject;
        //    //umbrella.transform.position = new Vector3(0,0,0);
        //}
	}

    public static void GameOver()
    {
        //dropletsPerFrame = 20;
    }

}
