using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Models.Cache;

namespace GameServer.Models.Cache
{
    /// <summary>
    /// Stores containers of information.
    /// </summary>
    public class Storage
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Storage()
        {
            this.Lobby = new Lobby();
            this.Mazes = new Mazes();
        }

        /// <summary>
        /// Lobby property.
        /// </summary>
        /// <value>Lobby.</value>
        public Lobby Lobby { get; private set; }

        /// <summary>
        /// Mazes property.
        /// </summary>
        /// <value>Mazes.</value>
        public Mazes Mazes { get; private set; }
    }
}