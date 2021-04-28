using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothDamp : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _spd = 10.0f;

    private void LateUpdate()
    {
        Vector3 targetPosition = _target.transform.position;
        transform.position = Vector3.Lerp(transform.position, targetPosition, _spd * Time.deltaTime);
        transform.rotation = Quaternion.Euler(_target.transform.rotation.eulerAngles);
    }
}
