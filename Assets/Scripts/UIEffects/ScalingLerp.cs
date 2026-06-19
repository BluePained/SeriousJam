using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UIEffects
{
    public class ScalingLerp : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Button button;
        [SerializeField] private Vector3 scaleOffset;
        [SerializeField] private float scaleSpeed;
        private Coroutine _coroutine;
        
        public void OnMouseHover(bool isHovering)
        {
            if (button != null)
            {
                if (!button.interactable)
                {
                    rectTransform.localScale = Vector3.one;
                    return;
                }
            }

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
            _coroutine = StartCoroutine(OnScaling(isHovering));
            
        }

        private IEnumerator OnScaling(bool isHovering)
        {
            float timer = 0;
            
            Vector3 rect = rectTransform.localScale;
            Vector3 targetScale = isHovering ? scaleOffset : Vector3.one;
            
            while (timer < scaleSpeed)
            {
                timer += Time.deltaTime;
                float elapsed = timer / scaleSpeed;
                
                rectTransform.localScale = Vector3.Lerp(rect, targetScale, elapsed);
                
                yield return null;
            }

            rectTransform.localScale = targetScale;
            _coroutine = null;
        }
    }
}
