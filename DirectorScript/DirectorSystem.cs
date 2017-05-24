using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public enum DifficultyOverride
{
    EASY = 0,
    MEDIUM,
    HARD,
    DEFAULT
}

public enum StressAdjustment
{
    INCREASE_STRESS_A_LOT = 0,
    INCREASE_STRESS_A_MODERATE,
    INCREASE_STRESS_A_LITTLE,
    DECREASE_STRESS_A_LOT,
    DECREASE_STRESS_A_MODERATE,
    DECREASE_STRESS_A_LITTLE
}



public class DirectorSystem : MonoBehaviour
{
    //Singleton
    private static DirectorSystem _instance;
    public static DirectorSystem Instance { get { return _instance; } }
    //

    //Events
    private Dictionary<string, UnityEvent> listenerList;
    //

    //Timers
    private Dictionary<string,float> timers;
    private List<string> timerKeys;
    private Dictionary<string, float> timerResetTimes;
    //

    //Stress 
    public float stressUpdateTime = 1.0f;
    public float startingStress = 120.0f;
    public float StressLevelEasy;
    public float StressLevelMedium;
    public float StressLevelHard;

    //Difficulty Modes
    public bool EasyMode;
    public bool MediumMode;
    public bool HardMode;
    public bool DynamicMode;

    //Difficulty Levels
    private const float EASY = 5.0f;
    private const float MEDIUM = 50.0f;
    private const float HARD = 120.0f;

    //Overrides
    private Dictionary<string, DifficultyOverride> overrides;

    private float checkStressTime;

    private float stress;

    public int overrideDifficulty = 0;

    private float minStress = 5;
    private float maxStress = 120;

    public float livingTimeModifier = 2;
    public float StressCopy;

    private float modifier = 1.0f;

    private void Awake()
    {
        if (_instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }

        else
        {
            _instance = this;
        }
    }

    public void AdjustStress(StressAdjustment adjustment)
    {
        float stressMod;

        if (adjustment == StressAdjustment.DECREASE_STRESS_A_LITTLE)
        {
            stress -= 2;
        }
        if (adjustment == StressAdjustment.DECREASE_STRESS_A_MODERATE)
        {
            stress -= 20;
        }
        if (adjustment == StressAdjustment.DECREASE_STRESS_A_LOT)
        {
            stress -= 50;
        }
        if (adjustment == StressAdjustment.INCREASE_STRESS_A_LITTLE)
        {
            stress += 2;
        }
        if (adjustment == StressAdjustment.INCREASE_STRESS_A_MODERATE)
        {
            stress += 4;
        }
        if (adjustment == StressAdjustment.INCREASE_STRESS_A_LOT)
        {
            stress += 8;
        }

        if (stress > 120)
            stress = 120;

        if (stress < 5)
            stress = 5;

        Debug.Log(stress);
    }

    public void RegisterListener(string key, ref UnityEvent eventToAdd)
    {
        listenerList.Add(key, eventToAdd);
    }


    // Use this for initialization
    void Start ()
    {
        timers = new Dictionary<string,float>();
        timerResetTimes = new Dictionary<string,float>();
        timerKeys = new List<string>();
        listenerList = new Dictionary<string, UnityEvent>();

        if (StressLevelEasy > 120 || StressLevelEasy < 5)
            StressLevelEasy = 5.0f;

        if (StressLevelMedium > 120 || StressLevelMedium < 5)
            StressLevelMedium = 50.0f;

        if (StressLevelHard > 120 || StressLevelHard < 5)
            StressLevelHard = 120.0f;


        CorrectDifficultyMode();



        checkStressTime = stressUpdateTime;
        stress = startingStress;
	}

