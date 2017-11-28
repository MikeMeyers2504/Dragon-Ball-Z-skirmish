using SdlDotNet.Core;
using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SdlDotNet.Input;

namespace Consoleapplicatie_gamedev
{
    public class Manager
    {
        //Hier wordt de muziek aangemaakt
        System.Media.SoundPlayer music = new System.Media.SoundPlayer();

        //Deze booleans dienen voor te zien of de vijanden dood zijn en om de kogel te volgen waar hij is
        public bool vijand1Dead;
        public bool vijand2Dead;
        public bool vijand3Dead;
        public bool vijand4Dead;
        public bool vijand5Dead;
        public bool vijand6Dead;
        public bool vijand7Dead;
        public bool vijand8Dead;
        public bool vijand9Dead;
        public bool vijand10Dead;
        public bool bullethit;
        public bool bulletOutScreen;
        //Dit is voor het scherm
        public Surface mVideo;
        //Hier wordt alles gedeclareerd
        private Hero hero;
        Vijand vijand;
        Vijand vijand2;
        Vijand vijand3;
        Vijand vijand4;
        Vijand vijand5;
        Vijand vijand6;
        Vijand vijand7;
        Vijand vijand8;
        Vijand vijand9;
        Vijand vijand10;
        level level1, activeLevel, level2;
        Health player;
        HealthEnemies enemyOne;
        HealthEnemies enemyTwo;
        HealthEnemies enemyThree;
        HealthEnemies enemyFour;
        HealthEnemies enemyFive;
        HealthEnemies enemySix;
        HealthEnemies enemySeven;
        HealthEnemies enemyAight;
        HealthEnemies enemyNine;
        HealthEnemies enemyTen;
        Charge PlayerCharge;
        Bullet bullet;
        
        //Hier worden nog wat afbeeldingen ingeladen
        Surface afbeeldingLevel1 = new Surface("earth.jpg");
        Surface afbeeldingLevel2 = new Surface("LookoutAir.jpg");
        Surface WinningGame = new Surface("balls.jpg");
        Surface LoseGame = new Surface("gameover.jpg");
        Surface m_FontForegroundSurface;
        Surface m_FontForegroundSurface1;
        Surface gameOver;

        //Hier wordt de font van de teksten ingeladen
        SdlDotNet.Graphics.Font font = new SdlDotNet.Graphics.Font(@"Fonts/pacifico/Pacifico.ttf", 75);
        //Deze bools dienen voor de fasen van het spel
        bool intro = true;
        bool gameover = false;
        public bool runningGame = false;
        bool gamewon = false;
        //Hier worden wat afbeeldingen ingeladen
        Surface deur = new Surface("deur.png");
        Surface flag = new Surface("finish flag.png");
        //Hier zijn collisions
        Rectangle colDeur;
        Rectangle colFlag;

        public Manager()
        {
            //Hier wordt de muziek ingeladen
            music.SoundLocation = "Gohan/DragonBZ.wav";
            //Hier wordt het scherm aangemaakt
            mVideo = Video.SetVideoMode(1500, 800, false, false,
            false, true);
            //Hier worden de hero, alle vijanden, de levens en de chargebar aangemaakt op het scherm
            hero = new Hero(mVideo);
            vijand = new Vijand(mVideo, 100, 730);
            vijand2 = new Vijand(mVideo, 1425, 610);
            vijand3 = new Vijand(mVideo, 100, 490);
            vijand4 = new Vijand(mVideo, 750, 70);
            vijand5 = new Vijand(mVideo, 650, 340);
            vijand6 = new Vijand(mVideo, 100, 730);
            vijand7 = new Vijand(mVideo, 750, 611);
            vijand8 = new Vijand(mVideo, 230, 491);
            vijand9 = new Vijand(mVideo, 750, 71);
            vijand10 = new Vijand(mVideo, 1000, 250);
            player = new Health(mVideo, 1350, 0);
            enemyOne = new HealthEnemies(mVideo, 0, 0);
            enemyTwo = new HealthEnemies(mVideo, 0, 20);
            enemyThree = new HealthEnemies(mVideo, 0, 40);
            enemyFour = new HealthEnemies(mVideo, 0, 60);
            enemyFive = new HealthEnemies(mVideo, 0, 80);
            enemySix = new HealthEnemies(mVideo, 0, 0);
            enemySeven = new HealthEnemies(mVideo, 0, 20);
            enemyAight = new HealthEnemies(mVideo, 0, 40);
            enemyNine = new HealthEnemies(mVideo, 0, 60);
            enemyTen = new HealthEnemies(mVideo, 0, 80);
            PlayerCharge = new Charge(mVideo, 1100, 30);
            bullet = new Bullet(mVideo, hero.position.X, hero.position.Y);
            //Deze dienen voor de levels en te beslissen welk level actief is
            level1 = new level1(mVideo);
            level2 = new level2(mVideo);
            activeLevel = level1;
            //Dit zijn de events van de muis en het spel
            Events.MouseMotion += new EventHandler<MouseMotionEventArgs>(ApplicationMouseMotionEventHandler);
            Events.MouseButtonDown += new EventHandler<MouseButtonEventArgs>(ApplicationMouseButtonEventHandler);
            Events.Quit += Events_Quit;
            Events.Tick += Events_Tick;
            Events.Run();
        }
        //Dit is voor de positie van de muis
        public Point cursorPosition;
        public Point lastCursorPos;

        public virtual void ApplicationMouseButtonEventHandler(object sender, MouseButtonEventArgs args)
        {
            //Met dit gaan we op de tekst start game kunnen drukken met de linkerknop van de muis waardoor het spel start en wordt de muziek ook gestart
            if (intro == true && gameover == false && args.Button == MouseButton.PrimaryButton && cursorPosition.X >= 300 && cursorPosition.X <= 300 + m_FontForegroundSurface1.Width && cursorPosition.Y >= 350 && cursorPosition.Y <= 350 + m_FontForegroundSurface1.Height)
            {
                Music();
                runningGame = true;
                intro = false;
            }
        }

