using UnityEngine;
using System.Collections;

public class MeteorShieldScript : MonoBehaviour 
{
    public GameObject meteors;

    private GameObject meteor1;
    private GameObject meteor2;
    private GameObject meteor3;
    private GameObject meteor4;
    private GameObject meteor5;

    private GameObject spawn1;
    private GameObject spawn2;
    private GameObject spawn3;
    private GameObject spawn4;
    private GameObject spawn5;

    private GameObject player;

    public int rotSpeed = 6;

    private bool hasCreated;

    private Vector3 zAxis = new Vector3(0, 0, 1);

	// Use this for initialization
	void Start () 
    {
        spawn1 = GameObject.Find("Player/MeteorShield/MeteorSpawn1");
        spawn2 = GameObject.Find("Player/MeteorShield/MeteorSpawn2");
        spawn3 = GameObject.Find("Player/MeteorShield/MeteorSpawn3");
        spawn4 = GameObject.Find("Player/MeteorShield/MeteorSpawn4");
        spawn5 = GameObject.Find("Player/MeteorShield/MeteorSpawn5");

        player = GameObject.Find("Player");

        hasCreated = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        MoveWithPlayer();

        OrbitPlayer();
	}

    public void CreateShield()
    {
        hasCreated = true;
        SpawnMeteorShield();
        hasCreated = false;
    }

    public void SpawnMeteorShield()
    {
        if (meteor1 != null)
        {
            Destroy(meteor1.gameObject);
        }

        if (meteor2 != null)
        {
            Destroy(meteor2.gameObject);
        }
        
        if (meteor3 != null)
        {
            Destroy(meteor3.gameObject);
        }
        
        if (meteor4 != null)
        {
            Destroy(meteor4.gameObject);
        }
        
        if (meteor5 != null)
        {
            Destroy(meteor5.gameObject);
        }

        meteor1 = Instantiate(meteors, new Vector3(spawn1.transform.position.x, spawn1.transform.position.y, spawn1.transform.position.z),
                    new Quaternion(spawn1.transform.rotation.x, spawn1.transform.rotation.y, spawn1.transform.rotation.z, spawn1.transform.rotation.w)) as GameObject;

        meteor2 = Instantiate(meteors, new Vector3(spawn2.transform.position.x, spawn2.transform.position.y, spawn2.transform.position.z),
                    new Quaternion(spawn2.transform.rotation.x, spawn2.transform.rotation.y, spawn2.transform.rotation.z, spawn2.transform.rotation.w)) as GameObject;

        meteor3 = Instantiate(meteors, new Vector3(spawn3.transform.position.x, spawn3.transform.position.y, spawn3.transform.position.z),
                    new Quaternion(spawn3.transform.rotation.x, spawn3.transform.rotation.y, spawn3.transform.rotation.z, spawn3.transform.rotation.w)) as GameObject;

        meteor4 = Instantiate(meteors, new Vector3(spawn4.transform.position.x, spawn4.transform.position.y, spawn4.transform.position.z),
                    new Quaternion(spawn4.transform.rotation.x, spawn4.transform.rotation.y, spawn4.transform.rotation.z, spawn4.transform.rotation.w)) as GameObject;

        meteor5 = Instantiate(meteors, new Vector3(spawn5.transform.position.x, spawn5.transform.position.y, spawn5.transform.position.z),
                    new Quaternion(spawn5.transform.rotation.x, spawn5.transform.rotation.y, spawn5.transform.rotation.z, spawn5.transform.rotation.w)) as GameObject;
    }

    private void MoveWithPlayer()
    {
        if (meteor1 != null)
        {
            meteor1.transform.position = spawn1.transform.position;
        }

        if (meteor2 != null)
        {
            meteor2.transform.position = spawn2.transform.position;
        }

        if (meteor3 != null)
        {
            meteor3.transform.position = spawn3.transform.position;
        }

        if (meteor4 != null)
        {
            meteor4.transform.position = spawn4.transform.position;
        }

        if (meteor5 != null)
        {
            meteor5.transform.position = spawn5.transform.position;
        }
    }

    private void OrbitPlayer()
    {
        spawn1.transform.RotateAround(player.transform.position, zAxis, rotSpeed);
        spawn2.transform.RotateAround(player.transform.position, zAxis, rotSpeed);
        spawn3.transform.RotateAround(player.transform.position, zAxis, rotSpeed);
        spawn4.transform.RotateAround(player.transform.position, zAxis, rotSpeed);
        spawn5.transform.RotateAround(player.transform.position, zAxis, rotSpeed);
    }
}
