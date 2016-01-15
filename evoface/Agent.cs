using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms; //for the panel manipulation

namespace EvolutionaryFaceForm
{
    class Agent
    {

        //Chromosome initialized with 19 nucleotides
        public Chromosome agentChromosome = new Chromosome(19);

        //WORLD (Panel) DIMENSION
        private int DIMENSION = 100;

        //FACE
        private Color FaceColor = Color.FromArgb(255, 255, 255);
        private Rectangle FaceDimensionRectangle;
        private int FaceShape = 0;   // 0=rectangle; 1=circle; 2=polygon

        //EYES
        private Color EyesColor = Color.FromArgb(255, 255, 255);
        private Rectangle EyeBulbRectangle;
        private Rectangle LeftEyeDimensionRectangle;
        private Rectangle RightEyeDimensionRectangle;

        //EARS
        private Rectangle EarDimensionRectangle;

        //MOUTH
        private Rectangle MouthRectangle;


        public Agent GenerateOffspring(double mutation)
        {
            Agent offspring = new Agent();
            offspring.agentChromosome = this.agentChromosome.GetClone();
            offspring.agentChromosome.Mutate(mutation);
            return offspring;
        }


        public bool DrawTheAgent(Panel PanelWhereDraw)
        {

            //Each byte into the Chromosome represents
            //a feature or a color.
            //
            //byte[0] = Face Shape ( 0=rectangle; 1=circle; 2=polygon )
            //byte[1] = Face Dimension
            //byte[2], byte[3], byte[4] = Face color RGB
            //byte[5] = Eye Shape ( 0=rectngle; 1=circle )
            //byte[6] = Eye Dimension
            //byte[7] = Eye Position (Y axe)
            //byte[8] = Eyes Distance (X axe)
            //byte[9], byte[10], byte[11] = Eye Color RGB
            //byte[12] = Mouth Shape ( 0=No; 1=rectangle; 2=circle )
            //byte[13] = Mouth Dimension
            //byte[14] = Mouth Position
            //byte[15], byte[16], byte[17] = Mouth Color RGB
            //byte[18] = ears Dimension

            using (Graphics g = PanelWhereDraw.CreateGraphics())
            {
                g.Clear(Color.LightGray);
                
                //Draw Face
                FaceShape = agentChromosome.GetNucleotide(0, 0, 2);
                FaceColor = Color.FromArgb(agentChromosome.GetNucleotide(2), agentChromosome.GetNucleotide(3), agentChromosome.GetNucleotide(4));
                FaceDimensionRectangle.Width = agentChromosome.GetNucleotide(1, 30, 80); // DecodeDna(DNA[1], 30, 80);
                FaceDimensionRectangle.Height = FaceDimensionRectangle.Width;
                FaceDimensionRectangle.X = (DIMENSION / 2) - (FaceDimensionRectangle.Width / 2);
                FaceDimensionRectangle.Y = (DIMENSION / 2) - (FaceDimensionRectangle.Height / 2);
                //0 - Face Shape Selector
                if (FaceShape == 0)
                {
                    g.FillRectangle(new SolidBrush(FaceColor), FaceDimensionRectangle);
                }
                else if (FaceShape == 1)
                {
                    g.FillEllipse(new SolidBrush(FaceColor), FaceDimensionRectangle);
                }

                //Ears must be drawn before the Eyes otherwise they cover them
                //18 - Draw Ears Left
                EarDimensionRectangle.Width = agentChromosome.GetNucleotide(18, 0, 30); //DecodeDna(DNA[18], 0, 30);
                EarDimensionRectangle.Height = EarDimensionRectangle.Width;
                EarDimensionRectangle.X = FaceDimensionRectangle.X - (EarDimensionRectangle.Width / 2);
                EarDimensionRectangle.Y = FaceDimensionRectangle.Y + (FaceDimensionRectangle.Height / 2) - EarDimensionRectangle.Width / 2;
                g.FillEllipse(new SolidBrush(FaceColor), EarDimensionRectangle);
                //18 Draw Ears Right
                EarDimensionRectangle.X += FaceDimensionRectangle.Width;
                g.FillEllipse(new SolidBrush(FaceColor), EarDimensionRectangle);

                //Draw Eyes
                EyesColor = Color.FromArgb(agentChromosome.GetNucleotide(9), agentChromosome.GetNucleotide(10), agentChromosome.GetNucleotide(11)); //Color.FromArgb((int)DNA[9], (int)DNA[10], (int)DNA[11]);
                //Left Eye
                LeftEyeDimensionRectangle.Height = agentChromosome.GetNucleotide(6, 5, 15); //DecodeDna(DNA[6], 5, 15);
                LeftEyeDimensionRectangle.Width = LeftEyeDimensionRectangle.Height;
                LeftEyeDimensionRectangle.X = FaceDimensionRectangle.X + agentChromosome.GetNucleotide(8, 0, FaceDimensionRectangle.X / 3); //DecodeDna(DNA[8], 0, FaceDimensionRectangle.X / 3);
                LeftEyeDimensionRectangle.Y = FaceDimensionRectangle.Y + agentChromosome.GetNucleotide(7, 0, FaceDimensionRectangle.Y / 3); //DecodeDna(DNA[7], 5, 25);
                //Draw Left Eye
                int EyeShape = agentChromosome.GetNucleotide(5, 0, 2);
                if (EyeShape == 0) //rectangle
                {
                    LeftEyeDimensionRectangle.Height -= 3;
                    LeftEyeDimensionRectangle.Width = LeftEyeDimensionRectangle.Height;
                    EyeBulbRectangle.Height = LeftEyeDimensionRectangle.Height + 6;
                    EyeBulbRectangle.Width = EyeBulbRectangle.Height;
                    EyeBulbRectangle.X = LeftEyeDimensionRectangle.X - 3;
                    EyeBulbRectangle.Y = LeftEyeDimensionRectangle.Y - 3;
                    g.FillEllipse(new SolidBrush(Color.White), EyeBulbRectangle);
                    g.FillRectangle(new SolidBrush(EyesColor), LeftEyeDimensionRectangle);
                }
                else if (EyeShape == 1) //circle
                {
                    EyeBulbRectangle.Height = LeftEyeDimensionRectangle.Height + 6;
                    EyeBulbRectangle.Width = EyeBulbRectangle.Height;
                    EyeBulbRectangle.X = LeftEyeDimensionRectangle.X - 3;
                    EyeBulbRectangle.Y = LeftEyeDimensionRectangle.Y - 3;
                    g.FillEllipse(new SolidBrush(Color.White), EyeBulbRectangle);
                    g.FillEllipse(new SolidBrush(EyesColor), LeftEyeDimensionRectangle);
                }

                //RIght Eye
                RightEyeDimensionRectangle = LeftEyeDimensionRectangle;
                RightEyeDimensionRectangle.X = FaceDimensionRectangle.Width + FaceDimensionRectangle.X;
                RightEyeDimensionRectangle.X -= RightEyeDimensionRectangle.Width;
                RightEyeDimensionRectangle.X -= LeftEyeDimensionRectangle.X - FaceDimensionRectangle.X;
                //Draw Right Eye
                if (EyeShape == 0)
                {
                    EyeBulbRectangle.X = RightEyeDimensionRectangle.X - 3;
                    EyeBulbRectangle.Y = RightEyeDimensionRectangle.Y - 3;
                    g.FillEllipse(new SolidBrush(Color.White), EyeBulbRectangle);
                    g.FillRectangle(new SolidBrush(EyesColor), RightEyeDimensionRectangle);
                }
                else if (EyeShape == 1)
                {
                    EyeBulbRectangle.X = RightEyeDimensionRectangle.X - 3;
                    EyeBulbRectangle.Y = RightEyeDimensionRectangle.Y - 3;
                    g.FillEllipse(new SolidBrush(Color.White), EyeBulbRectangle);
                    g.FillEllipse(new SolidBrush(EyesColor), RightEyeDimensionRectangle);
                }

                //Mouth
                int MouthShape = agentChromosome.GetNucleotide(12, 0, 3);

                if (MouthShape == 0) //Nothing
                {
                  
                }else if (MouthShape == 1) //rectangle
                {
                    MouthRectangle.Height = 5;
                    MouthRectangle.Width = 10;
                    MouthRectangle.X = (FaceDimensionRectangle.X + FaceDimensionRectangle.Width / 2) - MouthRectangle.Width / 2;
                    MouthRectangle.Y = FaceDimensionRectangle.Y + FaceDimensionRectangle.Height - MouthRectangle.Height;
                    g.FillRectangle(new SolidBrush(Color.DarkRed), MouthRectangle);
                }
                else if (MouthShape == 2) //circle
                {
                    MouthRectangle.Height = 10;
                    MouthRectangle.Width = 10;
                    MouthRectangle.X = (FaceDimensionRectangle.X + FaceDimensionRectangle.Width / 2) - MouthRectangle.Width / 2;
                    MouthRectangle.Y = FaceDimensionRectangle.Y + FaceDimensionRectangle.Height - MouthRectangle.Height;
                    g.FillEllipse(new SolidBrush(Color.DarkRed), MouthRectangle);
                }


                }

            return true;
        }

    }
}
