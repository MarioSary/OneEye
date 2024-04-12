using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCommand : Command
{
    public Player player;
    
    public DashCommand(Player player, KeyCode key) : base(key)
    {
        this.player = player;
    }

    public override void GetKeyDown()
    {
        player.Actions.DashCall();
    }
}
