using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class PopChainDelay : CustomYieldInstruction
{
    private float StartTime;
    private const float WAIT_TIME = 0.25f;

    public override bool keepWaiting
    {
        get
        {
            if (StartTime == 0)
                StartTime = Time.time;

            return Time.time < StartTime + WAIT_TIME;
        }
    }
}
