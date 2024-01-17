using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace car_racing_game
{
    public partial class Form1 : Form
    {
        //17/01/2024 RADona
        //https://www.youtube.com/watch?v=YIgQg_AAIIU&ab_channel=MooICT

        int roadSpeed;
        int trafficSpeed;
        int playerSpeed = 12;
        int score;
        int carImage;


        Random rand = new Random(); //Gets a new AI car onto the screen
        Random carPosition = new Random(); //Position of the AI cars on screen

        bool goleft, goright;




        public Form1()
        {
            InitializeComponent();
            ResetGame();    
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void keyisdown(object sender, KeyEventArgs e)
        {

            if(e.KeyCode == Keys.Left)
            {
                goleft = true;
            }
            if(e.KeyCode == Keys.Right)
            {
                goright = true;
            }
            //when key is pressed change values to true.

        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }
            //when key is let go/unmpressed, change values to false.
        }

        private void gameTimerEvent(object sender, EventArgs e)
        {

            txtScore.Text = "Score: " + score;
            score++;



            if(goleft == true && player.Left > 10)
            {
                player.Left -= playerSpeed;
            }

            if(goright == true && player.Left < 300) 
            {
                player.Left += playerSpeed;
            }


            roadTrack1.Top += roadSpeed; //top one
            roadTrack2.Top += roadSpeed; //main one

            if(roadTrack2.Top > 519)
            {
                roadTrack2.Top = -519;
            }

            if (roadTrack1.Top > 519)
            {
                roadTrack1.Top = -519;
            }
            //make the road move 

            AI1.Top += trafficSpeed;
            AI2.Top += trafficSpeed;

            if(AI1.Top > 530)
            {
                //AI1.Top = -20;
                changeAIcars(AI1);
            }

            if(AI2.Top > 530)
            {
                changeAIcars(AI2);
            }

            if(player.Bounds.IntersectsWith(AI1.Bounds) || player.Bounds.IntersectsWith(AI2.Bounds))
            {
                gameOver();
            }

            if(score > 40 && score < 500)
            {
                award.Image = Properties.Resources.bronze;
                roadSpeed = 20;
                trafficSpeed = 22;
            }

            if (score > 500 && score < 2000)
            {
                award.Image = Properties.Resources.silver;
                roadSpeed = 25;
                trafficSpeed = 27;
            }

            if (score > 2000)
            {
                award.Image = Properties.Resources.gold;
                roadSpeed = 27;
                trafficSpeed = 27;
            }



        }

        //custom functions for the game

        private void changeAIcars(PictureBox tempCar)
        {

            carImage = rand.Next(1, 8);
            switch (carImage)
            {
                case 1:
                    tempCar.Image = Properties.Resources.ambulance;
                    break;
                case 2:
                    tempCar.Image= Properties.Resources.carGreen;
                    break;
                case 3:
                    tempCar.Image = Properties.Resources.carOrange;
                    break;
                case 4:
                    tempCar.Image = Properties.Resources.carGrey;
                        break;
                case 5:
                    tempCar.Image = Properties.Resources.carPink;
                    break;
                case 6:
                    tempCar.Image = Properties.Resources.CarRed;
                    break;
                case 7:
                    tempCar.Image = Properties.Resources.carYellow;
                    break;
                case 8:
                    tempCar.Image = Properties.Resources.TruckBlue;
                    break;
                case 9:
                    tempCar.Image = Properties.Resources.TruckWhite;
                    break;
                    //change AI cars to the above randomly
            }

            tempCar.Top = carPosition.Next(100, 400) * -1;


            if((string)tempCar.Tag == "carLeft")
            {
                tempCar.Left = carPosition.Next(5, 200);
            }
            if((string)tempCar.Tag == "carRight")
            {
                tempCar.Left = carPosition.Next(245, 422);
            }
            //moves the AI cars around






        }
        private async void gameOver()
        {
            playSound();
            gameTimer.Stop();
            explosion.Visible = true;
            player.Controls.Add(explosion);
            explosion.Location = new Point(-8, 5);
            explosion.BackColor = Color.Transparent;

            award.Visible = true;
            award.BringToFront();

            buttonStart.Enabled = true;

        }
        private void ResetGame()
        {

            buttonStart.Enabled = false;
            explosion.Visible = false;
            award.Visible = false;
            goright=false;
            goleft=false;
            score=0;
            award.Image = Properties.Resources.bronze;

            roadSpeed = 12;
            trafficSpeed = 15;

            AI1.Top = carPosition.Next(200, 500) * -1;
            //so between 200 and 500 up and down the screen
            //the -1 makes it so if the AI chooses 100
            //itll  become -100 making the car aopear at the top of the screen and
            //move downwards like its driving.
            AI1.Left = carPosition.Next(5, 200);//between 5 and 200 left and right of screen


            AI2.Top = carPosition.Next(200, 500) * -1;
            AI2.Left = carPosition.Next(249, 418);

            gameTimer.Start();

        }

        private void roadTrack2_Click(object sender, EventArgs e)
        {

        }

        private void roadTrack1_Click(object sender, EventArgs e)
        {

        }

        private void restartGame(object sender, EventArgs e)
        {
            ResetGame();
        }

        private void playSound()
        {
            System.Media.SoundPlayer playCrash = new System.Media.SoundPlayer(Properties.Resources.hit);
            //calls audio
            playCrash.Play();
        }


    }
}
