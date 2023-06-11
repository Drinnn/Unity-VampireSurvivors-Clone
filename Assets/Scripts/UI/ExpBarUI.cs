using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private TextMeshProUGUI levelText;

    private void Start()
    {
        ExpSystem.Instance.OnExpUpdate += ExpSystem_OnExpUpdate;

        UpdateVisual();
    }

    private void ExpSystem_OnExpUpdate(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        barImage.fillAmount = ExpSystem.Instance.GetExpNormalized();
        levelText.text = "Level " + ExpSystem.Instance.CurrentLevel;
    }
}
