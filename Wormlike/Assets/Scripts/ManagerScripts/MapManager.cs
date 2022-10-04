
using ThirdPersonScripts;
using UnityEngine;
namespace ManagerScripts
{

    public class MapManager : MonoBehaviour, IGameService
    {
     
        //[SerializeField] private Transform _terrain;
        private TurnManager _turnManager;
        [SerializeField] private GameObject controllableCharacter;
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
                 GameObject newCharacter = Instantiate(controllableCharacter);
                 newCharacter.transform.position = new Vector3(Random.Range(0f,5f),0,Random.Range(0f,5f));
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

    }
}
