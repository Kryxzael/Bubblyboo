using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Bubble")]
public class ShopItemBubble : ShopItem
{
    public Bubble Bubble;

    public override string Name => Bubble.Name;
    public override string Description => Bubble.Description;

    public override void OnPlacePurchase(Bubble target, int targetX, int targetY)
    {
        Game.Instance.BubbleWrap.SpawnBubble(targetX, targetY, Bubble);
    }

    public override Sprite GetGhostSprite()
    {
        return Bubble.SpriteUnpopped;
    }

    public override Color GetGhostSpriteColor()
    {
        return Bubble.GetComponent<SpriteRenderer>().color;
    }
}
