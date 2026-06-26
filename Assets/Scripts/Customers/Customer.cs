using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public enum CustomerEmotion
{
    Normal,
    Confuse,
    Disappoint,
    Angry
}

[System.Serializable]
public class CustomerVisual
{
    [SerializeField] private CustomerSO customerData;
    
    [SerializeField] private SpriteRenderer emotionSprite;
    [SerializeField] private SpriteRenderer hairSprite;
    [SerializeField] private SpriteRenderer faceAccessorySprite;
    [SerializeField] private SpriteRenderer faceAccessoryExtraSprite;
    [SerializeField] private SpriteRenderer eyebrowsSprite;
    [SerializeField] private SpriteRenderer eyeSprite;
    [SerializeField] private SpriteRenderer headSprite;
    [SerializeField] private SpriteRenderer mouthSprite;
    [SerializeField] private SpriteRenderer clothSprite;
    [SerializeField] private SpriteRenderer backHairSprite;

    public void SetEmotion(CustomerEmotion emotion)
    {
        switch (emotion)
        {
            case CustomerEmotion.Normal:
                break;
            case CustomerEmotion.Confuse:
                break;
            case CustomerEmotion.Disappoint:
                break;
            case CustomerEmotion.Angry:
                break;
        }
        if(emotion != CustomerEmotion.Normal)
            emotionSprite.gameObject.SetActive(true);
    }
    
    public void SetCustomerVisual()
    {
        if (customerData != null)
        {
            int hairIndex = Random.Range(0, 1);
            
            emotionSprite.gameObject.SetActive(false);
            hairSprite.sprite = hairIndex == 0 ? GetRandom(customerData.BlackHair): GetRandom(customerData.BrownHair);
            faceAccessorySprite.sprite = GetRandom(customerData.HeadAccessories);
            faceAccessoryExtraSprite.sprite = GetRandom(customerData.ExtraHeadAccessories);
            eyebrowsSprite.sprite = GetRandom(customerData.Eyebrows);
            eyeSprite.sprite = GetRandom(customerData.Eyes);
            headSprite.sprite = GetRandom(customerData.HeadType);
            mouthSprite.sprite = GetRandom(customerData.Mouth);
            clothSprite.sprite = GetRandom(customerData.Cloth);
            backHairSprite.sprite = hairIndex == 0 ? GetRandom(customerData.BlackBackHair): GetRandom(customerData.BrownBackHair);
        }
        
    }

    private Sprite GetRandom(Sprite[] sprites)
    {
        if(sprites.Length == 0) return null;
        return sprites[Random.Range(0, sprites.Length)];
    }
}
public class Customer : InteractableObject
{
    [SerializeField] private CustomerVisual customerVisual;
    [SerializeField] private FoodSO chosenFood;
    [SerializeField] private float waitTime;
    Coroutine _orderCoroutine;

    private bool _startOrder;
    private bool _isOutOfTime;

    public void SetOrder(float time, FoodSO food)
    {
        waitTime = time;
        chosenFood = food;
    }

    private void OnEnable()
    {
        transform.localScale = Vector3.one;
        customerVisual.SetEmotion(CustomerEmotion.Normal);
        customerVisual.SetCustomerVisual();
        
        _orderCoroutine = StartCoroutine(OnStartOrdering());
    }


    private void OnDisable()
    {
        _startOrder = false;
        _isOutOfTime = false;
        if (_orderCoroutine != null)
        {
            StopCoroutine(_orderCoroutine);
        }
    }
    
    private IEnumerator OnStartOrdering()
    {
        _startOrder = true;
        yield return null;
        _orderCoroutine = null;
    }

    public void Update()
    {
        switch (GameManager.Instance.State)
        {
            case GameState.Playing:
                
                if(_startOrder)
                    waitTime -= Time.deltaTime;
                
                if (waitTime <= 0)
                {
                    _isOutOfTime = true;
                    StartCoroutine(EndOrder());
                }
                break;
        }
        
        
    }

    private IEnumerator EndOrder()
    {
        float animTimer = 0;
        
        Vector3 startScale = transform.localScale;
        Vector3 endScale = new Vector3(1.2f, 1.2f, 1.2f);
        while (animTimer < 0.2f)
        {
            animTimer += Time.deltaTime;
            float elapsedTime = animTimer/0.2f;
            
            transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime);
            yield return null;
        }
        
        transform.localScale = endScale;
        
        startScale = transform.localScale;
        endScale = Vector3.zero;
        animTimer = 0;
        
        while (animTimer < 0.3f)
        {
            animTimer += Time.deltaTime;
            float elapsedTime = animTimer/0.3f;
            
            transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime);
            yield return null;
        }
        
        transform.localScale = endScale;
        gameObject.SetActive(false);
        yield return null;
    }

    private void OnServedOrder(FoodSO food)
    {
        gameObject.SetActive(false);
    }

    public override void Interact()
    {
        if(_isOutOfTime) return;
        
        if (!PlayerManager.Instance.GetPlayerHandState()) return;
        
        FoodBase food = PlayerManager.Instance.GetFoodFromPlayer();

        if (food != null)
        {
            PlayerManager.Instance.ClearFoodFromPlayer();
            OnServedOrder(food.FoodData);
        }
    }

    /*[Header("Fields")]
    [SerializeField] private FoodSO[] foods;
    [SerializeField] private float waitingTimeInterval;
    [SerializeField] private float itemPickTime = 0.8f;
    [SerializeField] private float minWaitingTime;
    [Header("Refrences")]
    [SerializeField] private MeshRenderer foodDialogue;
    [SerializeField] private Material UpArrowMat;
    [SerializeField] private Material DownArrowMat;

    internal CustomerQueue customerQueue;
    internal float WaitingTimeInterval
    {
        get => waitingTimeInterval;
        set => waitingTimeInterval = Mathf.Max(this.minWaitingTime, value);
    }
    internal bool canBeUsed = false;
    private float waitingTime = default;
    private FoodSO chosedFood;

    private void OnEnable()
    {
        if (!this.canBeUsed) return;
        this.waitingTime = this.WaitingTimeInterval;
        Invoke(nameof(ChooseFood), itemPickTime);
        Invoke(nameof(WaitingTimeEnded), waitingTime + itemPickTime);
    }
    private void OnDisable()
    {
        foodDialogue.enabled = false;
        foodDialogue.material = null;
    }
    private void FoodServed()
    {
        customerQueue.RemoveCustomer(this.gameObject);
    }
    private void WaitingTimeEnded()
    {
        foodDialogue.material = DownArrowMat;
        Debug.Log("Customer angy and left noob");
        Invoke(nameof(LeaveShop), 1f);
    }
    private void ChooseFood()
    {
        foodDialogue.enabled = true;
        chosedFood = foods[Random.Range(0, foods.Length)];
        //foodDialogue.material = chosedFood.FoodSprite;
        Debug.Log($"Customer wants {chosedFood.FoodName}");
    }
    private void LeaveShop()
    {
        customerQueue.RemoveCustomer(this.gameObject);
    }
    private void Update()
    {
        Debug.Log(WaitingTimeInterval);
    }*/
}
