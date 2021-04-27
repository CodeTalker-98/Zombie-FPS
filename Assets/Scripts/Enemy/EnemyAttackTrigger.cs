using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackTrigger : MonoBehaviour
{
    private EnemyAI _ai;

    private void Start()
    {
        _ai = GetComponentInParent<EnemyAI>();

        if (_ai == null)
        {
            Debug.LogError("The ENEMY AI component is NULL");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _ai.StartAttack();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _ai.StopAttack();
        }
    }
}
