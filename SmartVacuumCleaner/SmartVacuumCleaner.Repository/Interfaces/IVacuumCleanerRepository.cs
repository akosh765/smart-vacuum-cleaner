namespace SmartVacuumCleaner.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Repository that also provides the functinality of the so called data access layer.
    /// </summary>
    /// <typeparam name="T">Generic Type.</typeparam>
    public interface IVacuumCleanerRepository<T>
    {
        /// <summary>
        /// Gets or sets the actual instance of the room.
        /// </summary>
        T Room { get; set; }

        /// <summary>
        /// Loads the map from a txt file and constructs a room object.
        /// </summary>
        /// <param name="filepath">The filepath of the map to be loaded.</param>
        /// <returns>IRoom instance.</returns>
        T LoadRoomData(string filepath);
    }
}
