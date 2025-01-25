using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TurnsLeftLabel : GenericLabel
{
    protected override string GetText()
    {
        return Game.Instance.Turns.ToString();
        }
}