using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class ShopUIItem : MonoBehaviour
{
    public ShopItem Item;

    [Header("Components")]
    public TextMeshProUGUI PriceTag;
    public Image Sprite;

    public void Initialize()
    {
        Sprite.sprite = Item.GetGhostSprite();
        Sprite.color = Item.GetGhostSpriteColor();

        PriceTag.text = "$" + Item.Price;
    }

    public void Click()
    {
        if (Game.Instance.Money >= Item.Price)
        {
            Game.Instance.Shop.Purchase(Item);
            GetComponentInParent<ShopUIBelt>().RegenerateItems();
        }
    }
}
