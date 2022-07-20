using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Game
{
    enum Chances{
        move,
        nextCard,
        birthday,
        bankingError,
        skip,
        jail,
        robbery,
        penalty,
        treasure,
    }
    public interface IChance
    {
        public void DoActions();
    }
    public class Chance : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _windowText;
        public MoveChance moveChance;
        public NextCardChance nextCardChance;
        private List<IChance> _chances = new List<IChance>();

        private void Awake()
        {
            Player activePlayer = GameField.gameFieldSingleton.ActivePlayer;
            _chances.Add(new MoveChance(this,activePlayer, moveChance.text));
            _chances.Add(new NextCardChance(this));
        }
        private void OnEnable()
        {
            Generate();
        }
        internal void Generate()
        {
            int number = UnityEngine.Random.Range(0, _chances.Count-1);
            IChance randomChance = _chances[number];
            randomChance.DoActions();
        }
        public void ChangeText(string newString)
        {
            _windowText.text = newString;
        }
    }
    [Serializable]
    public class MoveChance : IChance
    {
        public string[] text;
        private Player _player;
        private int _fieldSize;
        private Chance _chance;
        public MoveChance(Chance chance, Player player, string[] _text)
        {
            _chance = chance;
            _player = player;
            _fieldSize = GameField.gameFieldSingleton.FieldCellsCount;
            text = _text;
        }

        private void MoveTo(FieldCell fieldCell)
        {
            int targetPosition = fieldCell.position;
            int playerPosition = _player.Position;

            int result = targetPosition - playerPosition;
            if (result < 0) 
            {
                Move(_fieldSize + result + playerPosition);
            }
            else
            {
                Move(result);
            }
        }
        private void Move(int count)
        {
            _player.Move(count);

        }
        private void MoveBackTo(FieldCell fieldCell)
        {
            int targetPosition = fieldCell.position;
            int playerPosition = _player.Position;

            int result = targetPosition - playerPosition;
            if (result < 0)
            {
                MoveBack(playerPosition + (_fieldSize - targetPosition));
            }
            else
            {
                MoveBack(result);
            }
        }
        private void MoveBack(int count)
        {
            _player.MoveBack(count);
        }
        public void DoActions()
        {
            int direction = UnityEngine.Random.Range(-1,1);
            Debug.Log(direction);
            int stepsCount = UnityEngine.Random.Range(1, _fieldSize);
            if (direction < 0)
            {
                MoveBack(stepsCount);
                ShowMessage(text[1], stepsCount);
            }
            else 
            {
                Move(stepsCount);
                ShowMessage(text[0], stepsCount);
            }
        }

        public void ShowMessage(string str, int movesCount)
        {
            str = str.Replace("%number%", movesCount.ToString());
            _chance.ChangeText(str);
        }
    }

    [Serializable]
    public class NextCardChance : IChance
    {
        public string[] text;
        private Chance _chance;
        public NextCardChance(Chance chance)
        {
            _chance = chance;
        }
        public void DoActions()
        {
            ShowMessage();
            Chance chance = new Chance();
            chance.Generate();
        }
        public void ShowMessage()
        {
            int randNumber = UnityEngine.Random.Range(0, text.Length);
            string str = text[randNumber];
            Chance chance = new Chance();
            chance.ChangeText(str);
        }
    }

    public class BirthDayChance : IChance
    {
        public void DoActions()
        {

        }
        public void ShowMessage()
        {
            throw new NotImplementedException();
        }
    }

    public class BankingErrorChance : IChance
    {
        public void DoActions()
        {

        }
        public void ShowMessage()
        {
            throw new NotImplementedException();
        }
    }

    public class SkipChance : IChance
    {
        public void DoActions()
        {

        }
        public void ShowMessage()
        {
            throw new NotImplementedException();
        }
    }

    public class JailErrorChance : IChance
    {
        public void DoActions()
        {

        }
        public void ShowMessage()
        {
            throw new NotImplementedException();
        }
    }
    public class RobberyChance : IChance
    {
        public void DoActions()
        {

        }
        public void ShowMessage()
        {
            throw new NotImplementedException();
        }
    }

    public class PenaltyChance : IChance
    {
        public void DoActions()
        {

        }
        public void ShowMessage()
        {
            throw new NotImplementedException();
        }
    }

    public class TreasureChance : IChance
    {
        public void DoActions()
        {

        }
        public void ShowMessage()
        {
            throw new NotImplementedException();
        }
    }
}
