using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TMPro;

using UnityEngine;

public class MoneyLabel : GenericLabel
{
    protected override string GetText()
    {
        return "$" + Game.Instance.Money;
    }
}