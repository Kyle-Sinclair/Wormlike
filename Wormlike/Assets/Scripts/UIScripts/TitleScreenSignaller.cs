using StaticsAndUtilities;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UIScripts
{
    public class TitleScreenSignaller : MonoBehaviour
    {
        [FormerlySerializedAs("self")] [SerializeField] private Canvas _self;
        [SerializeField] private Slider _numOfTeamsSlider;
        [SerializeField] private Slider _numOfPlayersSlider;
        [SerializeField] private TextMeshProUGUI _numberOfPlayers; 
        [SerializeField] private TextMeshProUGUI _numberOfTeams;
        private LevelLoader _levelLoader;
        private int _numPlayers;
        private int _numTeams;
    

        public void Start()
        {
            _levelLoader = FindObjectOfType<LevelLoader>();
            _numOfPlayersSlider.value = 2;
            UpdateNumberOfPlayers(_numOfPlayersSlider.value);
            _numOfTeamsSlider.value = 2;
            UpdateNumberOfTeams(_numOfTeamsSlider.value);
        
        }
        private void UpdateNumberOfPlayers(float newValue)
        {
            if (newValue < _numTeams)
            {
                UpdateNumberOfTeams(newValue);
                _numOfTeamsSlider.value = _numTeams;
            }
            _numPlayers = (int)newValue;
            EqualizeTeamsAndPlayersNumbers();
            _numberOfPlayers.text = newValue.ToString();
        }  
        private void UpdateNumberOfTeams(float newValue)
        {
            _numTeams = (int)newValue;
            EqualizeTeamsAndPlayersNumbers();
            _numberOfTeams.text = newValue.ToString();
        }

        private void EqualizeTeamsAndPlayersNumbers()
        {
            if (_numTeams <= _numPlayers) return;
            UpdateNumberOfPlayers( _numTeams);
            _numOfPlayersSlider.value = _numTeams;
            _numberOfPlayers.text = _numTeams.ToString();
        }

        private void GenerateLevel()
        {
            _self.enabled = false;
            _levelLoader.StartLoadingBattleGround(_numPlayers, _numTeams);
        }
    }
}
