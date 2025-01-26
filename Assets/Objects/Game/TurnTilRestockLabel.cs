using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TurnTilRestockLabel : GenericLabel
{
    public override bool PerformSmack
    {
        get
        {
            return false;
        }
    }

    protected override string GetText()
    {
        int turnsLeft = Game.Instance.Shop.RestockTurns - (Game.Instance.TurnNumber % Game.Instance.Shop.RestockTurns);
        if (turnsLeft == 1)
            return "Restock NEXT turn";

        return "Restock in " + turnsLeft + " turns";
    }
}
