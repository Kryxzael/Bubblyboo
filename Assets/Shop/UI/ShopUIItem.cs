using Assets.Etc;

using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopUIItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ShopItem Item;

    public int ShopIndex;

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
            Game.Instance.Shop.Purchase(Item, ShopIndex);

            ShopUIBelt belt = GetComponentInParent<ShopUIBelt>();
            belt.RegenerateItems(false);

            //A hate myself:
            belt.transform.GetChild(ShopIndex).GetComponent<ScaleIn>().ScaleNow();

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        int pivotX = 0;

        if (ShopIndex >= Game.Instance.Shop.MaxItemSlots / 2f)
            pivotX = 1;

        (Game.Instance.Tooltip.transform as RectTransform).pivot = new Vector2(pivotX, 0);
        Game.Instance.SetTooltip(Item.Name, Item.Description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Game.Instance.UnsetTooltip();
    }
}
