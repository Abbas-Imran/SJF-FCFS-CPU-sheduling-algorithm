using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Windows_Form_SJF_Algo
{
    public partial class Form1 : Form
    {
        String[] PNo = new String[20];
        int[] AT = new int[20], BT = new int[20], CT = new int[20], TAT = new int[20], WT = new int[20];
        public Form1()
        {
            InitializeComponent();
        }
        public int counter = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            AT[counter] = Convert.ToInt32(numericUpDown1.Value);
            BT[counter] = Convert.ToInt32(numericUpDown2.Value);
            PNo[counter] = "P["+Convert.ToString(counter)+"]";
            dataGridView1.Rows.Add();
            dataGridView1.Rows[counter].Cells[0].Value = "P[" + Convert.ToString(counter) + "]";
            dataGridView1.Rows[counter].Cells[1].Value = numericUpDown1.Value;
            dataGridView1.Rows[counter].Cells[2].Value = numericUpDown2.Value;
            counter++;
        }

        public void BubbleSort()
        {
            int temp = 0;
            string tempo = "";
            for (int j = 0; j < counter-1; j++)
            {
                for (int i = 0; i < counter-1; i++)
                {
                    if (AT[i] > AT[i + 1])
                    {
                        tempo = PNo[i + 1];
                        PNo[i + 1] = PNo[i];
                        PNo[i] = tempo;

                        temp = AT[i + 1];
                        AT[i + 1] = AT[i];
                        AT[i] = temp;

                        temp = BT[i + 1];
                        BT[i + 1] = BT[i];
                        BT[i] = temp;
                    }
                }
            }
        }

        public string Grant_Chart(bool Flag)
        {
            string grant = "";
            if (Flag == false)
            {
                for (int i = counter - 1; i >= 0; i--)
                {
                    grant += PNo[i] + ",";
                }
            }
            else
            {
                for (int i = 0 ; i < counter; i++)
                {
                    grant += PNo[i] + ",";
                }
                
            }
            return grant;
        }

        

        public float Avg(int[] arra)
        {
            float Average = 0;
            for(int i = 0;i<arra.Length;i++)
            {
                Average += arra[i];
            }
            return Average/counter;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            counter = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void Sjf()
        {
            BubbleSort();
            int num, temp = 0, min = 0;

            temp = BT[0] + AT[0];
            CT[0] = temp;
            TAT[0] = CT[0] - AT[0];
            WT[0] = TAT[0] - BT[0];

            for (int i = 1; i < counter; i++)
            {
                min = i;
                //find min
                for (int j = i; j < counter; j++)
                {
                    if (AT[j] > temp)
                    {
                        break;
                    }
                    if (BT[j] < BT[min])
                    {
                        min = j;
                    }
                    if (BT[j] == BT[min])
                    {
                        if (AT[j] < AT[min])
                        {
                            min = j;
                        }
                    }

                }

                if (AT[min] > temp)
                {
                    num = AT[min] - temp;
                    temp += num;
                }

                temp = BT[min] + temp;
                CT[min] = temp;
                TAT[min] = CT[min] - AT[min];
                WT[min] = TAT[min] - BT[min];

                int temporary = 0;
                string tempo;

                for (int k = min; k > 0; k--)
                {
                    tempo = PNo[k];
                    PNo[k] = PNo[k - 1];
                    PNo[k - 1] = tempo;

                    temporary = AT[k];
                    AT[k] = AT[k - 1];
                    AT[k - 1] = temporary;

                    temporary = BT[k];
                    BT[k] = BT[k - 1];
                    BT[k - 1] = temporary;

                    temporary = CT[k];
                    CT[k] = CT[k - 1];
                    CT[k - 1] = temporary;

                    temporary = TAT[k];
                    TAT[k] = TAT[k - 1];
                    TAT[k - 1] = temporary;

                    temporary = WT[k];
                    WT[k] = WT[k - 1];
                    WT[k - 1] = temporary;


                }
               
            }
        }
        public void FCFS()
        {
            BubbleSort();
            
            int num,temp=0;
            for (int j = 0; j < counter-1; j++)
            {
                if (AT[j] > temp)
                {
                    num = AT[j] - temp;
                    temp += num;
                }
                temp = BT[j] + temp;

                CT[j] = temp;
                TAT[j] = CT[j] - AT[j];
                WT[j] = TAT[j] - BT[j];
            }
        }
        
        public void Insert_in_datagrid()
        {
            for(int i =0; i<= counter-1; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = PNo[i];
                dataGridView1.Rows[i].Cells[1].Value = AT[i];
                dataGridView1.Rows[i].Cells[2].Value = BT[i];
                dataGridView1.Rows[i].Cells[3].Value = CT[i];
                dataGridView1.Rows[i].Cells[4].Value = TAT[i];
                dataGridView1.Rows[i].Cells[5].Value = WT[i];
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                FCFS();
                Insert_in_datagrid();
                label7.Text = Convert.ToString(Avg(WT));
                label8.Text = Convert.ToString(Avg(TAT));
                textBox1.Text = Grant_Chart(true);
            }
            if (radioButton2.Checked)
            {
                Sjf();
                Insert_in_datagrid();
                label7.Text = Convert.ToString(Avg(WT));
                label8.Text = Convert.ToString(Avg(TAT));
                textBox1.Text = Grant_Chart(false);
            }
            
        }
    }
}
