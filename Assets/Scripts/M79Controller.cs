using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M79Controller : GunController {
    public Rigidbody bullet;

    protected void OnEnable()
    {
        if (LoadedAmmo <= 0)
        {
            anim.SetBool("needReload", true);
        }
    }

    protected override void UpdateFiring()
    {
        anim.ResetTrigger("Shoot");
        if (LoadedAmmo > 0)
        {
            anim.SetTrigger("Shoot");

            bullet.gameObject.SetActive(true);
            bullet.transform.position = firingPos.transform.position;
            bullet.transform.rotation = firingPos.transform.rotation;
            bullet.velocity = firingPos.forward * 10f;
            LoadedAmmo = 0;
            sfxShoot.Play();
        }
    }

    public override void OnReloadDone()
    {
        base.OnReloadDone();

        anim.SetBool("needReload", false);
    }
}
