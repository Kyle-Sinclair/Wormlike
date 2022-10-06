using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Cinemachine;
using ThirdPersonScripts;
using UnityEngine;
using UnityEngine.UI;
using WeaponSOs;

namespace ManagerScripts
{
    public class TurnManager : MonoBehaviour, IGameService
    {
        private WormController _currentlyControlled;
        private List<TeamManager> _teams;
        private float _turntimer;
        private bool currentTurnOngoing;
        private int _currentTeamIndex;
        [SerializeField]  private float _turnDuration = 15f;
        [SerializeField] private CinemachineFreeLook vcam;
        [SerializeField] private Image _turnTimerImage;
        private ChargeCounter _chargeCounter;
        private void Awake()
        {
            _teams = new List<TeamManager>();
            _currentTeamIndex = 0;
            vcam = FindObjectOfType<CinemachineFreeLook>();
            _chargeCounter = FindObjectOfType<ChargeCounter>();
            ServiceLocator.Current.Register(this);
            _turntimer = _turnDuration;
            currentTurnOngoing = false;

        }
        private void Update()
        {
            if (currentTurnOngoing)
            {
                Debug.Log("Decrementing time");
                
                _turntimer -= Time.deltaTime;
            }
            if (_turntimer <= 0)
            {
                Debug.Log("next turn");
                NextTurn();
            }

            _turnTimerImage.fillAmount = _turntimer / _turnDuration;
        }
        public void RegisterPlayer(WormController newTeamMember, int teamNumber)
        {
            newTeamMember.OnKilledWhileActive += ActivePlayerIsKilled;
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
            _currentlyControlled.weaponryController.OnCharging += _chargeCounter.UpdateCharge;
             currentTurnOngoing = true;
        }
        private void NextTurn()
        {
            currentTurnOngoing = false;
            _currentlyControlled.weaponryController.OnCharging -= _chargeCounter.UpdateCharge;

            _currentlyControlled.DeactivateAsControllable();
            _teams[_currentTeamIndex].IsActiveTeam = false;
            IncrementCurrentTeam();
            _currentlyControlled = _teams[_currentTeamIndex].NextTeamMember();
            _teams[_currentTeamIndex].IsActiveTeam = true;
            _currentlyControlled.ActivateAsControllable();
            ChangeCameraTarget(_currentlyControlled);
            _turntimer = _turnDuration;
            _currentlyControlled.weaponryController.OnCharging += _chargeCounter.UpdateCharge;

            currentTurnOngoing = true;
        }

        private void IncrementCurrentTeam()
        {
            _currentTeamIndex++;
            _currentTeamIndex %= _teams.Count;
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
            if (surrenderingTeam == _teams[_currentTeamIndex])
            {
                _currentTeamIndex--;
            }
            _teams.Remove(surrenderingTeam);
            if (_teams.Count == 1)
            {
                Debug.Log("We have a winner");
            }
        }

        private void ActivePlayerIsKilled()
        {
            NextTurn();
        }
        
        
    }
}
