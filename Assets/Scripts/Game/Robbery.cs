using UnityEngine;

public class Robbery
{
    private int _robberyChance;
    private int _startChancePercantage = 10;
    private int _maxChancePercantage = 70;
    public int GenerateRobberyChance()
    {
        _robberyChance = Random.Range(_startChancePercantage, _maxChancePercantage);
        return _robberyChance;
    }
}
