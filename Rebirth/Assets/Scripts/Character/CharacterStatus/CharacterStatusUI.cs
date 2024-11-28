using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharacterStatusUI : MonoBehaviour, ICharacterStatusUI
{
    private static string logPrefix = "[CharacterStatusUI] ";

    public GameObject characterStatusPanel3D;
    public GameObject characterStatusPanel2D;

    public CharacterStatusUI2D characterStatusUI2D;
    public CharacterStatusUI3D characterStatusUI3D;

    private ICharacterStatusUI currentCharacterStatusUI;

    private void OnEnable()
    {
        CharacterStatusManager.Instance.OnMoneyChanged += UpdateMoneyUI;
        CharacterStatusManager.Instance.OnHealthChanged += UpdateHealthUI;
    }

    private void Start()
    {
        UpdateCurrentCharacterStatusUI();
        SetMaxHealth(CharacterStatusManager.Instance.GetMaxHealth());   
        UpdateMoneyUI(CharacterStatusManager.Instance.Money);
        UpdateHealthUI(CharacterStatusManager.Instance.Health);
    }

    private void UpdateCurrentCharacterStatusUI()
    {
        if (DimensionManager.Instance.GetCurrentDimension() == Dimension.TWO_DIMENSION)
        {
            characterStatusPanel3D.SetActive(false);
            characterStatusPanel2D.SetActive(true);

            currentCharacterStatusUI = characterStatusUI2D;
        }
        else
        {
            characterStatusPanel2D.SetActive(false);
            characterStatusPanel3D.SetActive(true);

            currentCharacterStatusUI = characterStatusUI3D;
        }
        Debug.Log(logPrefix + "currentCharacterStatusUI: " + currentCharacterStatusUI);
    }
    
    public void UpdateMoneyUI(int currentMoney)
    {
        UpdateCurrentCharacterStatusUI();
        currentCharacterStatusUI.UpdateMoneyUI(currentMoney);
    }

    public void UpdateHealthUI(int currentHealth)
    {
        UpdateCurrentCharacterStatusUI();
        currentCharacterStatusUI.UpdateHealthUI(currentHealth);
    }

    public void SetMaxHealth(int maxHealth)
    {
        characterStatusUI2D.SetMaxHealth(maxHealth);
        characterStatusUI3D.SetMaxHealth(maxHealth);
    }

    private void OnDisable()
    {
        CharacterStatusManager.Instance.OnMoneyChanged -= UpdateMoneyUI;
        CharacterStatusManager.Instance.OnHealthChanged -= UpdateHealthUI;
    }
}