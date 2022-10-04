using System.Collections.Generic;
using System.Timers;
using Cinemachine;
using ThirdPersonScripts;
using UnityEngine;
using WeaponSOs;

namespace ManagerScripts
{
    public class TurnManager : MonoBehaviour, IGameService
    {
        private WormController _currentlyControlled;
        private List<TeamManager> _teams;
        private Timer _turntimer;
        private int _currentTeamIndex;
        [SerializeField] private CinemachineFreeLook vcam;
        private void Awake()
        {
            _teams = new List<TeamManager>();
            _currentTeamIndex = 0;
            vcam = FindObjectOfType<CinemachineFreeLook>();
            ServiceLocator.Current.Register(this);
        }
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.P))
            {
                NextTurn();
            }
        }
        public void RegisterPlayer(WormController newTeamMember, int teamNumber)
        {
            _teams[teamNumber].RegisterPlayer(newTeamMember);
        }
        public void InitializeLists(int numberOfTeams)
        {
            for (int i = 0; i < numberOfTeams; i++)
            {
                //Debug.Log("Adding a team list");
                TeamManager newTeam = new TeamManager();
                newTeam.onSurrender += RemoveTeam;
                _teams.Add(newTeam);
            }
        }
        public void BeginTurns()
        {
            _currentlyControlled = _teams[_currentTeamIndex].NextTeamMember();
            _teams[_currentTeamIndex].IsActiveTeam = true;
            _currentlyControlled.ActivateAsControllable();
            _currentlyControlled.isActivePlayer = true;
            ChangeCameraTarget(_currentlyControlled);
        }
        private void NextTurn()
        {
            _currentlyControlled.DeactivateAsControllable();
            _teams[_currentTeamIndex].IsActiveTeam = false;
            _currentTeamIndex++;
            _currentTeamIndex %= _teams.Count;
            _currentlyControlled = _teams[_currentTeamIndex].NextTeamMember();
            _teams[_currentTeamIndex].IsActiveTeam = true;
            _currentlyControlled.ActivateAsControllable();
            ChangeCameraTarget(_currentlyControlled);
        }
        public void EquipWeaponOnActiveWorm(WeaponSO weaponData)
        {
            _currentlyControlled.EquipWeapon(weaponData);
        }
        private void ChangeCameraTarget(WormController character)
        {
            vcam.Follow = character.GetCameraTarget();
            vcam.LookAt = character.GetCameraTarget();
        }
        private void RemoveTeam(TeamManager surrenderingTeam)
        {
            _teams.Remove(surrenderingTeam);
            if (_teams.Count == 1)
            {
                Debug.Log("We have a winner");
            }
        }
    }
}
