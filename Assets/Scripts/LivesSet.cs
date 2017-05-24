using UnityEngine;
using System.Collections;

public class LivesSet : MonoBehaviour 
{

    Transform life1;
    Transform life2;
    Transform life3;

    Camera camera;

    float camHeight;
    float camWidth;
    public float posBufferX;
    public float posBufferY;
	// Use this for initialization
	void Start () 
    {
        life1 = GameObject.Find("Lives/Life1").transform;
        life2 = GameObject.Find("Lives/Life2").transform;
        life3 = GameObject.Find("Lives/Life3").transform;

       

        camera = Camera.main;
        camHeight = 2f * camera.orthographicSize;
        camWidth = camHeight * camera.aspect;

        posBufferX = 1.0f;
        posBufferY = 0.6f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        life1.transform.position = new Vector3(-camWidth / 2  + posBufferX, camHeight / 2 - posBufferY, -1);
        life2.transform.position = new Vector3(-camWidth / 2 + (posBufferX * 2), camHeight / 2 - posBufferY, -1);
        life3.transform.position = new Vector3(-camWidth / 2 + (posBufferX * 3), camHeight / 2 - posBufferY, -1);
	}
}
