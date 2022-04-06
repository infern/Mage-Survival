using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    #region Variables

    [Header("Settings")]    /********/
    [SerializeField] float animTransitionDuration = 0.2f;
    [SerializeField] float animTransitionOffset = 0f;
    [SerializeField] float animTransitionTime = 0f;
    [SerializeField] bool animInteruptable = true;


    [Header("Data")]    /********/
    public Vector2 direction;
    const string idle = "Idle01";
    const string walkForward = "BattleWalkForward";
    const string walkBackward = "BattleWalkBack";
    const string walkSideR = "BattleWalkRight";
    const string walkSideRF = "BattleWalkRight";
    const string walkSideRB = "BattleWalkRight";
    const string walkSideL = "BattleWalkLeft";
    const string walkSideLF = "BattleWalkLeft";
    const string walkSideLB = "BattleWalkLeft";






    ////Stick recently moved
    [SerializeField]
    public float recentlyActiveTimer = 0f;



    [Header("Components")]    /********/
    [SerializeField]
    PlayerComponents player;
    public GameObject crosshair;




    #endregion


    #region Base Methods

    void Update()
    {
        MoveCrosshair();
        LookTowardsCrosshair();
        DetectAnimationState();
    }
    #endregion


    #region Unique Methods

    void MoveCrosshair()
    {
        Vector3 object_pos1 = Camera.main.WorldToScreenPoint(player.model.transform.position);
        Vector3 object_pos2 = crosshair.transform.position;
        crosshair.transform.position = Input.mousePosition;
        float tAngle = Mathf.Rad2Deg * Mathf.Atan2(object_pos1.y - object_pos2.y, object_pos1.x - object_pos2.x)+90f;
        crosshair.transform.rotation = Quaternion.Euler(0, 0, tAngle);

    }

    void LookTowardsCrosshair()
    {
        if (player.statusScript.CanAct())
        {
            Vector3 mouse_pos;
            Vector3 object_pos;
            float angle;
            mouse_pos = Input.mousePosition;
            mouse_pos.z = 5.23f; //The distance between the camera and object
            object_pos = Camera.main.WorldToScreenPoint(transform.position);
            mouse_pos.x = mouse_pos.x - object_pos.x;
            mouse_pos.y = mouse_pos.y - object_pos.y;
            angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, -angle + 90, 0));

        }
    }



    void DetectAnimationState()
    {
        if (player.statusScript.CanAct() && !player.statusScript.IsCasting())
        {

            Vector3 moveDirection = new Vector3(player.runScript.direction.x, 0f, player.runScript.direction.y);
            Vector3 aimDirection = new Vector3(player.model.transform.forward.x, 0f, player.transform.forward.z);
            float angle = Vector3.Angle(moveDirection, aimDirection);
            Vector3 side = Vector3.Cross(moveDirection, aimDirection);
            bool movingBackwards = (angle > 140f && angle < 181f);
            bool movingSidewaysL = ((side.y > 0.1f && side.y <= 1.1f));
            bool movingSidewaysR = ((side.y > -1.1f && side.y <= -0.1f));
            bool movingAtAngle = ((side.y > -0f && side.y <= 0.4f) || (side.y >= -0.4f && side.y <= 0f));

            if (player.runScript.walking)
            {
                if (!movingBackwards && movingAtAngle) AnimationTransition(walkForward);
                else if (movingBackwards && movingAtAngle) AnimationTransition(walkBackward);
                else if (movingSidewaysL && !movingSidewaysR) AnimationTransition(walkSideL);
                else if (movingSidewaysR && !movingSidewaysL) AnimationTransition(walkSideR);
            }

            else AnimationTransition(idle);
        }
    }

void AnimationTransition(string state)
{
    player.ChangeAnimationState(state, animTransitionDuration, animTransitionOffset, animTransitionTime);
}
    #endregion
}
