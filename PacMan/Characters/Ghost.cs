using PacMan.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PacMan.Characters
{
    internal class Ghost : ICharacter
    {
        /// <summary>
        /// Atributes
        /// </summary>
        
        // Basic atributes
        public string name { get; set; }
        public int X { get; set; } //Position X
        public int Y { get; set; } // Position Y
        public int speed { get; set; } // Movement speed
        public bool dead { get; set; }
        public bool vulnerable { get; set; } = false;

        // Directions:
        public bool goUp { get; set; }
        public bool goDown { get; set; }
        public bool goLeft { get; set; }
        public bool goRight { get; set; }
        public bool hitWall { get; set; }

        //Images:
        public Image img { get; set; } // The character gif image to show in the picture box
        public PictureBox charBox { get; set; }

        /// <summary>
        /// Methods
        /// </summary>
        public void Move()
        {
            //Move logic
            bool[] directions = {goUp, goDown, goLeft, goRight};
            if (goUp)
            {
                Y -= speed;
            }
            if (goDown)
            {
                Y += speed;
            }
            if (goRight)
            {
                X += speed;
            }
            if (goLeft)
            {
                X -= speed;
            }
            setPosition(X, Y);

        }

        public void setPosition(int x, int y) 
        { }

        public void charShow() 
        {
            if (vulnerable) 
            {
                img = Properties.Resources.ghosts_vulnerable;
            }
            charBox.Height = img.Height;
            charBox.Width = img.Width;
            charBox.Location = new Point(X, Y);
            charBox.Visible = true;
            charBox.Image = img;
            charBox.Show();
        }
        public void setSpeed(int speed)
        {
            this.speed = speed;
        }
        public int GetSpeed()
        {
            return speed;
        }

        public Ghost(string name, int x, int y, int speed, bool dead, Image img, PictureBox charBox)
        {
            this.name = name;
            X = x;
            Y = y;
            this.speed = speed;
            this.dead = dead;
            this.img = img;
            this.charBox = charBox;
        }
    }
}
