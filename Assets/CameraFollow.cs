using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform focus;

    private const float MoveSpeedX = 5f;
    private const float MoveSpeedY = 2f;
    private const float MoveSpeedYMax = 3f;

    private bool lerpY = false;

    private void Awake()
    {
        GameController.CharacterSwitchListener += SwitchFocus;
    }

    private void SwitchFocus(GameController.Character character)
    {
        focus = GameController.CurrentCharacters[character].transform;
    }

    private void FixedUpdate()
    {
        if (focus == null) focus = FindObjectOfType<CharacterController>().transform;
        var pos = transform.position;
        var fPos = focus.position;

        if (!lerpY) lerpY = (fPos.y - pos.y) > 3 | (fPos.y - pos.y) < -1;
        else lerpY = !((pos.y - fPos.y) < .2f & (fPos.y - pos.y)  > -.1f);


        var moveX = Mathf.Lerp(pos.x, fPos.x, MoveSpeedX*Time.deltaTime);
        var moveY = lerpY ? Mathf.Lerp(pos.y, fPos.y, MoveSpeedY*Time.deltaTime) : pos.y;
        
        
        transform.position = new Vector3(moveX, moveY, pos.z);
    }
}
