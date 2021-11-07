using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipLiteLibrary.Models
{
    public enum GridSpotStatus
    {
        Empty,
        Ship,
        Miss,
        Hit,
        Sunk
    }

    public enum GameStatus
    {
        Ongoing,
        Draw,
        End
    }

    public enum CurrentPlayer
    {
        PlayerOne,
        PlayerTwo
    }
}
