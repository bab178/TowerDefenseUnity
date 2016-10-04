using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    private int Health = 20;
    private int Money = 100;

	void Update () {
	    if(Health <= 0)
        {
            GameOver();
        }
	}

    public bool BuyUnit(Tower unit)
    {
        if(unit.Price > Money)
        {
            Debug.Log("Not enough money.");
            return false;
        }

        Money -= unit.Price;
        return true;
    }

    public void TakeDamage(int damageTaken = 1)
    {
        Health -= damageTaken;
    }

    public void EarnMoney(int moneyEarned = 1)
    {
        Money += moneyEarned;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
