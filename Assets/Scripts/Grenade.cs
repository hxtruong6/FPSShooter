using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

    public Transform explosionPrefab;
    public int damage;
    public float explosionRange;

    private void OnTriggerEnter(Collider other)
    {
        var explosion = Instantiate(explosionPrefab);
        explosion.position = transform.position;
        explosion.rotation = Quaternion.identity;
        explosion.gameObject.SetActive(true);

        Collider[] cols = Physics.OverlapSphere(transform.position, explosionRange,
            LayerMask.GetMask("Zombie"));

        List<Zombie> affectedZombie = new List<Zombie>();
        for (int i = 0; i < cols.Length; i++)
        {
            var zombie = cols[i].GetComponentInParent<Zombie>();
            if (affectedZombie.Contains(zombie))
            {
                continue;
            }
            zombie.TakeDamage(damage);
            affectedZombie.Add(zombie);
        }

        Destroy(explosion.gameObject, 2f);
        gameObject.SetActive(false);
    }
}
