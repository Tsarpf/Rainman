	using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveController : MonoBehaviour {

    Texture2D boxTexture;
    GUIStyle style = new GUIStyle();
    static public Dictionary<string, GameObject> parts;

	System.Random random;

    float left, right, top, controlCount, individualControlWidth, individualControlHeight;

	Object puddle;

    GameObject[] backgrounds;
    void Start()
    {
		random = new System.Random(System.DateTime.Now.Millisecond);

        puddle = Resources.Load("PuddlePrefab");
		backgrounds = new GameObject[]
		{
            GameObject.Find("Floor1"),
            GameObject.Find("Floor2"),
            GameObject.Find("Floor3")
        };
        parts = new Dictionary<string, GameObject>();
	    foreach(Transform part in transform)
        {
            parts[part.name] = part.gameObject;
        }

        boxTexture = new Texture2D(1, 1);	
        for(int y = 0; y < boxTexture.height; y++)
        {
            for(int x = 0; x < boxTexture.width; x++)
            {
                boxTexture.SetPixel(x, y, new Color(1, 1, 1, 1));
            }
        }

        style.normal.background = boxTexture;

        left = Screen.width / 3.0f;
    	right = Screen.width * (2.0f / 3.0f);
    	controlCount = 3;
    	individualControlWidth = (right - left) / controlCount;
    	individualControlHeight = Screen.height / 10;

		backgroundScale = GameObject.Find("Floor2").transform.localScale.x;
	}

    bool goLeft = false;
    bool goRight = false;
    bool jump = false;
    void getInput()
    {
        jump = false;
        goLeft = false;
        goRight = false;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            goLeft = true;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            goRight = true;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            jump = true;
        }

        if (!Input.GetMouseButton(0))
            return;

        float x = Input.mousePosition.x;
        float y = Input.mousePosition.y;

        float controlTop = individualControlHeight * 3;

        //jump
        if(y < controlTop && y > individualControlHeight)
        {
            jump = true;
        }

        int totalWidth = 300;
        int xOffset = Screen.width / 2 - totalWidth / 2;
        //reft
        //if(x < 100 + xOffset && y < 400)
        if(x < left + individualControlWidth && y < controlTop)
        {
            goLeft = true;
        }

        //light
        else if(x > right - individualControlWidth && y < controlTop)
        {
            goRight = true;
        }


    }

	int playerOldPos = 0;
	float backgroundScale;
	float lowestX=0,highestX=0;
	int puddleSpawnRatePercent = 25;
	
	void newPuddle(Vector3 position, Quaternion rot)
	{
	    Instantiate(puddle, position, rot);
	}
	void newPuddle(float xPos)
	{
		GameObject newPuddle = Instantiate(puddle) as GameObject;
		newPuddle.transform.position = new Vector3(xPos, newPuddle.transform.position.y, newPuddle.transform.position.z);
	}
	void createNewPuddles(int playerCurrentPos)
	{
		if(playerCurrentPos > highestX)
		{
			if (random.Next(0, 100) > puddleSpawnRatePercent)
				return;

			float lowerX = playerCurrentPos * backgroundScale + backgroundScale;
			//float lowerX = playerCurrentPos * backgroundScale;
			float upperX = lowerX + backgroundScale;
			float xPos = random.Next((int)lowerX, (int)upperX);
			Debug.Log("spawning puddle " + xPos);
			newPuddle(xPos);
			highestX = playerCurrentPos;
		}
		else if(playerCurrentPos < lowestX)
		{
			if (random.Next(0, 100) > puddleSpawnRatePercent)
				return;

			float upperX = playerCurrentPos * backgroundScale;
			float lowerX = upperX - backgroundScale;
			float xPos = random.Next((int)lowerX, (int)upperX);
			Debug.Log("spawning puddle " + xPos);
			newPuddle(xPos);
			lowestX = playerCurrentPos;
		}
	}
    void endlessMap()
	{
        int playerCurrentPos = (int)(transform.position.x / backgroundScale);

		//These have to be before the ugly hack below 

		if (transform.position.x < 0) //A bit ugly hack so hanging around 0 works too... :D
			playerCurrentPos--;

		if (playerOldPos == playerCurrentPos)
			return;

		createNewPuddles(playerCurrentPos);

		Debug.Log("player current position: " + playerCurrentPos);

        if(playerCurrentPos > playerOldPos)
		{
            //plus
			GameObject temp = backgrounds[2];
			backgrounds[2] = backgrounds[0];
			backgrounds[0] = backgrounds[1];
			backgrounds[1] = temp;

			backgrounds[2].transform.position = new Vector2(backgrounds[2].transform.position.x + backgroundScale * 3, backgrounds[2].transform.position.y);
		}
        else
		{
            //minus
			GameObject temp = backgrounds[0];
			backgrounds[0] = backgrounds[2];
			backgrounds[2] = backgrounds[1];
			backgrounds[1] = temp;

			backgrounds[0].transform.position = new Vector2(backgrounds[0].transform.position.x - backgroundScale * 3, backgrounds[0].transform.position.y);
		}

		playerOldPos = playerCurrentPos;
	}
    void OnGUI()
    {
        //Jump
        GUI.color = Color.blue;
    //left, top, width, height
        GUI.Box(new Rect(left, Screen.height - individualControlHeight * 2, individualControlWidth * 3, individualControlHeight), "This is a title", style);
        //Left
        GUI.color = Color.red;
    //left, top, width, height
        GUI.Box(new Rect(left, Screen.height - individualControlHeight, individualControlWidth, individualControlHeight * 2), "This is a title", style);
        //Right
        GUI.color = Color.green;
    //left, top, width, height
        GUI.Box(new Rect(left + individualControlWidth * 2, Screen.height - individualControlHeight, individualControlWidth, individualControlHeight * 2), "This is a title", style);
    }
    bool isOnGround = false;
    //int playerSpeed = 10;

    Vector2 playerSpeed = new Vector2(10, 10);
    const float jumpSpeed = 10;
    float currentJumpSpeed = -10;

    float lastProgress = 0;
    float lastSinProgress = 0;
	void Update () {
        getInput();
		endlessMap();

        //Debug.Log(Mathf.Sin(walkTotalProgress) + "      " + walkTotalProgress);

        if (transform.position.y < 2.50f)
        {
            transform.position = new Vector3(transform.position.x, 2.5f);
            isOnGround = true;
            currentJumpSpeed = 0;
        }
        if (jump && isOnGround)
        {
            isOnGround = false;
            currentJumpSpeed = jumpSpeed;
        }

        transform.position = new Vector2(transform.position.x, transform.position.y + currentJumpSpeed * Time.deltaTime);
        currentJumpSpeed -= (10.0f * Time.deltaTime);

        if (goLeft)
        {
            transform.position = new Vector2(transform.position.x - playerSpeed.x * Time.deltaTime, transform.position.y);
            leftWalkAnimate();
        }
        else if (goRight)
        {
            transform.position = new Vector2(transform.position.x + playerSpeed.x * Time.deltaTime, transform.position.y);
            rightWalkAnimate();
        }
        else
        {
            float sind = Mathf.Sin(walkTotalProgress);
            float mod = walkTotalProgress % Mathf.PI;
            float distance = Mathf.Abs(mod);
            if (distance > walkStep * 2)
            {
                if (distance > Mathf.PI / 2)
                {
                    if (mod >= 0)
                        walkTotalProgress += walkStep * 2;
                    else
                        walkTotalProgress -= walkStep * 2;
                }
                else if (distance <= Mathf.PI / 2)
                {
                    if (mod >= 0)
                        walkTotalProgress -= walkStep * 2;
                    else
                        walkTotalProgress += walkStep * 2;

                }
            }
            animateLegs();
            animateHands();
        }
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
	}
    static float walkTotalProgress = 0f;
    const float walkStep = 0.1f;
    void leftWalkAnimate()
    {
        walkTotalProgress-=walkStep;
        //Debug.Log("progress: " + walkTotalProgress + " sin'd: " + Mathf.Sin(walkTotalProgress));
        transform.localEulerAngles = new Vector3(0, 180, 0);
        animateHands();
        animateLegs();
    }
    void rightWalkAnimate()
    {
        walkTotalProgress+=walkStep;
        transform.localEulerAngles = new Vector3(0, 0, 0);
        animateHands();
        animateLegs();
    }
    void animateLegs()
    {
        parts["leftLeg"].transform.localEulerAngles = new Vector3(0, 0, Mathf.Sin(walkTotalProgress * 1) * 45);
        parts["rightLeg"].transform.localEulerAngles = new Vector3(0, 0, Mathf.Sin(walkTotalProgress * 1) * -45);
        //parts["leftLeg"].transform.localEulerAngles = new Vector3(0, 0, Mathf.Sin(walkTotalProgress) * 90);
        //parts["rightLeg"].transform.localEulerAngles = new Vector3(0, 0, Mathf.Sin(walkTotalProgress) * -90);
    }
    void animateHands()
    {
        parts["leftArm"].transform.localEulerAngles = new Vector3(0, 0, Mathf.Sin(walkTotalProgress) * 45);
        parts["rightArm"].transform.localEulerAngles = new Vector3(0, 0, Mathf.Sin(walkTotalProgress) * -45);
        //parts["leftArm"].transform.localEulerAngles = new Vector3(0, 0, Mathf.Sin(walkTotalProgress) * 90);
        //parts["rightArm"].transform.localEulerAngles = new Vector3(0, 0, Mathf.Sin(walkTotalProgress) * -90);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
		Debug.Log(collision.collider.name);
        //isOnGround = true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
		Debug.Log(collider.name);
        if(collider.name == "DropletPrefab(Clone)")
        {
			hitDroplet();
        }
		if (collider.name == "PuddlePrefab(Clone)")
		{
			Debug.Log("hit puddle");
			hitPuddle();
		}
    }

	void OnTriggerStay2D(Collider2D collider)
	{
		if (collider.name == "PuddlePrefab(Clone)")
		{
			Debug.Log("hit puddle");
			hitPuddle();
		}
	}
	
	void hitDroplet()
	{
		if(Player.umbrellaUses > 0)
		{
			Player.umbrellaUses--;
			return;
		}	
		Destroy(gameObject); 
	}

	float puddleHitGracePeriod = 2;
	float lastPuddleHit = 0;
	void hitPuddle()
	{
		if(Time.time - lastPuddleHit >= puddleHitGracePeriod)
		{
			if(Player.wellieUses == 0)
			{
				Destroy(gameObject);
			}
			else if(Player.wellieUses > 0)
			{
				Player.wellieUses--;
			}
			lastPuddleHit = Time.time;
		}
	}

    void OnCollisionExit2D()
    {
        //Debug.Log("spam");
        //isOnGround = false;
    }

    void OnCollisionStay2D()
    {
        Debug.Log("stay");
    }
}
