using System.Collections;
using System.Collections.Generic;
using ThirdPersonScripts;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace ManagerScripts
{

    public class MapManager : MonoBehaviour
    {
        [SerializeField] private int _numOfPlayers = 1;
        [SerializeField]private int _numOfTeams = 1;

        //[SerializeField] private Transform _terrain;
        [SerializeField] private float _mapScale;
        private TurnManager _turnManager;
        [SerializeField] private GameObject _controllableCharacter;
        void Awake()
        {
             Vector3 scaleChange = new Vector3(_mapScale * 3, 1, _mapScale);
             _turnManager = FindObjectOfType<TurnManager>();
        }

        // Start is called before the first frame update
        void Start()
        {
            Init();
            PopulateTeams();
            BeginTurns();
        }

        private void BeginTurns()
        {
            _turnManager.BeginTurns();
        }

        private void Init()
        {
            _turnManager = FindObjectOfType<TurnManager>();
            if (_numOfTeams > _numOfPlayers)
            {
                _numOfTeams = _numOfPlayers;
            }
            _turnManager.InitializeLists(_numOfTeams);
        }

        private void PopulateTeams()
        {
            int teamNumber =0;
            for (int i = 0; i < _numOfPlayers; i++)
            {
                GameObject newCharacter = Instantiate(_controllableCharacter);
                newCharacter.transform.position = new Vector3(Random.Range(0f,5f),0,Random.Range(0f,5f));

                WormController charController  = newCharacter.GetComponent<WormController>();
                _turnManager.RegisterPlayer(charController, teamNumber);
                teamNumber++;
                teamNumber %= _numOfTeams;
            }
        }
    }
}
