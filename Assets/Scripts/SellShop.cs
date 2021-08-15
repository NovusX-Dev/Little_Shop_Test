using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SellShop : MonoBehaviour
{
    public delegate void ActionClick();
    public static event ActionClick OnCloseSellShop;


    [Header("Sell Shop References")]
    [SerializeField] GameObject _itemPrefab;
    [SerializeField] Transform _sellItemsContainer;
    [SerializeField] TextMeshProUGUI _sellingNameText, _sellingPriceText;

    private int _currentItemSellingPrice;
    List<PlayerBodyEquipment> _equipedBodyEquipmentsList = new List<PlayerBodyEquipment>();
    ShopItemSO _itemToBeSold = null;
    GameObject _itemTobeDestroyedAfterSale = null;
    PlayerBodyEquipment _selectedEquipmentToBesold = null;


    private void OnEnable()
    {
        PopulateSellShop();
    }

    private void Update()
    {
        if(_equipedBodyEquipmentsList.Count == 0)
        {
            _sellingNameText.text = $"Select Item";
            _sellingPriceText.text = $"000 Coins";
        }
    }

    private void PopulateSellShop()
    {
        var equipedBodyEquipmentsArray = FindObjectsOfType<PlayerBodyEquipment>();

        Debug.Log(equipedBodyEquipmentsArray.Length);

        for (int i = 0; i < equipedBodyEquipmentsArray.Length; i++)
        {
            var currentEquipedObject = equipedBodyEquipmentsArray[i];
            var currentSO = currentEquipedObject.GetCurrentItem();
            
            if (currentEquipedObject.IsNaked()) continue;

            GameObject itemObejct = Instantiate(_itemPrefab, _sellItemsContainer);

            //Change prefab components based on current item
            // 1 Name
            itemObejct.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = currentSO.itemName;
            // 2 Price
            itemObejct.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"{currentSO.itemPrice} Coins";
            // 3 Image
            if (currentSO.centerSprite != null)
            {
                itemObejct.transform.GetChild(3).GetComponent<Image>().sprite = currentSO.centerSprite;
            }
            else if (currentSO.leftSprite != null)
            {
                itemObejct.transform.GetChild(3).GetComponent<Image>().sprite = currentSO.leftSprite;
            }
            //button
            itemObejct.GetComponent<Button>().onClick.AddListener(() => OnSellClick(currentSO, itemObejct, currentEquipedObject));

            _equipedBodyEquipmentsList.Add(currentEquipedObject);
        }
    }

    private void OnSellClick(ShopItemSO item, GameObject obj, PlayerBodyEquipment equipment)
    {
        _currentItemSellingPrice = item.itemPrice/2;

        _sellingNameText.text = item.itemName;
        _sellingPriceText.text = $"{_currentItemSellingPrice} Coins";
        _itemTobeDestroyedAfterSale = obj;
        _itemToBeSold = item;
        _selectedEquipmentToBesold = equipment;
    }

    public void SellItem()
    {
        if(_itemToBeSold == null) return;

        CoinManager.Instance.AddCoins(_currentItemSellingPrice);
        _itemToBeSold = null;
        Destroy(_itemTobeDestroyedAfterSale);
        _itemTobeDestroyedAfterSale = null;
        _equipedBodyEquipmentsList.Remove(_selectedEquipmentToBesold);
        _selectedEquipmentToBesold.UnEquipItem();
    }

    public void CloseShop()
    {
        gameObject.SetActive(false);

        OnCloseSellShop?.Invoke();
    }
}
