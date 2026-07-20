using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CustomerOrder : MonoBehaviour
{
    [SerializeField] private RectTransform paper;
    [SerializeField] private Image foodImage;
    [SerializeField] private Image[] pattern;
    private Coroutine _orderCoroutine;
    private float _timer;
    private readonly float _duration = 0.2f;

    public void SetOrder(Sprite foodSprite, SpriteRenderer[] patternSprite)
    {
        foodImage.sprite = foodSprite;
        for (int i = 0; i < patternSprite.Length; i++)
        {
            pattern[i].color = patternSprite[i].color;
        }
    }

    public void OnCustomerOrder(Sprite foodSprite, SpriteRenderer[] patternSprite)
    { 
        SetOrder(foodSprite, patternSprite);
        
        _orderCoroutine = null;
        _orderCoroutine = StartCoroutine(OrderCoroutine(true));
    }

    public void OnCustomerServed()
    {
        _orderCoroutine = null;
        _orderCoroutine = StartCoroutine(OrderCoroutine(false));
    }

    private IEnumerator OrderCoroutine(bool isOrdered)
    {
        Vector2 startPosition = paper.anchoredPosition;
        Vector2 endPosition = isOrdered ? Vector2.zero : new Vector2(0, -205);

        _timer = 0;
        
        while (_timer < _duration)
        {
            _timer += Time.deltaTime;
            float elapsedTime = _timer / _duration;
            
            paper.anchoredPosition = Vector2.Lerp(startPosition, endPosition, elapsedTime);
            
            yield return null;
        }
        
        paper.anchoredPosition = endPosition;
        
        yield return null;
        _orderCoroutine = null;
    }
}
