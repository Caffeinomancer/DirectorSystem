using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Controls : MonoBehaviour {

    public float rotationSpeed;
    public float thrustSpeed;

    public bool lineShot;
    public bool burstShot;
    public bool powerUp3;
    public bool powerUp4;
    private bool isMovingForward;

    private float singleShotDelayTime;
    private float burstShotDelayTime;

    private bool canShootLine;
    private bool canShootBurst;

    public float lineDelay;
    public float burstDelay;

    Vector3 lastPosition;
    Vector3 playerPosition;

    private Transform player;
    GameObject playerObj;
    GameObject leftCannon;
    GameObject rightCannon;

    public GameObject bulletPrefab;

    DirectorSystem director;
    WeaponTypeScript weaponTypeScript;

    //MeteorSpawns
    private MeteorShieldScript metShieldScript;
    GameObject metSpawn1;
    GameObject metSpawn2;
    GameObject metSpawn3;
    GameObject metSpawn4;
    GameObject metSpawn5;
    //

    AudioSource audio;
    public AudioClip laserSound;
    public AudioClip music;


    // Use this for initialization
    void Start () 
	{
        audio = GetComponent<AudioSource>();

        isMovingForward = false;

        player = GameObject.Find("Player").transform;

        director = GameObject.Find("Director").GetComponent<DirectorSystem>();
        weaponTypeScript = GameObject.Find("WeaponTypeBoxes").GetComponent<WeaponTypeScript>();

        if(lineDelay == 0)
        {
            lineDelay = 0.25f;

        }

        if(burstDelay == 0)
        {
            burstDelay = 0.75f;

        }

        playerObj = GameObject.Find("Player");
        leftCannon = GameObject.Find("Player/LeftCannon");
        rightCannon = GameObject.Find("Player/RightCannon");

        metSpawn1 = GameObject.Find("Player/MeteorShield/MeteorSpawn1");
        metSpawn2 = GameObject.Find("Player/MeteorShield/MeteorSpawn2");
        metSpawn3 = GameObject.Find("Player/MeteorShield/MeteorSpawn3");
        metSpawn4 = GameObject.Find("Player/MeteorShield/MeteorSpawn4");
        metSpawn5 = GameObject.Find("Player/MeteorShield/MeteorSpawn5");
        metShieldScript = GameObject.Find("Player/MeteorShield").GetComponent<MeteorShieldScript>();

        Debug.Log(leftCannon);

        canShootLine = true;
        lineShot = true;
        burstShot = false;
        powerUp3 = false;
        powerUp4 = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
        //transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
        //rigidBody.AddForce(transform.up * accelerationSpeed * Input.GetAxis("Vertical"));
        lastPosition = GameObject.Find("Player").transform.position;
		CheckInput();

        if(playerPosition.y > 8.45f || playerPosition.y < -8.45f)
        {
            
            player.position = new Vector3(player.position.x, lastPosition.y, player.position.z);
        }

        if (playerPosition.x < -16.5f || playerPosition.x > 16.5f)
        {
            player.position = new Vector3(lastPosition.x, player.position.y, player.position.z);
        }

        if(!canShootLine)
        {
            singleShotDelayTime -= Time.deltaTime;

            if(singleShotDelayTime <= 0)
            {
                canShootLine = true;
            }
        }

        if (!canShootBurst)
        {
            burstShotDelayTime -= Time.deltaTime;

            if (burstShotDelayTime <= 0)
            {
                canShootBurst = true;
            }
        }
    }

	private void CheckInput()
	{
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);

        }

        if (Input.GetKey(KeyCode.Alpha8))
        {
            director.overrideDifficulty = 3;
        }

        if (Input.GetKey(KeyCode.Alpha9))
        {
            director.overrideDifficulty = 2;
        }

        if (Input.GetKey(KeyCode.Alpha0))
        {
            director.overrideDifficulty = 1;
        }

        if (Input.GetKey(KeyCode.Minus))
        {
            director.overrideDifficulty = 0;
        }


        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
            Vector3 oldLoc1 = metSpawn1.transform.position,
                oldLoc2 = metSpawn2.transform.position,
                oldLoc3 = metSpawn3.transform.position,
                oldLoc4 = metSpawn4.transform.position,
                oldLoc5 = metSpawn5.transform.position;

            Vector3 newRotation = transform.rotation.eulerAngles;
            newRotation.z += rotationSpeed;
            transform.rotation = Quaternion.Euler(newRotation);

            metSpawn1.transform.position = oldLoc1;
            metSpawn2.transform.position = oldLoc2;
            metSpawn3.transform.position = oldLoc3;
            metSpawn4.transform.position = oldLoc4;
            metSpawn5.transform.position = oldLoc5;

     
		}

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 oldLoc1 = metSpawn1.transform.position,
                oldLoc2 = metSpawn2.transform.position,
                oldLoc3 = metSpawn3.transform.position,
                oldLoc4 = metSpawn4.transform.position,
                oldLoc5 = metSpawn5.transform.position;

            Vector3 newRotation = transform.rotation.eulerAngles;
            newRotation.z -= rotationSpeed;
            transform.rotation = Quaternion.Euler(newRotation);

            metSpawn1.transform.position = oldLoc1;
            metSpawn2.transform.position = oldLoc2;
            metSpawn3.transform.position = oldLoc3;
            metSpawn4.transform.position = oldLoc4;
            metSpawn5.transform.position = oldLoc5;

        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {

            transform.Translate(Vector3.up * thrustSpeed * Time.deltaTime);

        }

        if (Input.GetKeyUp(KeyCode.Z))
        {

            //metShieldScript.CreateShield();
            weaponTypeScript.AddPowerUp("MeteorShield");
        }

        if(Input.GetKey(KeyCode.Space))
        {

            FireWeapon();
        }

        if(Input.GetKey(KeyCode.Alpha1))
        {
            lineShot = true;
            burstShot = false;
            powerUp3 = false;
            powerUp4 = false;
            weaponTypeScript.typeSelected = 1;
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            lineShot = false;
            burstShot = true;
            powerUp3 = false;
            powerUp4 = false;
            weaponTypeScript.typeSelected = 2;
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            weaponTypeScript.UsedPowerUp(3);
        }

        if (Input.GetKey(KeyCode.Alpha4))
        {
            weaponTypeScript.UsedPowerUp(4);
        }

        playerPosition = GameObject.Find("Player").transform.position;
	}

    private void FireWeapon()
    {
        if(lineShot)
        {
            if(canShootLine)
            {
                audio.PlayOneShot(laserSound, 0.4F);


                GameObject newBullet = Instantiate(bulletPrefab, new Vector3(playerObj.transform.position.x, playerObj.transform.position.y, playerObj.transform.position.z),
                    new Quaternion(playerObj.transform.rotation.x, playerObj.transform.rotation.y, playerObj.transform.rotation.z, playerObj.transform.rotation.w)) as GameObject;
 
                canShootLine = false;
                singleShotDelayTime = lineDelay;
            }

        }

        else if(burstShot)
        {
            if(canShootBurst)
            {
                audio.PlayOneShot(laserSound, 0.4F);


                GameObject newBullet = Instantiate(bulletPrefab, new Vector3(playerObj.transform.position.x, playerObj.transform.position.y, playerObj.transform.position.z),
                    new Quaternion(playerObj.transform.rotation.x, playerObj.transform.rotation.y, playerObj.transform.rotation.z, playerObj.transform.rotation.w)) as GameObject;


                GameObject newBullet2 = Instantiate(bulletPrefab, new Vector3(leftCannon.transform.position.x, leftCannon.transform.position.y, leftCannon.transform.position.z),
                        new Quaternion(leftCannon.transform.rotation.x, leftCannon.transform.rotation.y, leftCannon.transform.rotation.z, leftCannon.transform.rotation.w)) as GameObject;


                GameObject newBullet3 = Instantiate(bulletPrefab, new Vector3(rightCannon.transform.position.x, rightCannon.transform.position.y, rightCannon.transform.position.z),
                     new Quaternion(rightCannon.transform.rotation.x, rightCannon.transform.rotation.y, rightCannon.transform.rotation.z, rightCannon.transform.rotation.w)) as GameObject;
                     
                canShootBurst = false;
                burstShotDelayTime = burstDelay;

            }
        }

        else
        {

        }
    }
}
