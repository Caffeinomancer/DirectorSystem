using UnityEngine;
using System.Collections;

public class ScoreSet : MonoBehaviour 
{
    Camera camera;

    float camHeight;
    float camWidth;
    public float posBufferX;
    public float posBufferY;

    Transform ones;
    Transform tens;
    Transform hundreds;
    Transform thousands;
    Transform tenThousands;
    Transform hundredThousands;
    Transform millions;



	// Use this for initialization
	void Start () 
    {
        ones = GameObject.Find("Score/Ones").transform;
        tens = GameObject.Find("Score/Tens").transform;
        hundreds = GameObject.Find("Score/Hundreds").transform;
        thousands = GameObject.Find("Score/Thousands").transform;
        tenThousands = GameObject.Find("Score/TenThousands").transform;
        hundredThousands = GameObject.Find("Score/HundredThousands").transform;
        millions = GameObject.Find("Score/Millions").transform;


        camera = Camera.main;
        camHeight = 2f * camera.orthographicSize;
        camWidth = camHeight * camera.aspect;

        posBufferX = 0.5f;
        posBufferY = 0.6f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        ones.transform.position = new Vector3(camWidth / 2 - posBufferX, camHeight / 2 - posBufferY, -1);
        tens.transform.position = new Vector3(camWidth / 2 - posBufferX * 2, camHeight / 2 - posBufferY, -1);
        hundreds.transform.position = new Vector3(camWidth / 2 - posBufferX * 3, camHeight / 2 - posBufferY, -1);
        thousands.transform.position = new Vector3(camWidth / 2 - posBufferX * 4, camHeight / 2 - posBufferY, -1);
        tenThousands.transform.position = new Vector3(camWidth / 2 - posBufferX * 5, camHeight / 2 - posBufferY, -1);
        hundredThousands.transform.position = new Vector3(camWidth / 2 - posBufferX * 6, camHeight / 2 - posBufferY, -1);
        millions.transform.position = new Vector3(camWidth / 2 - posBufferX * 7, camHeight / 2 - posBufferY, -1);


	}
}
