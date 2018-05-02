using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour {

    public Animator anim;
    public Transform firingPos;
    public AudioSource sfxShoot;
    public Image crosshair;
    public Text textAmmo;
    public int maxAmmo;
    public PlayerController player;

    protected bool isFiring;

    private int loadedAmmo;
    public int LoadedAmmo
    {
        get { return loadedAmmo; }
        set {
            loadedAmmo = value;
            textAmmo.text = loadedAmmo.ToString();
        }
    }

    protected virtual void Start()
    {
        LoadedAmmo = maxAmmo;
    }

    // Update is called once per frame
    protected virtual void Update () {
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        if (player.isBtnShootPressed)
        {
            StartFiring();
        }
        else //if (Input.GetKeyUp(KeyCode.Mouse0))
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

    public virtual void OnReloadDone()
    {
        LoadedAmmo = maxAmmo;
    }

    private void OnEnable()
    {
        crosshair.gameObject.SetActive(true);
        textAmmo.text = LoadedAmmo.ToString();
    }

    private void OnDisable()
    {
        if (crosshair != null)
        {
            crosshair.gameObject.SetActive(false);
        }
    }
}
