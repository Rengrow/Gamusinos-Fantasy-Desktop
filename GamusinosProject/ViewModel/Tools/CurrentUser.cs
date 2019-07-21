using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamusinosProject.Model;

namespace GamusinosProject.ViewModel.Tools
{
    public static class CurrentUser
    {
        public static long? id;

        public static long currentLvl(Player player)
        {
            return ((player.force - 5) + ((player.vitality - 20) / 5) + (player.resistance - 5)
                + (player.speed - 5) + (player.luck - 1)) + 1;
        }
    }
}