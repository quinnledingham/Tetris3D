using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris3D
{
    public partial class Settings : Form
    {
        int gridSize = 4;
        int cellSize = 30;

        public Settings(int gridSize, int cellSize)
        {
            this.gridSize = gridSize;
            this.cellSize = cellSize;
   
            InitializeComponent();

            tbGridSize.Value = gridSize;
            tbCellSize.Value = cellSize;
        }

        public int GridSize
        {
            get { return gridSize; }
        }

        public int CellSize
        {
            get { return cellSize; }
        }

        private void tbGridSize_ValueChanged(object sender, EventArgs e)
        {
            this.gridSize = tbGridSize.Value;
        }

        private void tbCellSize_ValueChanged(object sender, EventArgs e)
        {
            this.cellSize = tbCellSize.Value;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
