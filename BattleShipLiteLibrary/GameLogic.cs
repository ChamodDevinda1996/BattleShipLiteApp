using BattleShipLiteLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipLiteLibrary
{
    public static class GameLogic
    {

        public static void InitializeShipLocations(string player, PlayerInfoModel model, int numberOfBattleShips)
        {
            var count = 0;
            while (count < numberOfBattleShips)
            {
                Console.WriteLine($"{player} Please enter placeing for battleship number {model.ShipLocations.Count + 1}");
                string input = Console.ReadLine();
                if (isVaildLocation(input))
                {
                    string firstCharacter = input.Substring(0, 1);
                    string secondCharacter = input.Substring(1);

                    model.ShipLocations.Add(new GridSpotModel()
                    {
                        SpotLetter = firstCharacter,
                        SpotNumber = int.Parse(secondCharacter),
                        Status = GridSpotStatus.Ship
                    });
                    count++;
                }
            }
        }

        public static GameStatus Play(CurrentPlayer player, PlayerInfoModel player1, PlayerInfoModel player2)
        {
            GameStatus status = GameStatus.Ongoing;
            if (player == CurrentPlayer.PlayerOne)
            {
                LaunghAttackOnOpponent(player,player1,player2,status);
                status = player2.ShipLocations.All(x => x.Status == GridSpotStatus.Sunk) ? GameStatus.End : GameStatus.Ongoing;
            }
            else
            {
                LaunghAttackOnOpponent(player,player2, player2, status);
                status = player1.ShipLocations.All(x => x.Status == GridSpotStatus.Sunk) ? GameStatus.End : GameStatus.Ongoing;
            }

            return status;
        }

        private static void LaunghAttackOnOpponent(CurrentPlayer curplayer, PlayerInfoModel player,PlayerInfoModel opponent, GameStatus status)
        {
            string cordinates="s5" ;

            while (!isVaildLocation(cordinates))
            {
                string currentplayer = (int)curplayer == 1 ? "Player One" : "Player Two";
                Console.Write($"{currentplayer} Please enter the cordinates: ");
                cordinates = Console.ReadLine();
            }

            if (CheckForOpponentShipHit(opponent,cordinates))
            {
                player.ShotGrid.Add(new GridSpotModel()
                {
                    SpotLetter = cordinates.Substring(0, 1),
                    SpotNumber = int.Parse(cordinates.Substring(1)),
                    Status = GridSpotStatus.Hit

                });
            }
            else
            {
                player.ShotGrid.Add(new GridSpotModel()
                {
                    SpotLetter = cordinates.Substring(0, 1),
                    SpotNumber = int.Parse(cordinates.Substring(1)),
                    Status = GridSpotStatus.Miss
                });
            }


        }

        private static bool CheckForOpponentShipHit(PlayerInfoModel opponent, string cordinates)
        {
            return opponent.ShipLocations.Any(x => x.SpotLetter == cordinates.Substring(0, 1) && x.SpotNumber == int.Parse(cordinates.Substring(1)) && x.Status == GridSpotStatus.Ship);
        }

        public static GameStatus StopGame(CurrentPlayer player)
        {
            throw new NotImplementedException();
        }

        public static CurrentPlayer SwithchPlayer(CurrentPlayer player)
        {
            return player = (player == CurrentPlayer.PlayerOne) ? CurrentPlayer.PlayerTwo : CurrentPlayer.PlayerOne;
            
        }

        private static bool isVaildLocation(string input)
        {
            if (input.Length < 3)
            {
                string firstCharacter = input.Substring(0, 1);
                string secondCharacter = input.Substring(1);
                var letterArr = new string[] { "A","B","C","D","E" };
                var numberArr = new string[] { "1","2","3","4","5" };
                if (letterArr.Any(x => x.Equals(firstCharacter.ToUpper())) && numberArr.Any(x => x.Equals(secondCharacter.ToUpper())))
                {
                    return true;
                }
            }
            return false;
        }

        public static void InitializeShotGrid(PlayerInfoModel model)
        {
            List<string> spotLetters = new List<string>() { 
            "A","B","C","D","E"
            };
            List<int> spotNumbers = new List<int>()
            {
                1,2,3,4,5
            };

            foreach (var letter in spotLetters)
            {
                foreach (var number in spotNumbers)
                {
                    model.ShotGrid.Add(new GridSpotModel()
                    {
                        SpotLetter = letter,
                        SpotNumber = number,
                        Status = GridSpotStatus.Empty
                    });
                }
            }

        }

    }
}
