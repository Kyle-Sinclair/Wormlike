using System.Collections.Generic;
using ThirdPersonScripts;
using Unity.VisualScripting;
using UnityEngine;

namespace ManagerScripts
{
    public class TeamManager
    {
        private List<WormController> _teamMembers;
        private int _currentPlayerIndex;
        public bool IsActiveTeam;
        private bool HasPlayers => _teamMembers.Count > 0;
        public TeamManager()
        {
            IsActiveTeam = false;
            _teamMembers = new List<WormController>();
            _currentPlayerIndex = -1;
        }
        public WormController NextTeamMember()
        {
            IncrementCurrentPlayer();
            return _teamMembers[_currentPlayerIndex];

        }
        public void RegisterPlayer(WormController newTeamMember)
        {
            newTeamMember.OnDeath += RemoveWormOnDeath;
            _teamMembers.Add(newTeamMember);
            newTeamMember.TeamMemberIndex = _teamMembers.Count;
        }
        private void RemoveWormOnDeath(WormController deadWorm)
        {
            deadWorm.OnDeath -= RemoveWormOnDeath;
            _teamMembers.Remove(deadWorm);
            if (!HasPlayers)
            {
                onSurrender?.Invoke(this);
            }
        }
        private void IncrementCurrentPlayer()
        {
            _currentPlayerIndex++;
            _currentPlayerIndex %= _teamMembers.Count;
        }
        public delegate void OnSurrender(TeamManager surrenderingTeam);
        public OnSurrender onSurrender;
       
      
    }
}
