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

    [SerializeField] List<Transform> enemies;
    Transform target;

    void Start()
    {

        Transform pool = FindObjectOfType<ObjectPool>().gameObject.transform;
        foreach (Transform child in pool)
        {
            enemies.Add(child);
        }
        target = enemies[0];
        StartCoroutine(FindTarget());

    }
    void Update()
    {

        /*var emission = projectileParticles.emission;
        if (Vector3.Distance(transform.position, target.position) > range)
        {
            emission.enabled = false;
            StartCoroutine(IterateEnemies());
        }
        else if (!target.gameObject.activeInHierarchy)
        {
            emission.enabled = false;
            IterateEnemies();
        }
        else emission.enabled = true;

        */
        var emission = projectileParticles.emission;
        if (Vector3.Distance(transform.position, target.position) > range)
        {
            emission.enabled = false;
        }
        else
            emission.enabled = true;
        weapon.LookAt(target);
    }

    /*private IEnumerator IterateEnemies()
    {
        float minDistance = Vector3.Distance(transform.position, enemies[0].position);
        for (int i = 1; i < enemies.Count; i++)
        {
            float currentDistance = Vector3.Distance(transform.position, enemies[i].position);
            if (currentDistance < minDistance)
                target = enemies[i];


        }
        yield return new WaitForSeconds(0.25f);
    }*/

    private void GetTarget()
    {
        target = enemies[0];
        float minDistance = Vector3.Distance(transform.position, enemies[0].position);
        foreach (Transform enemy in enemies)
        {
            float currentDistance = Vector3.Distance(transform.position, enemy.position);
            if (currentDistance < minDistance)
            {
                target = enemy;
                minDistance = currentDistance;

            }
        }

    }
    private IEnumerator FindTarget()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, target.position) > range || !target.gameObject.activeInHierarchy)
            {
                GetTarget();
            }


            yield return new WaitForSeconds(0.5f);
        }
    }
    public int GetPrice()
    {
        return price;
    }
}
