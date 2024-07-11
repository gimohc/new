using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Bank : MonoBehaviour
{
    public static Bank Instance { get; private set; }

    [SerializeField] int money;
    TextMeshProUGUI label;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            label = GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddMoney(int x)
    {
        money += x;
        label.text = "$" + money.ToString();
    }
    public void ConsumeMoney(int x)
    {
        money -= x;
        label.text = "$" + money.ToString();
    }
    public int Balance()
    {
        return money;
    }
    void Start()
    {
        money = 300;
        label.text = "$" + money.ToString();
    }
}
