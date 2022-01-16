using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris3D
{
    class Piece2D
    {
        #region Parameters
        List<Line2D> holdGrid = new List<Line2D>();
        Point2D gridSize = new Point2D(4, 4);
        int cellSize = 20;
        Color[] colors = new Color[7] { Color.Yellow, Color.Blue, Color.Orange, Color.DarkRed, Color.Purple, Color.DarkBlue, Color.Green };
        int pieceType = 0;
        CellTypes[,] gameData;
        Point2D location;
        #endregion

        #region Constructors
        public Piece2D(Point2D gridSize, int cellSize, Point2D location)
        {
            this.gridSize = gridSize;
            this.cellSize = cellSize;
            this.location = location;

            gameData = new CellTypes[(int)gridSize.X, (int)gridSize.Y];

            for (int i = 0; i <= gridSize.Y; i++)
            {
                holdGrid.Add(new Line2D(new Point2D(location.X, (location.Y + (i * cellSize))),
                    new Point2D(location.X + gridSize.X * cellSize, (location.Y + i * cellSize))));
                i += 3;
            }
            for (int i = 0; i <= gridSize.X; i++)
            {
                holdGrid.Add(new Line2D(new Point2D(location.X + i * cellSize, location.Y),
                    new Point2D(location.X + i * cellSize, (location.Y + gridSize.Y * cellSize))));
                i += 3;
            }
        }

        public Piece2D(int pieceType, Point2D gridSize, int cellSize, Point2D location)
        {
            this.gridSize = gridSize;
            this.cellSize = cellSize;
            this.pieceType = pieceType;
            this.location = location;

            gameData = new CellTypes[(int)gridSize.X, (int)gridSize.Y];

            for (int i = 0; i <= gridSize.Y; i++)
            {
                holdGrid.Add(new Line2D(new Point2D(location.X, (location.Y + (i * cellSize))),
                    new Point2D(location.X + gridSize.X * cellSize, (location.Y + i * cellSize))));
                i += 3;
            }
            for (int i = 0; i <= gridSize.X; i++)
            {
                holdGrid.Add(new Line2D(new Point2D(location.X + i * cellSize, location.Y),
                    new Point2D(location.X + i * cellSize, (location.Y + gridSize.Y * cellSize))));
                i += 3;
            }

            #region Piece Setup
            if (pieceType == 0)
            {
                gameData[1, 1] = CellTypes.block;
                gameData[2, 1] = CellTypes.block;
                gameData[1, 2] = CellTypes.block;
                gameData[2, 2] = CellTypes.block;
            }
            if (pieceType == 1)
            {
                gameData[1, 0] = CellTypes.block;
                gameData[1, 1] = CellTypes.block;
                gameData[1, 2] = CellTypes.block;
                gameData[1, 3] = CellTypes.block;
            }
            if (pieceType == 2)
            {
                gameData[0, 2] = CellTypes.block;
                gameData[1, 2] = CellTypes.block;
                gameData[2, 2] = CellTypes.block;
                gameData[2, 1] = CellTypes.block;
            }
            if (pieceType == 3)
            {
                gameData[0, 1] = CellTypes.block;
                gameData[1, 1] = CellTypes.block;
                gameData[1, 2] = CellTypes.block;
                gameData[2, 2] = CellTypes.block;
            }
            if (pieceType == 4)
            {
                gameData[0, 2] = CellTypes.block;
                gameData[1, 2] = CellTypes.block;
                gameData[2, 2] = CellTypes.block;
                gameData[1, 1] = CellTypes.block;
            }
            if (pieceType == 5)
            {
                gameData[0, 1] = CellTypes.block;
                gameData[0, 2] = CellTypes.block;
                gameData[1, 2] = CellTypes.block;
                gameData[2, 2] = CellTypes.block;
            }
            if (pieceType == 6)
            {
                gameData[0, 2] = CellTypes.block;
                gameData[1, 2] = CellTypes.block;
                gameData[1, 1] = CellTypes.block;
                gameData[2, 1] = CellTypes.block;
            }
            #endregion
        }
        #endregion

        #region Properties
        public CellTypes[,] GameData
        {
            get { return gameData; }
        }
        public Point2D GridSize
        {
            get { return gridSize; }
        }
        public int CellSize
        {
            get { return cellSize; }
        }
        public Point2D Location
        {
            get { return location; }
        }
        public int PieceType
        {
            get { return pieceType; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Draws the 2D grid and the 2D polygons
        /// </summary>
        /// <param name="gr">graphics device</param>
        public void FillDraw(Graphics gr)
        {
            // Drawing the grid
            for (int i = 0; i <= gridSize.Y; i++)
            {
                holdGrid.Add(new Line2D(new Point2D(location.X, (location.Y + (i * cellSize))),
                    new Point2D(location.X + gridSize.X * cellSize, (location.Y + i * cellSize))));
                i += 3;
            }
            for (int i = 0; i <= gridSize.X; i++)
            {
                holdGrid.Add(new Line2D(new Point2D(location.X + i * cellSize, location.Y),
                    new Point2D(location.X + i * cellSize, (location.Y + gridSize.Y * cellSize))));
                i += 3;
            }
            foreach (Line2D line in holdGrid)
                line.Draw(gr, Pens.Red);

            // Draw the polygon if there is a block there
            Brush brush = new SolidBrush(Color.FromArgb(255, colors[pieceType]));
            for (int i = 0; i < gridSize.X; i++)
            {
                for (int j = 0; j < gridSize.Y; j++)
                {
                    if (gameData[i, j] == CellTypes.block)
                    {
                        Rectangle cell = new Rectangle((int)location.X + i * (int)cellSize, ((int)location.Y + j * (int)cellSize), (int)cellSize, (int)cellSize);
                        gr.FillRectangle(brush, cell);
                        gr.DrawRectangle(Pens.Red, cell);
                    }
                }
            }
        }
        #endregion
    }
}
