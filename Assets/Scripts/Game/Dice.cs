using UnityEngine;
using TMPro;
using System.Collections;

public class Dice : MonoBehaviour
{
    [SerializeField] private Animation[] _dicesModel;

    private int RollDice() 
    {
        return Random.Range(1, 7);
    }

    public int RollDices() 
    {
        int[] array = new int[2];
        array[0] = RollDice();
        array[1] = RollDice();

        _dicesModel[0].Play("Roll_"+array[0]);
        _dicesModel[1].Play("Roll_" + array[1]);

        return array[0]+array[1];
    }

    public void HideDices()
    {
        for(int i = 0; i < _dicesModel.Length; i++)
        {
            _dicesModel[i].gameObject.SetActive(false);
        }
    }
    public void HideDicesAfterTime(float time)
    {
        StartCoroutine(HideDicesAfterTimeCoroutine(time));
    }

    public void ShowDices()
    {
        for (int i = 0; i < _dicesModel.Length; i++)
        {
            _dicesModel[i].gameObject.SetActive(true);
        }
    }

    private IEnumerator HideDicesAfterTimeCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        HideDices();
        yield return new WaitForSeconds(0.2f);
    }
}
