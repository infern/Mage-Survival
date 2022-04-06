using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPortal : MonoBehaviour
{
    #region Variables

    [Header("Settings")]    /********/
    [SerializeField]
    [Range(0.5f, 6f)]
    float summonDuration = 2f;

    [Header("Data")]    /********/
    bool summoning = true;
    float summonTimer;
    bool canceled = false;
    float t = 0;

    [Header("Components")]   /********/
    [SerializeField]
    GameObject portal;
    [SerializeField]
    GameObject unit;
    [SerializeField] AudioSource aS;
    [SerializeField] AudioClip portalSound;
    [SerializeField] AudioClip summoningCompleteSound;





    #endregion


    #region Base Methods

    void OnEnable()
    {
        ResetValues();
        EventManager.RoundEndedTrigger += CancelPortal;
        EventManager.PlayerDiedTrigger += CancelPortal;



    }

    void OnDisable()
    {
        EventManager.RoundEndedTrigger -= CancelPortal;
        EventManager.PlayerDiedTrigger -= CancelPortal;


    }

    void Update()
    {
        SummonTimer();
        ScaleDownAnimation();

    }
    #endregion


    #region Unique Methods

    void SummonTimer()
    {
        if (summoning && !canceled)
        {
            if (summonTimer > 0) summonTimer -= Time.deltaTime;
            else
            {
                summoning = false;
                summonTimer = 0f;
                ActivateUnit();

            }
        }
    }

    void ActivateUnit()
    {
            portal.SetActive(false);
            unit.SetActive(true);
            aS.clip = summoningCompleteSound;
            aS.Play();
    }


    void CancelPortal()
    {
        canceled = true;
    }

    void ScaleDownAnimation()
    {
        if (canceled && summoning)
        {
            float value = Mathf.Lerp(portal.transform.localScale.x, 0, t);
            portal.transform.localScale = new Vector3(value, value, value);
            t += 0.05f  * Time.deltaTime;
            if (value <= 0.09f)
            {
                this.transform.gameObject.SetActive(false);
            }
        }
    }
    void ResetValues()
    {
        portal.SetActive(true);
        unit.SetActive(false);
        summonTimer = summonDuration;
        summoning = true;
        aS.clip = portalSound;
        aS.Play();
        canceled = false;
        portal.transform.localScale = new Vector3(1, 1, 1);


    }
    #endregion
}
