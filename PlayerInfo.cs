using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bischExe
{
    public class PlayerDataAddr
    {
        public int yaw = 0x40;
        public int pitch = 0x44;
        public int xPos = 0x4;
        public int yPos = 0x8;
        public int zPos = 0xc;
        public int health = 0xf8;

        public PlayerDataAddr()
        {
        }

    }

    public struct PlayerData
    {
        public int baseAddress;

        public PlayerDataAddr offsets;
    }

    public struct PlayerDataVec
    {
        public float yaw;
        public float pitch;
        public float xPos;
        public float yPos;
        public float zPos;
        public int health;
    }
}
