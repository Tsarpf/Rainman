using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveController : MonoBehaviour {

    Texture2D boxTexture;
    GUIStyle style = new GUIStyle();
    static public Dictionary<string, GameObject> parts;

    float left, right, top, controlCount, individualControlWidth, individualControlHeight;
    void Start()
    {
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
	}


    bool goLeft = false;
    bool goRight = false;
    bool jump = false;
    void getInput()
    {
        jump = false;
        goLeft = false;
        goRight = false;

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

    void Update()
    {
        getInput();
    }
    Vector2 playerSpeed = new Vector2(10, 10);
	void FixedUpdate () {

        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        gameObject.rigidbody2D.velocity = new Vector3(0, gameObject.rigidbody2D.velocity.y, 0);

        //float deltaTime = Time.deltaTime;

        if (jump && isOnGround)
        {
            gameObject.rigidbody2D.velocity = new Vector2(gameObject.rigidbody2D.velocity.x, playerSpeed.y);
        }


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
            float dist = Mathf.Abs((walkTotalProgress % Mathf.PI) - Mathf.PI / 2.0f);
            if((dist + walkStep) < Mathf.PI / 2.0f)
            {
                walkTotalProgress -= walkStep * 2;
            }
            else if((dist - walkStep) > Mathf.PI / 2.0f)
            {
                walkTotalProgress += walkStep * 2;
            }

            animateLegs();
            animateHands();

            //gameObject.rigidbody2D.velocity = new Vector2(0, gameObject.rigidbody2D.velocity.y);
        }

        //jump = false;
        //goLeft = false;
        //goRight = false;



        //transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
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
        isOnGround = true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.name == "DropletPrefab(Clone)")
        {
            Destroy(gameObject); 
        }
    }

    void OnCollisionExit2D()
    {
        Debug.Log("spam");
        isOnGround = false;
    }

    void OnCollisionStay2D()
    {
        //Debug.Log("stay");
    }
}
