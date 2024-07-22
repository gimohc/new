using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float time = 2f;
    void Start()
    {
        Build();
    }

    public void Build()
    {
        StartCoroutine(SetActive(false));
        StartCoroutine(SetActive(true));
    }
    private IEnumerator SetActive(bool x)
    {
        float tempTime = time / gameObject.GetComponentsInChildren<Transform>().Count();
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(x);
            if (x) yield return new WaitForSeconds(time);
        }
    }
}
