using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using ItemScripts;
using ThirdPersonScripts;
using UnityEngine;

namespace ManagerScripts
{
    public class TurnManager : MonoBehaviour
    {
        private WormController _currentlyControlled;
        [SerializeField] public List<TeamManager> Teams;

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

            Debug.Log("Registering player to " + teamNumber);
            Teams[teamNumber].RegisterPlayer(newTeamMember);
        }
        public void InitializeLists(int numberOfTeams)
        {
            for (int i = 0; i < numberOfTeams; i++)
            {
                Debug.Log("Adding a team list");
                Teams.Add(new TeamManager());
            }
        }
        public void BeginTurns()
        {
            _currentlyControlled = Teams[_currentTeamIndex].NextTeamMember();
            _currentlyControlled.ActivateAsControllable();
            ChangeCameraTarget(_currentlyControlled);
            // _currentlyControlled.PrioritizeVirtualCamera();
        }
        private void nextTurn()
        {
            // _currentlyControlled.DeprioritizeVirtualCamera();

            _currentlyControlled.DeactivateAsControllable();
            _currentTeamIndex++;
            _currentTeamIndex %= Teams.Count;
            _currentlyControlled = Teams[_currentTeamIndex].NextTeamMember();
            _currentlyControlled.ActivateAsControllable();
            ChangeCameraTarget(_currentlyControlled);
            // _currentlyControlled.PrioritizeVirtualCamera();
        }

        public void EquipWeaponOnActiveWorm(WeaponSO weaponData)
        {
            _currentlyControlled.EquipWeapon(weaponData);
        }
        private void ChangeCameraTarget(WormController character)
        {
            Transform cameraTarget = character.transform.Find("Camera Target").transform;
            _vcam.Follow = cameraTarget;
            _vcam.LookAt = cameraTarget;
        }
    }
}
