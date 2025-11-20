using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject firePoint;
    [SerializeField] GameObject vfxToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && Time.time >= timeToFire) {
            timeToFire = Time.time + 1f;
            SpawnVFX();
        }
    }

    private void SpawnVFX()
    {
        GameObject vfx = Instantiate(vfxToSpawn, firePoint.transform.position, Quaternion.identity);
        vfx.SetActive(true);
    }

    private float timeToFire;
}
