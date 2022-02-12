using System;
using System.Collections.Generic;
using System.Text;

namespace TitleScreen
{
    public enum Treasure
    {
        Empty = 0,
        Gun = 1,
        Coin = 2
    }
    public enum Direction
    {
        Down = 0,
        Right = 1,
        Up = 2,
        Left = 3
        /*
        Up = 2,
        Down = 0,
        Left = 3,
        Right = 1
        */
    }
    public enum SpawnLocation
    {
        Right = 0,
        Center = 1,
        Left = 2
    }
    public static class ScreenValues
    {
        public enum GameState 
        { 
            TitleScreen = 0,
            PauseMenu = 1,
            Tutorial = 2,
            Free = 3
        }

        public enum Tutorial
        {
            Scroll = 0,
            NewTerrain = 1,
            CollectItems = 2,
            PickUp = 3,
            Shoot = 4,
            Fight = 5,
            Completed = 6
        }

        public enum Areas 
        { 
            Blank,
            Bosses,
            Store,
            Chest,
            Trader,
            Wall,
        }

        public static void ResetClass()
        {
            State = GameState.TitleScreen;
            tutorial = 0;
            CurrentScreen = Areas.Blank;
            ScreenWidth = 0;
            ScreenHeight = 0;
            SickmanSpawnLocation = SpawnLocation.Center;
        }
        public static GameState State { get; set; } = GameState.TitleScreen;
        public static Tutorial tutorial = 0;
        public static Areas CurrentScreen = Areas.Blank;
        public static SpawnLocation SickmanSpawnLocation = SpawnLocation.Center;
        //public static Areas NextScreen;

        public static int ScreenWidth;
        public static int ScreenHeight;


        public static void NewArea()
        {
            switch(State)
            {
                case GameState.TitleScreen:
                    CurrentScreen = Areas.Blank;
                    State = GameState.Tutorial;
                    break;
                case GameState.Tutorial:
                    CurrentScreen = newTutorial();
                    break;
                case GameState.Free:
                    CurrentScreen = newRandomArea();
                    break;
            }
        }

        private static Areas newTutorial()
        {
            //Need to test
            switch (tutorial)
            {
                case Tutorial.Scroll:
                    tutorial += 1; 
                    break;
                case Tutorial.NewTerrain:
                    tutorial += 1;
                    break;
                case Tutorial.CollectItems:
                    tutorial += 1;
                    break;
                case Tutorial.PickUp:
                    tutorial += 1;
                    break;
                case Tutorial.Shoot:
                    tutorial += 1;
                    break;
                case Tutorial.Fight:
                    tutorial += 1;
                    State = GameState.Free;
                    break;
            }
            return Areas.Blank;
        }
        private static Areas newRandomArea()
        {
            Random rnd = new Random();
            int roll = (short)rnd.Next(0, 100);
            if (roll >= 0 || roll < 69)         //Bosses
            {
                return Areas.Bosses;
            }
            if (roll >= 69 || roll < 84)         //Store
            {
                return Areas.Store;
            }
            if (roll >= 84 || roll < 94)         //Trader
            {
                return Areas.Trader;
            }
            if (roll >= 94 || roll < 99)         //Wall
            {
                return Areas.Wall;
            }
            if (roll >= 99 || roll < 100)         //Money
            {
                return Areas.Chest;
            }
            return Areas.Blank;
        }
    }
}
