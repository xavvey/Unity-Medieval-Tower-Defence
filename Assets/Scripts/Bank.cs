using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 250;
    
    [SerializeField] int currentBalance;
    public int CurrentBalance { get { return currentBalance;} }

    [SerializeField] TextMeshProUGUI displayBalance;

    private void Awake() 
    {
        currentBalance = startingBalance;
        UpdateBalanceDisplay();    
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateBalanceDisplay();
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        UpdateBalanceDisplay();

        if(CurrentBalance < 0)
        {
            ReloadScene();
        }
    }

    void UpdateBalanceDisplay()
    {
        displayBalance.text = "Gold: " + currentBalance;
    }

    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
