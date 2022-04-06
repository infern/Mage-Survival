using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponents : MonoBehaviour
{
    public GameObject model;
    public Rigidbody rb;
    public Animator anim;
    public AudioSource aS;
    public AudioSource aS2;
    public CapsuleCollider cc;

    public PlayerStatus statusScript;
    public PlayerRun runScript;
    public PlayerAim aimScript;
    public PlayerAttack attackScript;
    public ReadPlayerInput inputScript;


    private string animCurrentState;

    public void PlaySound(AudioClip clip, int source)
    {
        if (source == 0)
        {
            aS.clip = clip;
            aS.Play();
        }
        else
        {
            aS2.clip = clip;
            aS2.Play();
        }

    }

    public void ChangeAnimationState(string newState, float duration, float offset, float time)
    {
        if (animCurrentState == newState) return;
            anim.CrossFade(newState, duration, -1, offset, time);
            animCurrentState = newState;

    }

}
