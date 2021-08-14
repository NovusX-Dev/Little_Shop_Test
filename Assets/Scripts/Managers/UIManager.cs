using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UI Manager is NULL!!");
            }
            return _instance;
        }
    }

    [SerializeField] GameObject _shopPanel;
    [SerializeField] TextMeshProUGUI _coins;

    void Awake()
    {
        _instance = this;
    }

    public void UpdateCoinsUI(int amount)
    {
        _coins.text = $"Coins: {amount}";
    }
}
