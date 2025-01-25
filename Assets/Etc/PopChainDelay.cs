using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class PopChainDelay : CustomYieldInstruction
{
    private float StartTime { get; set; }
    private bool Impact { get; }
    private const float IMPACT_TIME = 0.25f;
    private const float CHAIN_TIME = 0.1f;

    public override bool keepWaiting
    {
        get
        {
            if (StartTime == 0)
                StartTime = Time.time;

            return Time.time < StartTime + (Impact ? IMPACT_TIME : CHAIN_TIME);
        }
    }


    public PopChainDelay(bool impact)
    {
        Impact = impact;
    }
}
