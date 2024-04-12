using UnityEngine;

namespace _2__Scripts.SampleScene.CommandPattern
{
    public class AttackCommand : Command
    {
        public Player player;
    
        public AttackCommand(Player player, KeyCode key) : base(key)
        {
            this.player = player;
        }

        public override void GetKeyDown()
        {
            player.Actions.Attack();
        }
    }
}