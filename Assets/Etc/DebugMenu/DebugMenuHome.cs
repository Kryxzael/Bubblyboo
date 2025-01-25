using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DebugMenuHome : DebugPage
{
    public override string Header => "Bubblyboo";

    protected override void RunItems(DebugMenu caller)
    {
        ReadOnly("Points: " + Game.Instance.Points);
        ReadOnly("Turns: " + Game.Instance.Turns);
        Separator();

        if (Button("Shop"))
            UnityEngine.Object.FindObjectOfType<DebugMenu>().NavigationStack.Push(new ShopDebugMenu());

        Separator();

        if (Game.Instance.CurrentTurn != null)
        {
            ReadOnly("+" + Game.Instance.CurrentTurn.Points + " x " + Game.Instance.CurrentTurn.Multiplier);
        }
    }
}