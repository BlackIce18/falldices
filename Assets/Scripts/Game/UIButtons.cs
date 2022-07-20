using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class UIButtons : MonoBehaviour
    {
        [SerializeField] private GameObject _rollDices;
        [SerializeField] private GameObject _build;
        [SerializeField] private GameObject _skipTurn;

        public void RollDicesClick()
        {
            _rollDices.gameObject.SetActive(false);
            _build.gameObject.SetActive(true);
            _skipTurn.gameObject.SetActive(true);
        }

        public void Reset()
        {
            _rollDices.gameObject.SetActive(true);
            _build.gameObject.SetActive(false);
            _skipTurn.gameObject.SetActive(false);
        }
    }

}