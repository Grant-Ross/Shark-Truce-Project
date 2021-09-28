using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoController : CharacterController
{
    [SerializeField] private GameObject seedBullet;

    private const float BulletOffset = .8f;
    private const float BulletCooldown = .5f;

    private float _cooldownTimer = 0;
    
    protected override void Update()
    {
        if(!Current) return;
        base.Update();
        if (_cooldownTimer > 0) _cooldownTimer -= Time.deltaTime;
        else if (Input.GetKeyDown(KeyCode.J)) ShootSeed();
    }

    private void ShootSeed()
    {
        var bullet = Instantiate(seedBullet, transform.position + (facingRight ? Vector3.right: Vector3.left)*BulletOffset
            , Quaternion.identity);
        bullet.transform.Rotate(0,0, facingRight ? -90 : 90);
        _cooldownTimer = BulletCooldown;
    }
    
}
