using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : MonoBehaviour
{
    #region Variables

    /********/
    [Header("Settings")]
    [SerializeField]
    [Range(1f, 350f)]
    float runSpeed = 6.5f;
    [SerializeField]
    [Range(0f, 1f)]
    float speedSlowMultiplier = 0.5f;
    [Range(0.01f, 5f)]
    float speedUpRatio = 1f;
    [SerializeField]
    [Range(0.01f, 5f)]
    float speedDownRatio = 1f;


    [Header("Data")]    /********/
    public Vector2 direction;
    public Vector2 savedDirection;
    public bool walking = false;
    bool speedSlowing = false;
    bool speedIncreasing = false;
    float speedSlow;
    float t;

    /********/
    [Header("Components")]
    [SerializeField]
    PlayerComponents player;


    #endregion


    #region Base Methods

    private void Start()
    {
        speedSlow = runSpeed;
    }


    private void Update()
    {
        ModifySpeed();
    }
    void FixedUpdate()
    {
        Run();
    }

    #endregion

    #region Unique Methods



    void ModifySpeed()
    {
        if (speedSlowing)
        {
            speedSlow = Mathf.Lerp(speedSlow, runSpeed * speedSlowMultiplier, t);
            t += speedDownRatio * Time.deltaTime;
            if (speedSlow <= runSpeed * speedSlowMultiplier)
            {
                speedSlowing = false;
                speedIncreasing = true;
                t = 0f;
            }
        }
        else if (speedIncreasing)
        {
            speedSlow = Mathf.Lerp(speedSlow, runSpeed, t);
            t += speedUpRatio * Time.deltaTime;
            if (speedSlow >= runSpeed)
            {
                speedIncreasing = false;
                t = 0f;

            }
        }
    }
    public void Run()
    {
      
        if (player.statusScript.CanAct())
        {
            if (direction != Vector2.zero) savedDirection = direction.normalized;
            else walking = false;

                    if (direction != Vector2.zero)
                    {
                        Vector2 move = direction * speedSlow;
                        player.rb.AddForce(new Vector3(move.x, player.rb.velocity.y, move.y));
                        walking = true;


                    }
        }

    }


    public void SlowMovement()
    {
        if(!speedSlowing){
            speedSlowing = true;
            speedIncreasing = false;
            t = 0f;
        }
    }
    



    #endregion
}
