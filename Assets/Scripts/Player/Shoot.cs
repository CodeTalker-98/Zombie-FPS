﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject _blood;
    private void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 center = new Vector3(0.5f, 0.5f, 0.0f);
            Ray rayOrigin = Camera.main.ViewportPointToRay(center);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity, 1 << 9 | 1 << 0))
            {
                Debug.Log(hitInfo.collider.name);
                Health health = hitInfo.collider.GetComponent<Health>();
                
                if (health != null)
                {
                    Instantiate(_blood, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                    health.Damage(50);
                }
            }
        }
    }
}
