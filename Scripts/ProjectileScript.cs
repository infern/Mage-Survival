using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    #region Variables

    [Header("Settings")]    /********/
    [SerializeField][Range(0.2f, 2f)]
    float disappearDuration=0.4f;

    [Header("Data")]    /********/
    bool disappearing = false;
    float disappearTimer;


    [Header("Components")]   /********/
    [SerializeField] AudioSource aS;
    [SerializeField] Rigidbody rb;
    [SerializeField] SphereCollider sc;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject explosion;


    #endregion


    #region Base Methods

     void OnEnable()
    {
        rb.isKinematic = false;
        sc.enabled = true;
        projectile.SetActive(true);
        explosion.SetActive(false);
    }

    void Update()
    {
        DisappearTimer();
    }
    #endregion


    #region Unique Methods


     void OnCollisionEnter(Collision collision)
    {
        if (!disappearing)
        {
            Impact();
            EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
            if (enemy) enemy.Hurt();

        }

    }

    void Impact()
    {
        disappearing = true;
        disappearTimer = disappearDuration;
        aS.Play();
        rb.velocity = Vector3.zero;
        projectile.SetActive(false);
        explosion.SetActive(true);
    }


    void DisappearTimer()
    {
        if (disappearing)
        {
            if (disappearTimer > 0) disappearTimer -= Time.deltaTime;
            else
            {
                disappearing = false;
                disappearTimer = 0;
                this.gameObject.SetActive(false);
            }
        }
    }



    #endregion
}
