using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class BasicBubble : Bubble
{
    public int Points = 1;
    public int Retriggers = 0;
    public int Turns = 0;
    public int Money = 0;

    protected override IEnumerator OnPop(Turn turn)
    {
        int pnts = (int)(Points * turn.Multiplier);

        if (Points != 0)
        {
            yield return new PopChainDelay(true);
            Game.Instance.CreateDamageNumber("+" + pnts, Color.gray, 1f, this);
        }

        if (Retriggers != 0 && !turn.InRetrigger)
        {
            yield return new PopChainDelay(true);
            Game.Instance.CreateDamageNumber("Re-pop!", Color.green, 1.5f, this);
            turn.Retriggers += Retriggers;
        }

        if (Turns != 0)
        {
            yield return new PopChainDelay(true);
            string turnsText = Turns > 1 ? " turns" : " turn";

            Game.Instance.CreateDamageNumber("+" + Turns + turnsText, Color.blue, 1.5f, this);
            Game.Instance.Turns += Turns;
        }

        if (Money != 0)
        {
            yield return new PopChainDelay(true);

            Game.Instance.CreateDamageNumber("$" + Money, Color.yellow, 1.5f, this);
            Game.Instance.Money += Money;

            AudioSource goldSound = transform.Find("GoldSound").GetComponent<AudioSource>();
            goldSound.pitch = UnityEngine.Random.Range(0.5f, 0.7f);
        }

        Game.Instance.Points += pnts;
        Game.Instance.ExcitementLevel = Mathf.Min(1f, Game.Instance.ExcitementLevel + Game.Instance.ExcitementIncreasePerPoint * pnts);
    }
}