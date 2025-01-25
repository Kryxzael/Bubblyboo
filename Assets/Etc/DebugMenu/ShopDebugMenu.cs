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
        ReadOnly("$" + Game.Instance.Money);

        if (Button("Gib moneeeeey!"))
            Game.Instance.Money++;

        Separator();

        int index = 0;
        foreach (ShopItem i in Game.Instance.Shop.Items.ToArray())
        {
            if (Button(i.name + " ($" + i.Price + ")"))
            {
                if (Game.Instance.Money >= i.Price)
                    Game.Instance.Shop.Purchase(i, index);
            }

            index++;
        }
    }
}
