using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class Shop : MonoBehaviour
{
    public ShopItem PurchasingItem;

    public int MaxItemSlots = 3;
    public List<ShopItem> Items;

    public void Purchase(ShopItem item)
    {
        Game.Instance.Money -= item.Price;
        PurchasingItem = item;
        Items.Remove(item);
        
        Game.Instance.GhostSprite.enabled = true;
        Game.Instance.GhostSprite.sprite = item.GetGhostSprite();

        Color ghostColor = item.GetGhostSpriteColor();
        Game.Instance.GhostSprite.color = new Color(ghostColor.r, ghostColor.g, ghostColor.b, 0.5f);
    }
}
