using System.Collections.Generic;
using Cinemachine;
using ThirdPersonScripts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using WeaponSOs;

namespace ManagerScripts
{
    public class TurnManager : MonoBehaviour, IGameService
    {
        [SerializeField]  private float _turnDuration = 15f;
        [FormerlySerializedAs("vcam")]
        [SerializeField] private CinemachineFreeLook _vcam;
        [SerializeField] private Image _turnTimerImage;
        private WormController _currentlyControlled;
        private List<TeamManager> _teams;
        private int _currentTeamIndex;
        private float _turntimer;
        private bool _currentTurnOngoing;
        private ChargeCounter _chargeCounter;
        private void Awake()
        {
            _teams = new List<TeamManager>();
            _currentTeamIndex = 0;
            _vcam = FindObjectOfType<CinemachineFreeLook>();
            _chargeCounter = FindObjectOfType<ChargeCounter>();
            ServiceLocator.Current.Register(this);
            _turntimer = _turnDuration;
            _currentTurnOngoing = false;

        }
        private void Update()
        {
            if (_currentTurnOngoing)
            {
                _turntimer -= Time.deltaTime;
            }
            if (_turntimer <= 0)
            {
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
            for (var i = 0; i < numberOfTeams; i++)
            {
                TeamManager newTeam = new TeamManager();
                newTeam.onSurrender += RemoveTeam;
                _teams.Add(newTeam);
            }
        }
        public void BeginTurns() {
            ActivateNextWorm();
        }
        private void NextTurn()
        {
            DeactivateCurrentWorm();
            _chargeCounter.ResetCharge();
            IncrementCurrentTeam();
            ActivateNextWorm();
        }

        private void DeactivateCurrentWorm() {
            _currentTurnOngoing = false;
            _currentlyControlled.WeaponryController.OnCharging -= _chargeCounter.UpdateCharge;
            _currentlyControlled.DeactivateAsControllable();
            _teams[_currentTeamIndex].IsActiveTeam = false;
        }
        private void ActivateNextWorm() {
            _currentlyControlled = _teams[_currentTeamIndex].NextTeamMember();
            _teams[_currentTeamIndex].IsActiveTeam = true;
            _currentlyControlled.ActivateAsControllable();
            ChangeCameraTarget(_currentlyControlled);
            _turntimer = _turnDuration;
            _currentlyControlled.WeaponryController.OnCharging += _chargeCounter.UpdateCharge;
            _currentTurnOngoing = true;
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
            _vcam.Follow = character.GetCameraTarget();
            _vcam.LookAt = character.GetCameraTarget();
        }
        private void RemoveTeam(TeamManager surrenderingTeam)
        {
            if (surrenderingTeam == _teams[_currentTeamIndex])
            {
                _currentTeamIndex--;
            }
            _teams.Remove(surrenderingTeam);
            IncrementCurrentTeam();
            if (_teams.Count <= 1)
            {
                Debug.Log("Game over");
                Destroy(this);
            }
        }

        private void ActivePlayerIsKilled()
        {
            NextTurn();
        }
        
        
    }
}
