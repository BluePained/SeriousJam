using System;
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
public class Customer : MonoBehaviour
{
    [SerializeField] private FoodContainer foodChoice;
    [SerializeField] private CustomerVisual customerVisual;
    
    private void Awake()
    {
        customerVisual.SetEmotion(CustomerEmotion.Normal);
        customerVisual.SetCustomerVisual();
    }

    public void Order()
    {
        
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
