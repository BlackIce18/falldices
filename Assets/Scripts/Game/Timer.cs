using UnityEngine;
using TMPro;
using System.Collections;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private int _timeToNextTurn = 60;
    private int _timer;

    [SerializeField] private Color _baseColor;
    [SerializeField] private Color _warningColor;
    [SerializeField] private int _warningTime = 10;


    private int TimeToNextTurn 
    {
        get { return _timeToNextTurn; }
    }

    private int CountingTimer 
    {
        get { return _timer; }
        set { _timer = value; }
    }
    private void Start()
    {
        _text.text = TimeToNextTurn.ToString();
        StartTimer();
    }

    private void StartTimer()
    {
        CountingTimer = _timeToNextTurn;
        StartCoroutine(TimerCoroutine());
    }

    public void ResetTimer()
    {
        SetColor(_baseColor);
        CountingTimer = TimeToNextTurn;
    }

    private void SetColor(Color newColor)
    {
        _text.color = newColor;
    }

    IEnumerator TimerCoroutine() {
        if (CountingTimer == _warningTime)
        {
            SetColor(_warningColor);
        }

        if (CountingTimer <= 0)
        {
            ResetTimer();
            StopCoroutine("TimerCoroutine");
        }
        else {
            CountingTimer--;
        }

        _text.text = CountingTimer.ToString();
        yield return new WaitForSeconds(1);
        StartCoroutine(TimerCoroutine());
    }
}
