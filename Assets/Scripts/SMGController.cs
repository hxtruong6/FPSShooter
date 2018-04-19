﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMGController : GunController
{
    public float coolDown;
    public AnimationClip shootingClip;
    public AudioSource sfxShoot;
    public GameObject flash;
    public ParticleSystem bulletTrail;
    public Transform aimingCamera;
    public Transform cube;

    private float counter;
    private float shootClipLen;
    private float flashCounter;

    private void Start()
    {
        shootClipLen = shootingClip.length;
        float animSpeed = shootClipLen / coolDown;
        anim.SetFloat("firingSpeed", animSpeed);
        flash.SetActive(false);
    }

    protected override void Update()
    {
        if (flashCounter > 0) // prevent overheating
        {
            flashCounter -= Time.deltaTime;
            if (flashCounter < 0)
            {
                flash.gameObject.SetActive(false);
            }
        }
        base.Update();
    }

    protected override void UpdateFiring()
    {
        base.UpdateFiring();

        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            anim.Play("Shoot01", 0, 0f);
            counter += coolDown;
            sfxShoot.Play();
            flash.SetActive(true);
            flashCounter = 0.05f;
            bulletTrail.Emit(1);

            RaycastHit hitInfo;
            if (Physics.Raycast(aimingCamera.position, aimingCamera.forward, 
                out hitInfo))
            {
                var newCube = Instantiate(cube);
                newCube.transform.position = hitInfo.point;
            }            
        }
    }
}
