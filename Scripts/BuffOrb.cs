using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffOrb : MonoBehaviour
{
    #region Variables

    [Header("Settings")]   /********/
    [SerializeField]
    [Range(0.3f, 3f)]
    float disappearDuration=0.3f;

    [Header("Data")]    /********/
    bool active = false;
    bool disappearing = false;
    float disappearTimer;


    [Header("Components")]   /********/
    [SerializeField]
    AudioSource aS;
    [SerializeField]
    Animator anim;
    [SerializeField]
    AudioClip appearSound;
    [SerializeField]
    AudioClip disappearSound;


    #endregion


    #region Base Methods


    void OnEnable()
    {
        anim.Play("appear");
        aS.clip = appearSound;
        aS.Play();
        disappearing = false;
        active = false;
            
    }

    void Update()
    {
        DisappearTimer();
    }
    #endregion


    #region Unique Methods

    private void OnTriggerEnter(Collider other)
    {
        if (active && !disappearing)
        {
            PlayerComponents player = other.gameObject.GetComponent<PlayerComponents>();
            if (player != null && player.statusScript.IsAlive())
            {
                player.attackScript.ActivateBuff();
                disappearTimer = disappearDuration;
                disappearing = true;
                anim.Play("disappear");
                aS.clip = disappearSound;
                aS.Play();
                EventManager.BuffPicked();
            }
        }

    }
     void Activate()
    {
        active = true;
    }
    void DisappearTimer()
    {
        if (disappearing)
        {
            if (disappearTimer > 0) disappearTimer -= Time.deltaTime;
            else
            {
                disappearing = false;
                    this.gameObject.SetActive(false);
            }
        }

    }


    #endregion
}
