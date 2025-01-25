using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ShopDebugMenu : DebugPage
{
    public override string Header => "Shop";

    protected override void RunItems(DebugMenu caller)
    {
        foreach (ShopItem i in Game.Instance.Shop.Items.ToArray())
        {
            if (Button(i.name + " ($" + i.Price + ")"))
            {
                Game.Instance.Shop.Purchase(i);
            }
        }
    }
}
