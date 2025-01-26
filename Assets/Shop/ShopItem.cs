using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public abstract class ShopItem : ScriptableObject
{
    public int Price;

    public abstract string Name { get; }
    public abstract string Description { get; }

    public abstract void OnPlacePurchase(Bubble target, int targetX, int targetY);

    public abstract Sprite GetGhostSprite();

    public virtual Color GetGhostSpriteColor()
    {
        return Color.white;
    }
}
