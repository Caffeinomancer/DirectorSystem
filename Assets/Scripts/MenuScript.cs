using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private Camera camera;
    private float camHeight;
    private float camWidth;
    public float ButtonYBuff;
    public float MenuEnemyXBuff;

    public GameObject MenuEnemyPrefab;
    public GameObject BulletPrefab;
    public GameObject ExplosionPrefab;

    public GameObject StartButton;
    public GameObject DifficultyButton;
    public GameObject AboutButton;
    public GameObject CreditsButton;
    public GameObject ControlsButton;
    public GameObject QuitButton;
    public GameObject BackButton;
    public GameObject PlayerShip;

    public GameObject AboutScreen;
    public GameObject ControlsScreen;
    public GameObject CreditsScreen;
    public GameObject DifficultyScreen;

    private GameObject StartEnemy;
    private GameObject DifficultyEnemy;
    private GameObject AboutEnemy;
    private GameObject CreditsEnemy;
    private GameObject ControlsEnemy;
    private GameObject QuitEnemy;
    private GameObject BackEnemy;

    private GameObject bullet;
    private GameObject startAnim;
    private GameObject creditsAnim;
    private GameObject aboutAnim;
    private GameObject controlsAnim;
    private GameObject quitAnim;
    private GameObject difficultyAnim;

    private EnemySpawnerSystem spawner;


    
    private int ShipLocation = 0;
    private int ButtonSelected = 0;

    private bool HasMoved = false;
    private bool HasShot = false;
    private bool startAnimPlay = false;
    private bool difficultyAnimPlay = false;
    private bool creditsAnimPlay = false;
    private bool aboutAnimPlay = false;
    private bool controlsAnimPlay = false;
    private bool quitAnimPlay = false;
    private bool selectorTimeActivated = false;

    private float selectorTimer = 1.5f;
    public float selectTime;
    public float wanderSpawnTime;
    private float timeToSpawnWanderer;
    public float seekerSpawnTime;
    private float timeToSpawnSeeker;


    // Use this for initialization
    void Start ()
    {
        camera = Camera.main;
        camHeight = 2f * camera.orthographicSize;
        camWidth = camHeight * camera.aspect;

        StartButton.transform.position = new Vector3(0, camHeight / 2 - (2.5f * ButtonYBuff), -1);
        DifficultyButton.transform.position = new Vector3(0, StartButton.transform.position.y - ButtonYBuff, -1);
        ControlsButton.transform.position = new Vector3(0, DifficultyButton.transform.position.y - ButtonYBuff, -1);
        AboutButton.transform.position = new Vector3(0, ControlsButton.transform.position.y - ButtonYBuff, -1);
        CreditsButton.transform.position = new Vector3(0, AboutButton.transform.position.y - ButtonYBuff, -1);
        QuitButton.transform.position = new Vector3(0, CreditsButton.transform.position.y - ButtonYBuff, -1);
        BackButton.transform.position = new Vector3(3000, QuitButton.transform.position.y - ButtonYBuff, -1);

        StartEnemy = Instantiate(MenuEnemyPrefab, new Vector3(StartButton.transform.position.x - MenuEnemyXBuff, StartButton.transform.position.y, StartButton.transform.position.z), new Quaternion()) as GameObject;
        DifficultyEnemy = Instantiate(MenuEnemyPrefab, new Vector3(DifficultyButton.transform.position.x - MenuEnemyXBuff, DifficultyButton.transform.position.y, StartButton.transform.position.z), new Quaternion()) as GameObject;
        ControlsEnemy = Instantiate(MenuEnemyPrefab, new Vector3(ControlsButton.transform.position.x - MenuEnemyXBuff, ControlsButton.transform.position.y, StartButton.transform.position.z), new Quaternion()) as GameObject;
        AboutEnemy = Instantiate(MenuEnemyPrefab, new Vector3(AboutButton.transform.position.x - MenuEnemyXBuff, AboutButton.transform.position.y, StartButton.transform.position.z), new Quaternion()) as GameObject;
        CreditsEnemy = Instantiate(MenuEnemyPrefab, new Vector3(CreditsButton.transform.position.x - MenuEnemyXBuff, CreditsButton.transform.position.y, StartButton.transform.position.z), new Quaternion()) as GameObject;
        QuitEnemy = Instantiate(MenuEnemyPrefab, new Vector3(QuitButton.transform.position.x - MenuEnemyXBuff, QuitButton.transform.position.y, StartButton.transform.position.z), new Quaternion()) as GameObject;
        BackEnemy = Instantiate(MenuEnemyPrefab, new Vector3(BackButton.transform.position.x - MenuEnemyXBuff, BackButton.transform.position.y, StartButton.transform.position.z), new Quaternion()) as GameObject;

        spawner = GameObject.Find("Main Camera").GetComponent<EnemySpawnerSystem>();
        timeToSpawnWanderer = wanderSpawnTime;
        timeToSpawnSeeker = timeToSpawnSeeker;

        PlayerShip.transform.position = new Vector3(StartButton.transform.position.x - 5, StartButton.transform.position.y, StartButton.transform.position.z);
        GameObject.Find("Player/MeteorShield").GetComponent<MeteorShieldScript>().SpawnMeteorShield();
    }

    // Update is called once per frame
    void Update ()
    {
        UpdateInput();
        MovePlayer();
        RotateMenuEnemies();
        CheckBulletAlive();
        CheckAnim();
        DeltaTimeStuff();

        
	}

    private void DeltaTimeStuff()
    {
        if (selectorTimeActivated)
            selectorTimer -= Time.deltaTime;

        if (selectorTimer <= 0)
        {
            selectorTimer = selectTime;
            selectorTimeActivated = false;
        }

      //  timeToSpawnSeeker -= Time.deltaTime;
        timeToSpawnWanderer -= Time.deltaTime;

        if(timeToSpawnWanderer <= 0)
        {
            timeToSpawnWanderer = wanderSpawnTime;
            spawner.Spawn(EnemySpawnerSystem.EnemyType.WANDER);
        }

        /*if(timeToSpawnSeeker <= 0)
        {
            timeToSpawnSeeker = seekerSpawnTime;
            spawner.Spawn(EnemySpawnerSystem.EnemyType.SEEKER);
        }*/
    }

    private void RotateMenuEnemies()
    {
        if(StartEnemy != null)
            StartEnemy.transform.Rotate(Vector3.forward * 2);

        if(DifficultyEnemy != null)
            DifficultyEnemy.transform.Rotate(Vector3.forward * 2);

        if(ControlsEnemy != null)
            ControlsEnemy.transform.Rotate(Vector3.forward * 2);

        if(AboutEnemy != null)
            AboutEnemy.transform.Rotate(Vector3.forward * 2);

        if(CreditsEnemy != null)
            CreditsEnemy.transform.Rotate(Vector3.forward * 2);

        if(QuitEnemy != null)
            QuitEnemy.transform.Rotate(Vector3.forward * 2);

        if(BackEnemy != null)
            BackEnemy.transform.Rotate(Vector3.forward * 2);
    }

    private void CheckAnim()
    {
        if(startAnimPlay)
        {
            if(startAnim == null)
            {
                startAnimPlay = false;
                SceneManager.LoadScene(1);
            }
        }

        if (aboutAnimPlay)
        {
            if (aboutAnim == null)
            {
                aboutAnimPlay = false;
                AboutEnemy = Instantiate(MenuEnemyPrefab, new Vector3(AboutButton.transform.position.x - MenuEnemyXBuff, AboutButton.transform.position.y, StartButton.transform.position.z), new Quaternion()) as GameObject;

            }
        }

        if (controlsAnimPlay)
        {
            if (controlsAnim == null)
            {
                controlsAnimPlay = false;
                ControlsEnemy = Instantiate(MenuEnemyPrefab, new Vector3(ControlsButton.transform.position.x - MenuEnemyXBuff, ControlsButton.transform.position.y, StartButton.transform.position.z), new Quaternion()) as GameObject;
            }
        }

        if (difficultyAnimPlay)
        {
            if (difficultyAnim == null)
            {
                difficultyAnimPlay = false;
                DifficultyEnemy = Instantiate(MenuEnemyPrefab, new Vector3(DifficultyButton.transform.position.x - MenuEnemyXBuff, DifficultyButton.transform.position.y, StartButton.transform.position.z), new Quaternion()) as GameObject;

            }
        }

        if (creditsAnimPlay)
        {
            if (creditsAnim == null)
            {
                creditsAnimPlay = false;
                CreditsEnemy = Instantiate(MenuEnemyPrefab, new Vector3(CreditsButton.transform.position.x - MenuEnemyXBuff, CreditsButton.transform.position.y, StartButton.transform.position.z), new Quaternion()) as GameObject;
            }
        }

        if (quitAnimPlay)
        {
            if (quitAnim == null)
            {
                quitAnimPlay = false;
                Application.Quit();
                QuitEnemy = Instantiate(MenuEnemyPrefab, new Vector3(QuitButton.transform.position.x - MenuEnemyXBuff, QuitButton.transform.position.y, StartButton.transform.position.z), new Quaternion()) as GameObject;
            }
        }
    }

    private void UpdateInput()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if(!HasMoved)
            {
                HasMoved = true;
                ShipLocation--;
                if (ShipLocation < 0)
                {
                    ShipLocation = 0;
                }
            }
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if(!HasMoved)
            {
                HasMoved = true;
                ShipLocation++;
                if (ShipLocation > 5)
                {
                    ShipLocation = 5;
                }
            } 
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(!selectorTimeActivated)
            {
                bullet = Instantiate(BulletPrefab, new Vector3(PlayerShip.transform.position.x, PlayerShip.transform.position.y, PlayerShip.transform.position.z),
                    new Quaternion(PlayerShip.transform.rotation.x, PlayerShip.transform.rotation.y, PlayerShip.transform.rotation.z, PlayerShip.transform.rotation.w)) as GameObject;

                ButtonSelected = ShipLocation;
                selectorTimeActivated = true;
                HasShot = true;
            }
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            HasMoved = false;
        }

        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            HasMoved = false;
        }
    }

    private void MovePlayer()
    {
        if(ShipLocation == 0)
        {
            PlayerShip.transform.position = new Vector3(StartButton.transform.position.x - 5, StartButton.transform.position.y, StartButton.transform.position.z);
        }

        else if(ShipLocation == 1)
        {
            PlayerShip.transform.position = new Vector3(DifficultyButton.transform.position.x - 5, DifficultyButton.transform.position.y, StartButton.transform.position.z);
        }

        else if (ShipLocation == 2)
        {
            PlayerShip.transform.position = new Vector3(ControlsButton.transform.position.x - 5, ControlsButton.transform.position.y, StartButton.transform.position.z);
        }

        else if (ShipLocation == 3)
        {
            PlayerShip.transform.position = new Vector3(AboutButton.transform.position.x - 5, AboutButton.transform.position.y, StartButton.transform.position.z);
        }

        else if (ShipLocation == 4)
        {
            PlayerShip.transform.position = new Vector3(CreditsButton.transform.position.x - 5, CreditsButton.transform.position.y, StartButton.transform.position.z);
        }

        else if (ShipLocation == 5)
        {
            PlayerShip.transform.position = new Vector3(QuitButton.transform.position.x - 5, QuitButton.transform.position.y, StartButton.transform.position.z);
        }

        else
        {
            //default
        }
    }

    private void CheckBulletAlive()
    {
        if(bullet == null && HasShot == true)
        {
            if(ButtonSelected == 0)
            {
                startAnim = Instantiate(ExplosionPrefab) as GameObject;
                startAnim.transform.position = StartEnemy.transform.position;
                startAnimPlay = true;
                Destroy(StartEnemy);
            }

            else if(ButtonSelected == 1)
            {
                difficultyAnim = Instantiate(ExplosionPrefab) as GameObject;
                difficultyAnim.transform.position = DifficultyEnemy.transform.position;
                difficultyAnimPlay = true;

                DifficultyScreen.transform.position = new Vector3(StartButton.transform.position.x + 5, StartButton.transform.position.y - 3, StartButton.transform.position.z);
                AboutScreen.transform.position = new Vector3(2000, StartButton.transform.position.y - 3, StartButton.transform.position.z);
                CreditsScreen.transform.position = new Vector3(2000, StartButton.transform.position.y - 3, StartButton.transform.position.z);
                ControlsScreen.transform.position = new Vector3(2000, StartButton.transform.position.y - 3, StartButton.transform.position.z);


                Destroy(DifficultyEnemy);
            }

            else if (ButtonSelected == 2)
            {
                controlsAnim = Instantiate(ExplosionPrefab) as GameObject;
                controlsAnim.transform.position = ControlsEnemy.transform.position;
                controlsAnimPlay = true;

                DifficultyScreen.transform.position = new Vector3(2000, StartButton.transform.position.y - 3, StartButton.transform.position.z);
                AboutScreen.transform.position = new Vector3(2000, StartButton.transform.position.y - 3, StartButton.transform.position.z);
                CreditsScreen.transform.position = new Vector3(2000, StartButton.transform.position.y - 3, StartButton.transform.position.z);
                ControlsScreen.transform.position = new Vector3(StartButton.transform.position.x + 5, StartButton.transform.position.y - 3, StartButton.transform.position.z);

                Destroy(ControlsEnemy);
            }

            else if (ButtonSelected == 3)
            {
                aboutAnim = Instantiate(ExplosionPrefab) as GameObject;
                aboutAnim.transform.position = AboutEnemy.transform.position;
                aboutAnimPlay = true;

                DifficultyScreen.transform.position = new Vector3(2000, StartButton.transform.position.y - 3, StartButton.transform.position.z);
                AboutScreen.transform.position = new Vector3(StartButton.transform.position.x + 5, StartButton.transform.position.y - 3, StartButton.transform.position.z);
                CreditsScreen.transform.position = new Vector3(2000, StartButton.transform.position.y - 3, StartButton.transform.position.z);
                ControlsScreen.transform.position = new Vector3(2000, StartButton.transform.position.y - 3, StartButton.transform.position.z);

                Destroy(AboutEnemy);
            }

            else if (ButtonSelected == 4)
            {
                creditsAnim = Instantiate(ExplosionPrefab) as GameObject;
                creditsAnim.transform.position = CreditsEnemy.transform.position;
                creditsAnimPlay = true;

                DifficultyScreen.transform.position = new Vector3(2000, StartButton.transform.position.y - 3, StartButton.transform.position.z);
                AboutScreen.transform.position = new Vector3(2000, StartButton.transform.position.y - 3, StartButton.transform.position.z);
                CreditsScreen.transform.position = new Vector3(StartButton.transform.position.x + 5, StartButton.transform.position.y - 3, StartButton.transform.position.z);
                ControlsScreen.transform.position = new Vector3(2000, StartButton.transform.position.y - 3, StartButton.transform.position.z);

                Destroy(CreditsEnemy);
            }

            else if (ButtonSelected == 5)
            {
                quitAnim = Instantiate(ExplosionPrefab) as GameObject;
                quitAnim.transform.position = QuitEnemy.transform.position;
                quitAnimPlay = true;
                
               

                Destroy(QuitEnemy);
            }

            else
            {
                //default
            }

            HasShot = false;
        }
    }
}
