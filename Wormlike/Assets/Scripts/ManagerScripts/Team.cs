using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team
{
    private List<ThirdPersonMovementController> _teamMembers;
    private int _currentPlayerIndex;
    public bool HasPlayers => _teamMembers.Count > 0;
    public Team()
    {
        _teamMembers = new List<ThirdPersonMovementController>();
        _currentPlayerIndex = -1;
    }
    public ThirdPersonMovementController NextTeamMember()
    {
        IncrementCurrentPlayer();
        return _teamMembers[_currentPlayerIndex];

    }
    public void RegisterPlayer(ThirdPersonMovementController newTeamMember)
    {
        _teamMembers.Add(newTeamMember);
    }

    private void IncrementCurrentPlayer()
    {
        _currentPlayerIndex++;
        _currentPlayerIndex %= _teamMembers.Count;
    }

    
}
