using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    #region Variables

    [Header("Settings")]    /********/
    [SerializeField]
    [Range(0.1f, 3f)]
    float dyingDuration = 1f;
  [SerializeField] [Range(0.1f, 3f)]
    float idleDuration = 1f;
    [SerializeField]
    [Range(0.1f, 555f)]
    float speed = 1f;
    [SerializeField]
    [Range(0.01f, 2f)]
    float disappearSpeed = 1f;

    [Header("Data")]    /********/
    bool idle = true;
    bool alive = true;
    float dyingTimer;
    bool disappearing = false;
    bool active=false;

    float idleTimer;
    string animCurrentState;
     float t = 0.0f;
    [Header("Components")]   /********/
    [SerializeField] AudioSource aS;
    [SerializeField] Animator anim;
    [SerializeField] Transform target;
    [SerializeField] Rigidbody rb;
    [SerializeField] AudioClip attackSound;
    [SerializeField]  List<AudioClip> deathSounds = new List<AudioClip>();
    [SerializeField] CapsuleCollider cc;
    [SerializeField] GameObject deathEffect;


    #endregion


    #region Base Methods

    void OnEnable()
    {
        EventManager.PlayerDiedTrigger += Victory;
        EventManager.RoundEndedTrigger += TriggerDisappearing;

        ResetValues();
    }

     void OnDisable()
    {
        EventManager.PlayerDiedTrigger -= Victory;
        EventManager.RoundEndedTrigger -= TriggerDisappearing;


    }
    void Update()
    {
        IdleTimer();
        DeathTimer();
        ScaleDownAnimation();
    }
     void FixedUpdate()
    {
        RunTowardsPlayer();
    }
    #endregion


    #region Unique Methods

    void IdleTimer()
    {
        if (idle)
        {
            if (idleTimer > 0) idleTimer -= Time.deltaTime;
            else
            {
                if (alive)
                {
                    idleTimer = 0;
                    idle = false;
                    ChangeAnimationState("RunFWD", 0.2f, 0f, 0f);
                }
            }
        }
    }

    void RunTowardsPlayer()
    {
        if (active)
        {
            if (alive && !idle)
            {
                Vector3 direction = (target.position - this.transform.position).normalized;
                rb.AddForce(new Vector3(direction.x, 0, direction.z) * speed, ForceMode.Acceleration);
            }
            if (alive) transform.LookAt(target);
        }


    }

    public void Hurt()
    {
        if (alive && active)
        {
            cc.enabled = false;
            dyingTimer = dyingDuration;
            alive = false;
            ChangeAnimationState("death", 0.2f, 0f, 0f);
            int random = Random.Range(0, deathSounds.Capacity);
            aS.clip = deathSounds[random];
            aS.Play();
            EventManager.EnemyDied();
        }
    }


    void DeathTimer()
    {
        if (!alive && !disappearing)
        {
            if (dyingTimer > 0) dyingTimer -= Time.deltaTime;
            else
            {
                dyingTimer = 0;
                disappearing = true;
                deathEffect.SetActive(true);

            }
        }
    }

    public void ChangeAnimationState(string newState, float duration, float offset, float time)
    {
        if (animCurrentState == newState || animCurrentState == "Attack01") return;
        anim.CrossFade(newState, duration, -1, offset, time);
        animCurrentState = newState;

    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerComponents player = collision.gameObject.GetComponent<PlayerComponents>();
        if (player && alive && !idle)
        {
            ChangeAnimationState("Attack01", 0.2f, 0f, 0f);
            player.statusScript.Death();
            aS.clip = attackSound;
            aS.Play();
            idle = true;
            idleTimer = 50f;
        }
    }

    void Victory()
    {
        ChangeAnimationState("Victory", 0.2f, 0f, 0f);
        idle = true;
        idleTimer = 50f;
    }

    void TriggerDisappearing()
    {
        disappearing = true;
        active = false;
    }
    void ScaleDownAnimation()
    {
        if (disappearing)
        {
            float value = Mathf.Lerp(transform.localScale.x, 0, t);
            transform.localScale = new Vector3(value, value, value);
            t += disappearSpeed * Time.deltaTime;
            if (value <= 0.09f)
            {
                disappearing = false;
                deathEffect.SetActive(false);
                this.transform.parent.gameObject.SetActive(false);
            }
        }
    }

    void ResetValues()
    {
        idleTimer = idleDuration;
        idle = true;
        cc.enabled = true;
        t = 0f;
        transform.localScale = new Vector3(1, 1, 1);
        disappearing = false;
        alive = true;
        transform.position = new Vector3(transform.parent.position.x, transform.position.y, transform.parent.position.z);
        active = true;
        ChangeAnimationState("IdleBattle", 0.2f, 0f, 0f);
        speed = PlayerPrefs.GetFloat("sMS", 280f);

    }
    #endregion
}
