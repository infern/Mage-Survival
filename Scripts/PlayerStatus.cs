using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    #region Variables

    [Header("Settings")]    /********/
    [SerializeField] [Range(0.3f, 5f)]
    float dyingDuration=0.7f;

    [SerializeField]
    AudioClip deathSound;


    [Header("Data")]    /********/
    float dyingTimer;
    [SerializeField]
    bool alive = true;
    [SerializeField]
    bool stunned = false;
    bool active = true;
    [SerializeField]
    bool casting;
    float castingTimer;

    [Header("Components")]   /********/
    [SerializeField]
    PlayerComponents player;

    #endregion


    #region Base Methods

    void Update()
    {
        DyingTimer();
        CastingTimer();
    }


    #endregion


    #region Unique Methods


    public void Death()
    {
        EventManager.PlayerDied();
        player.aimScript.crosshair.SetActive(false);
        player.rb.velocity = Vector3.zero;
        player.cc.enabled = false;
        dyingTimer = dyingDuration;
        BeginCasting(dyingDuration);
        player.ChangeAnimationState("Death", 0.2f, 0f, 0f);
        alive = false;
        player.PlaySound(deathSound,1);
 }

    void DyingTimer()
    {
        if (!alive)
        {
            if (dyingTimer > 0) dyingTimer -= Time.unscaledDeltaTime;
            else
            {
                //reset game
            }
        }

    }

    public void BeginCasting(float duration)
    {
        castingTimer = duration;
        casting = true;
    }
    void CastingTimer()
    {
        if (casting)
        {
            if (castingTimer > 0) castingTimer -= Time.unscaledDeltaTime;
            else casting = false;
        }
    }

    public bool IsCasting()
    {
        if (casting) return true;
        else return false;
    }

    public bool IsAlive()
    {
        if (alive) return true;
        else return false;
    }

    public bool IsActive()
    {
        if (active) return true;
        else return false;
    }

    public bool CanAct()
    {
        if (IsAlive() && IsActive()) return true;
        else return false;
    }


    #endregion
}
