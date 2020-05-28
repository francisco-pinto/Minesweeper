using System;
using Minesweeper.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Menu = Minesweeper.Models.Menu; //diferenciar o menu classe do form.menu


namespace Minesweeper.View_Controller
{
    
    public partial class FormMenu : Form
    {
        public event startGame play;

        private TextBox TBnumLinhas = new TextBox();
        private TextBox TBnumColunas = new TextBox();
        private TextBox TBnumBombas = new TextBox();
        public FormMenu()
        {
            InitializeComponent();
            ConfigRadioButtons();
            
        }

        private void buttonJogar_Click(object sender, EventArgs e)
        {
            int nLinhasCustom, nColunasCustom, nBombasCustom;
            

            if (TBnumBombas.Visible)
            {
                if(!((Int32.TryParse(TBnumLinhas.Text, out nLinhasCustom)) && (Int32.TryParse(TBnumColunas.Text, out nColunasCustom) && (Int32.TryParse(TBnumBombas.Text, out nBombasCustom)))))
                {
                    MessageBox.Show("Insira valores corretos!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                 
                    if(nLinhasCustom < 9)
                    {
                        nLinhasCustom = 9;
                    }
                    if (nColunasCustom < 9)
                    {
                        nColunasCustom = 9;
                    }
                    if (nLinhasCustom > 20)
                    {
                        nLinhasCustom = 20;
                    }
                    if (nColunasCustom > 22)
                    {
                        nColunasCustom = 22;
                    }

                    int numMaxBombas = (nLinhasCustom * nColunasCustom) - (nLinhasCustom + nColunasCustom) + 1;
                    int numMinBombas = 10;


                    if (nBombasCustom > numMaxBombas)
                    {
                        this.Hide();
                        play(nLinhasCustom, nColunasCustom, numMaxBombas);
                    }else if(nBombasCustom < numMinBombas)
                    {
                        this.Hide();
                        play(nLinhasCustom, nColunasCustom, numMinBombas);
                    }
                    else
                    {
                        this.Hide();
                        play(nLinhasCustom, nColunasCustom, nBombasCustom);
                    }
                }
            }
            else if (radioButtonFacil.Checked)
            {
                this.Hide();
                if (play != null)
                {
                play(9, 9, 10);
                }
            }
            else if (radioButtonMedia.Checked)
            {
                this.Hide();
                play(16, 16, 40);
            }
            else
            {
                MessageBox.Show("Escolha uma das dificuldades", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void buttonInstrucoes_Click(object sender, EventArgs e)
        {
            Program.V_Instrucoes.ShowDialog();
        }
        private void FormMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }
        private void radioButtonCustom_Click(object sender, EventArgs e)
        {
            TBnumColunas.Visible = true;
            TBnumLinhas.Visible = true;
            TBnumBombas.Visible = true;
        }
        private void radioButtonMedia_Click(object sender, EventArgs e)
        {
            TBnumColunas.Visible = false;
            TBnumLinhas.Visible = false;
            TBnumBombas.Visible = false;
            TBnumLinhas.Text = "NumLinhas";
            TBnumColunas.Text = "NumColunas";
            TBnumBombas.Text = "NumBombas";
        }
        private void radioButtonFacil_Click(object sender, EventArgs e)
        {
            TBnumColunas.Visible = false;
            TBnumLinhas.Visible = false;
            TBnumBombas.Visible = false;

            TBnumLinhas.Text = "NumLinhas";
            TBnumColunas.Text = "NumColunas";
            TBnumBombas.Text = "NumBombas";
        }
        private void ConfigRadioButtons()
        {
            TBnumLinhas.Size = new System.Drawing.Size(70, 20);
            TBnumLinhas.Text = "NumLinhas";
            TBnumLinhas.Location = new System.Drawing.Point(351, 220);
            TBnumLinhas.Click += TB_Click;
            this.Controls.Add(TBnumLinhas);

            TBnumColunas.Size = new System.Drawing.Size(70, 20);
            TBnumColunas.Text = "NumColunas";
            TBnumColunas.Location = new System.Drawing.Point(351, 250);
            TBnumColunas.Click += TB_Click;
            this.Controls.Add(TBnumColunas);

            TBnumBombas.Size = new System.Drawing.Size(70, 20);
            TBnumBombas.Text = "NumBombas";
            TBnumBombas.Location = new System.Drawing.Point(351, 280);
            TBnumBombas.Click += TB_Click;
            this.Controls.Add(TBnumBombas);
        }
        private void TB_Click(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
        }

        private void buttonConsultarPerfil_Click(object sender, EventArgs e)
        {
            Program.V_ConsultarPerfil.Show();
        }

        public void AlteraImagem()
        {
            pictureBoxOnline.Image = Image.FromFile(Environment.CurrentDirectory + @"/Botoes/online.png");
            

        }
        public void ShowConsultaPerfil()
        {
            buttonConsultarPerfil.Visible = true;
        }
    }
}
