using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public Animator anim;
    public Transform firingPos;

    protected int loadedAmmo;
    protected bool isFiring;

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartFiring();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            StopFiring();
        }

        if (isFiring)
        {
            UpdateFiring();
        }
	}

    protected virtual void StartFiring() {
        isFiring = true;
    }
    protected virtual void StopFiring() {
        isFiring = false;
    }

    protected virtual void UpdateFiring() { }

    private void Reset()
    {
        anim = GetComponent<Animator>();
    }

    public void OnReloadDone()
    {
        loadedAmmo = 1;
    }
}
