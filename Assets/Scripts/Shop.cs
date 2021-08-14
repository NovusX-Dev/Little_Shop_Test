using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [Header("List of items in the shop")]
    [SerializeField] ShopItemSO[] shopItem;
    
    [Header("References")]
    [SerializeField] Transform _itemsContainer;
    [SerializeField] GameObject _itemPrefab;

    List<GameObject> _currentItemsList = null;

    void Start()
    {
        PopulateCategory("HeadWear");
    }


    public void PopulateCategory(string itemType)
    {
        if (_currentItemsList != null)
        {
            foreach (var currentItem in _currentItemsList)
            {
                Destroy(currentItem);
            }
        }

        List<GameObject> newtItemsList = new List<GameObject>();

        for (int i = 0; i < shopItem.Length; i++)
        {
            var currentItem = shopItem[i];

            //populate items
            if (shopItem[i].itemType.ToString() == itemType)
            {
                GameObject itemObejct = Instantiate(_itemPrefab, _itemsContainer);
                newtItemsList.Add(itemObejct);

                //Change prefab components based on current item

                // 0 Name
                itemObejct.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentItem.itemName;
                // 1 Image
                if (currentItem.centerSprite != null)
                {
                    itemObejct.transform.GetChild(1).GetComponent<Image>().sprite = currentItem.centerSprite;
                }
                else if (currentItem.leftSprite != null)
                {
                   itemObejct.transform.GetChild(1).GetComponent<Image>().sprite = currentItem.leftSprite;
                }
                // 2 Price
                itemObejct.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"{currentItem.itemPrice} Coins";

                //Add listner to the button
                itemObejct.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(currentItem));
            }
        }

        _currentItemsList = newtItemsList;
    }

    private void OnButtonClick(ShopItemSO item)
    {
        if (CoinManager.Instance.GetCoinAmount() >= item.itemPrice)
        {
            CoinManager.Instance.DeductCoins(item.itemPrice);
            Debug.Log("Item Bought: " + item.itemName);
        }
        else
        {
            Debug.Log("Not enough coins.");
        }
    }
}
