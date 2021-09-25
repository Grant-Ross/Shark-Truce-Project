using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform focus;

    private const float MoveSpeed = 5f;

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
        transform.position = Vector3.Lerp(transform.position, 
            new Vector3(focus.position.x, transform.position.y, transform.position.z),MoveSpeed*Time.deltaTime);
    }
}
