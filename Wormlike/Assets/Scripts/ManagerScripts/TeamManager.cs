using System.Collections.Generic;
using ThirdPersonScripts;

namespace ManagerScripts
{
    public class TeamManager
    {
        private List<WormController> _teamMembers;
        private int _currentPlayerIndex;
        public bool HasPlayers => _teamMembers.Count > 0;
        public TeamManager()
        {
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
            _teamMembers.Add(newTeamMember);
        }

        private void IncrementCurrentPlayer()
        {
            _currentPlayerIndex++;
            _currentPlayerIndex %= _teamMembers.Count;
        }

    
    }
}
