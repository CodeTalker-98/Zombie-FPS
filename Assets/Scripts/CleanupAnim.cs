using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanupAnim : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject, 1.05f);
    }
}
