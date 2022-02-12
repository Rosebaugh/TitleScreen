using System;
using System.Collections.Generic;
using System.Text;

namespace TitleScreen
{

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
            Shoot = 3,
            Completed = 4
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
        }
        public static GameState State { get; set; } = GameState.TitleScreen;
        private static Tutorial tutorial = 0;
        public static Areas CurrentScreen = Areas.Blank;
        //public static Areas NextScreen;

        public static int ScreenWidth;
        public static int ScreenHeight;


        public static void NewArea()
        {
            switch(State)
            {
                case GameState.TitleScreen:
                    CurrentScreen = Areas.Blank;
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
                case Tutorial.Shoot:
                    tutorial += 1;
                    break;
            }
            return Areas.Blank;
        }
        private static Areas newRandomArea()
        {
            //Still need to do
            return Areas.Blank;
        }
    }
}
