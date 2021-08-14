using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField] private GameObject _pressEObject;

    private bool _buttonPressed = false;

    public void ActivateButton(bool active)
    {
        _pressEObject.SetActive(active);
    }

    public bool ButtonPressed(bool pressed)
    {
       return _buttonPressed = pressed;
    }
}
