namespace cse210_04.Game.Casting
{

    /// <summary>
    /// <para>A rock or geode.</para>
    /// <para>
    /// The responsibility of an Mineral is to provide a point value corresponding with itself.
    /// </para>
    /// </summary>
    public class Mineral: Actor
    {
        private int points = 0;
        private string mineralType = "";
        /// <summary>
        /// Constructs a new instance of Mineral.
        /// </summary>
        public Mineral()
        {

        }

        /// <summary>
        /// Gets the mineral's point value.
        /// </summary>
        /// <returns>The point value as an int.</returns>
        public int GetPoints()
        {
            if (mineralType == "rock")
            {
                points = -1;
            }
            else if (mineralType = "gem")
            {
                points = 1;
            }
            else
            {
                points = 0;
            }

            return points;
        }

        /// <summary>
        /// Sets the mineral's type to either .
        /// </summary>
        /// <param name="mineralType">The given mineral type.</param>
        public void SetMineral(string mineralType)
        {
            if (mineralType == "*")
            {
                this.mineralType = "gem";
            }
            else
            {
                this.mineralType = "rock";
            }
        }

    }

}