using UnityEngine;
using UnityEngine.UIElements;

public class Robbery
{
    private int _robberyChance;
    private int _startChancePercantage = 10;
    private int _maxChancePercantage = 70;
    private int _bet = 2000;
    private int _multiplier = 2;

    public int GenerateRobberyChance()
    {
        _robberyChance = Random.Range(_startChancePercantage, _maxChancePercantage);
        return _robberyChance;
    }

    public void StartRobbery(Player player, int jailCeilNumber)
    {
        if(player.Balance.Money >= _bet)
        {
            player.Balance.Money -= _bet;
            int randomNumber = Random.Range(0, 100);

            if(randomNumber <= _robberyChance)
            {
                player.Balance.Money += _bet * _multiplier;
            }
            else
            {
                player.MoveBack(jailCeilNumber);
                player.ChangeStatusToPrisoner();
            }
        }
        else
        {

        }
    }
}
