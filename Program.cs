using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using cse210_04.Game.Casting;
using cse210_04.Game.Directing;
using cse210_04.Game.Services;


namespace cse210_04
{
    /// <summary>
    /// The program's entry point.
    /// </summary>
    class Program
    {
        private static int FRAME_RATE = 12;
        private static int MAX_X = 900;
        private static int MAX_Y = 600;
        private static int CELL_SIZE = 15;
        private static int FONT_SIZE = 30;
        private static int COLS = 60;
        private static int ROWS = 40;
        private static Color WHITE = new Color(255, 255, 255);
        private static int DEFAULT_MINERALS = 40;
        private static string CAPTION = "GREED";


        /// <summary>
        /// Starts the program using the given arguments.
        /// </summary>
        /// <param name="args">The given arguments.</param>
        static void Main(string[] args)
        {
            // create the cast
            Cast cast = new Cast();

            // create the banner
            Actor banner = new Actor();
            banner.SetText("0");
            banner.SetFontSize(FONT_SIZE);
            banner.SetColor(WHITE);
            banner.SetPosition(new Point(CELL_SIZE, 0));
            cast.AddActor("banner", banner);

            // create the robot
            Actor robot = new Actor();
            robot.SetText("#");
            robot.SetFontSize(FONT_SIZE);
            robot.SetColor(WHITE);
            int col = 30;
            int row = 36;
            Point start = new Point(col, row);
            start = start.Scale(CELL_SIZE);
            robot.SetPosition(start);
            cast.AddActor("robot", robot);

            // create the minerals
            Random random = new Random();
            for (int i = 0; i < DEFAULT_MINERALS; i++)
            {
                string[] minerals = {"o", "*"};
                int iMineral = random.Next(2);
                string text = minerals[iMineral].ToString();

                int x = random.Next(1, COLS);
                int y = random.Next(1, ROWS);
                Point position = new Point(x, y);
                position = position.Scale(CELL_SIZE);

                int r = random.Next(0, 256);
                int g = random.Next(0, 256);
                int b = random.Next(0, 256);
                Color color = new Color(r, g, b);

                Mineral mineral = new Mineral();
                mineral.SetText(text);
                mineral.SetFontSize(FONT_SIZE);
                mineral.SetColor(color);
                mineral.SetPosition(position);
                mineral.SetMineral(text);
                cast.AddActor("minerals", mineral);
            }

            // start the game
            KeyboardService keyboardService = new KeyboardService(CELL_SIZE);
            VideoService videoService 
                = new VideoService(CAPTION, MAX_X, MAX_Y, CELL_SIZE, FRAME_RATE, false);
            Director director = new Director(keyboardService, videoService);
            director.StartGame(cast);
        }
    }
}