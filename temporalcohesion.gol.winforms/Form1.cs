using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using temporalcohesion.gol.core;

namespace temporalcohesion.gol.winforms
{
    public partial class Form1 : Form
    {
        private Life _life;

        public Form1()
        {
            var boardSize = 100;

            InitializeComponent();
            _life = new Life(boardSize, boardSize, new DefaultGridPopulationStrategy());

            for (var i = 0; i < boardSize; i++)
            {
                grid.Columns.Add(new DataGridViewColumn(new DataGridViewTextBoxCell()));
                grid.Rows.Add(new DataGridViewRow());
            }
        }
    }
}
