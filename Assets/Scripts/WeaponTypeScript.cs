using UnityEngine;
using System.Collections;

public class WeaponTypeScript : MonoBehaviour
{

    Camera camera;
    float camHeight;
    float camWidth;

    public int typeSelected;

    SpriteRenderer type1;
    SpriteRenderer type2;
    SpriteRenderer type3;
    SpriteRenderer type4;

    GameObject type1Obj;
    GameObject type2Obj;
    GameObject type3Obj;
    GameObject type4Obj;

    public Sprite singleShot;
    public Sprite singleShotSel;
    public Sprite burstShot;
    public Sprite burstShotSel;
    public Sprite powerUp3;
    public Sprite powerUp3Sel;
    public Sprite powerUp4;
    public Sprite powerUp4Sel;
    public Sprite met3;
    public Sprite met4;


    public bool p3Filled;
    public bool p4Filled;

    int[] powerUpTypes;

    float halfWidth = Screen.width / 2;
    float screenHeight = Screen.height;

    private MeteorShieldScript metShieldScript;


    void Start ()
    {
        p3Filled = false;
        p4Filled = false;

        powerUpTypes = new int[2] { 0, 0 };

        type1Obj = GameObject.Find("Weapon1");
        type2Obj = GameObject.Find("Weapon2");
        type3Obj = GameObject.Find("Weapon3");
        type4Obj = GameObject.Find("Weapon4");

        type1 = transform.FindChild("Weapon1").GetComponent<SpriteRenderer>();
        type2 = transform.FindChild("Weapon2").GetComponent<SpriteRenderer>();
        type3 = transform.FindChild("Weapon3").GetComponent<SpriteRenderer>();
        type4 = transform.FindChild("Weapon4").GetComponent<SpriteRenderer>();

        typeSelected = 1;

        camera = Camera.main;
        camHeight = 2f * camera.orthographicSize;
        camWidth = camHeight * camera.aspect;

        type1Obj.transform.position = new Vector3((-camWidth + camWidth) - 1.75f, -camHeight / 2 + 0.5f, -1);
        type2Obj.transform.position = new Vector3((-camWidth + camWidth) - 0.75f, -camHeight / 2 + 0.5f, -1);
        type3Obj.transform.position = new Vector3((-camWidth + camWidth) + 0.75f, -camHeight / 2 + 0.5f, -1);
        type4Obj.transform.position = new Vector3((-camWidth + camWidth) + 1.75f, -camHeight / 2 + 0.5f, -1);

        metShieldScript = GameObject.Find("Player/MeteorShield").GetComponent<MeteorShieldScript>();
    }

    // Update is called once per frame
    void Update ()
    {
	    switch(typeSelected)
        {
            case 1:
                type1.sprite = singleShotSel;
                type2.sprite = burstShot;
                break;

            case 2:
                type1.sprite = singleShot;
                type2.sprite = burstShotSel;
                break;

            default:

                break;
        }
	}

    public void AddPowerUp(string type)
    {
        if(!p3Filled)
        {
            p3Filled = true;
            
            if(type == "MeteorShield")
            {
                type3.sprite = met3;
                powerUpTypes[0] = 1;
            }
        }

        else
        {
            if(!p4Filled)
            {
                p4Filled = true;

                if(type == "MeteorShield")
                {
                    type4.sprite = met4;
                    powerUpTypes[1] = 1;
                }
            }
        }
    }

    public void UsedPowerUp(int numberKey)
    {
        if(powerUpTypes[numberKey - 3] != 0)
        {
            if(numberKey - 3 == 0)
            {
                type3.sprite = powerUp3;
                metShieldScript.CreateShield();
                p3Filled = false;
                powerUpTypes[numberKey - 3] = 0;
            }

            else if(numberKey - 3 == 1)
            {
                type4.sprite = powerUp4;
                metShieldScript.CreateShield();
                p4Filled = false;
                powerUpTypes[numberKey - 3] = 0;

            }

            else
            {
                //default
            }
            
        }
    }
}
