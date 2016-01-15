using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EvolutionaryFaceForm
{
    public partial class Form1 : Form
    {

        double MUTATION_PROBABILITY = 1.0;

        Agent First = new Agent();

        List<Agent> agentList = new List<Agent>();
        List<Panel> panelList = new List<Panel>();



        public Form1()
        {
            InitializeComponent();

            //Creating newAgents
            for (int i = 0; i < 9; i++)
            {
                Agent myAgent = new Agent();
                agentList.Add(myAgent);
            }

            panelList.Add(panel1);
            panelList.Add(panel2);
            panelList.Add(panel3);
            panelList.Add(panel4);
            panelList.Add(panel5);
            panelList.Add(panel6);
            panelList.Add(panel7);
            panelList.Add(panel8);
            panelList.Add(panel9);

        }


        private void button2_Click(object sender, EventArgs e)
        {

            //Randomizing all the agent and drawing them
            for(int i=0; i < 9; i++)
            {
                agentList[i].agentChromosome.Randomize();
                agentList[i].DrawTheAgent(panelList[i]);
            }

            textBox1.Text = "Oh Great! A new population was created...\r\n" + "Select an agent to generate his offsprings.";
        }

 
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

            MUTATION_PROBABILITY = (double)numericUpDown1.Value;
            label1.Text = "Mutation Probability: " + MUTATION_PROBABILITY + "%";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "Mutation Probability: " + MUTATION_PROBABILITY + "%";
            textBox1.Text = "Welcome! Push the button below to generate a new random population";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            agentList[0].DrawTheAgent(panelList[0]);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            agentList[1].DrawTheAgent(panelList[1]);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            agentList[2].DrawTheAgent(panelList[2]);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            agentList[3].DrawTheAgent(panelList[3]);
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            agentList[4].DrawTheAgent(panelList[4]);
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            agentList[5].DrawTheAgent(panelList[5]);
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {
            agentList[6].DrawTheAgent(panelList[6]);
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {
            agentList[7].DrawTheAgent(panelList[7]);
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {
            agentList[8].DrawTheAgent(panelList[8]);
        }



        private void PanelOrganizer(int selectedPanel, List<Panel> panelList)
        {

            for (int i=0; i<panelList.Count; i++)
             {
                if (i == selectedPanel)
                {
                    agentList[i].DrawTheAgent(panelList[i]);
                    panelList[i].BorderStyle = BorderStyle.FixedSingle;
                }
                else {

                    panelList[i].BorderStyle = BorderStyle.None;
                    agentList[i] = agentList[selectedPanel].GenerateOffspring(MUTATION_PROBABILITY);
                    agentList[i].DrawTheAgent(panelList[i]);
                }
            }
        }


        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            PanelOrganizer(0, panelList);
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            PanelOrganizer(1, panelList);
        }

        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            PanelOrganizer(2, panelList);
        }

        private void panel4_MouseClick(object sender, MouseEventArgs e)
        {
            PanelOrganizer(3, panelList);
        }

        private void panel5_MouseClick(object sender, MouseEventArgs e)
        {
            PanelOrganizer(4, panelList);
        }

        private void panel6_MouseClick(object sender, MouseEventArgs e)
        {
            PanelOrganizer(5, panelList);
        }

        private void panel7_MouseClick(object sender, MouseEventArgs e)
        {
            PanelOrganizer(6, panelList);
        }

        private void panel8_MouseClick(object sender, MouseEventArgs e)
        {
            PanelOrganizer(7, panelList);
        }

        private void panel9_MouseClick(object sender, MouseEventArgs e)
        {
            PanelOrganizer(8, panelList);
        }

    }
}