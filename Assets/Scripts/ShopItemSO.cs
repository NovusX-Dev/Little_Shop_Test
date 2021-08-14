using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(menuName = "Shop/Shop Item")]
public class ShopItemSO : ScriptableObject
{
    public enum Type { HeadWear, FootWear}
    public Type itemType;

    public string itemName;
    public string itemPrice;
    public Sprite centerSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
}
