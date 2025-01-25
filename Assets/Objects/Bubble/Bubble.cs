
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bubble : MonoBehaviour
{
    private SpriteRenderer _sprite;
    private Vector3 _cursorLastPosition;
    private Vector3 _originalPosition;

    public Vector2Int GridPosition { get; set; }
    public bool IsCurrentlyScoring { get; private set; }

    public bool IsPopped;

    [Header("Greefiks")]
    public Sprite SpriteUnpopped;
    public Sprite SpritePopped;
    
    public float PoppingScale = 1.25f;
    public float PoppingTime = 0.15f;
    public float PopBackTime = 0.075f;

    public float SpawnScale = 2f;
    public float SpawnScaleTime = 0.5f;

    public float DragWithCursorForce = 0.008f;
    public float DragWithCursorResistance = 10f;
    public float DragWithCursorReleaseForce = 0.03f;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.sprite = SpriteUnpopped;
        _originalPosition = transform.position;

        StartCoroutine(CoScaleTo(SpawnScale, 1f, SpawnScaleTime));
    }

    private void OnMouseDown()
    {
        if (Game.Instance.Shop.PurchasingItem != null)
        {
            Game.Instance.Shop.PurchasingItem.OnPlacePurchase(this, GridPosition.x, GridPosition.y);
            Game.Instance.Shop.PurchasingItem = null;
            Game.Instance.GhostSprite.enabled = false;
        }
        else if (Game.Instance.Turns > 0)
        {
            Pop(Game.Instance.BeginTurn());
        }
    }

    private void OnMouseEnter()
    {
        _cursorLastPosition = Input.mousePosition;
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
        if (IsPopped)
            return;

        StartCoroutine(CoPop(turn));
    }

    public IEnumerator CoPop(Turn turn) 
    {
        turn.ProcessingLevel++;

        IsCurrentlyScoring = true;
        IsPopped = true;
        StartCoroutine(CoPopAnimation());
        yield return StartCoroutine(OnPop(turn));

        IsCurrentlyScoring = false;

        if (--turn.ProcessingLevel <= 0)
            Game.Instance.EndTurn(turn);

    }

    protected abstract IEnumerator OnPop(Turn turn);

    private IEnumerator CoPopAnimation()
    {
        yield return StartCoroutine(CoScaleTo(1f, PoppingScale, PoppingTime));
        _sprite.sprite = SpritePopped;
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
