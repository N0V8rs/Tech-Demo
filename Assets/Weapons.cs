using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            Enemies enemy = hit.transform.GetComponent<Enemies>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
