using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace ManagerScripts
{
    public class TurnManager : MonoBehaviour
    {
        private List<GameObject> _players;
        private ThirdPersonMovementController _currentlyControlled;
        public List<List<GameObject>> Teams;
        private int teamIndex;
        private int _playerIndex;
        [SerializeField] private CinemachineFreeLook _vcam;

        private void Awake()
        {
            Teams = new List<List<GameObject>>();
            _players = new List<GameObject>();
            _playerIndex = 0;
            _vcam = FindObjectOfType<CinemachineFreeLook>();
        }

        // Start is called before the first frame update
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.P))
            {
                nextTurn();
            }
        }

        public void RegisterPlayer(GameObject newTeamMember, int teamNumber)
        {

            Debug.Log("Registering player to " + teamNumber);
            Teams[teamNumber].Add(newTeamMember);
            _players.Add(newTeamMember);
        }

        public void InitializeLists(int numberOfTeams)
        {
            for (int i = 0; i < numberOfTeams; i++)
            {
                Debug.Log("Adding a team list");
                Teams.Add(new List<GameObject>());
            }
        }

        public void BeginTurns()
        {

            _currentlyControlled = Teams[0][0].GetComponent<ThirdPersonMovementController>();
            _currentlyControlled.ActivateAsControllable();
            ChangeCameraTarget(_currentlyControlled);

            // _currentlyControlled.PrioritizeVirtualCamera();


        }

        private void nextTurn()
        {
            // _currentlyControlled.DeprioritizeVirtualCamera();

            _currentlyControlled.DeactivateAsControllable();
            _playerIndex++;
            if (_playerIndex == _players.Count)
            {
                _playerIndex %= _players.Count;
            }

            _currentlyControlled = _players[_playerIndex].GetComponent<ThirdPersonMovementController>();
            _currentlyControlled.ActivateAsControllable();
            ChangeCameraTarget(_currentlyControlled);
            // _currentlyControlled.PrioritizeVirtualCamera();
        }

        private void ChangeCameraTarget(ThirdPersonMovementController character)
        {
            Transform cameraTarget = character.transform.Find("Camera Target").transform;
            _vcam.Follow = cameraTarget;
            _vcam.LookAt = cameraTarget;


        }
    }
}
