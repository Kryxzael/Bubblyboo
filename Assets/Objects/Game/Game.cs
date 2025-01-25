
using System;
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
    public DamageNumber DamageNumberPrefab;

    private void Awake()
    {
        Instance = this;
        BubbleWrap = FindObjectOfType<BubbleWrap>();
        Shop = FindObjectOfType<Shop>();
        GhostSprite = FindObjectOfType<FollowCursor>(includeInactive: true).GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        ExcitementLevel = Mathf.Max(0f, ExcitementLevel - ExcitementDecay);
    }

    public Turn BeginTurn()
    {
        Turns--;
        CurrentTurn = new Turn();
        return CurrentTurn;
    }

    public void EndTurn(Turn turn)
    {
        //TODO: Straight up not working right
        if (turn.AllowRetriggers)
        {
            turn.AllowRetriggers = false;

            for (int i = 0; i < turn.Retriggers; i++)
            {
                foreach (Bubble bubble in turn.Popped)
                {
                    bubble.Pop(turn);
                }
            }
        }

        if (Points >= Shop.NextRestock)
        {
            Shop.Restock();
        }
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
}
