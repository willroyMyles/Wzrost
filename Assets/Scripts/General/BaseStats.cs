using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.General
{
    public class BaseStats
    {
        public float hp = 10;
        public float speed = 10;
        public float attackRate = 3;
        public float attack = 10;
        public float defense = 10;
        public float recoveryRate = 1;
        public float level = 1;
      
        public BaseStats()
        {

        }
        public void ApplyMultiplier(BaseStats b)
        {
            speed *= b.speed;
            attackRate *= b.attackRate;
            attack *= b.attack;
            defense *= b.defense;
            recoveryRate *= b.recoveryRate;
        }

        public void SetLevel(float lvl)
        {
            level = lvl;
        }

        public void LevelUp(float amount)
        {
            level += amount;
        }
    }
}
