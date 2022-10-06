
using System.Collections.Generic;
using StaticsAndUtilities;
using ThirdPersonScripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ManagerScripts
{

    public class MapManager : MonoBehaviour, IGameService
    {
     
        //[SerializeField] private Transform _terrain;
        private TurnManager _turnManager;
        [SerializeField] private WormController controllableCharacter;

        [SerializeField]
        private List<SpawnZone> _spawnZones;
        void Awake()
        {
             ServiceLocator.Current.Register(this);
        }
        void Start()
        {
            _turnManager = ServiceLocator.Current.Get<TurnManager>();
        }
        public void BeginGeneratingMap(int numPlayers, int numTeams)
        {
            if (_turnManager == null)
            {
                _turnManager = ServiceLocator.Current.Get<TurnManager>();
            }
            _turnManager.InitializeLists(numTeams);
            PopulateTeams(numPlayers,numTeams);
            BeginTurns();
        }
        
        private void PopulateTeams(int numPlayers, int numTeams)
        {
            //Debug.Log(numPlayers);
            int teamNumber =0;
            for (int i = 0; i < numPlayers; i++)
            {
                 WormController newCharacter = Instantiate(controllableCharacter,_spawnZones[teamNumber].SpawnPoint(), Quaternion.identity);
                 newCharacter.SetTeamColor(teamNumber);
                 WormController charController  = newCharacter.GetComponent<WormController>();
                 _turnManager.RegisterPlayer(charController, teamNumber);
                teamNumber++;
                teamNumber %= numTeams;
            }

        }
        
        private void BeginTurns()
        {
            _turnManager.BeginTurns();
        }

        public void RegisterSpawnZone(SpawnZone spawnZone)
        {
            _spawnZones.Add(spawnZone);
        }

       
    }
}
