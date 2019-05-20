using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{
    class Player
    {
        private Room currentRoom;
        public Player()
        {
        }
        public void setCurrentRoom(Room input)
        {
            currentRoom = input;
        }
        public Room getCurrentRoom()
        {
            return currentRoom;
        }
    }
}
