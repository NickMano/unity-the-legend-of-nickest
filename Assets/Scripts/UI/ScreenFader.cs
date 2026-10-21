using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class ScreenFader : MonoBehaviour
{
    // private float alpha = 0;
    [SerializeField, Min(0f)] private float duration = 1f;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public IEnumerator FadeToBlack()
    {
        canvasGroup.blocksRaycasts = true;
        yield return FadeTo(1f);
    }

    public IEnumerator FadeToClear()
    {
        yield return FadeTo(0f);
        canvasGroup.blocksRaycasts = false;
    }

    private IEnumerator FadeTo(float targetAlpha)
    {
        float initialAlpha = canvasGroup.alpha;

        if (duration <= 0f)
        {
            canvasGroup.alpha = targetAlpha;
            yield break;
        }

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;

            canvasGroup.alpha = Mathf.Lerp(
                initialAlpha,
                targetAlpha,
                Mathf.Clamp01(elapsed / duration)
            );

            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }
}
