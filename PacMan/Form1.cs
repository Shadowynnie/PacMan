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
            blinky = new Ghost("blinky", 240, 220, 8, false, Properties.Resources.blinky_right, redGhostBox);
            clyde = new Ghost("clyde", 270, 220, 8, false, Properties.Resources.clyde_right, orangeGhostBox);
            inky = new Ghost("inky", 300, 220, 8, false, Properties.Resources.inky_right, inkyGhostBox);
            pinky = new Ghost("pinky", 330, 220, 8, false, Properties.Resources.pinky_right, pinkyGhostBox);
            currentSpeed = pacman.GetSpeed();

            label2.Visible = false;

            pacman.charShow();
            blinky.charShow();
            clyde.charShow();
            inky.charShow();
            pinky.charShow();
            

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
            pacman.setSpeed(currentSpeed);
            pacman.ResumeMoving(); // With the change of the direction, the movement is resumed

            if (e.KeyCode == Keys.Left)
            {
                pacman.goUp = false;
                pacman.goDown = false;
                pacman.goRight = false;
                pacman.goLeft = true;
                pacman.img = Properties.Resources.pacman_left;
            }
            if (e.KeyCode == Keys.Right)
            {
                pacman.goUp = false;
                pacman.goDown = false;
                pacman.goLeft = false;
                pacman.goRight = true;
                pacman.img = Properties.Resources.pacman_right;
            }
            if (e.KeyCode == Keys.Up)
            {
                pacman.goUp = true;
                pacman.goDown = false;
                pacman.goRight = false;
                pacman.goLeft = false;
                pacman.img = Properties.Resources.pacman_up;
            }
            if (e.KeyCode == Keys.Down)
            {
                pacman.goUp = false;
                pacman.goDown = true;
                pacman.goRight = false;
                pacman.goLeft = false;
                pacman.img = Properties.Resources.pacman_down;
            }
            pacman.charShow();
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
            if (pacman.dead)
            {
                timer1.Stop();
                gameTimer.Stop();
                pacman.charBox.Visible = false;
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
            HitObject(pacman.charBox);
            ticks++;
        }

        // Hit object logic
        private void HitObject(PictureBox charBox)
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
                        pacman.setSpeed(8);
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
                        
                        label2.Visible = true;
                        label2.Text = "!!!GAME OVER!!!";
                        
                        pacman.img = Properties.Resources.pacman_death;
                        pacman.charShow();
                        timer1.Interval = 1300;
                        
                        //pacman.charBox.Visible = false;
                        pacman.dead = true;
                        return;
                    }
                }
            }
        }

        // Random choose between special foods and placing it on the game field
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
            blinky.setSpeed(5);
            blinky.charShow();

            clyde.vulnerable = true;
            clyde.setSpeed(5);
            clyde.charShow();

            inky.vulnerable = true;
            inky.setSpeed(5);
            inky.charShow();

            pinky.vulnerable = true;
            pinky.setSpeed(5);
            pinky.charShow();

            vulnerableTimer.Start();
        }

        // Countdown of the ghosts vulnerability
        private void vulnerableTimer_Tick(object sender, EventArgs e)
        {
            if (vulnticks == 10)
            {
                blinky.vulnerable = false;
                blinky.setSpeed(8);

                clyde.vulnerable = false;
                clyde.setSpeed(8);

                inky.vulnerable = false;
                inky.setSpeed(8);

                pinky.vulnerable = false;
                pinky.setSpeed(8);

                GhostsRecover();
                blinky.charShow();
                clyde.charShow();
                inky.charShow();
                pinky.charShow();

                pacman.setSpeed(5);
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
            if (blinky.goUp) 
            {
                blinky.img = Properties.Resources.blinky_up;
            }
            if (blinky.goDown) 
            {
                blinky.img = Properties.Resources.blinky_down;
            }
            if (blinky.goLeft) 
            {
                blinky.img = Properties.Resources.blinky_left;
            }
            if (blinky.goRight) 
            {
                blinky.img = Properties.Resources.blinky_right;
            }

            // Clyde:

            if (clyde.goUp)
            {
                clyde.img = Properties.Resources.clyde_up;
            }
            if (clyde.goDown)
            {
                clyde.img = Properties.Resources.clyde_down;
            }
            if (clyde.goLeft)
            {
                clyde.img = Properties.Resources.clyde_left;
            }
            if (clyde.goRight)
            {
                clyde.img = Properties.Resources.clyde_right;
            }

            // Inky:
            if (inky.goUp)
            {
                inky.img = Properties.Resources.inky_up;
            }
            if (inky.goDown)
            {
                inky.img = Properties.Resources.inky_down;
            }
            if (inky.goLeft)
            {
                inky.img = Properties.Resources.inky_left;
            }
            if (inky.goRight)
            {
                inky.img = Properties.Resources.inky_right;
            }

            // Pinky:
            if (pinky.goUp)
            {
                pinky.img = Properties.Resources.pinky_up;
            }
            if (pinky.goDown)
            {
                pinky.img = Properties.Resources.pinky_down;
            }
            if (pinky.goLeft)
            {
                pinky.img = Properties.Resources.pinky_left;
            }
            if (pinky.goRight)
            {
                pinky.img = Properties.Resources.pinky_right;
            }
        }

    }
}
