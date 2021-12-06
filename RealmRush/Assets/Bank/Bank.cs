using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{

    [SerializeField]int startingBalance = 150;
    [SerializeField]int currentBalance;

    public int CurrentBalance 
    {
        get 
        { 
            return currentBalance;
        }
    }
 
    private void Awake() 
    {
        currentBalance = startingBalance;
    }

    public void Deposit(int amount)
    {
        //절대값 만큼 플러스
        currentBalance += Mathf.Abs(amount);
    }

    public void WithDraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);

        if(currentBalance < 0)
        {
            //Lose the game
            ReloadScene();
        }
    }

    void ReloadScene()
    {
        //액티브 되어있는 신을 읽을
        Scene currentScene = SceneManager.GetActiveScene();
        //지금 액티브 되어 있는 신을 로드함.
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
