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
        string Name { get; set; } // Name used in the tag of the oject picture box
        int X { get; set; } //Position X
        int Y { get; set; } // Position Y
        int Speed { get; set; } // Movement speed
        bool Dead { get; set; } // flag if pacman is dead or alive

        //Directions:
        bool GoUp { get; set; }
        bool GoDown { get; set; }
        bool GoLeft { get; set; }
        bool GoRight { get; set; }
        //bool hitWall { get; set; }

        bool CanMove { get; set; }

        Image Img { get; set; } // The character gif image to show in the picture box
        PictureBox CharBox { get; set; } // PictureBox to show the pacman image

        /// <summary>
        /// Methods
        /// </summary>
        void Move();
        void StopMoving();
        void ResumeMoving();
        void CharShow();
        void SetSpeed(int speed);
        int GetSpeed();
        void SetPosition(int x, int y);

    }
}
