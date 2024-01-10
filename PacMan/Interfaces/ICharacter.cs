using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacMan.Interfaces
{
    internal interface ICharacter
    {
        /// <summary>
        /// Properties
        /// </summary>
        
        //Basic atributes
        string name { get; set; } // Name used in the tag of the oject picture box
        int X { get; set; } //Position X
        int Y { get; set; } // Position Y
        int speed { get; set; } // Movement speed
        bool dead { get; set; } // flag if pacman is dead or alive

        //Directions:
        bool goUp { get; set; }
        bool goDown { get; set; }
        bool goLeft { get; set; }
        bool goRight { get; set; }
        //bool hitWall { get; set; }

        Image img { get; set; } // The character gif image to show in the picture box
        PictureBox charBox { get; set; } // PictureBox to show the pacman image

        /// <summary>
        /// Methods
        /// </summary>
        void Move();
        void charShow();
        void setSpeed(int speed);
        int GetSpeed();
        void setPosition(int x, int y);

    }
}
