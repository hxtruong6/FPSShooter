using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMGController : GunController
{
    public float coolDown;

    private float counter;

    protected override void UpdateFiring()
    {
        base.UpdateFiring();

        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            anim.Play("Shoot01", 0, 0f);
            counter += coolDown;
        }
    }
}
