using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LettuceController : CharacterController
{
    private bool doubleJump = false;

    protected override void Update()
    {
        if (!Current) return;
        base.Update();
        if (Grounded) doubleJump = true;
        if (Input.GetButtonDown("Jump") && !Grounded && doubleJump)
        {
            Jump();
            doubleJump = false;
        }
    }
}

