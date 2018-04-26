using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{

    public Animator anim;
    public Transform firingPos;
    public AudioSource sfxShoot;
    public Image CrossHair;
    public Text numberAmmo;

    private int loadedAmmo;

    public int LoadedAmmo
    {
        get { return loadedAmmo; }
        set {
            loadedAmmo = value;
            numberAmmo.text = loadedAmmo.ToString();
        }

    }

    protected bool isFiring;

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartFiring();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            StopFiring();
        }
        else if (Input.GetKeyDown(KeyCode.R)) {
            ReloadAmmo();
        }

        if (isFiring)
        {
            UpdateFiring();
        }

    }

    protected virtual void ReloadAmmo()
    {
        
    }

    protected virtual void StartFiring()
    {
        isFiring = true;
    }
    protected virtual void StopFiring()
    {
        isFiring = false;
    }

    protected virtual void UpdateFiring() { }

    private void Reset()
    {
        anim = GetComponent<Animator>();
    }

    public virtual void OnReloadDone()
    {
    }

    private void OnEnable()
    {
        CrossHair.gameObject.SetActive(true);
        numberAmmo.text = LoadedAmmo.ToString();
    }

    private void OnDisable()
    {
        if (CrossHair != null)
        {
            CrossHair.gameObject.SetActive(false);
        }
    }
}
