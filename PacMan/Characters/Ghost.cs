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
        public string Name { get; set; }
        public int X { get; set; } //Position X
        public int Y { get; set; } // Position Y
        public int Speed { get; set; } // Movement speed
        public bool Dead { get; set; }
        public bool vulnerable { get; set; } = false;

        // Directions:
        public bool GoUp { get; set; }
        public bool GoDown { get; set; }
        public bool GoLeft { get; set; }
        public bool GoRight { get; set; }
        public bool CanMove { get; set; } = true;

        //Images:
        public Image Img { get; set; } // The character gif image to show in the picture box
        public PictureBox CharBox { get; set; }

        /// <summary>
        /// Methods
        /// </summary>
        public void Move()
        {
            bool[] directions = {GoUp, GoDown, GoLeft, GoRight};
            Random rndMove = new Random();
            //Move logic
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
                ResetRandomDirection();
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

        public void SetPosition(int x, int y) 
        {
            CharBox.Location = new Point(x, y);
        }

        //=========================================================================================
        public void ResetRandomDirection() 
        {
            Random rndMove = new Random();
            int rndDir = 0; //0 -> GoUp; 1 -> GoDown; 2 -> GoLeft; 3 -> GoRight

            // Reset ghost direction while going up
            if (GoUp)
            {
                // Until his current direction is GoUp
                while (rndDir == 0)
                {
                    rndDir = rndMove.Next(4);
                }
                switch (rndDir) 
                {
                    case 1: 
                        GoUp = false;
                        GoDown = true;
                        GoLeft = false;
                        GoRight = false;
                    return;
                    case 2:
                        GoUp = false;
                        GoDown = false;
                        GoLeft = true;
                        GoRight = false;
                    return;
                    case 3:
                        GoUp = false;
                        GoDown = false;
                        GoLeft = false;
                        GoRight = true;
                    return;
                }

                
            }
            // Reset ghost direction while going down
            if (GoDown)
            {
                // Until his current direction is GoDown
                while (rndDir == 1)
                {
                    rndDir = rndMove.Next(4);
                }
                switch (rndDir)
                {
                    case 0:
                        GoUp = true;
                        GoDown = false;
                        GoLeft = false;
                        GoRight = false;
                    return;
                    case 2:
                        GoUp = false;
                        GoDown = false;
                        GoLeft = true;
                        GoRight = false;
                    return;
                    case 3:
                        GoUp = false;
                        GoDown = false;
                        GoLeft = false;
                        GoRight = true;
                    return;
                }
            }
            // Reset ghost direction while going left
            if (GoLeft)
            {
                // Until his current direction is GoLeft
                while (rndDir == 2)
                {
                    rndDir = rndMove.Next(4);
                }
                switch (rndDir)
                {
                    case 0:
                        GoUp = true;
                        GoDown = false;
                        GoLeft = false;
                        GoRight = false;
                        return;
                    case 1:
                        GoUp = false;
                        GoDown = true;
                        GoLeft = false;
                        GoRight = false;
                        return;
                    case 3:
                        GoUp = false;
                        GoDown = false;
                        GoLeft = false;
                        GoRight = true;
                        return;
                }
            }
            // Reset ghost direction while going right
            if (GoRight)
            {
                // Until his current direction is GoRight
                while (rndDir == 3)
                {
                    rndDir = rndMove.Next(4);
                }
                switch (rndDir)
                {
                    case 0:
                        GoUp = true;
                        GoDown = false;
                        GoLeft = false;
                        GoRight = false;
                        return;
                    case 1:
                        GoUp = false;
                        GoDown = true;
                        GoLeft = false;
                        GoRight = false;
                        return;
                    case 2:
                        GoUp = false;
                        GoDown = false;
                        GoLeft = true;
                        GoRight = false;
                        return;
                }
            }
        }

        //=======================================================================================
        public void CharShow() 
        {
            if (vulnerable) 
            {
                Img = Properties.Resources.ghosts_vulnerable;
            }
            CharBox.Height = Img.Height;
            CharBox.Width = Img.Width;
            CharBox.Location = new Point(X, Y);
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

        public Ghost(string name, int x, int y, int speed, bool dead, Image img, PictureBox charBox)
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
