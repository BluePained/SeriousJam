using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDialogueManager : MonoBehaviour
{
    [SerializeField] private FoodStateColorSO foodStateColorSo;
    [SerializeField] private Transform dialogueBox;
    [SerializeField] private SpriteRenderer dialogueBoxRenderer;
    [SerializeField] private SpriteRenderer foodRenderer;
    [SerializeField] private SpriteRenderer[] patternRenderer;
    private static readonly Color WhiteAlpha = new Color(1, 1, 1, 0);

    public void EndDialogue()
    {
        dialogueBox.localScale = Vector3.one;
        dialogueBox.localRotation = Quaternion.Euler(0, 0, 0);
        dialogueBoxRenderer.color = Color.white;

        StartCoroutine(DialogueClose());
    }

    private IEnumerator DialogueClose()
    {
        float timer = 0;
        
        Vector3 startScale = dialogueBox.localScale;
        Vector3 endScale = Vector3.zero;
        
        Quaternion startRotation = dialogueBox.localRotation;
        Quaternion endRot = Quaternion.Euler(0,0,90);
        
        Color startColor = dialogueBoxRenderer.color;
        Color endColor = WhiteAlpha;

        while (timer <= 0.2f)
        {
            timer += Time.deltaTime;
            float elapsed = timer / 0.2f;
            dialogueBox.localScale =  Vector3.Lerp(startScale, endScale, elapsed);
            dialogueBox.localRotation = Quaternion.Slerp(startRotation, endRot, elapsed);
            dialogueBoxRenderer.color = Color.Lerp(startColor, endColor, elapsed);
            
            yield return null;
        }
        
        dialogueBox.localScale = endScale;
        dialogueBox.localRotation = endRot;
        dialogueBoxRenderer.color = endColor;
        
        yield return null;
        dialogueBox.gameObject.SetActive(false);
    }

    private void StartDialogue()
    {
        dialogueBox.localScale = Vector3.zero;
        dialogueBox.localRotation = Quaternion.Euler(0, 0, 90);
        dialogueBoxRenderer.color = WhiteAlpha;
        dialogueBox.gameObject.SetActive(true);

        StartCoroutine(DialogueOpen());
    }

    private IEnumerator DialogueOpen()
    {
        float timer = 0;
        
        Vector3 startScale = dialogueBox.localScale;
        Vector3 endScale = Vector3.one;
        
        Quaternion startRotation = dialogueBox.localRotation;
        Quaternion endRot = Quaternion.Euler(0,0,0);
        
        Color startColor = dialogueBoxRenderer.color;
        Color endColor = Color.white;

        while (timer <= 0.3f)
        {
            timer += Time.deltaTime;
            float elapsed = timer / 0.3f;
            dialogueBox.localScale =  Vector3.Lerp(startScale, endScale, elapsed);
            dialogueBox.localRotation = Quaternion.Slerp(startRotation, endRot, elapsed);
            dialogueBoxRenderer.color = Color.Lerp(startColor, endColor, elapsed);
            
            yield return null;
        }
        
        dialogueBox.localScale = endScale;
        dialogueBox.localRotation = endRot;
        dialogueBoxRenderer.color = endColor;
        yield return null;
    }


}
