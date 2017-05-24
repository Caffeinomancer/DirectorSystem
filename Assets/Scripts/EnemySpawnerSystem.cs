using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class EnemySpawnerSystem : MonoBehaviour 
{
    public GameObject wandererPrefab;
    public GameObject seekerPrefab;

    private const float SpawnRightXMax = 35;
    private const float SpawnRightXMin = 25;
    private const float SpawnRightYMax = 9;
    private const float SpawnRightYMin = -9;

    private const float SpawnLeftXMax = -35;
    private const float SpawnLeftXMin = -25;
    private const float SpawnLeftYMax = 9;
    private const float SpawnLeftYMin = -9;

    private const float SpawnTopXMax = 18;
    private const float SpawnTopXMin = -18;
    private const float SpawnTopYMax = 22;
    private const float SpawnTopYMin = 13;

    private const float SpawnBotXMax = 18;
    private const float SpawnBotXMin = -18;
    private const float SpawnBotYMax = -22;
    private const float SpawnBotYMin = -13;

    public int wandererToSpawn = 0;
    public int seekerToSpawn = 0;
    public bool shouldSpawnWanderer = false;
    public bool shouldSpawnSeeker = false;

    DirectorSystem director;
    UnityEvent SpawnWandererEvent;
    UnityEvent SpawnSeekerEvent;

    public enum EnemyType
    {
        WANDER = 1,
        SEEKER
    }

	// Use this for initialization
	void Start () 
    {
        director = DirectorSystem.Instance;

        SpawnWandererEvent = new UnityEvent();
        SpawnSeekerEvent = new UnityEvent();


        SpawnSeekerEvent.AddListener(SpawnSeekers);
        director.RegisterListener("SpawnSeeker", ref SpawnSeekerEvent);
        director.AddTimer("SpawnSeeker", 4);

        SpawnWandererEvent.AddListener(SpawnWanderers);
        director.RegisterListener("SpawnWander", ref SpawnWandererEvent);
        director.AddTimer("SpawnWander", 2);

        

      //  SpawnSeekerEvent = new UnityEvent();
        //SpawnSeekerEvent.AddListener(SpawnSeekers);
        //director.RegisterListener(ref SpawnSeekerEvent);

        //director.AddTimer()

    }
	
    public void Reset()
    {
        GameObject[] toDel;
        toDel = GameObject.FindGameObjectsWithTag("SeekerEnemy");

        for (int i = 0; i < toDel.Length; i++)
        {
            Destroy(toDel[i]);
        }

        toDel = GameObject.FindGameObjectsWithTag("WanderEnemy");

        for (int i = 0; i < toDel.Length; i++)
        {
            Destroy(toDel[i]);
        }


    }

	// Update is called once per frame
	void Update () 
    {
        SpawnEnemies();

        DebugSpawnEnemies();
	}

    void SpawnWanderers()
    {
        Spawn(EnemyType.WANDER);
    }

    void SpawnSeekers()
    {
        Spawn(EnemyType.SEEKER);
    }

    void SpawnEnemies()
    {
        if(shouldSpawnWanderer)
        {
            for(int i = 0; i < wandererToSpawn; i++)
            {
                Spawn(EnemyType.WANDER);
            }
        }

        if(shouldSpawnSeeker)
        {
            for(int i = 0; i < seekerToSpawn; i++)
            {
                Spawn(EnemyType.SEEKER);
            }
        }

        seekerToSpawn = 0;
        wandererToSpawn = 0;
        shouldSpawnSeeker = false;
        shouldSpawnWanderer = false;
    }

    public void Spawn(EnemyType type)
    {
        int spawnZone = Random.Range(1, 5);
        Vector3 spawnLoc = new Vector3(0, 0, 0);

        switch (spawnZone)
        {
            case 4:
                spawnLoc = new Vector3(Random.Range(SpawnLeftXMin, SpawnLeftXMax), Random.Range(SpawnLeftYMin, SpawnLeftYMax), 0);
                break;

            case 3:
                spawnLoc = new Vector3(Random.Range(SpawnBotXMin, SpawnBotXMax), Random.Range(SpawnBotYMin, SpawnBotYMax), 0);
                break;

            case 2:
                spawnLoc = new Vector3(Random.Range(SpawnRightXMin, SpawnRightXMax), Random.Range(SpawnRightYMin, SpawnRightYMax), 0);
                break;

            case 1:
                spawnLoc = new Vector3(Random.Range(SpawnTopXMin, SpawnTopXMax), Random.Range(SpawnTopYMin, SpawnTopYMax), 0);
                break;
        }

        switch(type)
        {
            case EnemyType.WANDER:
                GameObject newWander = Instantiate(wandererPrefab) as GameObject;
                newWander.transform.position = spawnLoc;
                break;

            case EnemyType.SEEKER:
                GameObject newSeeker = Instantiate(seekerPrefab) as GameObject;
                newSeeker.transform.position = spawnLoc;
                break;
        }
       
    }

    //DELETE FUNCTION LATER
    void DebugSpawnEnemies()
    {

        if (Input.GetKey(KeyCode.F))
        {
            Spawn(EnemyType.WANDER);
        }

        if (Input.GetKey(KeyCode.G))
        {
            Spawn(EnemyType.SEEKER);
        }
    }
}
