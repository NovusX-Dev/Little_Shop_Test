using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private static CoinManager _instance;
    public static CoinManager Instance 
    {
        get 
        { 
        if( _instance == null )
        {
            Debug.LogError("Coin Manager is null!!!");
        }
        return _instance;
        }
    }

    private int _coins;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        UIManager.Instance.UpdateCoinsUI(_coins);
    }

    void Update()
    {
        #if UNITY_EDITOR

        if(Input.GetKeyDown(KeyCode.K))
        {
            AddCoins(50);
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            DeductCoins(50);
        }

        #endif
    }

    public void AddCoins(int amount)
    {
        _coins += amount;

        UIManager.Instance.UpdateCoinsUI(_coins);
    }

    public void DeductCoins(int amount)
    {
        _coins -= amount;

        if(_coins < 1)
        {
            _coins = 0;
        }
        UIManager.Instance.UpdateCoinsUI(_coins);
    }
}
