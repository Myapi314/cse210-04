using System.Collections.Generic;
using cse210_04.Game.Casting;
using cse210_04.Game.Services;


namespace cse210_04.Game.Directing
{
    /// <summary>
    /// <para>A person who directs the game.</para>
    /// <para>
    /// The responsibility of a Director is to control the sequence of play.
    /// </para>
    /// </summary>
    public class Director
    {
        private KeyboardService keyboardService = null;
        private VideoService videoService = null;
        private int points = 0;

        /// <summary>
        /// Constructs a new instance of Director using the given KeyboardService and VideoService.
        /// </summary>
        /// <param name="keyboardService">The given KeyboardService.</param>
        /// <param name="videoService">The given VideoService.</param>
        public Director(KeyboardService keyboardService, VideoService videoService)
        {
            this.keyboardService = keyboardService;
            this.videoService = videoService;
        }

        /// <summary>
        /// Starts the game by running the main game loop for the given cast.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void StartGame(Cast cast)
        {
            videoService.OpenWindow();
            while (videoService.IsWindowOpen())
            {
                GetInputs(cast);
                DoUpdates(cast);
                DoOutputs(cast);
            }
            videoService.CloseWindow();
        }

        /// <summary>
        /// Gets directional input from the keyboard and applies it to the robot.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void GetInputs(Cast cast)
        {
            Actor robot = cast.GetFirstActor("robot");
            Point velocity = keyboardService.GetDirection();
            robot.SetVelocity(velocity);
        }

        /// <summary>
        /// Updates the robot's position and resolves any collisions with Minerals.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void DoUpdates(Cast cast)
        {
            Actor banner = cast.GetFirstActor("banner");
            Actor robot = cast.GetFirstActor("robot");
            List<Actor> minerals = cast.GetActors("minerals");

            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();
            robot.MoveNext(maxX, maxY);

            Point velocity = new Point (0, 1);
            velocity = velocity.Scale(15);

            foreach (Actor actor in minerals)
            {
                actor.SetVelocity(velocity);
                actor.MoveNext(maxX, maxY);
                Random random = new Random();
                Point actorPosition = actor.GetPosition();

                if (robot.GetPosition().Equals(actorPosition))
                {
                    Mineral mineral = (Mineral) actor;
                    points += mineral.GetPoints();
                    cast.RemoveActor("minerals", actor);
                    string message = points.ToString();
                    banner.SetText(message);

                    int x = random.Next(1, 60);
                    int y = 1;

                    Point position = new Point(x, y);
                    position = position.Scale(15);
                    actor.SetPosition(position);
                    cast.AddActor("minerals", actor);

                }
                
                // Implements minerals randomizing their position 
                // after passing the player.
                int bottomY = actorPosition.GetY();
                int newX = random.Next(1, 60);
                if (bottomY >= 580)
                {
                    Point position = new Point(newX, 1);
                    position = position.Scale(15);
                    actor.SetPosition(position);
                }
                
            } 
        }

        /// <summary>
        /// Draws the actors on the screen.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void DoOutputs(Cast cast)
        {
            List<Actor> actors = cast.GetAllActors();
            videoService.ClearBuffer();
            videoService.DrawActors(actors);
            videoService.FlushBuffer();
        }

    }
}