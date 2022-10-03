using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Cinemachine;
using ItemScripts;
using ThirdPersonScripts;
using UnityEngine;

namespace ManagerScripts
{
    public class TurnManager : MonoBehaviour
    {
        private WormController _currentlyControlled;
        private List<TeamManager> Teams;
        private Timer turntimer;
        private int _currentTeamIndex;
        [SerializeField] private CinemachineFreeLook _vcam;

        private void Awake()
        {
            Teams = new List<TeamManager>();
            _currentTeamIndex = 0;
            _vcam = FindObjectOfType<CinemachineFreeLook>();
        }
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.P))
            {
                nextTurn();
            }
        }
        public void RegisterPlayer(WormController newTeamMember, int teamNumber)
        {
            Teams[teamNumber].RegisterPlayer(newTeamMember);
        }
        public void InitializeLists(int numberOfTeams)
        {
            for (int i = 0; i < numberOfTeams; i++)
            {
                //Debug.Log("Adding a team list");
                TeamManager newTeam = new TeamManager();
                newTeam.onSurrender += RemoveTeam;
                Teams.Add(newTeam);
            }
        }
        public void BeginTurns()
        {
            _currentlyControlled = Teams[_currentTeamIndex].NextTeamMember();
            Teams[_currentTeamIndex]._isActiveTeam = true;
            _currentlyControlled.ActivateAsControllable();
            _currentlyControlled.IsActivePlayer = true;
            ChangeCameraTarget(_currentlyControlled);
        }
        private void nextTurn()
        {
            _currentlyControlled.DeactivateAsControllable();
            Teams[_currentTeamIndex]._isActiveTeam = false;
            _currentTeamIndex++;
            _currentTeamIndex %= Teams.Count;
            _currentlyControlled = Teams[_currentTeamIndex].NextTeamMember();
            Teams[_currentTeamIndex]._isActiveTeam = true;
            _currentlyControlled.ActivateAsControllable();
            ChangeCameraTarget(_currentlyControlled);
        }

        public void EquipWeaponOnActiveWorm(WeaponSO weaponData)
        {
            _currentlyControlled.EquipWeapon(weaponData);
        }
        private void ChangeCameraTarget(WormController character)
        {
            _vcam.Follow = character.GetCameraTarget();
            _vcam.LookAt = character.GetCameraTarget();
        }

        private void RemoveTeam(TeamManager surrenderingTeam)
        {
            Teams.Remove(surrenderingTeam);
            if (Teams.Count == 1)
            {
                Debug.Log("We have a winner");
            }
        }
    }
}
