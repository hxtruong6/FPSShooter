using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

    public Transform explosionPrefab;

    private void OnTriggerEnter(Collider other)
    {
        var explosion = Instantiate(explosionPrefab);
        explosion.position = transform.position;
        explosion.rotation = Quaternion.identity;
        explosion.gameObject.SetActive(true);

        Destroy(explosion.gameObject, 2f);
        gameObject.SetActive(false);
    }
}
