using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Tetris3D
{
    class Piece3D
    {
        #region Parameters
        List<Box> parts = new List<Box>();
        CellTypes[,,] gameData;
        MoveTypes[,,] moveData;
        Point3D gridSize = new Point3D();
        int cellSize = 30;
        int gridHeight = 200;
        double height = 0;
        double dropSpeed = 0;
        int pieceType;
        Point3D pivotPoint = new Point3D();
        bool drop = true;
        bool raise = true;

        bool ghost = false;
        bool move = false;

        Point3D rotationAmount = new Point3D();
        #endregion

        #region Constructors
        public Piece3D(int pieceType, Point3D gridSize, int cellSize, double height, double dropSpeed, Point3D pivotPoint, int gridHeight)
        {
            this.gridSize = gridSize;
            this.cellSize = cellSize;
            this.pieceType = pieceType;
            this.pivotPoint = pivotPoint;
            this.height = height;
            this.dropSpeed = dropSpeed;
            this.gridHeight = gridHeight;

            gameData = new CellTypes[(int)gridSize.X, (int)gridSize.Y, (int)gridSize.Z];
            moveData = new MoveTypes[(int)gridSize.X, (int)gridSize.Y, (int)gridSize.Z];

            #region Piece GameData
            if (pieceType == 0) //O-Block
            {
                gameData[(int)pivotPoint.X, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
                gameData[(int)pivotPoint.X - 1, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
                gameData[(int)pivotPoint.X - 1, (int)pivotPoint.Y, (int)pivotPoint.Z - 1] = CellTypes.block;
                gameData[(int)pivotPoint.X, (int)pivotPoint.Y, (int)pivotPoint.Z - 1] = CellTypes.block;
            }
            if (pieceType == 1) //I-Block
            {
                gameData[(int)pivotPoint.X, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
                gameData[(int)pivotPoint.X - 1, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
                gameData[(int)pivotPoint.X + 1, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
                gameData[(int)pivotPoint.X + 2, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
            }
            if (pieceType == 2) //L-Block
            {
                gameData[(int)pivotPoint.X, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
                gameData[(int)pivotPoint.X - 1, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
                gameData[(int)pivotPoint.X + 1, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
                gameData[(int)pivotPoint.X + 1, (int)pivotPoint.Y, (int)pivotPoint.Z - 1] = CellTypes.block;
            }
            if (pieceType == 3) //Z-Block
            {
                gameData[(int)pivotPoint.X, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
                gameData[(int)pivotPoint.X, (int)pivotPoint.Y, (int)pivotPoint.Z - 1] = CellTypes.block;
                gameData[(int)pivotPoint.X - 1, (int)pivotPoint.Y, (int)pivotPoint.Z - 1] = CellTypes.block;
                gameData[(int)pivotPoint.X + 1, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
            }
            if (pieceType == 4) //T-Block
            {
                gameData[(int)pivotPoint.X - 1, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
                gameData[(int)pivotPoint.X, (int)pivotPoint.Y, (int)pivotPoint.Z - 1] = CellTypes.block;
                gameData[(int)pivotPoint.X + 1, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
                gameData[(int)pivotPoint.X, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
            }
            if (pieceType == 5) //J-Block
            {
                gameData[(int)pivotPoint.X - 1, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
                gameData[(int)pivotPoint.X - 1, (int)pivotPoint.Y, (int)pivotPoint.Z - 1] = CellTypes.block;
                gameData[(int)pivotPoint.X + 1, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
                gameData[(int)pivotPoint.X, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
            }
            if (pieceType == 6) //S-Block
            {
                gameData[(int)pivotPoint.X - 1, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
                gameData[(int)pivotPoint.X, (int)pivotPoint.Y, (int)pivotPoint.Z - 1] = CellTypes.block;
                gameData[(int)pivotPoint.X + 1, (int)pivotPoint.Y, (int)pivotPoint.Z - 1] = CellTypes.block;
                gameData[(int)pivotPoint.X, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
            }
            #endregion
        }
        public Piece3D(int pieceType, Point3D gridSize, int cellSize, double height, double dropSpeed, Point3D pivotPoint, bool ghost, int gridHeight)
        {
            this.gridSize = gridSize;
            this.cellSize = cellSize;
            this.pieceType = pieceType;
            this.pivotPoint = pivotPoint;
            this.height = height;
            this.dropSpeed = dropSpeed;
            this.ghost = ghost;
            this.gridHeight = gridHeight;

            gameData = new CellTypes[(int)gridSize.X, (int)gridSize.Y + 6, (int)gridSize.Z];
            moveData = new MoveTypes[(int)gridSize.X, (int)gridSize.Y, (int)gridSize.Z];

            #region Piece GameData
            if (pieceType == 0) //O-Block
            {
                gameData[(int)pivotPoint.X, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
                gameData[(int)pivotPoint.X - 1, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
                gameData[(int)pivotPoint.X - 1, (int)pivotPoint.Y, (int)pivotPoint.Z - 1] = CellTypes.block;
                gameData[(int)pivotPoint.X, (int)pivotPoint.Y, (int)pivotPoint.Z - 1] = CellTypes.block;
            }
            if (pieceType == 1) //I-Block
            {
                gameData[(int)pivotPoint.X, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
                gameData[(int)pivotPoint.X - 1, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
                gameData[(int)pivotPoint.X + 1, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
                gameData[(int)pivotPoint.X + 2, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
            }
            if (pieceType == 2) //L-Block
            {
                gameData[(int)pivotPoint.X, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
                gameData[(int)pivotPoint.X - 1, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
                gameData[(int)pivotPoint.X + 1, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
                gameData[(int)pivotPoint.X + 1, (int)pivotPoint.Y, (int)pivotPoint.Z - 1] = CellTypes.block;
            }
            if (pieceType == 3) //Z-Block
            {
                gameData[(int)pivotPoint.X, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
                gameData[(int)pivotPoint.X, (int)pivotPoint.Y, (int)pivotPoint.Z - 1] = CellTypes.block;
                gameData[(int)pivotPoint.X - 1, (int)pivotPoint.Y, (int)pivotPoint.Z - 1] = CellTypes.block;
                gameData[(int)pivotPoint.X + 1, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
            }
            if (pieceType == 4) //T-Block
            {
                gameData[(int)pivotPoint.X - 1, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
                gameData[(int)pivotPoint.X, (int)pivotPoint.Y, (int)pivotPoint.Z - 1] = CellTypes.block;
                gameData[(int)pivotPoint.X + 1, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
                gameData[(int)pivotPoint.X, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
            }
            if (pieceType == 5) //J-Block
            {
                gameData[(int)pivotPoint.X - 1, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
                gameData[(int)pivotPoint.X - 1, (int)pivotPoint.Y, (int)pivotPoint.Z - 1] = CellTypes.block;
                gameData[(int)pivotPoint.X + 1, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
                gameData[(int)pivotPoint.X, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
            }
            if (pieceType == 6) //S-Block
            {
                gameData[(int)pivotPoint.X - 1, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
                gameData[(int)pivotPoint.X, (int)pivotPoint.Y, (int)pivotPoint.Z - 1] = CellTypes.block;
                gameData[(int)pivotPoint.X + 1, (int)pivotPoint.Y, (int)pivotPoint.Z - 1] = CellTypes.block;
                gameData[(int)pivotPoint.X, (int)pivotPoint.Y, (int)pivotPoint.Z] = CellTypes.block;
            }
            #endregion
        }
        #endregion

        #region Properties
        public CellTypes[,,] GameData
        {
            get { return gameData; }
        }
        public MoveTypes[,,] MoveData
        {
            get { return moveData; }
        }
        public bool Drop
        {
            get { return drop; }
            set { drop = value; }
        }
        public bool Raise
        {
            get { return raise; }
            set { raise = value; }
        }
        public Point3D GridSize
        {
            get { return gridSize; }
            set { gridSize = value; }
        }
        public Point3D CellSize
        {
            get { return CellSize; }
            set { CellSize = value; }
        }
        public List<Box> Parts
        {
            get { return parts; }
            set { parts = value; }
        }
        public int PieceType
        {
            get { return pieceType; }
        }
        public Point3D PivotPoint
        {
            get { return pivotPoint; }
            set { pivotPoint = value; }
        }
        public double Height
        {
            get { return height; }
        }
        public Point3D RotationAmount
        {
            get { return rotationAmount; }
        }
        public bool Move
        {
            get { return move; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Rotate the piece
        /// </summary>
        /// <param name="angle">How much to rotate the piece</param>
        public void RotateD(Point3D angle)
        {
            foreach (Box part in parts)
                part.RotateD(angle);
        }
        /// <summary>
        /// UnRotate the piece
        /// </summary>
        /// <param name="angle">How much to unrotate the piece</param>
        public void UnRotateD(Point3D angle)
        {
            foreach (Box part in parts)
                part.UnRotateD(angle);
        }

        /// <summary>
        /// Turns the piece's gameData into boxes to paint them
        /// </summary>
        public void Create(int ghost)
        {
            parts = new List<Box>();
            for (int i = 0; i < gridSize.X; i++)
            {
                for (int j = 0; j < gridSize.Y; j++)
                {
                    for (int k = 0; k < gridSize.Z; k++)
                    {
                        if (gameData[i, j, k] == CellTypes.block)
                        {
                            parts.Add(new Box(new Point3D((i * cellSize) + (cellSize / 2) - (cellSize * (gridSize.X / 2)), ((-cellSize * gridSize.Y) + (j * cellSize)) +
                                (gridHeight + (cellSize / 2)) + (height - cellSize), (k * cellSize) + (cellSize / 2) - (cellSize * (gridSize.Z / 2))), pieceType, (int)cellSize, ghost));
                        }
                    }
                }
            }

        }
        /// <summary>
        /// Lowers the block down into the cell
        /// </summary>
        /// <param name="completeGameData">gameData of all of the non-moving blocks</param>
        public void DropDown(CellTypes[,,] completeGameData)
        {
            // If the piece should still be moving lower by a number
            if (drop == true)
                height += dropSpeed;

            // If the height is 20 the block has dropped an cell and should be moved to the next one
            if (height >= cellSize)
            {
                DropCell(completeGameData);
            }
        }

        /// <summary>
        /// Drops the block down to the cell below
        /// </summary>
        /// <param name="completeGameData">gameData of all of the non-moving blocks</param>
        public void DropCell(CellTypes[,,] completeGameData)
        {
            height = 0;
            bool move = true;
            ResetMoveData();

            // Checks if the block can be moved and moves it if it can
            for (int i = 0; i < gridSize.X; i++)
            {
                for (int j = 0; j < gridSize.Y; j++)
                {
                    for (int k = 0; k < gridSize.Z; k++)
                    {
                        if (gameData[i, j, k] == CellTypes.block)
                        {
                            // If there is a spot to move and if that spot isn't already filled
                            if (CheckBounds(i, j + 1, k) && completeGameData[i, j + 1, k] != CellTypes.block)
                                moveData[i, j, k] = MoveTypes.move;
                            // Stops moving if it can't drop anymore
                            else
                            {
                                height = cellSize;
                                move = false;
                                drop = false;
                            }
                        }

                    }
                }
            }
            // Moves the block down one because it is able to drop
            if (move == true)
            {
                ResetGameData();
                for (int i = 0; i < gridSize.X; i++)
                {
                    for (int j = 0; j < gridSize.Y; j++)
                    {
                        for (int k = 0; k < gridSize.Z; k++)
                        {
                            if (moveData[i, j, k] == MoveTypes.move)
                            {
                                gameData[i, j + 1, k] = CellTypes.block;
                            }

                        }
                    }
                }
                // Change the pivotPoint for rotating
                pivotPoint.Y += 1;
            }
        }

        /// <summary>
        /// Raises the piece up a cell
        /// </summary>
        public void RaiseCell()
        {
            height = 0;
            move = true;
            ResetMoveData();

            for (int i = 0; i < gridSize.X; i++)
            {
                for (int j = 0; j < gridSize.Y; j++)
                {
                    for (int k = 0; k < gridSize.Z; k++)
                    {
                        if (gameData[i, j, k] == CellTypes.block)
                        {
                            // If there is a spot to move and if that spot isn't already filled
                            if (CheckBounds(i, j - 3, k))
                                moveData[i, j, k] = MoveTypes.move;
                            // Stops moving if it can't raise anymore
                            else
                            {
                                move = false;
                                raise = false;
                            }
                        }

                    }
                }
            }
            // Moves the block down one because it is able to raise
            if (move == true)
            {
                ResetGameData();
                for (int i = 0; i < gridSize.X; i++)
                {
                    for (int j = 0; j < gridSize.Y; j++)
                    {
                        for (int k = 0; k < gridSize.Z; k++)
                        {
                            if (moveData[i, j, k] == MoveTypes.move)
                            {
                                gameData[i, j - 1, k] = CellTypes.block;
                            }

                        }
                    }
                }
                // Change the pivotPoint for rotating
                pivotPoint.Y -= 1;
            }
        }

        /// <summary>
        /// Moves the piece along the X and Z axis
        /// </summary>
        /// <param name="e">Keydown information</param>
        /// <param name="completeGameData">gameData of all non-moving blocks</param>
        public void MoveShift(KeyEventArgs e, CellTypes[,,] completeGameData)
        {
            // Can't move it if it is not dropping anymore
            if (drop == false && ghost != true)
                return;

            int moveZ = 0;
            int moveX = 0;

            // Figures out which way to move it
            if (e.KeyCode == Keys.Up)
                moveZ = -1;
            if (e.KeyCode == Keys.Down)
                moveZ = 1;
            if (e.KeyCode == Keys.Left)
                moveX = -1;
            if (e.KeyCode == Keys.Right)
                moveX = 1;

            move = true;

            // Checks if it can move the piece because there isn't a block in the way and isn't at the edge
            for (int i = 0; i < gridSize.X; i++)
            {
                for (int j = 0; j < gridSize.Y; j++)
                {
                    for (int k = 0; k < gridSize.Z; k++)
                    {
                        moveData[i, j, k] = MoveTypes.doNotMove;
                        if (gameData[i, j, k] == CellTypes.block)
                        {
                            // If there isn't a block or isn't at the edge
                            if (CheckBounds(i + moveX, j, k + moveZ) && completeGameData[i + moveX, j, k + moveZ] == CellTypes.blank)
                            {
                                moveData[i, j, k] = MoveTypes.move;
                            }
                            // Don't move if a block is in the way
                            else
                            {
                                move = false;
                            }
                        }
                    }
                }
            }

            // Moves the piece because it was able to move
            if (move == true)
            {
                ResetGameData();
                for (int i = 0; i < gridSize.X; i++)
                {
                    for (int j = 0; j < gridSize.Y; j++)
                    {
                        for (int k = 0; k < gridSize.Z; k++)
                        {
                            if (moveData[i, j, k] == MoveTypes.move)
                            {
                                moveData[i, j, k] = MoveTypes.doNotMove;
                                gameData[i + moveX, j, k + moveZ] = CellTypes.block;
                            }
                        }
                    }
                }
                // Change the pivotpoint for rotating
                pivotPoint.X += moveX;
                pivotPoint.Z += moveZ;
            }
        }

        /// <summary>
        /// Turn the piece around a pivot point
        /// </summary>
        /// <param name="e">Keydown Information</param>
        /// <param name="completeGameData">gameData of all non-moving boxes</param>
        public void Rotate(KeyEventArgs e, CellTypes[,,] completeGameData)
        {
            // Don't rotate if it isn't dropping and it isn't a ghost piece
            if (drop == false && ghost != true)
                return;

            move = true;
            // Rotate the other way if shift is pressed
            bool shift = false;
            if (e.Modifiers == Keys.Shift)
                shift = true;

            ResetMoveData();
            for (int i = 0; i < gridSize.X; i++)
            {
                for (int j = 0; j < gridSize.Y; j++)
                {
                    for (int k = 0; k < gridSize.Z; k++)
                    {
                        if (gameData[i, j, k] == CellTypes.block)
                        {
                            // Checks if the block can be rotated without going out of the grid and with out land on another block
                            // Rotate on the Y-Axis
                            if (e.KeyCode == Keys.C)
                            {
                                if (CheckBounds((int)CalculateRotate((int)pivotPoint.X, (int)pivotPoint.Z, i, k, shift).X, j, (int)CalculateRotate((int)pivotPoint.X, (int)pivotPoint.Z, i, k, shift).Y)
                                    && completeGameData[(int)CalculateRotate((int)pivotPoint.X, (int)pivotPoint.Z, i, k, shift).X, j, (int)CalculateRotate((int)pivotPoint.X, (int)pivotPoint.Z, i, k, shift).Y] == CellTypes.blank)
                                {
                                    moveData[(int)CalculateRotate((int)pivotPoint.X, (int)pivotPoint.Z, i, k, shift).X, j,
                                    (int)CalculateRotate((int)pivotPoint.X, (int)pivotPoint.Z, i, k, shift).Y] = MoveTypes.move;
                                }
                                else
                                    move = false;
                            }
                            // Rotate the X-Axis
                            if (e.KeyCode == Keys.X)
                            {
                                if (CheckBounds((int)CalculateRotate((int)pivotPoint.X, (int)pivotPoint.Y, i, j, shift).X, (int)CalculateRotate((int)pivotPoint.X, (int)pivotPoint.Y, i, j, shift).Y, k)
                                    && completeGameData[(int)CalculateRotate((int)pivotPoint.X, (int)pivotPoint.Y, i, j, shift).X, (int)CalculateRotate((int)pivotPoint.X, (int)pivotPoint.Y, i, j, shift).Y, k] == CellTypes.blank)
                                {
                                    moveData[(int)CalculateRotate((int)pivotPoint.X, (int)pivotPoint.Y, i, j, shift).X,
                                    (int)CalculateRotate((int)pivotPoint.X, (int)pivotPoint.Y, i, j, shift).Y, k] = MoveTypes.move;
                                }
                                else
                                    move = false;
                            }
                            // Rotate the Z-Axis
                            if (e.KeyCode == Keys.Z)
                            {
                                if (CheckBounds(i, (int)CalculateRotate((int)pivotPoint.Y, (int)pivotPoint.Z, j, k, shift).X, (int)CalculateRotate((int)pivotPoint.Y, (int)pivotPoint.Z, j, k, shift).Y)
                                    && completeGameData[i, (int)CalculateRotate((int)pivotPoint.Y, (int)pivotPoint.Z, j, k, shift).X, (int)CalculateRotate((int)pivotPoint.Y, (int)pivotPoint.Z, j, k, shift).Y] == CellTypes.blank)
                                {
                                    moveData[i, (int)CalculateRotate((int)pivotPoint.Y, (int)pivotPoint.Z, j, k, shift).X,
                                    (int)CalculateRotate((int)pivotPoint.Y, (int)pivotPoint.Z, j, k, shift).Y] = MoveTypes.move;
                                }
                                else
                                    move = false;
                            }
                        }
                    }
                }
            }
            // Moves it since it was confirmed it was able to move
            if (move == true)
            {
                ResetGameData();
                for (int i = 0; i < gridSize.X; i++)
                {
                    for (int j = 0; j < gridSize.Y; j++)
                    {
                        for (int k = 0; k < gridSize.Z; k++)
                        {
                            if (moveData[i, j, k] == MoveTypes.move)
                            {
                                moveData[i, j, k] = MoveTypes.doNotMove;
                                gameData[i, j, k] = CellTypes.block;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Calculates where the block should be placed if it is rotated 90 degrees
        /// </summary>
        /// <param name="pX">X of the pivot point</param>
        /// <param name="pY">Y of the pivot point</param>
        /// <param name="mX">X of the block being rotated</param>
        /// <param name="mY">Y of the block being rotated</param>
        /// <param name="shift">Reverse rotate</param>
        /// <returns>Where the box should be placed after a 90 degrees rotation</returns>
        public Point2D CalculateRotate(int pX, int pY, int mX, int mY, bool shift)
        {
            int Vrx = mX - pX;
            int Vry = mY - pY;

            int Vtx = 0;
            int Vty = 0;
            if (shift == false)
            {
                Vtx = (0 * Vrx) + (-1 * Vry);
                Vty = (1 * Vrx) + (0 * Vry);
            }
            else if (shift == true)
            {
                Vtx = (0 * Vrx) + (1 * Vry);
                Vty = (-1 * Vrx) + (0 * Vry);
            }

            int Vx = pX + Vtx;
            int Vy = pY + Vty;
            return new Point2D(Vx, Vy);
        }

        /// <summary>
        /// Sets every cell in gameData to blank
        /// </summary>
        public void ResetGameData()
        {
            for (int i = 0; i < gridSize.X; i++)
            {
                for (int j = 0; j < gridSize.Y; j++)
                {
                    for (int k = 0; k < gridSize.Z; k++)
                    {
                        gameData[i, j, k] = CellTypes.blank;
                    }
                }
            }
        }

        /// <summary>
        /// Sets every cell in moveData to doNotMove
        /// </summary>
        public void ResetMoveData()
        {
            for (int i = 0; i < gridSize.X; i++)
            {
                for (int j = 0; j < gridSize.Y; j++)
                {
                    for (int k = 0; k < gridSize.Z; k++)
                    {
                        moveData[i, j, k] = MoveTypes.doNotMove;
                    }
                }
            }
        }

        public bool CheckBounds(int x, int y, int z) //Checks if the coordinates sent are on screen
        {
            if (x < 0 || x >= gridSize.X || y < 0 || y >= gridSize.Y || z < 0 || z >= gridSize.Z)
                return false; //out of bounds
            else
                return true; //in bounds
        }
        #endregion
    }
}