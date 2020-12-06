using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Platformer
{
    public class StoryManager : MonoBehaviour
    {
        [SerializeField] private string[] storylines;
        [SerializeField] private Text _text;

        private void Awake()
        {
            _text.text = storylines[GameManager.Level];
        }
    }
}