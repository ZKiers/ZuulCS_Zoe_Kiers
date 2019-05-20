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
        private int health;
        public Player()
        {
            health = 100;
        }
        public void heal(int amount)
        {
            health += amount;
        }
        public void damage(int amount)
        {
            health -= amount;
        }
        public bool isAlive()
        {
            if(health <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
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
