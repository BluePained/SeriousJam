using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                emotionSprite.sprite = customerData.DisappointIcon[0];
                eyebrowsSprite.sprite = GetRandom(customerData.PresetEyebrowsSad);
                mouthSprite.sprite = GetRandom(customerData.PresetMouthSad);
                break;
            case CustomerEmotion.Disappoint:
                emotionSprite.sprite = customerData.DisappointIcon[1];
                eyebrowsSprite.sprite = GetRandom(customerData.PresetEyebrowsIrritate);
                mouthSprite.sprite = GetRandom(customerData.PresetMouthIrritate);
                break;
            case CustomerEmotion.Angry:
                emotionSprite.sprite = customerData.AngryIcon[0];
                eyebrowsSprite.sprite = GetRandom(customerData.PresetEyebrowsAngry);
                mouthSprite.sprite = GetRandom(customerData.PresetMouthAngry);
                break;
        }

        if (emotion != CustomerEmotion.Normal)
        {
            emotionSprite.gameObject.SetActive(true);
        }
        else
        {
            emotionSprite.gameObject.SetActive(false);
        }
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
    [SerializeField] private FoodContainer allFoodData;
    [SerializeField] private List<FoodPattern> chosenPattern = new List<FoodPattern>();
    [SerializeField] private CustomerMeter customerMeter;
    [SerializeField] private float waitTime;
    [SerializeField] private Collider coll;
    [SerializeField] private float popupTime = 0.3f;
    [SerializeField] private float customerThoughtTime = 0.3f;
    
    [Header("Dialogue")]
    [SerializeField] private FoodStateColorSO foodStateColorSo;
    [SerializeField] private Transform dialogueBox;
    [SerializeField] private SpriteRenderer dialogueBoxRenderer;
    [SerializeField] private SpriteRenderer foodRenderer;
    [SerializeField] private SpriteRenderer[] patternRenderer;
    private static readonly Color WhiteAlpha = new Color(1, 1, 1, 0);
    
    private float _maxWaitTime;
    Coroutine _orderCoroutine;

    private bool _startOrder;
    private bool _isOutOfTime;

   public void SetOrder(float time, FoodSO food)
    {
        coll.enabled = false;
        transform.localScale = Vector3.zero;
        dialogueBox.localScale = Vector3.zero;
        dialogueBox.localRotation = Quaternion.Euler(0, 0, 90);
        dialogueBoxRenderer.color = WhiteAlpha;
        customerVisual.SetEmotion(CustomerEmotion.Normal);
        customerVisual.SetCustomerVisual();
        
        waitTime = time;
        _maxWaitTime = time;
        chosenFood = food;

        chosenPattern.Clear();
        
        switch (chosenFood.FoodName)
        {
            case "Chicken Skewer":
                
                int chickenPatternIndex = Random.Range(0, allFoodData.ChickenPattern.Length);
                
                var tempPatternChicken = allFoodData.ChickenPattern[chickenPatternIndex].Patterns;
                foreach (var t in tempPatternChicken)
                {
                    chosenPattern.Add(t);
                }
                
                break;
            case "Fish Skewer":
                
                int fishPatternIndex = Random.Range(0, allFoodData.FishPattern.Length);
                
                var tempPatternFish = allFoodData.FishPattern[fishPatternIndex].Patterns;
                foreach (var t in tempPatternFish)
                {
                    chosenPattern.Add(t);
                }
                
                break;
            case "Meat Skewer":
                
                int  meatPatternIndex = Random.Range(0, allFoodData.MeatPattern.Length);
                
                var meatPattern = allFoodData.MeatPattern[meatPatternIndex].Patterns;
                foreach (var t in meatPattern)
                {
                    chosenPattern.Add(t);
                }
                break;
            case "Mushroom Skewer":
                
                int mushroomPatternIndex = Random.Range(0, allFoodData.MushroomPattern.Length);
                
                var mushroomPattern = allFoodData.MushroomPattern[mushroomPatternIndex].Patterns;
                foreach (var t in mushroomPattern)
                {
                    chosenPattern.Add(t);
                }
                
                break;
            case "Corn Skewer":
                
                int cornPatternIndex = Random.Range(0, allFoodData.CornPattern.Length);
                
                var cornPattern = allFoodData.CornPattern[cornPatternIndex].Patterns;
                foreach (var t in cornPattern)
                {
                    chosenPattern.Add(t);
                }
                
                break;
            default:
                print($"Wtf is {chosenFood.FoodName}?");
                break;
        }
        
        SetPattern(chosenFood, chosenPattern); //Set all the pattern
        _orderCoroutine = StartCoroutine(OrderCoroutine());
    }

    private IEnumerator OrderCoroutine()
    {
        float timer = 0;

        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.one;
        print($"Start Scale {startScale} and EndScale {endScale}");

        while (timer <= popupTime)
        {
            timer += Time.deltaTime;
            float elapsedTime = timer / popupTime;
            
            transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime);
            yield return null;
        }
        
        transform.localScale = endScale;
        print($"Transform {transform.localScale}");
        yield return new WaitForSeconds(customerThoughtTime);
        //Call dialogue

        timer = 0;
        
        Vector3 startScaleDialogue = dialogueBox.localScale;
        Vector3 endScaleDialogue = Vector3.one;
        
        Quaternion startRotation = dialogueBox.localRotation;
        Quaternion endRot = Quaternion.Euler(0,0,0);
        
        Color startColor = dialogueBoxRenderer.color;
        Color endColor = Color.white;

        while (timer <= popupTime)
        {
            timer += Time.deltaTime;
            float elapsed = timer / popupTime;
            dialogueBox.localScale =  Vector3.Lerp(startScaleDialogue, endScaleDialogue, elapsed);
            dialogueBox.localRotation = Quaternion.Slerp(startRotation, endRot, elapsed);
            dialogueBoxRenderer.color = Color.Lerp(startColor, endColor, elapsed);
            
            yield return null;
        }
        
        dialogueBox.localScale = endScaleDialogue;
        dialogueBox.localRotation = endRot;
        dialogueBoxRenderer.color = endColor;
        
        yield return new WaitForSeconds(customerThoughtTime);
        customerMeter.Activate();
        coll.enabled = true;
        _startOrder = true;
        yield return null;
        _orderCoroutine = null;
    }
    
    public void SetPattern(FoodSO food ,List<FoodPattern> pattern)
    {
        foodRenderer.sprite = food.FoodSprite;

        if (pattern.Count == 2)
        {
            patternRenderer[0].color = pattern[0].Cookedness switch
            {
                Cookedness.Raw => foodStateColorSo.Raw,
                Cookedness.Undercooked => foodStateColorSo.Undercooked,
                Cookedness.Cooked => foodStateColorSo.Cooked,
                Cookedness.Burnt => foodStateColorSo.Burnt,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            patternRenderer[1].color = foodStateColorSo.Disable;
            patternRenderer[2].color = foodStateColorSo.Disable;
            
            patternRenderer[3].color = pattern[1].Cookedness switch
            {
                Cookedness.Raw => foodStateColorSo.Raw,
                Cookedness.Undercooked => foodStateColorSo.Undercooked,
                Cookedness.Cooked => foodStateColorSo.Cooked,
                Cookedness.Burnt => foodStateColorSo.Burnt,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        else
        {
            for (int i = 0; i < pattern.Count; i++)
            {
                patternRenderer[i].color = pattern[i].Cookedness switch
                {
                    Cookedness.Raw => foodStateColorSo.Raw,
                    Cookedness.Undercooked => foodStateColorSo.Undercooked,
                    Cookedness.Cooked => foodStateColorSo.Cooked,
                    Cookedness.Burnt => foodStateColorSo.Burnt,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }
    }
    
    private void OnDisable()
    {
        Debug.Log($"{name} Disabled");
        
        _startOrder = false;
        _isOutOfTime = false;
        if (_orderCoroutine != null)
        {
            StopCoroutine(_orderCoroutine);
        }
    }

    public void Update()
    {
        switch (GameManager.Instance.State)
        {
            case GameState.Playing:

                if (_startOrder)
                {
                    waitTime -= Time.deltaTime;
                
                    float ratio = waitTime / _maxWaitTime;
                    customerMeter.SetMeter(ratio);
                
                    if (waitTime <= 0)
                    {
                        print("wait time reached");
                        _startOrder = false;
                        StartCoroutine(EndOrder(CustomerEmotion.Disappoint));
                    }
                }
                break;
        }
        
    }
    
    private void OnEnable()
    {
        Debug.Log($"{name} Enabled");
    }

    private IEnumerator EndOrder(CustomerEmotion emotion)
    {
        customerVisual.SetEmotion(emotion);
        float timer = 0;
        
        Vector3 startScaleDialogue = dialogueBox.localScale;
        Vector3 endScaleDialogue = Vector3.zero;
        
        Quaternion startRotation = dialogueBox.localRotation;
        Quaternion endRot = Quaternion.Euler(0,0,90);
        
        Color startColor = dialogueBoxRenderer.color;
        Color endColor = WhiteAlpha;

        while (timer <= popupTime)
        {
            timer += Time.deltaTime;
            float elapsed = timer / popupTime;
            dialogueBox.localScale =  Vector3.Lerp(startScaleDialogue, endScaleDialogue, elapsed);
            dialogueBox.localRotation = Quaternion.Slerp(startRotation, endRot, elapsed);
            dialogueBoxRenderer.color = Color.Lerp(startColor, endColor, elapsed);
            
            yield return null;
        }
        
        dialogueBox.localScale = endScaleDialogue;
        dialogueBox.localRotation = endRot;
        dialogueBoxRenderer.color = endColor;

        yield return new WaitForSeconds(popupTime);
        
        timer = 0;
        
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;

        while (timer <= popupTime)
        {
            timer += Time.deltaTime;
            float elapsedTime = timer / popupTime;
            
            transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime);
            yield return null;
        }
        
        transform.localScale = endScale;
        
        yield return null;
        gameObject.SetActive(false);
    }
    
    private bool MatchesPatternThreeState(FoodSideThreeState[] sides, List<FoodPattern> patterns)
    {
        if (sides.Length != patterns.Count)
            return false;

        for (int i = 0; i < sides.Length; i++)
        {
            if (sides[i].Side != patterns[i].FoodSide ||
                sides[i].Cookedness != patterns[i].Cookedness)
            {
                return false;
            }
        }

        return true;
    }
    
    private bool MatchesPatternFourState(FoodSideDefault[] sides, List<FoodPattern> patterns)
    {
        if (sides.Length != patterns.Count)
            return false;

        for (int i = 0; i < sides.Length; i++)
        {
            if (sides[i].Side != patterns[i].FoodSide ||
                sides[i].Cookedness != patterns[i].Cookedness)
            {
                return false;
            }
        }

        return true;
    }
    
    private bool CanBeServed(FoodBase food)
    {
        switch (food.FoodData.FoodName)
        {
            case "Chicken Skewer":
            case "Fish Skewer":
                if (food is FoodFourState fours)
                {
                    return fours.FoodSide.Any(s => s.Cookedness != Cookedness.Raw || s.Cookedness != Cookedness.Undercooked);
                }
                break;

            case "Meat Skewer":
                if (food is FoodFourState four)
                {
                    return four.FoodSide.Any(s => s.Cookedness != Cookedness.Raw);
                }
                break;
        }

        return true;
    }

    private void OnServedOrder(FoodBase food)
    {
        FoodThreeState foodThreeState = food as FoodThreeState;
        FoodFourState foodFourState = food as FoodFourState;
        
        if (food.FoodData != chosenFood) //if served food isn't the ordered food or Raw/UnderCooked
        {
            Penalty(20);
            StartCoroutine(EndOrder(CustomerEmotion.Angry));
        }
        else if (!CanBeServed(food))
        {
            Penalty(20);
            StartCoroutine(EndOrder(CustomerEmotion.Confuse));
        }
        else
        {
            bool isCorrect = foodThreeState != null
                ? MatchesPatternThreeState(foodThreeState.FoodSide, chosenPattern)
                : MatchesPatternFourState(foodFourState.FoodSide, chosenPattern);

            if (isCorrect)
            {
                Reward(20,ScoreType.perfect);
                StartCoroutine(EndOrder(CustomerEmotion.Normal));
            }
            else
            {
                Reward(10,ScoreType.wrongCookedness); //lower
                StartCoroutine(EndOrder(CustomerEmotion.Disappoint));
            }
        }
    }

    private void Reward(int score, ScoreType scoreType)
    {
        print("reward");
        GameManager.Instance.ScoreManager.AddScore(score, scoreType);
    }

    private void Penalty(int score)
    {
        print("penalty");
        GameManager.Instance.ScoreManager.DecreaseScore(score);
    }

    public override void Interact(RaycastHit hit)
    {
        if(_isOutOfTime) return;
        if (!PlayerManager.Instance.GetPlayerHandState()) return;
        
        FoodBase food = PlayerManager.Instance.GetFoodFromPlayer();

        if (food != null)
        {
            GameObject obj = PlayerManager.Instance.GetFoodFromPlayer().gameObject;
            PlayerManager.Instance.ClearFoodFromPlayer();
            Destroy(obj);
            _startOrder = false;
            OnServedOrder(food);
        }
    }
    
}
