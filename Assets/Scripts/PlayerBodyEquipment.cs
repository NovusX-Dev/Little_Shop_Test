using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBodyEquipment : MonoBehaviour
{
    [SerializeField] Sprite _nakedEquipment;
    [SerializeField] Type _slotType;
    [SerializeField] bool _centerEqiupment, _leftEquipment, _rightEquipment;

    public enum Type { HeadWear, ChestWear, LowerChest, FootWear, Belts }
    private bool _isNaked = true;

    SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void EquipItem(ShopItemSO itemToBeEquiped)
    {
        if((int)itemToBeEquiped.itemType != (int) _slotType) return;

        if(_centerEqiupment)
        {
            _spriteRenderer.sprite = itemToBeEquiped.centerSprite;
            _isNaked = false;
        }
        else if(_leftEquipment)
        {
            _spriteRenderer.sprite = itemToBeEquiped.leftSprite;
            _isNaked = false;
        }
        else if(_rightEquipment)
        {
            _spriteRenderer.sprite = itemToBeEquiped.rightSprite;
            _isNaked = false;
        }
    }

}
