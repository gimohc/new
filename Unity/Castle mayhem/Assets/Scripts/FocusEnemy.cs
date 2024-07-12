using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FocusEnemy : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField] Transform weapon;
    [SerializeField] float range = 15f;
    [SerializeField] ParticleSystem projectileParticles;
    [SerializeField] int price = 150;

    Transform target;

    void Start()
    {
        target = FindObjectOfType<Enemy>().transform;
    }
    void Update()
    {
        FindFirstTarget();
        weapon.LookAt(target);
    }
    void FindFirstTarget()
    {
        /*Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closest = null;
        float maxDistance = Mathf.Infinity;

        foreach(Enemy enemy in enemies) {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if(targetDistance < maxDistance) {
                closest = enemy.transform;
                maxDistance = targetDistance;
            }

            target = closest;
        }*/
        var emission = projectileParticles.emission;
        if (Vector3.Distance(transform.position, target.transform.position) > range)
            emission.enabled = false;
        else
            emission.enabled = true;
        if (!target.parent.gameObject.activeInHierarchy || target == null)
            target = FindObjectOfType<Enemy>().transform;
    }
    public int GetPrice()
    {
        return price;
    }
}