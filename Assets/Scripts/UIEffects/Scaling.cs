using UnityEngine;
using UnityEngine.UI;

namespace UIEffects
{
    public class Scaling : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Button button;
        [SerializeField] private Vector3 scaleOffset;
        [SerializeField] private float scaleSpeed;
        private bool _isHovering;

        public void OnMouseHover(bool isHovering)
        {
            _isHovering = isHovering;
        }

        private void Update()
        {
            if (button is not null)
            {
                if (!button.interactable)
                {
                    rectTransform.localScale = Vector3.one;
                    return;
                }
            }

            rectTransform.localScale = Vector3.MoveTowards(rectTransform.localScale, _isHovering ? scaleOffset : Vector3.one, Time.deltaTime * scaleSpeed);
        }
    }
}
