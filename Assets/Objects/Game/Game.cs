
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;

    public int Points;
    public int Turns = 10;
    public int Money = 0;

    public int MinimumPickup = 1;
    public int MaxiumPickup = 5;

    [Range(0f, 1f)]
    public float ExcitementLevel = 1f;
    public float ExcitementDecay = 0.05f;
    public float ExcitementIncreasePerPoint = 0.02f;

    [Range(0f, 1f)]
    public float MoneySpawnRate = 0.05f;

    public Turn CurrentTurn;

    public BubbleWrap BubbleWrap { get; private set; }
    public Shop Shop { get; private set; }
    public SpriteRenderer GhostSprite { get; private set; }
    public Tooltip Tooltip { get; private set; }
    public DamageNumber DamageNumberPrefab;

    public int ClearAddedTurns = 20;
    public int ClearAddedMoney = 50;
    public int TurnNumber = 0;

    private void Awake()
    {
        Screen.SetResolution(560, 428, false);

        Instance = this;
        BubbleWrap = FindObjectOfType<BubbleWrap>();
        Shop = FindObjectOfType<Shop>();
        GhostSprite = FindObjectOfType<FollowCursor>(includeInactive: true).GetComponent<SpriteRenderer>();
        Tooltip = FindObjectOfType<Tooltip>();

        Tooltip.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        ExcitementLevel = Mathf.Max(0f, ExcitementLevel - ExcitementDecay);
    }

    public Turn BeginTurn()
    {
        TurnNumber++;
        Turns--;

        if (TurnNumber % Shop.RestockTurns == 0)
        {
            Shop.Restock();
        }

        CurrentTurn = new Turn();
        return CurrentTurn;
    }

    public void EndTurn(Turn turn)
    {
        turn.InRetrigger = true;

        if (turn.Retriggers > 0)
        {
            turn.Retriggers--;
            StartCoroutine(CoRetrigger(turn));
            return;
        }

        foreach (Bubble i in BubbleWrap.Grid)
        {
            if (!i.IsPopped)
                return;
        }

        StartCoroutine(CoClearSheet(turn));
    }

    private IEnumerator CoRetrigger(Turn turn)
    {
        foreach (Bubble bubble in turn.Popped)
        {
            bubble.Pop(turn);
            yield return new PopChainDelay(false);
        }
    }

    private IEnumerator CoClearSheet(Turn turn)
    {
        Bubble center = BubbleWrap.Grid[BubbleWrap.SizeX / 2, BubbleWrap.SizeY / 2];

        CreateDamageNumber("Sheet Cleared!", Color.cyan, 3f, center);
        yield return new WaitForSeconds(1f);

        CreateDamageNumber("+" + ClearAddedTurns + " turns", Color.blue, 2f, center);
        Turns += ClearAddedTurns;
        yield return new WaitForSeconds(1f);

        CreateDamageNumber("$" + ClearAddedMoney, Color.yellow, 2f, center);
        Money += ClearAddedMoney;
        yield return new WaitForSeconds(1f);


        BubbleWrap.SpawnInitialGrid();
    }

    public int PickUpMoney()
    {
        int money = UnityEngine.Random.Range(MinimumPickup, MaxiumPickup + 1);
        Money += money;
        return money;
    }

    public void CreateDamageNumber(string text, Color color, float size, Bubble atLocation)
    {
        DamageNumber spawned = Instantiate(DamageNumberPrefab, atLocation.transform.position, Quaternion.identity, null);
        spawned.Text = text;
        spawned.Color = color;
        spawned.Size = size;
    }

    public void SetTooltip(string header, string body)
    {
        Tooltip.Header.text = header;
        Tooltip.Body.text = body;
        Tooltip.Update(); //Ouch
        Invoke(nameof(ShowTooltip), 1f);
    }

    public void UnsetTooltip()
    {
        Tooltip.gameObject.SetActive(false);
        CancelInvoke(nameof(ShowTooltip));
    }

    private void ShowTooltip()
    {
        Tooltip.gameObject.SetActive(true);
    }
}