        private void Music()
        {
            //De muziek blijft spelen tot het spel afgesloten word
            music.PlayLooping();
        }

        private void ApplicationMouseMotionEventHandler(object sender, MouseMotionEventArgs args)
        {
            //Because the Visual Pointer of the Cursor is 6 pixels inside
            //Dient voor de positie van de muis
            cursorPosition = args.Position;
            cursorPosition.X -= 6; 
            lastCursorPos = cursorPosition;
        }

        private bool GetBoundingBoxCollisionStatus()
        {
            if (hero.colRectangle.IntersectsWith(vijand.colRectangle))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void AttackswithinReach()
        {
            //Hier wordt er bepaald welke aanvallen de vijanden moeten doen wanneer de hero dichtbij is
            if (activeLevel == level1)
            {
                if (vijand1Dead == false)
                {
                    if (hero.position.Y == 709 && hero.position.X >= vijand.position.X && hero.position.X <= vijand.position.X + 175 && vijand.Elektrikschok == false)
                    {
                        vijand.Psybeam = true;
                    }
                    if (hero.position.Y == 709 && hero.position.X >= vijand.position.X - 50 && hero.position.X <= vijand.position.X + 75)
                    {
                        vijand.Elektrikschok = true;
                    }
                }
                if (vijand2Dead == false)
                {
                    if (hero.position.Y == 589 && hero.position.X >= vijand2.position.X && hero.position.X <= vijand2.position.X + 175 && vijand2.Elektrikschok == false)
                    {
                        vijand2.Psybeam = true;
                    }
                    if (hero.position.Y == 589 && hero.position.X >= vijand2.position.X - 50 && hero.position.X <= vijand2.position.X + 75)
                    {
                        vijand2.Elektrikschok = true;
                    }
                }
                if (vijand3Dead == false)
                {
                    if (hero.position.Y == 469 && hero.position.X >= vijand3.position.X && hero.position.X <= vijand3.position.X + 175 && vijand3.Elektrikschok == false)
                    {
                        vijand3.Psybeam = true;
                    }
                    if (hero.position.Y == 469 && hero.position.X >= vijand3.position.X - 50 && hero.position.X <= vijand3.position.X + 75)
                    {
                        vijand3.Elektrikschok = true;
                    }
                }
                if (vijand4Dead == false)
                {
                    if (hero.position.Y == 49 && hero.position.X >= vijand4.position.X && hero.position.X <= vijand4.position.X + 175 && vijand4.Elektrikschok == false)
                    {
                        vijand4.Psybeam = true;
                    }
                    if (hero.position.Y == 49 && hero.position.X >= vijand4.position.X - 50 && hero.position.X <= vijand4.position.X + 75)
                    {
                        vijand4.Elektrikschok = true;
                    }
                }
                if (vijand5Dead == false)
                {
                    if (hero.position.Y == 319 && hero.position.X >= vijand5.position.X && hero.position.X <= vijand5.position.X + 175 && vijand5.Elektrikschok == false)
                    {
                        vijand5.Psybeam = true;
                    }
                    if (hero.position.Y == 319 && hero.position.X >= vijand5.position.X - 50 && hero.position.X <= vijand5.position.X + 75)
                    {
                        vijand5.Elektrikschok = true;
                    }
                }
            }
            if (activeLevel == level2)
            {
                if (vijand6Dead == false)
                {
                    if (hero.position.Y == 714 && hero.position.X >= vijand6.position.X && hero.position.X <= vijand6.position.X + 175 && vijand6.Elektrikschok == false)
                    {
                        vijand6.Psybeam = true;
                    }
                    if (hero.position.Y == 714 && hero.position.X >= vijand6.position.X - 50 && hero.position.X <= vijand6.position.X + 100)
                    {
                        vijand6.Elektrikschok = true;
                    }
                }
                if (vijand7Dead == false)
                {
                    if (hero.position.Y == 594 && hero.position.X >= vijand7.position.X && hero.position.X <= vijand7.position.X + 175 && vijand7.Elektrikschok == false)
                    {
                        vijand7.Psybeam = true;
                    }
                    if (hero.position.Y == 594 && hero.position.X >= vijand7.position.X - 50 && hero.position.X <= vijand7.position.X + 100)
                    {
                        vijand7.Elektrikschok = true;
                    }
                }
                if (vijand8Dead == false)
                {
                    if (hero.position.Y == 474 && hero.position.X >= vijand8.position.X && hero.position.X <= vijand8.position.X + 175 && vijand8.Elektrikschok == false)
                    {
                        vijand8.Psybeam = true;
                    }
                    if (hero.position.Y == 474 && hero.position.X >= vijand8.position.X - 50 && hero.position.X <= vijand8.position.X + 100)
                    {
                        vijand8.Elektrikschok = true;
                    }
                }
                if (vijand9Dead == false)
                {
                    if (hero.position.Y == 54 && hero.position.X >= vijand9.position.X && hero.position.X <= vijand9.position.X + 175 && vijand9.Elektrikschok == false)
                    {
                        vijand9.Psybeam = true;
                    }
                    if (hero.position.Y == 54 && hero.position.X >= vijand9.position.X - 50 && hero.position.X <= vijand9.position.X + 100)
                    {
                        vijand9.Elektrikschok = true;
                    }
                }
                if (vijand10Dead == false)
                {
                    if (hero.position.Y == 234 && hero.position.X >= vijand10.position.X && hero.position.X <= vijand10.position.X + 175 && vijand10.Elektrikschok == false)
                    {
                        vijand10.Psybeam = true;
                    }
                    if (hero.position.Y == 234 && hero.position.X >= vijand10.position.X - 50 && hero.position.X <= vijand10.position.X + 100)
                    {
                        vijand10.Elektrikschok = true;
                    }
                }
            }
        }

        private void CollisionAttacksWithEnemies()
        {
            //Hier wordt er nagegaan of dat de aanvallen van de hero de vijanden raken en beslist hoeveel damage het doet door de counter te gebruiken en dan worden de levens van de vijanden geupdate
            if (activeLevel == level1)
            {
                if (vijand1Dead == false)
                {
                    if (hero.colSuperpower.IntersectsWith(vijand.colVijand1) && hero.AnimSuperpower == true)
                    {
                        enemyOne.CounterMeleeOrSuper = 2;
                        enemyOne.Update();
                    }
                    else if (hero.colSuperpowerSecond.IntersectsWith(vijand.colVijand1) && hero.AnimSuperpowerSecond == true)
                    {
                        enemyOne.CounterMeleeOrSuper = 2;
                        enemyOne.Update();
                    }
                    else if (hero.colSuperpowerKick.IntersectsWith(vijand.colVijand1) && hero.Animkick == true)
                    {
                        enemyOne.CounterMeleeOrSuper = 1;
                        enemyOne.Update();
                    }
                    else if (hero.colSuperpowerMelee.IntersectsWith(vijand.colVijand1) && hero.Animmelee == true)
                    {
                        enemyOne.CounterMeleeOrSuper = 1;
                        enemyOne.Update();
                    }
                    else if (bullet.colBullet.IntersectsWith(vijand.colVijand1))
                    {
                        enemyOne.CounterMeleeOrSuper = 3;
                        enemyOne.Update();
                        bullethit = true;
                        bullet.Ball = false;
                    }
                }
                if (vijand2Dead == false)
                {
                    if (hero.colSuperpower.IntersectsWith(vijand2.colVijand2) && hero.AnimSuperpower == true)
                    {
                        enemyTwo.CounterMeleeOrSuper = 2;
                        enemyTwo.Update();
                    }
                    else if (hero.colSuperpowerSecond.IntersectsWith(vijand2.colVijand2) && hero.AnimSuperpowerSecond == true)
                    {
                        enemyTwo.CounterMeleeOrSuper = 2;
                        enemyTwo.Update();
                    }
                    else if (hero.colSuperpowerKick.IntersectsWith(vijand2.colVijand2) && hero.Animkick == true)
                    {
                        enemyTwo.CounterMeleeOrSuper = 1;
                        enemyTwo.Update();
                    }
                    else if (hero.colSuperpowerMelee.IntersectsWith(vijand2.colVijand2) && hero.Animmelee == true)
                    {
                        enemyTwo.CounterMeleeOrSuper = 1;
                        enemyTwo.Update();
                    }
                    else if (bullet.colBullet.IntersectsWith(vijand2.colVijand2))
                    {
                        enemyTwo.CounterMeleeOrSuper = 3;
                        enemyTwo.Update();
                        bullethit = true;
                        bullet.Ball = false;
                    }
                }
                if (vijand3Dead == false)
                {
                    if (hero.colSuperpower.IntersectsWith(vijand3.colVijand3) && hero.AnimSuperpower == true)
                    {
                        enemyThree.CounterMeleeOrSuper = 2;
                        enemyThree.Update();
                    }
                    else if (hero.colSuperpowerSecond.IntersectsWith(vijand3.colVijand3) && hero.AnimSuperpowerSecond == true)
                    {
                        enemyThree.CounterMeleeOrSuper = 2;
                        enemyThree.Update();
                    }
                    else if (hero.colSuperpowerKick.IntersectsWith(vijand3.colVijand3) && hero.Animkick == true)
                    {
                        enemyThree.CounterMeleeOrSuper = 1;
                        enemyThree.Update();
                    }
                    else if (hero.colSuperpowerMelee.IntersectsWith(vijand3.colVijand3) && hero.Animmelee == true)
                    {
                        enemyThree.CounterMeleeOrSuper = 1;
                        enemyThree.Update();
                    }
                    else if (bullet.colBullet.IntersectsWith(vijand3.colVijand3))
                    {
                        enemyThree.CounterMeleeOrSuper = 3;
                        enemyThree.Update();
                        bullethit = true;
                        bullet.Ball = false;
                    }
                }
                if (vijand4Dead == false)
                {
                    if (hero.colSuperpower.IntersectsWith(vijand4.colVijand4) && hero.AnimSuperpower == true)
                    {
                        enemyFour.CounterMeleeOrSuper = 2;
                        enemyFour.Update();
                    }
                    else if (hero.colSuperpowerSecond.IntersectsWith(vijand4.colVijand4) && hero.AnimSuperpowerSecond == true)
                    {
                        enemyFour.CounterMeleeOrSuper = 2;
                        enemyFour.Update();
                    }
                    else if (hero.colSuperpowerKick.IntersectsWith(vijand4.colVijand4) && hero.Animkick == true)
                    {
                        enemyFour.CounterMeleeOrSuper = 1;
                        enemyFour.Update();
                    }
                    else if (hero.colSuperpowerMelee.IntersectsWith(vijand4.colVijand4) && hero.Animmelee == true)
                    {
                        enemyFour.CounterMeleeOrSuper = 1;
                        enemyFour.Update();
                    }
                    else if (bullet.colBullet.IntersectsWith(vijand4.colVijand4))
                    {
                        enemyFour.CounterMeleeOrSuper = 3;
                        enemyFour.Update();
                        bullethit = true;
                        bullet.Ball = false;
                    }
                }
                if (vijand5Dead == false)
                {
                    if (hero.colSuperpower.IntersectsWith(vijand5.colVijand5) && hero.AnimSuperpower == true)
                    {
                        enemyFive.CounterMeleeOrSuper = 2;
                        enemyFive.Update();
                    }
                    else if (hero.colSuperpowerSecond.IntersectsWith(vijand5.colVijand5) && hero.AnimSuperpowerSecond == true)
                    {
                        enemyFive.CounterMeleeOrSuper = 2;
                        enemyFive.Update();
                    }
                    else if (hero.colSuperpowerKick.IntersectsWith(vijand5.colVijand5) && hero.Animkick == true)
                    {
                        enemyFive.CounterMeleeOrSuper = 1;
                        enemyFive.Update();
                    }
                    else if (hero.colSuperpowerMelee.IntersectsWith(vijand5.colVijand5) && hero.Animmelee == true)
                    {
                        enemyFive.CounterMeleeOrSuper = 1;
                        enemyFive.Update();
                    }
                    else if (bullet.colBullet.IntersectsWith(vijand5.colVijand5))
                    {
                        enemyFive.CounterMeleeOrSuper = 3;
                        enemyFive.Update();
                        bullethit = true;
                        bullet.Ball = false;
                    }
                }
            }
            if (activeLevel == level2)
            {
                if (vijand6Dead == false)
                {
                    if (hero.colSuperpower.IntersectsWith(vijand6.colVijand6) && hero.AnimSuperpower == true)
                    {
                        enemySix.CounterMeleeOrSuper = 4;
                        enemySix.Update();
                    }
                    else if (hero.colSuperpowerSecond.IntersectsWith(vijand6.colVijand6) && hero.AnimSuperpowerSecond == true)
                    {
                        enemySix.CounterMeleeOrSuper = 4;
                        enemySix.Update();
                    }
                    else if (hero.colSuperpowerKick.IntersectsWith(vijand6.colVijand6) && hero.Animkick == true)
                    {
                        enemySix.CounterMeleeOrSuper = 5;
                        enemySix.Update();
                    }
                    else if (hero.colSuperpowerMelee.IntersectsWith(vijand6.colVijand6) && hero.Animmelee == true)
                    {
                        enemySix.CounterMeleeOrSuper = 5;
                        enemySix.Update();
                    }
                    else if (bullet.colBullet.IntersectsWith(vijand6.colVijand6))
                    {
                        enemySix.CounterMeleeOrSuper = 6;
                        enemySix.Update();
                        bullethit = true;
                        bullet.Ball = false;
                    }
                }
                if (vijand7Dead == false)
                {
                    if (hero.colSuperpower.IntersectsWith(vijand7.colVijand7) && hero.AnimSuperpower == true)
                    {
                        enemySeven.CounterMeleeOrSuper = 4;
                        enemySeven.Update();
                    }
                    else if (hero.colSuperpowerSecond.IntersectsWith(vijand7.colVijand7) && hero.AnimSuperpowerSecond == true)
                    {
                        enemySeven.CounterMeleeOrSuper = 4;
                        enemySeven.Update();
                    }
                    else if (hero.colSuperpowerKick.IntersectsWith(vijand7.colVijand7) && hero.Animkick == true)
                    {
                        enemySeven.CounterMeleeOrSuper = 5;
                        enemySeven.Update();
                    }
                    else if (hero.colSuperpowerMelee.IntersectsWith(vijand7.colVijand7) && hero.Animmelee == true)
                    {
                        enemySeven.CounterMeleeOrSuper = 5;
                        enemySeven.Update();
                    }
                    else if (bullet.colBullet.IntersectsWith(vijand7.colVijand7))
                    {
                        enemySeven.CounterMeleeOrSuper = 6;
                        enemySeven.Update();
                        bullethit = true;
                        bullet.Ball = false;
                    }
                }
                if (vijand8Dead == false)
                {
                    if (hero.colSuperpower.IntersectsWith(vijand8.colVijand8) && hero.AnimSuperpower == true)
                    {
                        enemyAight.CounterMeleeOrSuper = 4;
                        enemyAight.Update();
                    }
                    else if (hero.colSuperpowerSecond.IntersectsWith(vijand8.colVijand8) && hero.AnimSuperpowerSecond == true)
                    {
                        enemyAight.CounterMeleeOrSuper = 4;
                        enemyAight.Update();
                    }
                    else if (hero.colSuperpowerKick.IntersectsWith(vijand8.colVijand8) && hero.Animkick == true)
                    {
                        enemyAight.CounterMeleeOrSuper = 5;
                        enemyAight.Update();
                    }
                    else if (hero.colSuperpowerMelee.IntersectsWith(vijand8.colVijand8) && hero.Animmelee == true)
                    {
                        enemyAight.CounterMeleeOrSuper = 5;
                        enemyAight.Update();
                    }
                    else if (bullet.colBullet.IntersectsWith(vijand8.colVijand8))
                    {
                        enemyAight.CounterMeleeOrSuper = 6;
                        enemyAight.Update();
                        bullethit = true;
                        bullet.Ball = false;
                    }
                }
                if (vijand9Dead == false)
                {
                    if (hero.colSuperpower.IntersectsWith(vijand9.colVijand9) && hero.AnimSuperpower == true)
                    {
                        enemyNine.CounterMeleeOrSuper = 4;
                        enemyNine.Update();
                    }
                    else if (hero.colSuperpowerSecond.IntersectsWith(vijand9.colVijand9) && hero.AnimSuperpowerSecond == true)
                    {
                        enemyNine.CounterMeleeOrSuper = 4;
                        enemyNine.Update();
                    }
                    else if (hero.colSuperpowerKick.IntersectsWith(vijand9.colVijand9) && hero.Animkick == true)
                    {
                        enemyNine.CounterMeleeOrSuper = 5;
                        enemyNine.Update();
                    }
                    else if (hero.colSuperpowerMelee.IntersectsWith(vijand9.colVijand9) && hero.Animmelee == true)
                    {
                        enemyNine.CounterMeleeOrSuper = 5;
                        enemyNine.Update();
                    }
                    else if (bullet.colBullet.IntersectsWith(vijand9.colVijand9))
                    {
                        enemyNine.CounterMeleeOrSuper = 6;
                        enemyNine.Update();
                        bullethit = true;
                        bullet.Ball = false;
                    }
                }
                if (vijand10Dead == false)
                {
                    if (hero.colSuperpower.IntersectsWith(vijand10.colVijand10) && hero.AnimSuperpower == true)
                    {
                        enemyTen.CounterMeleeOrSuper = 4;
                        enemyTen.Update();
                    }
                    else if (hero.colSuperpowerSecond.IntersectsWith(vijand10.colVijand10) && hero.AnimSuperpowerSecond == true)
                    {
                        enemyTen.CounterMeleeOrSuper = 4;
                        enemyTen.Update();
                    }
                    else if (hero.colSuperpowerKick.IntersectsWith(vijand10.colVijand10) && hero.Animkick == true)
                    {
                        enemyTen.CounterMeleeOrSuper = 5;
                        enemyTen.Update();
                    }
                    else if (hero.colSuperpowerMelee.IntersectsWith(vijand10.colVijand10) && hero.Animmelee == true)
                    {
                        enemyTen.CounterMeleeOrSuper = 5;
                        enemyTen.Update();
                    }
                    else if (bullet.colBullet.IntersectsWith(vijand10.colVijand10))
                    {
                        enemyTen.CounterMeleeOrSuper = 6;
                        enemyTen.Update();
                        bullethit = true;
                        bullet.Ball = false;
                    }
                }
            }
        }

        private void ColisionAttacksWithHero()
        {
            //Hier wordt er nagegaan of dat de aanvallen van de vijanden de hero raakt en dan wordt het leven van de hero geupdate
            if (activeLevel == level1)
            {
                if (vijand1Dead == false)
                {
                    if (hero.colRectangle.IntersectsWith(vijand.colPsyBeam) && vijand.Psybeam == true)
                    {
                        player.Update();
                    }
                    else if (hero.colRectangle.IntersectsWith(vijand.colElektrikShok) && vijand.Elektrikschok == true)
                    {
                        player.Update();
                    }
                }
                if (vijand2Dead == false)
                {
                    if (hero.colRectangle.IntersectsWith(vijand2.colPsyBeam) && vijand2.Psybeam == true)
                    {
                        player.Update();
                    }
                    else if (hero.colRectangle.IntersectsWith(vijand2.colElektrikShok) && vijand2.Elektrikschok == true)
                    {
                        player.Update();
                    }
                }
                if (vijand3Dead == false)
                {
                    if (hero.colRectangle.IntersectsWith(vijand3.colPsyBeam) && vijand3.Psybeam == true)
                    {
                        player.Update();
                    }
                    else if (hero.colRectangle.IntersectsWith(vijand3.colElektrikShok) && vijand3.Elektrikschok == true)
                    {
                        player.Update();
                    }
                }
                if (vijand4Dead == false)
                {
                    if (hero.colRectangle.IntersectsWith(vijand4.colPsyBeam) && vijand4.Psybeam == true)
                    {
                        player.Update();
                    }
                    else if (hero.colRectangle.IntersectsWith(vijand4.colElektrikShok) && vijand4.Elektrikschok == true)
                    {
                        player.Update();
                    }
                }
                if (vijand5Dead == false)
                {
                    if (hero.colRectangle.IntersectsWith(vijand5.colPsyBeam) && vijand5.Psybeam == true)
                    {
                        player.Update();
                    }
                    else if (hero.colRectangle.IntersectsWith(vijand5.colElektrikShok) && vijand5.Elektrikschok == true)
                    {
                        player.Update();
                    }
                }
            }
            if (activeLevel == level2)
            {
                if (vijand6Dead == false)
                {
                    if (hero.colRectangle.IntersectsWith(vijand6.colPsyBeam) && vijand6.Psybeam == true)
                    {
                        player.Update();
                    }
                    else if (hero.colRectangle.IntersectsWith(vijand6.colElektrikShok) && vijand6.Elektrikschok == true)
                    {
                        player.Update();
                    }
                }
                if (vijand7Dead == false)
                {
                    if (hero.colRectangle.IntersectsWith(vijand7.colPsyBeam) && vijand7.Psybeam == true)
                    {
                        player.Update();
                    }
                    else if (hero.colRectangle.IntersectsWith(vijand7.colElektrikShok) && vijand7.Elektrikschok == true)
                    {
                        player.Update();
                    }
                }
                if (vijand8Dead == false)
                {
                    if (hero.colRectangle.IntersectsWith(vijand8.colPsyBeam) && vijand8.Psybeam == true)
                    {
                        player.Update();
                    }
                    else if (hero.colRectangle.IntersectsWith(vijand8.colElektrikShok) && vijand8.Elektrikschok == true)
                    {
                        player.Update();
                    }
                }
                if (vijand9Dead == false)
                {
                    if (hero.colRectangle.IntersectsWith(vijand9.colPsyBeam) && vijand9.Psybeam == true)
                    {
                        player.Update();
                    }
                    else if (hero.colRectangle.IntersectsWith(vijand9.colElektrikShok) && vijand9.Elektrikschok == true)
                    {
                        player.Update();
                    }
                }
                if (vijand10Dead == false)
                {
                    if (hero.colRectangle.IntersectsWith(vijand10.colPsyBeam) && vijand10.Psybeam == true)
                    {
                        player.Update();
                    }
                    else if (hero.colRectangle.IntersectsWith(vijand10.colElektrikShok) && vijand10.Elektrikschok == true)
                    {
                        player.Update();
                    }
                }
            }                      
        }

        private void ChargeAttacks()
        {
            //Deze dienen voor na te zien of dat de aanvallen van de hero gebruikt kunnen worden, dus als de charge bar voldoende breed is
            if (PlayerCharge.visibleRectangleTen.Width >= 200)
            {
                hero.SuperpowerCharge = true;
            }
            else
            {
                hero.SuperpowerCharge = false;
            }

            if (PlayerCharge.visibleRectangleTen.Width >= 150)
            {
                hero.SuperpowerSecondCharge = true;
            }
            else
            {
                hero.SuperpowerSecondCharge = false;
            }

            if (PlayerCharge.visibleRectangleTen.Width >= 50)
            {
                hero.SuperpowerKickCharge = true;
                hero.SuperpowerMeleeCharge = true;
            }
            else
            {
                hero.SuperpowerKickCharge = false;
                hero.SuperpowerMeleeCharge = false;
            }

            if (PlayerCharge.visibleRectangleTen.Width >= 350)
            {
                hero.SuperpowerShotCharge = true;
            }
            else
            {
                hero.SuperpowerShotCharge = false;
            }
        }

        private void VijandenDead()
        {
            //Hier gaan de vijanden dood als hun leven op is
            if (activeLevel == level1)
            {
                if (enemyOne.visibleRectangleOne.Width <= 0)
                {
                    vijand1Dead = true;
                }
                if (enemyTwo.visibleRectangleOne.Width <= 0)
                {
                    vijand2Dead = true;
                }
                if (enemyThree.visibleRectangleOne.Width <= 0)
                {
                    vijand3Dead = true;
                }
                if (enemyFour.visibleRectangleOne.Width <= 0)
                {
                    vijand4Dead = true;
                }
                if (enemyFive.visibleRectangleOne.Width <= 0)
                {
                    vijand5Dead = true;
                }
            }
            if (activeLevel == level2)
            {
                if (enemySix.visibleRectangleOne.Width <= 0)
                {
                    vijand6Dead = true;
                }
                if (enemySeven.visibleRectangleOne.Width <= 0)
                {
                    vijand7Dead = true;
                }
                if (enemyAight.visibleRectangleOne.Width <= 0)
                {
                    vijand8Dead = true;
                }
                if (enemyNine.visibleRectangleOne.Width <= 0)
                {
                    vijand9Dead = true;
                }
                if (enemyTen.visibleRectangleOne.Width <= 0)
                {
                    vijand10Dead = true;
                }
            }         
        }

        private void HealthVijanden()
        {
            //Hier wordt er nagegaan dat zolang het leven niet op is en de vijand niet dood is, de vijand en zijn leven worden getekend 
            if (activeLevel == level1)
            {
                if (enemyTwo.visibleRectangleOne.Width >= 1 && vijand2Dead == false)
                {
                    enemyTwo.Draw();
                    vijand2.Draw();
                }
                if (enemyOne.visibleRectangleOne.Width >= 1 && vijand1Dead == false)
                {
                    enemyOne.Draw();
                    vijand.Draw();
                }
                if (enemyThree.visibleRectangleOne.Width >= 1 && vijand3Dead == false)
                {
                    enemyThree.Draw();
                    vijand3.Draw();
                }
                if (enemyFour.visibleRectangleOne.Width >= 1 && vijand4Dead == false)
                {
                    enemyFour.Draw();
                    vijand4.Draw();
                }
                if (enemyFive.visibleRectangleOne.Width >= 1 && vijand5Dead == false)
                {
                    enemyFive.Draw();
                    vijand5.Draw();
                }
            }
            if (activeLevel == level2)
            {
                if (enemySix.visibleRectangleOne.Width >= 1 && vijand6Dead == false)
                {
                    enemySix.Draw();
                    vijand6.Draw();
                }
                if (enemySeven.visibleRectangleOne.Width >= 1 && vijand7Dead == false)
                {
                    enemySeven.Draw();
                    vijand7.Draw();
                }
                if (enemyAight.visibleRectangleOne.Width >= 1 && vijand8Dead == false)
                {
                    enemyAight.Draw();
                    vijand8.Draw();
                }
                if (enemyNine.visibleRectangleOne.Width >= 1 && vijand9Dead == false)
                {
                    enemyNine.Draw();
                    vijand9.Draw();
                }
                if (enemyTen.visibleRectangleOne.Width >= 1 && vijand10Dead == false)
                {
                    enemyTen.Draw();
                    vijand10.Draw();
                } 
            }     
        }

        private void Events_Tick(object sender, TickEventArgs e)
        {
            //Deze if dient voor het startscherm
            if (intro == true && gameover == false && gamewon == false)
            {
                // Create the Font Surfaces
                m_FontForegroundSurface = font.Render("Dragon Ball Z Skirmish", Color.White);
                m_FontForegroundSurface1 = font.Render("Start game", Color.White);
                mVideo.Blit(m_FontForegroundSurface, new Point(300, 200));
                mVideo.Blit(m_FontForegroundSurface1, new Point(300, 350));
                mVideo.Update();
            }
            else if (runningGame == true && gameover == false && gamewon == false)
            {
                //Deze if voor wanneer het spel bezig is
                //Hier worden alle methodes opgeroepen die boven zijn aangemaakt
                AttackswithinReach();
                ChargeAttacks();
                CollisionAttacksWithEnemies();
                ColisionAttacksWithHero();
                VijandenDead();
                HealthVijanden();
                //Hier wordt er nagegaan of level 1 actief is
                if (activeLevel == level1)
                {
                //Deze if dient voor te zeggen wanneer het game over is
                if (player.visibleRectangle.Width <= 0)
                {
                    runningGame = false;
                    gameover = true;
                }
                    //Deze if tekent de kogel en update deze aan de hand van wat voorwaarden
                if (bullet.Ball == true && bullethit == false && bulletOutScreen == false || bullethit == true && bullet.Ball == true)
                {
                    bullet.Draw();
                    bullet.Update();
                }

                //Hier wordt er beslist wanneer de kogel uit het scherm gaat dat er dan een boolean true gaat
                if (bullet.colBullet.X < mVideo.Width)
                {
                    bulletOutScreen = false;
                }
                else
                {
                    bulletOutScreen = true;
                }
                //Deze dienen voor als 1 van de vijanden hun aanvallen true is dat het dan de vijand update
                if (vijand.Psybeam == true || vijand.Elektrikschok == true)
                {
                    vijand.Update();
                }
                else if (vijand2.Psybeam == true || vijand2.Elektrikschok == true)
                {
                    vijand2.Update();
                }
                else if (vijand3.Psybeam == true || vijand3.Elektrikschok == true)
                {
                    vijand3.Update();
                }
                else if (vijand4.Psybeam == true || vijand4.Elektrikschok == true)
                {
                    vijand4.Update();
                }
                else if (vijand5.Psybeam == true || vijand5.Elektrikschok == true)
                {
                    vijand5.Update();
                }
                //Hier komt er een deur tevoorschijn wanneer alle vijanden van het eerste level dood zijn
                if (vijand1Dead && vijand2Dead && vijand3Dead && vijand4Dead && vijand5Dead)
                {
                    mVideo.Blit(deur, new Point(1450, 185));
                    colDeur = new Rectangle(1450, 185, 40, 84);
                }
                //Wanneer je naar de deur gaat en collisie maakt ga je naar het volgend level
                if (hero.colRectangle.IntersectsWith(colDeur))
                {
                    activeLevel = level2;
                }
                //Hier wordt alles nog getekend en geupdate
                player.Draw();
                PlayerCharge.Draw();
                hero.Update();
                hero.Draw();
                vijand.Update();
                vijand2.Update();
                vijand3.Update();
                vijand4.Update();
                vijand5.Update();
                level1.DrawWorld();
                mVideo.Update();
                mVideo.Blit(afbeeldingLevel1);
                //Deze dienen voor de charge bar te updaten en te zien wat er mee moet gebeuren 
                if (hero.Charge == true)
                {
                    PlayerCharge.tellerChargePlayer = 1;
                    PlayerCharge.Update();
                }
                else if (hero.Superpower == true)
                {
                    PlayerCharge.tellerChargePlayer = 2;
                    hero.Superpower = false;
                    PlayerCharge.Update();
                }
                else if (hero.SuperpowerSecond == true)
                {
                    PlayerCharge.tellerChargePlayer = 3;
                    PlayerCharge.Update();
                    hero.SuperpowerSecond = false;
                }
                else if (hero.melee == true)
                {
                    PlayerCharge.tellerChargePlayer = 4;
                    PlayerCharge.Update();
                    hero.melee = false;
                }
                else if (hero.kick == true)
                {
                    PlayerCharge.tellerChargePlayer = 5;
                    PlayerCharge.Update();
                    hero.kick = false;
                }
                else if (hero.Shot == true)
                {
                    //Hier word de collision van de bullet nog geupdate
                    PlayerCharge.tellerChargePlayer = 6;
                    PlayerCharge.Update();
                    hero.Shot = false;
                    bullet.Ball = true;
                    bullet.position.X = hero.position.X + 60;
                    bullet.position.Y = hero.position.Y + 10;
                    bullet.colBullet.X = hero.position.X + 70;
                    bullet.colBullet.Y = hero.position.Y + 10;
                }
                //Deze dienen voor te zien waar de hero geraakt wordt door de omgeving
                hero.hitUp = false;
                hero.hitUnder = false;
                hero.hitLeft = false;
                hero.hitRight = false;
                hero.hit = false;

                for (int i = 0; i < level1.spriteTileArray.GetLength(0); i++)
                {
                    for (int j = 0; j < level1.spriteTileArray.GetLength(1); j++)
                    {
                        if (level1.intTileArray[i, j] == 1)
                        {
                            if (hero.fall == true)
                            {
                                //Dit dient voor na te zien of dat de hero op een platfrom staat na het vallen
                                if (level1.spriteTileArray[i, j].col.IntersectsWith(hero.colRectangle))
                                {
                                    hero.hit = true;
                                    hero.fall = false;
                                    hero.Air = false; 
                                }
                            }
                            if (level1.spriteTileArray[i, j].col.IntersectsWith(hero.colRectangleLevel))
                            {
                                //Deze dient voor na te zien of dat de hero op een platform staat
                                hero.hit = true;
                                hero.fall = false;
                                hero.position.Y = level1.spriteTileArray[i, j].col.Y - hero.image.Height + 5;
                            }

                            if (level1.spriteTileArray[i, j].col.IntersectsWith(hero.colBoven))
                            {
                                //Hier wordt de hero geraakt langs boven 
                                Console.WriteLine("raak langs boven");
                                hero.hitUp = true;
                            }
                            else if (hero.hit == false)
                            {
                                //Hier gaat de hero vallen
                                hero.fall = true;
                            }

                            else if (hero.left == true)
                            {
                                if (level1.spriteTileArray[i, j].col.IntersectsWith(hero.colLinks))
                                {
                                    //Hier wordt de hero geraakt langs links
                                    Console.WriteLine("raak langs links");
                                    hero.hitLeft = true;
                                }
                            }
                            else if (hero.right == true)
                            {
                                if (level1.spriteTileArray[i, j].col.IntersectsWith(hero.colRechts))
                                {
                                    //Hier wordt de hero geraakt langs rechts
                                    Console.WriteLine("raak langs rechts");
                                    hero.hitRight = true;
                                }
                            }
                        }
                    }
                    }
                }
                if (activeLevel == level2)
                {
                    //Hier wordt er nagegaan of level 2 actief is
                    //Als de hero zijn leven weg is is het game over
                if (player.visibleRectangle.Width <= 0)
                {
                    runningGame = false;
                    gameover = true;
                }

                if (bullet.Ball == true && bullethit == false && bulletOutScreen == false || bullet.Ball == true && bullethit == true && bulletOutScreen == false)
                {
                    bullet.Draw();
                    bullet.Update();
                }

                if (bullet.colBullet.X < mVideo.Width)
                {
                    bulletOutScreen = false;
                }
                else
                {
                    bulletOutScreen = true;
                }

                if (vijand6.Psybeam == true || vijand6.Elektrikschok == true)
                {
                    vijand6.Update();
                }
                else if (vijand7.Psybeam == true || vijand7.Elektrikschok == true)
                {
                    vijand7.Update();
                }
                else if (vijand8.Psybeam == true || vijand8.Elektrikschok == true)
                {
                    vijand8.Update();
                }
                else if (vijand9.Psybeam == true || vijand9.Elektrikschok == true)
                {
                    vijand9.Update();
                }
                else if (vijand10.Psybeam == true || vijand10.Elektrikschok == true)
                {
                    vijand10.Update();
                }
                    //Als de vijanden dood zijn van level 2 verschijnt er een vlag 
                if (vijand6Dead && vijand7Dead && vijand8Dead && vijand9Dead && vijand10Dead)
                {
                    mVideo.Blit(flag, new Point(0, 80));
                    colFlag = new Rectangle(0, 80, 60, 51);
                }
                    //Je moet naar deze vlag gaan om het spel uit te spelen
                if (hero.colRectangle.IntersectsWith(colFlag))
                {
                    gamewon = true;
                    runningGame = false;
                }

                player.Draw();
                PlayerCharge.Draw();
                hero.Update();
                hero.Draw();
                vijand6.Update();
                vijand7.Update();
                vijand8.Update();
                vijand9.Update();
                vijand10.Update();
                level2.DrawWorldSecond();
                mVideo.Update();
                mVideo.Blit(afbeeldingLevel2);

                if (hero.Charge == true)
                {
                    PlayerCharge.tellerChargePlayer = 1;
                    PlayerCharge.Update();
                }
                else if (hero.Superpower == true)
                {
                    PlayerCharge.tellerChargePlayer = 2;
                    hero.Superpower = false;
                    PlayerCharge.Update();
                }
                else if (hero.SuperpowerSecond == true)
                {
                    PlayerCharge.tellerChargePlayer = 3;
                    PlayerCharge.Update();
                    hero.SuperpowerSecond = false;
                }
                else if (hero.melee == true)
                {
                    PlayerCharge.tellerChargePlayer = 4;
                    PlayerCharge.Update();
                    hero.melee = false;
                }
                else if (hero.kick == true)
                {
                    PlayerCharge.tellerChargePlayer = 5;
                    PlayerCharge.Update();
                    hero.kick = false;
                }
                else if (hero.Shot == true)
                {
                    PlayerCharge.tellerChargePlayer = 6;
                    PlayerCharge.Update();
                    hero.Shot = false;
                    bullet.Ball = true;
                    bullet.position.X = hero.position.X + 60;
                    bullet.position.Y = hero.position.Y + 10;
                    bullet.colBullet.X = hero.position.X + 70;
                    bullet.colBullet.Y = hero.position.Y + 10;
                }

                hero.hitUp = false;
                hero.hitUnder = false;
                hero.hitLeft = false;
                hero.hitRight = false;
                hero.hit = false;

                    for (int i = 0; i < level2.spriteCloudArray.GetLength(0); i++)
                    {
                        for (int j = 0; j < level2.spriteCloudArray.GetLength(1); j++)
                        {
                            if (level2.intTileArray[i, j] == 1)
                            {
                                if (hero.fall == true)
                                {
                                    if (level2.spriteCloudArray[i, j].colCloud.IntersectsWith(hero.colRectangle))
                                    {
                                        hero.hit = true;
                                        hero.fall = false;
                                        hero.Air = false; 
                                    }
                                }
                                if (level2.spriteCloudArray[i, j].colCloud.IntersectsWith(hero.colRectangleLevel))
                                {
                                    hero.hit = true;
                                    hero.fall = false;
                                    hero.position.Y = level2.spriteCloudArray[i, j].colCloud.Y - hero.image.Height + 10;
                                }

                                if (level2.spriteCloudArray[i, j].colCloud.IntersectsWith(hero.colBoven))
                                {
                                    Console.WriteLine("raak langs boven");
                                    hero.hitUp = true;
                                }
                                else if (hero.hit == false)
                                {
                                    hero.fall = true;
                                }

                                else if (hero.left == true)
                                {
                                    if (level2.spriteCloudArray[i, j].colCloud.IntersectsWith(hero.colLinks))
                                    {
                                        Console.WriteLine("raak langs links");
                                        hero.hitLeft = true;
                                    }
                                }
                                else if (hero.right == true)
                                {
                                    if (level2.spriteCloudArray[i, j].colCloud.IntersectsWith(hero.colRechts))
                                    {
                                        Console.WriteLine("raak langs rechts");
                                        hero.hitRight = true;
                                    }
                                }
                            }
                        }
                        }
                    
                }
            }
            else if (intro == false && gameover == false && gamewon == true && runningGame == false)
            {
                //Dit is het scherm wanneer je het spel uit hebt gespeeld
                // Create the Font Surfaces
                m_FontForegroundSurface = font.Render("Congratulations", Color.Black);
                m_FontForegroundSurface1 = font.Render("You have won the game !!!", Color.Black);
                mVideo.Blit(WinningGame);
                mVideo.Blit(m_FontForegroundSurface, new Point(300, 200));
                mVideo.Blit(m_FontForegroundSurface1, new Point(300, 350));
                mVideo.Update();
            }
            else
            {
                if (gameover == true && intro == false && runningGame == false && gamewon == false)
                {
                    //Dit dient voor wanneer het game over is
                    gameOver = font.Render("Thanx for playing!", Color.White);
                    mVideo.Blit(LoseGame);
                    mVideo.Blit(gameOver, new Point(400, 600));
                    mVideo.Update();
                    Console.WriteLine(gameover);
                }
            }
        }
        private void Events_Quit(object sender, QuitEventArgs e)
        {
            //Deze sluit de applicatie
            Events.QuitApplication();
        }
    }
}
