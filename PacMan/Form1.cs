// This is a personal academic project. Dear PVS-Studio, please check it.
using PacMan.Characters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacMan
{
    public partial class Form1 : Form
    {
        private int score = 0;
        private int eatenCount = 0;
        private byte lifes = 3;
        private int ticks = 0;
        private Stopwatch gameTimer = new Stopwatch();
        private int vulnticks = 0;
        private int currentSpeed = 0;
        


        // Jidel je 129!!!

        private Pacman pacman;
        private Ghost blinky;
        private Ghost clyde;
        private Ghost inky;
        private Ghost pinky;
        

        public Form1()
        {
            InitializeComponent();
            
            pacman = new Pacman("pacman", 275, 275, 5, false, Properties.Resources.pacman_right,pacmanBox);
            blinky = new Ghost("blinky", 240, 220, 1, false, Properties.Resources.blinky_right, redGhostBox);
            clyde = new Ghost("clyde", 270, 220, 1, false, Properties.Resources.clyde_right, orangeGhostBox);
            inky = new Ghost("inky", 300, 220, 1, false, Properties.Resources.inky_right, inkyGhostBox);
            pinky = new Ghost("pinky", 330, 220, 1, false, Properties.Resources.pinky_right, pinkyGhostBox);
            currentSpeed = pacman.GetSpeed();
            
            label2.Visible = false;

            pacman.CharShow();
            blinky.CharShow();
            clyde.CharShow();
            inky.CharShow();
            pinky.CharShow();

            blinky.GoRight = true;
            clyde.GoUp = true;
            inky.GoLeft = true;
            pinky.GoRight = true;
            

            //Start timers and stopwatch
            gameTimer.Start();
            timer1.Start();

            //label2.Visible = true;
            //label2.Text = countFood().ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            pacman.SetSpeed(currentSpeed);
            pacman.ResumeMoving(); // With the change of the direction, the movement is resumed

            if (e.KeyCode == Keys.Left)
            {
                pacman.GoUp = false;
                pacman.GoDown = false;
                pacman.GoRight = false;
                pacman.GoLeft = true;
                pacman.Img = Properties.Resources.pacman_left;
            }
            if (e.KeyCode == Keys.Right)
            {
                pacman.GoUp = false;
                pacman.GoDown = false;
                pacman.GoLeft = false;
                pacman.GoRight = true;
                pacman.Img = Properties.Resources.pacman_right;
            }
            if (e.KeyCode == Keys.Up)
            {
                pacman.GoUp = true;
                pacman.GoDown = false;
                pacman.GoRight = false;
                pacman.GoLeft = false;
                pacman.Img = Properties.Resources.pacman_up;
            }
            if (e.KeyCode == Keys.Down)
            {
                pacman.GoUp = false;
                pacman.GoDown = true;
                pacman.GoRight = false;
                pacman.GoLeft = false;
                pacman.Img = Properties.Resources.pacman_down;
            }
            pacman.CharShow();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            label1.Text = "Score: " + score; // show the score on the board
            label3.Text = gameTimer.Elapsed.Minutes.ToString() +" : "+ gameTimer.Elapsed.Seconds.ToString();

            //pacman.hitWall = HitWall(pacman.charBox);
            if (eatenCount == 129) 
            {
                label2.Visible = true;
                label2.Text = "!!!YOU WON!!!";
                timer1.Stop();
                gameTimer.Stop();
                return;
            }
            if (pacman.Dead)
            {
                timer1.Stop();
                gameTimer.Stop();
                pacman.CharBox.Visible = false;
                return;
            }
            // Placing special food (cherries and strawberries) every 30 seconds:
            if (ticks == 600)
            {
                PlaceSpecialFood();
                ticks = 0;
            }

            blinky.Move();
            clyde.Move();
            inky.Move();
            pinky.Move();

            pacman.Move();

            HitObject(pacman.CharBox);
            HitObject(blinky.CharBox);
            HitObject(clyde.CharBox);
            HitObject(inky.CharBox);
            HitObject(pinky.CharBox);

            ticks++;
        }

        //===============================Hit object logic======================================
        private void HitObject(PictureBox charBox)
        {
            if (charBox.Tag == "pacman")
            {
                foreach (Control obj in this.Controls)
                {
                    // Wall colisions:
                    if (obj is PictureBox && obj.Tag == "wall")
                    {
                        if (charBox.Bounds.IntersectsWith(obj.Bounds))
                        {
                            pacman.StopMoving(); // Stop moving when the pacman hits the wall
                            return;
                        }
                    
                    }
                    // Small food:
                    if (obj is PictureBox && obj.Tag == "food")
                    {
                        if (charBox.Bounds.IntersectsWith(obj.Bounds))
                        {
                            this.Controls.Remove(obj); // Pacman eats the food, score increases by 10
                            score += 10;
                            eatenCount++;
                            return;
                        }
                    }
                    // Large food (stars):
                    if (obj is PictureBox && obj.Tag == "foodLarge")
                    {
                        if (charBox.Bounds.IntersectsWith(obj.Bounds))
                        {
                            this.Controls.Remove(obj);
                            score += 50;
                            eatenCount++;
                            GhostsVulnerable();
                            pacman.SetSpeed(8);
                            currentSpeed = pacman.GetSpeed();
                            return;
                        }
                    }
                    // Special food (stawberries and cherries):
                    if (obj is PictureBox && obj.Tag == "foodSpecial")
                    {
                        if (charBox.Bounds.IntersectsWith(obj.Bounds))
                        {
                            this.Controls.Remove(obj);
                            score += 100;
                            return;
                        }
                    }
                    // Ghost hit:
                    if (obj is PictureBox && obj.Tag == "ghost")
                    {
                        if (obj.Bounds.IntersectsWith(charBox.Bounds))
                        {
                            if (blinky.vulnerable || clyde.vulnerable || inky.vulnerable || pinky.vulnerable)
                            {
                                blinky.SetPosition(240, 220);
                                this.Controls.Remove(obj);
                                score += 200;
                                return;
                            }
                            else 
                            {
                                label2.Visible = true;
                                label2.Text = "!!!GAME OVER!!!";
                        
                                pacman.Img = Properties.Resources.pacman_death;
                                pacman.CharShow();
                                timer1.Interval = 1300;
                        
                                //pacman.charBox.Visible = false;
                                pacman.Dead = true;
                                return;
                            }

                        }
                    }
                }                
            }
            else // If charbox belongs to a ghost:
            {
                foreach (Control obj in this.Controls) 
                {
                    // Ghost wall colisions:
                    if (obj is PictureBox && obj.Tag == "wall")
                    {
                        if (charBox.Bounds.IntersectsWith(obj.Bounds))
                        {
                            blinky.StopMoving();
                            //clyde.StopMoving();
                            //inky.StopMoving();
                            //pinky.StopMoving();

                            return;
                        }
                    }
                }
            }
        }

        //=======Random choose between special foods and placing it on the game field=========
        private void PlaceSpecialFood() 
        {
            this.Controls.Remove(specialFoodBox);
            Random foodChoice = new Random();
            int choice = foodChoice.Next(2);
            Image img;
            specialFoodBox.Visible = true;
            if (choice == 1)
            {
                img = Properties.Resources.cherries;
            }
            else 
            {
                img = Properties.Resources.strawberry;
            }
            specialFoodBox.Size = img.Size;
            specialFoodBox.Image = img;
            this.Controls.Add(specialFoodBox);
            specialFoodBox.Show();
        }

        private void GhostsVulnerable() 
        {
            vulnerableTimer.Stop();
            vulnticks = 0;

            blinky.vulnerable = true;
            blinky.SetSpeed(5);
            blinky.CharShow();

            clyde.vulnerable = true;
            clyde.SetSpeed(5);
            clyde.CharShow();

            inky.vulnerable = true;
            inky.SetSpeed(5);
            inky.CharShow();

            pinky.vulnerable = true;
            pinky.SetSpeed(5);
            pinky.CharShow();

            vulnerableTimer.Start();
        }

        // Countdown of the ghosts vulnerability
        private void vulnerableTimer_Tick(object sender, EventArgs e)
        {
            if (vulnticks == 10)
            {
                blinky.vulnerable = false;
                blinky.SetSpeed(8);

                clyde.vulnerable = false;
                clyde.SetSpeed(8);

                inky.vulnerable = false;
                inky.SetSpeed(8);

                pinky.vulnerable = false;
                pinky.SetSpeed(8);

                GhostsRecover();

                pacman.SetSpeed(5);
                currentSpeed = pacman.GetSpeed();
                vulnticks = 0;
                vulnerableTimer.Stop();
                return;
            }
            vulnticks++;
        }

        // Just a control method, it will not be used in the main game
        private int countFood()
        {

            int foodCount = 0;
            foreach (Control food in this.Controls)
            {
                if (food is PictureBox && food.Tag == "food" || food is PictureBox && food.Tag == "foodLarge")
                {
                    foodCount++;
                }
            }
            return foodCount;
        }

        private void GhostsRecover()
        {
            //Blinky:
            if (blinky.GoUp) 
            {
                blinky.Img = Properties.Resources.blinky_up;
            }
            if (blinky.GoDown) 
            {
                blinky.Img = Properties.Resources.blinky_down;
            }
            if (blinky.GoLeft) 
            {
                blinky.Img = Properties.Resources.blinky_left;
            }
            if (blinky.GoRight) 
            {
                blinky.Img = Properties.Resources.blinky_right;
            }

            // Clyde:

            if (clyde.GoUp)
            {
                clyde.Img = Properties.Resources.clyde_up;
            }
            if (clyde.GoDown)
            {
                clyde.Img = Properties.Resources.clyde_down;
            }
            if (clyde.GoLeft)
            {
                clyde.Img = Properties.Resources.clyde_left;
            }
            if (clyde.GoRight)
            {
                clyde.Img = Properties.Resources.clyde_right;
            }

            // Inky:
            if (inky.GoUp)
            {
                inky.Img = Properties.Resources.inky_up;
            }
            if (inky.GoDown)
            {
                inky.Img = Properties.Resources.inky_down;
            }
            if (inky.GoLeft)
            {
                inky.Img = Properties.Resources.inky_left;
            }
            if (inky.GoRight)
            {
                inky.Img = Properties.Resources.inky_right;
            }

            // Pinky:
            if (pinky.GoUp)
            {
                pinky.Img = Properties.Resources.pinky_up;
            }
            if (pinky.GoDown)
            {
                pinky.Img = Properties.Resources.pinky_down;
            }
            if (pinky.GoLeft)
            {
                pinky.Img = Properties.Resources.pinky_left;
            }
            if (pinky.GoRight)
            {
                pinky.Img = Properties.Resources.pinky_right;
            }


            blinky.CharShow();
            clyde.CharShow();
            inky.CharShow();
            pinky.CharShow();
        }

    }
}
