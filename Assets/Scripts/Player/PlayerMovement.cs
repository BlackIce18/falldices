using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameField _gameField; // Ячейки поля (point)
    private Player _player => GetComponent<Player>();
    private int _movesLeft = 0; // Осталось ходов

    public int MovesCount 
    { 
        get{ return _movesLeft; }
        set 
        {
            try
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                if (value >= 0)
                {
                    _movesLeft = value;
                }
            }
            catch (ArgumentOutOfRangeException argumentException) 
            {
                Debug.Log("Значение должно быть больше 0");
            }
        }
    }

    // Анимация перемещения вверх 
    IEnumerator UpMoveAnimation(Vector3 playerPosition, Vector3 nextPointPosition)
    {
        transform.DORotate(new Vector3(-25, 0, 0), 0.5f);
        Tween myTween = transform.DOMove(new Vector3(nextPointPosition.x, 2.25f, (playerPosition.z - nextPointPosition.z) / 2 + nextPointPosition.z), 0.25f);
        yield return myTween.WaitForCompletion();
        transform.DORotate(new Vector3(0, 0, 0), 0.5f);
        myTween = transform.DOMove(new Vector3(nextPointPosition.x, 0.3f, nextPointPosition.z), 0.25f);
        yield return myTween.WaitForCompletion();
    }

    // Анимация перемещения вправо 
    IEnumerator RightMoveAnimation(Vector3 playerPosition, Vector3 nextPointPosition)
    {
        transform.DORotate(new Vector3(0, 0, 25), 0.5f);
        Tween myTween = transform.DOMove(new Vector3((playerPosition.x - nextPointPosition.x) / 2 + nextPointPosition.x, 2.25f, nextPointPosition.z), 0.25f);
        yield return myTween.WaitForCompletion();
        transform.DORotate(new Vector3(0, 0, 0), 0.5f);
        myTween = transform.DOMove(new Vector3(nextPointPosition.x, 0.3f, nextPointPosition.z), 0.25f);
        yield return myTween.WaitForCompletion();
    }

    // Анимация перемещения вниз 
    IEnumerator DownMoveAnimation(Vector3 playerPosition, Vector3 nextPointPosition)
    {
        transform.DORotate(new Vector3(25, 0, 0), 0.5f);
        Tween myTween = transform.DOMove(new Vector3((playerPosition.x - nextPointPosition.x) / 2 + nextPointPosition.x, 2.25f, nextPointPosition.z), 0.25f);
        yield return myTween.WaitForCompletion();
        transform.DORotate(new Vector3(0, 0, 0), 0.5f);
        myTween = transform.DOMove(new Vector3(nextPointPosition.x, 0.3f, nextPointPosition.z), 0.25f);
        yield return myTween.WaitForCompletion();
    }

    // Анимация перемещения влево 
    IEnumerator LeftMoveAnimation(Vector3 playerPosition, Vector3 nextPointPosition)
    {
        transform.DORotate(new Vector3(0, 0, -25), 0.5f);
        Tween myTween = transform.DOMove(new Vector3((playerPosition.x - nextPointPosition.x) / 2 + nextPointPosition.x, 2.25f, nextPointPosition.z), 0.25f);
        yield return myTween.WaitForCompletion();
        transform.DORotate(new Vector3(0, 0, 0), 0.5f);
        myTween = transform.DOMove(new Vector3(nextPointPosition.x, 0.3f, nextPointPosition.z), 0.25f);
        yield return myTween.WaitForCompletion();
    }

    public IEnumerator Move(int movesCount)
    {
        MovesCount = movesCount;

        while (MovesCount > 0)
        {
            PlayerAnimationDirection direction = _gameField.GetPointPlayerAnimationDirection(_player.Position);

            Vector3 playerPosition = _player.transform.localPosition;
            Vector3 nextPointPosition = _gameField.GetPointPosition(_player.Position + 1);

            if (direction == PlayerAnimationDirection.Up)
            {
                yield return StartCoroutine(UpMoveAnimation(playerPosition, nextPointPosition));
            }
            else if (direction == PlayerAnimationDirection.Right)
            {
                yield return StartCoroutine(RightMoveAnimation(playerPosition, nextPointPosition));
            }
            else if (direction == PlayerAnimationDirection.Down)
            {
                yield return StartCoroutine(DownMoveAnimation(playerPosition, nextPointPosition));
            }
            else if (direction == PlayerAnimationDirection.Left)
            {
                yield return StartCoroutine(LeftMoveAnimation(playerPosition, nextPointPosition));
            }


            if (_player.Position + 1 >= _gameField.FieldCellsCount)
            {
                _player.Position = 0;
            }
            else 
            {
                _player.Position++;
            }


            MovesCount--;
        }
    }

}
