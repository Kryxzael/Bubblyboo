using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TurnsLeftLabel : GenericLabel
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
        return Game.Instance.Turns.ToString();
    }
}