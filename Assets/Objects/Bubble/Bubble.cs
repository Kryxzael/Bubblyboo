
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bubble : MonoBehaviour
{
    private SpriteRenderer _sprite;
    private Vector3 _cursorLastPosition;
    private Vector3 _originalPosition;
    private AudioSource _popAudio;

    [NonSerialized]
    public SpriteRenderer MoneySprite;


    public Vector2Int GridPosition { get; set; }
    public bool IsCurrentlyScoring { get; private set; }

    public bool IsPopped;

    public bool HasMoney;

    [Header("Greefiks")]
    public Sprite SpriteUnpopped;
    public Sprite SpritePopped;
    
    public float PoppingScale = 1.25f;
    public float PoppingTime = 0.15f;
    public float PopBackTime = 0.075f;

    public float DragWithCursorForce = 0.008f;
    public float DragWithCursorResistance = 10f;
    public float DragWithCursorReleaseForce = 0.03f;

    [Header("Tooltip")]
    public string Name;
    public string Description;

    private void Awake()
    {
        MoneySprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _sprite = GetComponent<SpriteRenderer>();
        _popAudio = transform.Find("PopSound").GetComponent<AudioSource>();
    }

    void Start()
    {
        _sprite.sprite = SpriteUnpopped;
        _originalPosition = transform.position;
    }

    private void OnMouseDown()
    {
        if (Game.Instance.Shop.PurchasingItem != null)
        {
            Game.Instance.Shop.PurchasingItem.OnPlacePurchase(this, GridPosition.x, GridPosition.y);
            Game.Instance.Shop.PurchasingItem = null;
            Game.Instance.GhostSprite.enabled = false;
        }
        else if (Game.Instance.Turns > 0 && !IsPopped)
        {
            Pop(Game.Instance.BeginTurn());
        }
    }

    private void OnMouseEnter()
    {
        _cursorLastPosition = Input.mousePosition;
        (Game.Instance.Tooltip.transform as RectTransform).pivot = new Vector2(0, 1);
        Game.Instance.SetTooltip(Name, Description);

        if (!IsPopped)
        {
            AudioSource rubSound = transform.Find("HoverSound").GetChild(UnityEngine.Random.Range(0, 3)).GetComponent<AudioSource>();
            rubSound.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            rubSound.Play();
        }
    }

    private void OnMouseExit()
    {
        Game.Instance.UnsetTooltip();
    }

    private void OnMouseOver()
    {
        if (IsPopped)
            return; 

        Vector3 mouseDelta = Input.mousePosition - _cursorLastPosition;
        transform.position = Vector2.Lerp(transform.position, transform.position + mouseDelta, DragWithCursorForce);

        _cursorLastPosition = Input.mousePosition;
    }

    private void Update()
    {
        transform.position = Vector2.Lerp(transform.position, _originalPosition, DragWithCursorReleaseForce);
    }

    public void Pop(Turn turn)
    {
        if (IsPopped && !turn.InRetrigger)
            return;

        _sprite.sprite = SpriteUnpopped;
        turn.Popped.Add(this);
        StartCoroutine(CoPop(turn));
    }

    public IEnumerator CoPop(Turn turn) 
    {
        turn.ProcessingLevel++;

        IsCurrentlyScoring = true;
        IsPopped = true;

        if (HasMoney)
        {
            AudioSource goldSound = transform.Find("GoldSound").GetComponent<AudioSource>();
            goldSound.pitch = UnityEngine.Random.Range(0.5f, 0.7f);
            goldSound.Play();

            int money = Game.Instance.PickUpMoney();
            Game.Instance.CreateDamageNumber("$" + money, Color.yellow, 1.1f, this);
            yield return new PopChainDelay(true);
        }

        StartCoroutine(CoPopAnimation(turn.Multiplier));
        yield return StartCoroutine(OnPop(turn));
        turn.Multiplier += 1;

        IsCurrentlyScoring = false;

        if (--turn.ProcessingLevel <= 0)
            Game.Instance.EndTurn(turn);

    }

    protected abstract IEnumerator OnPop(Turn turn);

    private IEnumerator CoPopAnimation(float multiplier)
    {
        yield return StartCoroutine(CoScaleTo(1f, PoppingScale, PoppingTime));
        _sprite.sprite = SpritePopped;

        _popAudio.pitch = 1f + 0.1f * multiplier;
        _popAudio.Play();

        foreach (Bubble i in Game.Instance.BubbleWrap.GetNeighbors(GridPosition.x, GridPosition.y, false))
        {
            if (i.IsPopped)
                continue;

            i.transform.position += (i.transform.position - transform.position) * 0.1f;
        }

        MoneySprite.enabled = false;
        yield return StartCoroutine(CoScaleTo(PoppingScale, 1f, PoppingTime));
    }

    private IEnumerator CoScaleTo(float fromScale, float toScale, float time) 
    {
        float timer = 0;
        Vector3 oldScale = Vector3.one * fromScale;
        Vector3 newScale = Vector3.one * toScale;

        while (timer < time)
        {
            transform.localScale = Vector3.Lerp(oldScale, newScale, timer / time);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.localScale = newScale;
    }
}
