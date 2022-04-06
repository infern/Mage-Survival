using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameMananger : MonoBehaviour
{

    #region Variables

    [Header("Settings")]    /********/
    [SerializeField]
    int roundNumber = 0;
    [SerializeField]
    [Range(1f, 6f)]
    float restartDuration = 4f;
    [SerializeField]
    [Range(1f, 6f)]
    float blackScreenDuration = 4f;
    [SerializeField]
    int maximumEnemyCount = 100;
    [SerializeField]
    [Range(0.1f, 6f)]
    float spawnDuration=2f;
    [SerializeField]
    [Range(1f, 2f)]
    int spawnCount = 1;
    [SerializeField]
    [Range(1, 1000)]
    int killsRequired=3;
    [SerializeField]
    [Range(0.2f, 6f)]
    float roundDelayDuration = 3f;
    [SerializeField]
    [Range(100f, 1000f)]
    float skeletonDefaultMS = 290;
    [SerializeField]
    Vector2 buffOrbDuration = new Vector2(5f,30f);

    [Header("Data")]    /********/
    bool gameActive = true;
    bool roundActive = false;
    float restartTimer;
    bool restarting = false;
    float blackScreenTimer;
    bool blackScreenTransition;
    int currentEnemyCount;
    float spawnTimer;
    int killsRound = 0;
    int killsTotal = 0;
    float roundDelayTimer;
    bool buffOrbTrigger = false;
    float buffOrbTimer;
    bool buffOrbSpawning = false;
    bool gamePaused = false;

[Header("Components")]   /********/
    [SerializeField] PlayerComponents player;
    [SerializeField] Animator anim;
    [SerializeField] BoxCollider arenaCollider;
    [SerializeField] TextMeshProUGUI roundTMP;
    [SerializeField] TextMeshProUGUI killsTMP;
    [SerializeField] Animator roundAnim;
    [SerializeField] AudioSource aS;
    [SerializeField] GameObject buffOrb;
    [SerializeField] GameObject pauseObject;
    [SerializeField] TextMeshProUGUI pauseTMP;
    [SerializeField] GameObject roundDoneObject;
    [SerializeField] TextMeshProUGUI roundDoneTMP;
    [SerializeField] AudioLowPassFilter lowPassFilter;





    #endregion


    #region Base Methods

    void OnEnable()
    {
        EventManager.PlayerDiedTrigger += TriggerRestart;
        EventManager.EnemyDiedTrigger += AddPoint;
        EventManager.BuffPickedTrigger += BuffPickedUp;
        EventManager.PauseGameTrigger += PauseToggle;


    }

    void OnDisable()
    {
        EventManager.PlayerDiedTrigger -= TriggerRestart;
        EventManager.EnemyDiedTrigger -= AddPoint;
        EventManager.BuffPickedTrigger -= BuffPickedUp;
        EventManager.PauseGameTrigger -= PauseToggle;



    }

    void Start()
    {
        AdjustRound();
        PlayerPrefs.SetFloat("sMS", skeletonDefaultMS);
    }
    void Update()
    {
        RestartTimer();
        BlackScreenTimer();
        SpawnTimer();
        RoundDelayTimer();
        BuffOrbTimer();
    }
    #endregion


    #region Restart


    void TriggerRestart()
    {
        anim.Play("red");
        restartTimer = restartDuration;
        restarting = true;
        roundActive = false;
            }
    void RestartTimer()
    {
        if (restarting)
        {
            if (restartTimer > 0) restartTimer -= Time.deltaTime;
            else
            {
                restarting = false;
                restartTimer = 0f;
                blackScreenTimer = blackScreenDuration;
                blackScreenTransition = true;
                anim.Play("fadeIn");

            }
        }
    }

    void BlackScreenTimer()
    {
        if (blackScreenTransition)
        {
            if (blackScreenTimer > 0) blackScreenTimer -= Time.deltaTime;
            else
            {
                blackScreenTransition = false;
                blackScreenTimer = 0f;
                pauseTMP.fontSize = 113.3f;
                pauseTMP.text = "Game Over!";
                roundDoneObject.SetActive(true);
                if(roundNumber>1) roundDoneTMP.text = "Survived <color=yellow>" + roundNumber + "</color> rounds";
                else roundDoneTMP.text = "Survived <color=yellow>" + roundNumber + "</color> round";

                gameActive = false;
                
                PauseOn();
            }
        }
    }

    #endregion


    #region Spawner
    void SpawnTimer()
    {
        if (roundActive)
        {
            if (spawnTimer > 0) spawnTimer -= Time.deltaTime;
            else
            {
                spawnTimer = spawnDuration;
                CreateEnemyPortal();
            }
        }

    }

    void CreateEnemyPortal()
    {
        if(currentEnemyCount <= maximumEnemyCount-1)
        {
            GameObject temp = ObjectPool.SharedInstance.GetEnemyFromPool();
            if (temp != null)
            {
                temp.transform.position = RandomPointInBounds(arenaCollider.bounds);
                currentEnemyCount++;

            }
            else spawnTimer = 0.2f;
        }
        else spawnTimer = 0.2f;


    }

    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            -10.14f,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }


    void BuffOrbTimer()
    {
        if (buffOrbSpawning && buffOrbTrigger)
        {
            if (buffOrbTimer > 0) buffOrbTimer -= Time.deltaTime;
            else
            {
                buffOrbSpawning = false;
                SpawnBuffOrb();
            }

        }
    }
    void SpawnBuffOrb()
    {
        buffOrb.transform.position= RandomPointInBounds(arenaCollider.bounds);
        buffOrb.SetActive(true);
    }

    void BuffPickedUp()
    {
        buffOrbSpawning = true;
        buffOrbTimer = Random.Range(buffOrbDuration.x, buffOrbDuration.y);
        anim.Play("buff");
    }

    #endregion





    #region Rounds
    void RoundDelayTimer()
    {
        if (!roundActive)
        {
            if (roundDelayTimer > 0) roundDelayTimer -= Time.deltaTime;
            else
            {
                roundActive = true;
                StartNewRound();
            }
        }
    }
    void StartNewRound()
    {
        roundActive = true;
    }

    void AdjustRound()
    {
        aS.Play();
        EventManager.RoundEnded();
        spawnTimer = spawnDuration;
        roundDelayTimer = roundDelayDuration;
        roundNumber++;
        roundTMP.text = ("Round " + roundNumber);
        roundAnim.Play("appear");
        killsRound = 0;
        killsTMP.text = killsRequired.ToString();
        currentEnemyCount = 0;
        Scaling();
    }

    void Scaling()
    {
        killsRequired++;


        if (roundNumber % 2 == 0)
        {
            maximumEnemyCount++;
        }
        if (roundNumber % 4 == 0)
        {
            spawnDuration = Mathf.Clamp(spawnDuration - 0.05f, 0.1f, 4f);
            skeletonDefaultMS += 15f;
            PlayerPrefs.SetFloat("sMS", skeletonDefaultMS);
        }

        if (roundNumber % 6 == 0)
        {
            killsRequired = killsRequired + 2;
            maximumEnemyCount = maximumEnemyCount + 2;
        }

        if (roundNumber > 8)
        {
            killsRequired++;
            maximumEnemyCount++;
        }

        if (roundNumber == 10)
        {
            skeletonDefaultMS += 30f;
            PlayerPrefs.SetFloat("sMS", skeletonDefaultMS);
        }

        if (roundNumber > 14)
        {
            killsRequired=killsRequired+2;
            maximumEnemyCount = maximumEnemyCount + 2;
        }

        if (roundNumber == 5)
        {
            buffOrbTimer = buffOrbTimer = Random.Range(buffOrbDuration.x, buffOrbDuration.y);
            buffOrbSpawning = true;
            buffOrbTrigger = true;
        }


    }
    void CheckWinCondition()
    {
        if (killsRound >= killsRequired - 1)
        {
            AdjustRound();
        }
    }

    void AddPoint()
    {
        killsRound++;
        killsTotal++;
        currentEnemyCount--;
        if ((killsRequired - killsRound - 1) == 1) killsTMP.color = Color.yellow;
        else killsTMP.color = Color.white;
        killsTMP.text = (killsRequired - killsRound - 1).ToString();
        CheckWinCondition();
    }
    #endregion

    #region Other

     void PauseToggle()
    {
        if (!gamePaused) PauseOn();
        else PauseOff();
    }

    void PauseOn()
    {
        gamePaused = true;
        Time.timeScale = 0f;
        pauseObject.SetActive(true);
        lowPassFilter.cutoffFrequency=800f;
    }

    void PauseOff()
    {
        Time.timeScale = 1f;
        if (gameActive)
        {
            gamePaused = false;

            pauseObject.SetActive(false);
            lowPassFilter.cutoffFrequency = 5007.7f;
        }
        else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   

    }

    #endregion


}
