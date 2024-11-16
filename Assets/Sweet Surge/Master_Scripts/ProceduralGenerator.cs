using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProceduralGenerator : MonoBehaviour
{
    [SerializeField] private Transform levelModule_Start;
    [SerializeField] private Transform levelModule_1;

    public List<Transform> easySlowBucket;
    public List<Transform> easyMediumBucket;
    public List<Transform> easyFastBucket;
    public List<Transform> hardSlowBucket;
    public List<Transform> hardMediumBucket;
    public List<Transform> hardFastBucket;

    public int playerDistance;
    public float f;

    Vector3 lastEndPosition;








    private void Awake()
    {
        lastEndPosition = levelModule_Start.Find("EndPoint").position;

        InvokeRepeating("SpawnCounter", 1f, 1f);


    }

    private void Start()
    {


    }

    private void Update()
    {


    }

    void SpawnModule()
    {
        Transform newModuleGenerated = SpawnModule(lastEndPosition);
        lastEndPosition = newModuleGenerated.Find("EndPoint").position;
    }

    Transform ChooseModuleToSpawn()
    {
        Transform chooseModule = null;

        /*if(Time.time < 10f)
        {
            chosenModule = easySlowBucket[Random.Range(0, easySlowBucket.Count)];
        }
        else if (Time.time >= 10f)
        {
            chosenModule = easyMediumBucket[Random.Range(0, easyMediumBucket.Count)];
        }*/




        if (Time.time < 30f)
        {
            float f = Random.value;
            if (playerDistance < 30)
            {
                if (f < 0.9)//10%
                {
                    chooseModule = easySlowBucket[Random.Range(0, easySlowBucket.Count)];
                }
                else if (f >= 0.9)//90%
                {
                    chooseModule = easyMediumBucket[Random.Range(0, easyMediumBucket.Count)];
                }

            }
            else if (playerDistance >= 30)
            {
                if (f < 0.7) //30%
                {
                    chooseModule = easySlowBucket[Random.Range(0, easySlowBucket.Count)];
                }
                else if (f >= 0.7) //70%
                {
                    chooseModule = easyMediumBucket[Random.Range(0, easyMediumBucket.Count)];
                }
            }
        }

        if (Time.time < 60f)
        {
            if (playerDistance < 60)
            {
                if (f < 0.85) //15%
                {
                    chooseModule = easySlowBucket[Random.Range(0, easySlowBucket.Count)];
                }
                else if (f >= 0.85) //85%
                {
                    chooseModule = easyMediumBucket[Random.Range(0, easyMediumBucket.Count)];
                }

            }
            else if (playerDistance >= 60)
            {
                if (f < 0.6) //40%
                {
                    chooseModule = easySlowBucket[Random.Range(0, easySlowBucket.Count)];
                }
                else if (f >= 0.6) //60%
                {
                    chooseModule = easyMediumBucket[Random.Range(0, easyMediumBucket.Count)];
                }
            }
        }

        if (Time.time > 60 && Time.time < 90f)
        {
            if (playerDistance > 60 && playerDistance < 90)
            {
                if (f < 0.8) //20%
                {
                    chooseModule = easySlowBucket[Random.Range(0, easySlowBucket.Count)];
                }
                else if (f >= 0.8) //80%
                {
                    chooseModule = easyMediumBucket[Random.Range(0, easyMediumBucket.Count)];
                }

            }
            else if (playerDistance >= 60 && playerDistance < 90)
            {
                if (f < 0.4) //60%
                {
                    chooseModule = easySlowBucket[Random.Range(0, easySlowBucket.Count)];
                }
                else if (f >= 0.4) //40%
                {
                    chooseModule = easyMediumBucket[Random.Range(0, easyMediumBucket.Count)];
                }
            }
        }

        if (Time.time > 60 && Time.time < 200f)
        {
            if (playerDistance > 90 && playerDistance < 130)
            {
                if (f < 0.5) //50%
                {
                    chooseModule = easySlowBucket[Random.Range(0, easySlowBucket.Count)];
                }
                else if (f >= 0.15 && f < 0.2) //15%
                {
                    chooseModule = easyMediumBucket[Random.Range(0, easyMediumBucket.Count)];
                }
                else if (f >= 0.2) //20%
                {
                    chooseModule = easyFastBucket[Random.Range(0, easyFastBucket.Count)];
                }
                else if (f >= 0.1) //10%
                {
                    chooseModule = hardSlowBucket[Random.Range(0, hardSlowBucket.Count)];
                }

            }
            else if (playerDistance > 90 && playerDistance >= 130)
            {
                if (f < 0.2) //20%
                {
                    chooseModule = easySlowBucket[Random.Range(0, easySlowBucket.Count)];
                }
                if (f < 0.15) //15%
                {
                    chooseModule = easyMediumBucket[Random.Range(0, easyMediumBucket.Count)];
                }
                else if (f >= 0.3) //30%
                {
                    chooseModule = easyFastBucket[Random.Range(0, easyFastBucket.Count)];
                }
                else if (f > 0.35) //35%
                {
                    chooseModule = hardSlowBucket[Random.Range(0, hardSlowBucket.Count)];
                }
            }
        }

        if (Time.time > 200 && Time.time < 360f)
        {
            if (playerDistance > 130 && playerDistance < 240)
            {
                if (f < 0.3) //30%
                {
                    chooseModule = easySlowBucket[Random.Range(0, easySlowBucket.Count)];
                }
                else if (f >= 0.15 && f < 0.2) //15%
                {
                    chooseModule = easyMediumBucket[Random.Range(0, easyMediumBucket.Count)];
                }
                else if (f >= 0.19) //19%
                {
                    chooseModule = easyFastBucket[Random.Range(0, easyFastBucket.Count)];
                }
                else if (f >= 0.05) //5%
                {
                    chooseModule = hardSlowBucket[Random.Range(0, hardSlowBucket.Count)];
                }
                else if (f >= 0.1) //10%
                {
                    chooseModule = hardMediumBucket[Random.Range(0, hardMediumBucket.Count)];
                }
                else if (f >= 0.21) //21%
                {
                    chooseModule = hardFastBucket[Random.Range(0, hardFastBucket.Count)];
                }

            }
            else if (playerDistance > 130 && playerDistance >= 240)
            {
                if (f < 0.5) //5%
                {
                    chooseModule = easySlowBucket[Random.Range(0, easySlowBucket.Count)];
                }
                else if (f >= 0.1) //10%
                {
                    chooseModule = easyMediumBucket[Random.Range(0, easyMediumBucket.Count)];
                }
                else if (f >= 0.19) //19%
                {
                    chooseModule = easyFastBucket[Random.Range(0, easyFastBucket.Count)];
                }
                else if (f >= 0.3) //30%
                {
                    chooseModule = hardSlowBucket[Random.Range(0, hardSlowBucket.Count)];
                }
                else if (f >= 0.15 && f < 0.2) //15%
                {
                    chooseModule = hardMediumBucket[Random.Range(0, hardMediumBucket.Count)];
                }
                else if (f >= 21) //21%
                {
                    chooseModule = hardFastBucket[Random.Range(0, hardFastBucket.Count)];
                }
            }
        }

        if (Time.time > 360 && Time.time < 480f)
        {
            if (playerDistance > 240 && playerDistance < 480)
            {
                if (f < 0) //0%
                {
                    chooseModule = easySlowBucket[Random.Range(0, easySlowBucket.Count)];
                }
                else if (f >= 0) //0%
                {
                    chooseModule = easyMediumBucket[Random.Range(0, easyMediumBucket.Count)];
                }
                else if (f >= 0.1) //10%
                {
                    chooseModule = easyFastBucket[Random.Range(0, easyFastBucket.Count)];
                }
                else if (f >= 0.65) //65%
                {
                    chooseModule = hardSlowBucket[Random.Range(0, hardSlowBucket.Count)];
                }
                else if (f >= 0.25) //25%
                {
                    chooseModule = hardMediumBucket[Random.Range(0, hardMediumBucket.Count)];
                }
                else if (f >= 0.05) //5%
                {
                    chooseModule = hardFastBucket[Random.Range(0, hardFastBucket.Count)];
                }

            }
            else if (playerDistance > 240 && playerDistance >= 480)
            {
                if (f < 0) //0%
                {
                    chooseModule = easySlowBucket[Random.Range(0, easySlowBucket.Count)];
                }
                else if (f >= 0) //0%
                {
                    chooseModule = easyMediumBucket[Random.Range(0, easyMediumBucket.Count)];
                }
                else if (f >= 0.03) //3%
                {
                    chooseModule = easyFastBucket[Random.Range(0, easyFastBucket.Count)];
                }
                else if (f >= 0.25) //25%
                {
                    chooseModule = hardSlowBucket[Random.Range(0, hardSlowBucket.Count)];
                }
                else if (f >= 0.25 && f < 0.35) //35%
                {
                    chooseModule = hardMediumBucket[Random.Range(0, hardMediumBucket.Count)];
                }
                else if (f >= 0.3) //37%
                {
                    chooseModule = hardFastBucket[Random.Range(0, hardFastBucket.Count)];
                }
            }
        }

        if (Time.time > 480 && Time.time < 960f)
        {
            if (playerDistance > 480 && playerDistance < 960)
            {
                if (f < 0) //0%
                {
                    chooseModule = easySlowBucket[Random.Range(0, easySlowBucket.Count)];
                }
                else if (f >= 0) //0%
                {
                    chooseModule = easyMediumBucket[Random.Range(0, easyMediumBucket.Count)];
                }
                else if (f >= 0) //0%
                {
                    chooseModule = easyFastBucket[Random.Range(0, easyFastBucket.Count)];
                }
                else if (f >= 0.3) //30%
                {
                    chooseModule = hardSlowBucket[Random.Range(0, hardSlowBucket.Count)];
                }
                else if (f >= 0.5) //50%
                {
                    chooseModule = hardMediumBucket[Random.Range(0, hardMediumBucket.Count)];
                }
                else if (f >= 0.2) //20%
                {
                    chooseModule = hardFastBucket[Random.Range(0, hardFastBucket.Count)];
                }

            }
            else if (playerDistance > 480 && playerDistance >= 960)
            {
                if (f < 0) //0%
                {
                    chooseModule = easySlowBucket[Random.Range(0, easySlowBucket.Count)];
                }
                else if (f >= 0.0) //0%
                {
                    chooseModule = easyMediumBucket[Random.Range(0, easyMediumBucket.Count)];
                }
                else if (f >= 0.0) //0%
                {
                    chooseModule = easyFastBucket[Random.Range(0, easyFastBucket.Count)];
                }
                else if (f >= 0.05) //5%
                {
                    chooseModule = hardSlowBucket[Random.Range(0, hardSlowBucket.Count)];
                }
                else if (f >= 0.05 && f < 0.45) //45%
                {
                    chooseModule = hardMediumBucket[Random.Range(0, hardMediumBucket.Count)];
                }
                else if (f >= 0.5) //50%
                {
                    chooseModule = hardFastBucket[Random.Range(0, hardFastBucket.Count)];
                }
            }






        }

        return chooseModule;

    }
    private Transform SpawnModule(Vector3 spawnPosition)
    {
        Transform pickedModule = ChooseModuleToSpawn();
        Transform levelModule = Instantiate(pickedModule, spawnPosition, Quaternion.identity);

        return levelModule;
    }

    void SpawnCounter()
    {
        SpawnModule();
    }
}