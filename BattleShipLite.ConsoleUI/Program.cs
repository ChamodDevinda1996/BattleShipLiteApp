using BattleShipLiteLibrary;
using BattleShipLiteLibrary.Models;
using System; 
using System.Collections.Generic;

namespace BattleShipLite.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {

            DisplayWellcomeMessage();
            
            PlayerInfoModel Player1 = CreatePlayer("Player 1");
            PlayerInfoModel Player2 = CreatePlayer("Player 2");

            GameStatus status = GameStatus.Ongoing;
            CurrentPlayer player = CurrentPlayer.PlayerOne;
            while (status != GameStatus.End || status != GameStatus.Draw)
            {
                status = GameLogic.Play(player,Player1,Player2);
                if (status == GameStatus.Ongoing)
                    player = GameLogic.SwithchPlayer(player);
                else
                    GameLogic.StopGame(player);
            }

        }

        private static PlayerInfoModel CreatePlayer(string player)
        {
            PlayerInfoModel output = new PlayerInfoModel()
            {
                Name = "",
                ShipLocations = new List<GridSpotModel>(),
                ShotGrid = new List<GridSpotModel>()
            };

            AskForUserName(player, output);

            GameLogic.InitializeShotGrid(output);

            GameLogic.InitializeShipLocations(player, output, 5);

            Console.Clear();

            return output;
        }

        private static void AskForUserName(string player, PlayerInfoModel output)
        {
            Console.WriteLine($"{player}   Enter your first Name: ");
            output.Name = Console.ReadLine();
        }

        private static void DisplayWellcomeMessage()
        {
            Console.WriteLine("\t\tWellcome To The BattleShip Lite.");
        }
    }
}
