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

    public RandomValueFromSet<ShopItem> AvailableItems;
    public List<ShopItem> Items = new();

    public int[] RestockThresholds = { 0, 5, 10, 20, 50, 100, 250, 800, 2000, 6000, 10_000, 50_000, 200_000, 4_000_000, 100_000_000, 500_000_000, 999_999_999 };

    public int RestockCount;
    public int NextRestock = 0;

    private void Start()
    {
        Restock();
    }

    public void Purchase(ShopItem item, int shopIndex)
    {
        Game.Instance.Money -= item.Price;
        PurchasingItem = item;
        Items[shopIndex] = null;
        
        Game.Instance.GhostSprite.enabled = true;
        Game.Instance.GhostSprite.sprite = item.GetGhostSprite();

        Color ghostColor = item.GetGhostSpriteColor();
        Game.Instance.GhostSprite.color = new Color(ghostColor.r, ghostColor.g, ghostColor.b, 0.5f);
    }

    public void Restock()
    {
        NextRestock = RestockThresholds[RestockCount];
        RestockCount++;

        Items.Clear();

        for (int i = 0; i < MaxItemSlots; i++)
        {

            Items.Add(AvailableItems.Pick());
        }

        FindObjectOfType<ShopUIBelt>().RegenerateItems(true);
    }
}