    private void CorrectDifficultyMode()
    {
        if (!DynamicMode)
        {
            if (EasyMode)
                if (MediumMode || HardMode)
                {
                    EasyMode = false;
                    MediumMode = false;
                    HardMode = false;
                    DynamicMode = true;
                }

            if (MediumMode)
                if (EasyMode || HardMode)
                {
                    EasyMode = false;
                    MediumMode = false;
                    HardMode = false;
                    DynamicMode = true;
                }

            if (HardMode)
                if (EasyMode || MediumMode)
                {
                    EasyMode = false;
                    MediumMode = false;
                    HardMode = false;
                    DynamicMode = true;
                }

            if (!EasyMode && !MediumMode && !HardMode)
                DynamicMode = true;
        }
    }

    

    public void AddTimer(string timerName, float timerLength, bool shouldOverrideDynamic = false, DifficultyOverride difficultyOverride = DifficultyOverride.EASY)
    {
        timers.Add(timerName, timerLength);
        timerResetTimes.Add(timerName, timerLength);
        timerKeys.Add(timerName);

        if(shouldOverrideDynamic)
        {
            overrides.Add(timerName, difficultyOverride);
        }
    }

    public void RemoveTimer(string timerName)
    {
        timers.Remove(timerName);
        timerResetTimes.Remove(timerName);
        timerKeys.Remove(timerName);

        if (overrides.ContainsKey(timerName))
            overrides.Remove(timerName);

        listenerList.Remove(timerName);
    }

    private void UpdateTimers()
    {
        for (int i = 0; i < timers.Count; i++)
        {
            string currKey = timerKeys[i];
            float currTimer = timers[currKey] -= Time.deltaTime;
            
            if(currTimer <= 0)
            {
                timers[currKey] = timerResetTimes[currKey] * modifier;
                if(listenerList.ContainsKey(currKey))
                {
                    listenerList[currKey].Invoke();
                }
            }

        }
    }

    public void Reset()
    {
        checkStressTime = stressUpdateTime;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //New
        CorrectDifficultyMode();
        UpdateTimers();


        //Countdown our timers 
        checkStressTime -= Time.deltaTime;
        //timeToSpawnPowerUp -= Time.deltaTime;

        StressCopy = stress;

        //Update Stress after a certain amount of time
        if(checkStressTime <= 0)
        {
            checkStressTime = stressUpdateTime;
            AdjustStress(StressAdjustment.INCREASE_STRESS_A_LITTLE);
        }

        /*if(timeToSpawnPowerUp <= 0)
        {
            //powerUpStressTime; //= stress / 10;
            timeToSpawnPowerUp = powerUpStressTime;
            SpawnPowerUp();
        }*/

	}

    private void SpawnPowerUp()
    {
        int ran = Random.Range(0, 2);
        float locX = 0, locY;

        locY = GameObject.Find("Score/Millions").transform.position.y + 3;
        
        switch(ran)
        {
            case 0:
                locX = GameObject.Find("Lives/Life1").transform.position.x;
                break;

            case 1:
                locX = GameObject.Find("WeaponTypeBoxes/Weapon2").transform.position.x;
                break;

            case 2:
                locX = GameObject.Find("Score/Millions").transform.position.x;
                break;

            default:

                break;
        }


        //GameObject newPowerUp = Instantiate(PowerUP) as GameObject;
        //newPowerUp.transform.position = new Vector3(locX, locY, -1);
 
    }


    public float GetPointModifier()
    {
        float mod = 0.0f;

        if (stress >= 0 && stress <= 9)
            mod = 1.5f;
        else if (stress >= 10 && stress <= 19)
            mod = 1.4f;
        else if (stress >= 20 && stress <= 29)
            mod = 1.3f;
        else if (stress >= 30 && stress <= 39)
            mod = 1.2f;
        else if (stress >= 40 && stress <= 49)
            mod = 1.1f;
        else if (stress >= 50 && stress <= 59)
            mod = 1.0f;
        else if (stress >= 60 && stress <= 69)
            mod = 0.9f;
        else if (stress >= 70 && stress <= 79)
            mod = 0.7f;
        else if (stress >= 80 && stress <= 89)
            mod = 0.5f;
        else if (stress >= 90 && stress <= 99)
            mod = 0.4f;
        else if (stress >= 100)
            mod = 0.3f;
        else
            mod = 1.0f;

        modifier = mod;
        return mod;
    }
}
