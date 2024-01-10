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
        public string Name { get; set; } // Name used in the tag of the picture box
        public int X { get; set; } //Position X
        public int Y { get; set; } // Position Y
        public int Speed { get; set; } // Movement speed
        public bool Dead { get; set; } // Dead / Alive

        //Directions:
        public bool GoUp { get; set; }
        public bool GoDown { get; set; }
        public bool GoLeft { get; set; }
        public bool GoRight { get; set; }
        //public bool hitWall { get; set; }

        public bool CanMove { get; set; } = true;

        //Character displaying atributes
        public Image Img { get; set; } // The character gif image to show in the picture box
        public PictureBox CharBox { get; set; } // The object's picture box to show the character image
        
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
                if (GoUp)
                {
                    Y -= Speed;
                }
                if (GoDown)
                {
                    Y += Speed;
                }
                if (GoRight)
                {
                    X += Speed;
                }
                if (GoLeft)
                {
                    X -= Speed;
                }
                SetPosition(X, Y);
            }
            else 
            {
                if (GoUp) 
                {
                    //setPosition(X, Y + 16);
                    Speed = -Speed;
                    Y -= Speed;
                    GoUp = false;
                    
                }
                if (GoDown) 
                {
                    //setPosition(X, Y - 16);
                    Speed = -Speed;
                    Y += Speed;
                    GoDown = false;
                }
                if (GoRight) 
                {
                    //setPosition(X - 16, Y);
                    Speed = -Speed;
                    X += Speed;
                    GoRight = false;
                }
                if (GoLeft) 
                {
                    //setPosition(X + 16, Y);
                    Speed = -Speed;
                    X -= Speed;
                    GoLeft = false;
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
        public void SetPosition(int x, int y) 
        {
            CharBox.Location = new Point(x, y);
        }

        // Sets the object's picture box for showing the pacman in the form
        public void CharShow() 
        {
            CharBox.Height = Img.Height;
            CharBox.Width = Img.Width;
            CharBox.Location = new Point(X, Y);
            CharBox.Tag = Name;
            CharBox.Visible = true;
            CharBox.Image = Img;
            CharBox.Show();
        }
        public void SetSpeed(int speed) 
        {
            this.Speed = speed;
        }
        public int GetSpeed() 
        {
            return Speed; 
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
            this.Name = name;
            X = x;
            Y = y;
            this.Speed = speed;
            this.Dead = dead;
            this.Img = img;
            this.CharBox = charBox;
        }
    }
}
