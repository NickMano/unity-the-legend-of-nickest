using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private HealthController controller;

    [SerializeField] private Image[] vida;

    void OnEnable()
    {
        controller.HealthChanged += UpdateVisual;
        UpdateVisual();
    }

    void OnDisable()
    {
        controller.HealthChanged -= UpdateVisual;
    }

    void Start()
    {
        UpdateVisual();
        SetMaxHearts();
    }

    private void UpdateVisual()
    {
        for (int i = 0; i < vida.Length; i++)
        {
            var remaining = controller.currentHealth - i * 2;
            if (remaining >= 2)
                vida[i].sprite = fullHeart;
            else if (remaining == 1)
                vida[i].sprite = halfHeart;
            else
                vida[i].sprite = emptyHeart;
        }
    }

    private void SetMaxHearts()
    {
        for (int i = 0; i < vida.Length; i++)
        {
            if (i < (controller.GetMaxHealth() + 1) / 2)
                vida[i].enabled = true;
            else
                vida[i].enabled = false;
        }
    }
}