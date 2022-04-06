using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region Variables

    [Header("Settings")]    /********/
    [SerializeField] [Range(1f, 55f)]
    float projectileSpeed = 0f;
    [SerializeField]
    Vector3 spawnOffset;
    [SerializeField]
    [Range(0.05f, 2f)]
    float castingDuration;
    [SerializeField]
    [Range(0.05f, 2f)]
    float spawnDelayDuration;
    [SerializeField]
    [Range(5f, 20f)]
    float buffDuration = 5f;
    bool buffActive = false;
    [SerializeField]
    [Range(0.05f, 2f)]
    float castingBuffDuration;
    [SerializeField]
    [Range(0.05f, 2f)]
    float spawnDelayBuffDuration;
    [SerializeField]
    [Range(1f, 5f)]
    float buffAnimationSpeed=1.4f;

    [Header("Data")]    /********/
    bool buttonPressed = false;
    float spawnDelayTimer;
    bool casting = false;
    bool alternateAnimation = false;
    float buffTimer;
    float defaultCastingDuration;
    float defaultDelayDuration;


    [Header("Components")]   /********/
    [SerializeField]
    PlayerComponents player;
    [SerializeField]
    GameObject projectile;
    [SerializeField] AudioClip castSound1;
    [SerializeField] AudioClip castSound2;




    #endregion


    #region Base Methods

    void Start()
    {
        defaultCastingDuration = castingDuration;
        defaultDelayDuration = spawnDelayDuration;
    }

    void Update()
    {
        SpawnDelayTimer();
        BuffTimer();
        CastSpell();
    }
    #endregion


    #region Unique Methods
    


    public void ButtonDown()
    {
        buttonPressed = true;
    }

    public void ButtonUp()
    {
        buttonPressed = false;

    }

    void CastSpell()
    {
        if (buttonPressed && !player.statusScript.IsCasting() && player.statusScript.CanAct())
        {
            casting = true;
            spawnDelayTimer = spawnDelayDuration;
            player.statusScript.BeginCasting(castingDuration);
            if (!alternateAnimation) player.ChangeAnimationState("Attack1", 0.2f, 0f, 0f);
            else player.ChangeAnimationState("Attack2", 0.2f, 0f, 0f);
            alternateAnimation = !alternateAnimation;
            player.runScript.SlowMovement();
        }

    }




    void SpawnDelayTimer()
    {
        if (casting)
        {
            if (spawnDelayTimer > 0) spawnDelayTimer -= Time.deltaTime;
            else
            {
                casting = false;
                spawnDelayTimer = 0;
                CreateProjectile();
            }
        }
    }

    void CreateProjectile()
    {
        if (player.statusScript.CanAct())
        {
            if (alternateAnimation) player.PlaySound(castSound1, 1);
            else player.PlaySound(castSound2, 1);

            GameObject temp = ObjectPool.SharedInstance.GetProjectileFromPool();
            temp.transform.position = player.model.transform.position + (player.model.transform.forward + spawnOffset);;
            temp.transform.LookAt(player.aimScript.crosshair.transform);
            temp.GetComponent<Rigidbody>().velocity = player.model.transform.forward * projectileSpeed;
        }

    }

    void BuffTimer()
    {
        if (buffActive)
        {
            if (buffTimer > 0) buffTimer -= Time.deltaTime;
            else
            {
                DeactivateBuff();
            }
        }
    }

    public void ActivateBuff()
    {
        buffTimer = buffDuration;
        buffActive = true;
        castingDuration = castingBuffDuration;
        spawnDelayDuration = spawnDelayBuffDuration;
        player.anim.SetFloat("speed", buffAnimationSpeed);
    }

    void DeactivateBuff()
    {
        buffActive = false;
        castingDuration = defaultCastingDuration;
        spawnDelayDuration = defaultDelayDuration;
        player.anim.SetFloat("speed", 1f);

    }

    #endregion
}
