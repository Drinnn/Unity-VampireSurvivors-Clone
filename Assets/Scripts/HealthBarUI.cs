using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image healthBarImage;
    [SerializeField] private float fadeTime = 2f;

    private float _timeSinceLastUpdate;
    private float _fadeTimer;
    
    private void Start()
    {
        PlayerController.Instance.OnTookDamage += PlayerController_OnTookDamage;
        
        Hide();
    }

    private void Update()
    {
        _timeSinceLastUpdate += Time.deltaTime;
        
        if (_timeSinceLastUpdate > fadeTime)
        {
            _fadeTimer -= Time.deltaTime;
            if (_fadeTimer <= 0)
            {
                Hide();
            }
        }
    }

    private void PlayerController_OnTookDamage(object sender, EventArgs e)
    {
        Show();
        healthBarImage.fillAmount = PlayerController.Instance.CurrentHealth / 100;
        _timeSinceLastUpdate = 0f;
    }

    private void Hide()
    {
        gameObject.SetActive(false);
        
        _fadeTimer = fadeTime;
        _timeSinceLastUpdate = 0f;
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
