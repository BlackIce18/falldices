using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

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
    public class ChanceController : MonoBehaviour
    {
        [SerializeField] private ChanceWindow _chanceWindow;
        public MoveChance moveChance;
        public NextCardChance nextCardChance;
        public BirthDayChance birthDayChance;
        private List<IChance> _chances = new List<IChance>();

        private void Start()
        {
            List<Player> players = PlayerController.Singleton.Players;
            //_chances.Add(new MoveChance(this, moveChance.text));
            //_chances.Add(new NextCardChance(this, nextCardChance.text));
            _chances.Add(new BirthDayChance(this, players, birthDayChance.text));
        }
        internal void Generate()
        {
            int number = UnityEngine.Random.Range(0, _chances.Count);
            IChance randomChance = _chances[number];
            randomChance.DoActions();
        }
        public void ChangeText(string newString)
        {
            _chanceWindow.SetText(newString);
        }

        public ChanceWindow CreateNewChanceWindow()
        {
            GameObject chanceWindow = Instantiate(_chanceWindow.transform.gameObject, _chanceWindow.transform.parent.transform);
            return chanceWindow.GetComponent<ChanceWindow>();
        }
    }
    [Serializable]
    public class MoveChance : IChance
    {
        public string[] text;
        private int _fieldSize;
        private ChanceController _chance;
        public MoveChance(ChanceController chance, string[] _text)
        {
            _chance = chance;
            _fieldSize = GameController.Singleton.FieldCellsCount;
            text = _text;
        }
        public void DoActions()
        {
            int direction = UnityEngine.Random.Range(-1, 1);
            int stepsCount = UnityEngine.Random.Range(1, _fieldSize);
            Player activePlayer = PlayerController.Singleton.ActivePlayer;
            if (direction < 0)
            {
                MoveBack(activePlayer, stepsCount);
                ShowMessage(text[1], stepsCount);
            }
            else
            {
                Move(activePlayer, stepsCount);
                ShowMessage(text[0], stepsCount);
            }
        }

        private void MoveTo(Player player, FieldCell fieldCell)
        {
            int targetPosition = fieldCell.position;
            int playerPosition = player.Position;

            int result = targetPosition - playerPosition;
            if (result < 0) 
            {
                result = _fieldSize + result + playerPosition;
            }
            Move(player, result);
        }
        private void Move(Player player, int count)
        {
            player.Move(count);
        }
        private void MoveBackTo(Player player, FieldCell fieldCell)
        {
            int targetPosition = fieldCell.position;
            int playerPosition = player.Position;

            int result = targetPosition - playerPosition;
            if (result < 0)
            {
                result = playerPosition + (_fieldSize - targetPosition);
            }
            MoveBack(player, result);
        }
        private void MoveBack(Player player, int count)
        {
            player.MoveBack(count);
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
        private ChanceController _chance;
        public NextCardChance(ChanceController chance, string[] _text)
        {
            _chance = chance;
            text = _text;
        }
        public void DoActions()
        {
            ShowMessage();
            _chance.CreateNewChanceWindow();
        }
        public void ShowMessage()
        {
            int randNumber = UnityEngine.Random.Range(0, text.Length-1);
            string str = text[randNumber];
            _chance.ChangeText(str);
        }
    }
    [Serializable]
    public class BirthDayChance : IChance
    {
        public string[] text;
        private ChanceController _chance;
        private List<Player> _players; 
        private Player _activePlayer;
        private readonly int[] _payNumbers = { 100, 200, 500 };
        public BirthDayChance(ChanceController chance, List<Player> players, string[] _text)
        {
            _players = PlayerController.Singleton.Players;
            _activePlayer = PlayerController.Singleton.ActivePlayer;
            _chance = chance;
            text = _text;
        }

        public void DoActions()
        {
            int randomIndex = UnityEngine.Random.Range(0, _payNumbers.Length);
            int randomTextIndex = UnityEngine.Random.Range(0, text.Length);
            int payNumber = _payNumbers[randomIndex];

            for(int i = 0; i < _players.Count; i++)
            {
                if (_players[i] == _activePlayer) continue;
                _activePlayer.Balance.AddMoney(payNumber);
                _players[i].Balance.AddMoney(-payNumber);
            }

            PlayerController.Singleton.activePlayerMoney.text = _activePlayer.Balance.Money.ToString();
            ShowMessage(text[randomTextIndex], _payNumbers[randomIndex].ToString());
        }
        public void ShowMessage(string str, string pay)
        {
            str = str.Replace("%number%", pay);
            _chance.ChangeText(str);
        }
    }

    public class BankingErrorChance : IChance
    {
        private ChanceController _chance;
        public void DoActions()
        {

        }
        public void ShowMessage(string str, string pay)
        {
            str = str.Replace("%number%", pay);
            _chance.ChangeText(str);
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
