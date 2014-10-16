using UnityEngine;
using System.Collections;

public class WorldScript : MonoBehaviour {

    static int dropletsPerSecond;
	// Use this for initialization
    Object droplet;
    System.Random random = new System.Random();

    GameObject umbrella;
    Object umbrellaPrefab;

    GameObject player;

    float rainSize, rainLeftPos;


	void Start () {
		//dropletsPerSecond = 30;
        dropletsPerSecond = 40;
		//dropletsPerSecond = 15;
        //dropletsPerSecond = 1;
        droplet = Resources.Load("DropletPrefab");
        //umbrellaPrefab = Resources.Load("umbrella");
        player = GameObject.Find("Player");
        rainSize = 100;
	}

    float dropletsFraction = 0;
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.LoadLevel(0);

		if (Input.GetKeyDown(KeyCode.P))
			Debug.Break();

        dropletsFraction += Time.deltaTime * dropletsPerSecond;


        int droplets = (int)dropletsFraction; 

        if(droplets > 0)
        {
            dropletsFraction -= droplets;
        }


		if (player == null)
			return;
        rainLeftPos = player.transform.position.x - rainSize / 2;

        for (int i = 0; i < droplets; i++)
        {
            int rnd = random.Next();
            float withDecimals = rnd / 100.0f;
            float clamped = withDecimals % rainSize;

            //newthingy.transform.position = new Vector2(clamped - rainLeftPos, 22);
            GameObject newthingy = Instantiate(droplet, new Vector2(clamped + rainLeftPos, 22), new Quaternion()) as GameObject;
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
