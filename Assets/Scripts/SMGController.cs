using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMGController : GunController
{
    private const int LayerZombie = 8;

    public float coolDown;
    public AnimationClip shootingClip;
    public GameObject flash;
    public ParticleSystem bulletTrail;
    public Transform aimingCamera;
    //public ParticleSystem dustEffect;
    public Transform dustPrefab;
    public Transform bloodPrefab;
    public int damage;

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
        if (LoadedAmmo <= 0) {
            anim.SetBool("needReload", true);
            return;
        }
        if (counter <= 0 && LoadedAmmo >0) {
           
            anim.Play("Shoot01", 0, 0f);
            counter += coolDown;
            sfxShoot.Play();
            LoadedAmmo--;

            flash.SetActive(true);
            flashCounter = 0.05f;
            bulletTrail.Emit(1);

            RaycastHit hitInfo;
            if (Physics.Raycast(aimingCamera.position, aimingCamera.forward, 
                out hitInfo,
                100f, LayerMask.GetMask("Zombie", "Terrain", "Default")))
            {
                if (hitInfo.collider.gameObject.layer == LayerZombie)
                {
                    var bloodEffect = Instantiate(bloodPrefab);
                    bloodEffect.transform.position = hitInfo.point;
                    bloodEffect.transform.forward = -aimingCamera.forward;

                    var zombie = hitInfo.collider.GetComponentInParent<Zombie>();
                    zombie.TakeDamage(damage);
                }
                else
                {
                    var dustEffect = Instantiate(dustPrefab);
                    dustEffect.transform.position = hitInfo.point;
                    dustEffect.transform.forward = hitInfo.normal;
                }

                float distance = Vector3.Distance(
                    hitInfo.point, bulletTrail.transform.position);
                var main = bulletTrail.main;
                main.startLifetime = distance / main.startSpeed.constant;
            }
            else
            {
                var main = bulletTrail.main;
                main.startLifetime = 1f;
            }
        }

    
    }

    public override void OnReloadDone() {
        LoadedAmmo = 35;
        counter = 0;
        anim.SetBool("needReload", false);
    }

    protected override void ReloadAmmo() {
        LoadedAmmo = 0;
        anim.SetBool("needReload", true);
    }
}
