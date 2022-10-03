using System.Collections;
using System.Collections.Generic;
using ThirdPersonScripts;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace ManagerScripts
{

    public class MapManager : MonoBehaviour
    {
        [SerializeField]private int numOfPlayers = 1;
        [SerializeField]private int numOfTeams = 1;

        //[SerializeField] private Transform _terrain;
        [SerializeField] private float mapScale;
        private TurnManager _turnManager;
        [SerializeField] private GameObject controllableCharacter;
        void Awake()
        {
             var scaleChange = new Vector3(mapScale * 3, 1, mapScale);
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
            if (numOfTeams > numOfPlayers)
            {
                numOfTeams = numOfPlayers;
            }
            _turnManager.InitializeLists(numOfTeams);
        }

        private void PopulateTeams()
        {
            int teamNumber =0;
            for (int i = 0; i < numOfPlayers; i++)
            {
                GameObject newCharacter = Instantiate(controllableCharacter);
                newCharacter.transform.position = new Vector3(Random.Range(0f,5f),0,Random.Range(0f,5f));

                WormController charController  = newCharacter.GetComponent<WormController>();
                _turnManager.RegisterPlayer(charController, teamNumber);
                teamNumber++;
                teamNumber %= numOfTeams;
            }
        }
    }
}
