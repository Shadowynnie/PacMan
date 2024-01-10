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
    public class Pacman : ICharacter
    {
        /// <summary>
        /// Atributes
        /// </summary>
        
        // Basic atributes
        public string name { get; set; } // Name used in the tag of the picture box
        public int X { get; set; } //Position X
        public int Y { get; set; } // Position Y
        public int speed { get; set; } // Movement speed
        public bool dead { get; set; } // Dead / Alive

        //Directions:
        public bool goUp { get; set; }
        public bool goDown { get; set; }
        public bool goLeft { get; set; }
        public bool goRight { get; set; }
        //public bool hitWall { get; set; }

        public bool CanMove { get; set; } = true;

        //Character displaying atributes
        public Image img { get; set; } // The character gif image to show in the picture box
        public PictureBox charBox { get; set; } // The object's picture box to show the character image
        
        /// <summary>
        /// Methods
        /// </summary>
        
        // Pacman's movement logic:
        public void Move() 
        {
            //int defaultSpeed = 5;
            //speed = defaultSpeed;

            if (CanMove)
            {
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
            else 
            {
                if (goUp) 
                {
                    //setPosition(X, Y + 16);
                    speed = -speed;
                    Y -= speed;
                    goUp = false;
                    
                }
                if (goDown) 
                {
                    //setPosition(X, Y - 16);
                    speed = -speed;
                    Y += speed;
                    goDown = false;
                }
                if (goRight) 
                {
                    //setPosition(X - 16, Y);
                    speed = -speed;
                    X += speed;
                    goRight = false;
                }
                if (goLeft) 
                {
                    //setPosition(X + 16, Y);
                    speed = -speed;
                    X -= speed;
                    goLeft = false;
                }
                //ResumeMoving();
            }

        }

        // Method to stop moving
        public void StopMoving()
        {
            CanMove = false;
        }

        // Method to resume moving
        public void ResumeMoving()
        {
            CanMove = true;
        }

        // Sets the picture box with the pacman at the new position according to a speed increment
        public void setPosition(int x, int y) 
        {
            charBox.Location = new Point(x, y);
        }

        // Sets the object's picture box for showing the pacman in the form
        public void charShow() 
        {
            charBox.Height = img.Height;
            charBox.Width = img.Width;
            charBox.Location = new Point(X, Y);
            charBox.Tag = name;
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

        public int GetX() 
        {
            return X;
        }

        public int GetY() 
        {
            return Y;        
        }

        
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="speed"></param>
        /// <param name="dead"></param>
        /// <param name="img"></param>
        public Pacman(string name, int x, int y, int speed, bool dead, Image img, PictureBox charBox)
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
