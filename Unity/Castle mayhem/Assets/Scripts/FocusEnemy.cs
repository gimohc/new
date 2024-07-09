using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FocusEnemy : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField] Transform weapon;
    Transform target;

    void Start()
    {
        target = FindObjectOfType<EnemyMover>().transform;
    }
    void Update()
    {
        weapon.LookAt(target);
    }
}
