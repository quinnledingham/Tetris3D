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
    enum Face { front = 0, back = 1 };
    enum CellTypes { blank = 0, block = 1 };
    enum MoveTypes { doNotMove = 0, move = 1 };

    public partial class Form1 : Form
    {
        CellTypes[,,] gameData;

        Point3D gridSize = new Point3D(4, 15, 4);
        int cellSize = 30;
        int piece2DCellSize = 25;
        List<Point3D> gridPoints = new List<Point3D>();
        List<Piece3D> pieces = new List<Piece3D>();
        List<Piece3D> ghostPieces = new List<Piece3D>();

        double distance = 1000;
        List<Line3D> grid = new List<Line3D>();
        List<Polygon3D> gridPolygons = new List<Polygon3D>();

        List<Line2D> holdGrid = new List<Line2D>();
        Point2D holdGridSize = new Point2D(4, 4);
        Point2D holdCellSize = new Point2D(20, 20);
        Color[] colors = new Color[7] { Color.Yellow, Color.Blue, Color.Orange, Color.DarkRed, Color.Purple, Color.DarkBlue, Color.Green };
        int pieceType = 0;
        bool holdUsed = false;
        bool holdEmpty = true;

        double dropSpeed = 0.5;
        int score = 0;

        Piece2D hold;

        Point3D lightSrc = new Point3D(0, 0, 2000);
        Point2D originalClick = null;
        Point3D rotationAngle = new Point3D();
        List<Polygon3D> polygons = new List<Polygon3D>();

        int gridHeight = 200;
        bool boxDisplayed = false;

        List<Piece2D> upcomingPieces = new List<Piece2D>();

        public Form1()
        {
            InitializeComponent();
            this.MouseWheel += Form1_MouseWheel;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblScore.Location = new Point(5, 30);
            lblHold.Location = new Point(35, 185);
            lblNext.Location = new Point(ClientRectangle.Width - 163, 30);

            Restart();
        }

        /// <summary>
        /// Resets everything so a new game can be played.
        /// </summary>
        private void Restart()
        {
            timer1.Enabled = true;
            // Reset gameData and moveData
            gameData = new CellTypes[(int)gridSize.X, (int)gridSize.Y, (int)gridSize.Z];
            hold = new Piece2D(holdGridSize, piece2DCellSize, new Point2D((ClientRectangle.Width / 2 * -1) + 10, (ClientRectangle.Height / 2 * -1) + 55));
            holdEmpty = true;
            holdUsed = false;
            pieces = new List<Piece3D>();

            // Recreate the bottom grid with the new gridsize and cell size
            gridPolygons = new List<Polygon3D>();
            for (int i = 0; i < gridSize.X; i++)
            {
                for (int k = 0; k < gridSize.Z; k++)
                {
                    List<Point3D> gridPoints = new List<Point3D>();
                    gridPoints.Add(new Point3D((-(cellSize * (gridSize.X / 2)) + cellSize * i), gridHeight, (-(cellSize * (gridSize.Z / 2))) + cellSize * k));
                    gridPoints.Add(new Point3D((-(cellSize * (gridSize.X / 2)) + cellSize * i + cellSize), gridHeight, (-(cellSize * (gridSize.Z / 2))) + cellSize * k));
                    gridPoints.Add(new Point3D((-(cellSize * (gridSize.X / 2)) + cellSize * i + cellSize), gridHeight, (-(cellSize * (gridSize.Z / 2))) + cellSize * k + cellSize));
                    gridPoints.Add(new Point3D((-(cellSize * (gridSize.X / 2)) + cellSize * i), gridHeight, (-(cellSize * (gridSize.Z / 2))) + cellSize * k + cellSize));

                    gridPolygons.Add(new Polygon3D(gridPoints, 3));
                }
            }

            // Create the upcoming pieces
            Random r = new Random();
            upcomingPieces = new List<Piece2D>();
            for (int i = 0; i < 3; i++)
            {
                int g = r.Next(0, 7);
                upcomingPieces.Add(new Piece2D(g, holdGridSize, piece2DCellSize, new Point2D((ClientRectangle.Width / 2) - 110, (-1 * ClientRectangle.Height / 2) + (i * piece2DCellSize * holdGridSize.Y))));
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Rotates the pieces and lines for the graphics
            foreach (Piece3D piece in pieces)
                piece.RotateD(rotationAngle);
            foreach (Piece3D ghostPiece in ghostPieces)
                ghostPiece.RotateD(rotationAngle);
            foreach (Line3D line in grid)
                line.RotateD(rotationAngle);
            foreach (Polygon3D polygon in gridPolygons)
                polygon.RotateD(rotationAngle);

            // Make (0,0) the center of the screen
            e.Graphics.TranslateTransform((float)(ClientSize.Width / 2), (float)(ClientSize.Height / 2) + menuStrip1.Height);

            foreach (Line3D line in grid)
                line.Draw(e.Graphics, new Pen(Color.Red), distance);

            // Draw the grid on the bottom
            gridPolygons.Sort();
            foreach (Polygon3D gridPolygon in gridPolygons)
            {
                gridPolygon.Draw(e.Graphics, Pens.Red, distance);
            }

            // Draw the polygons of the pieces and ghost piece
            polygons.Sort();
            foreach (Polygon3D polygon in polygons)
            {
                polygon.Fill(e.Graphics, distance, Face.back, lightSrc);
                polygon.Draw(e.Graphics, Pens.Red, distance);
            }

            // Fill the grid on the bottom
            foreach (Polygon3D gridPolygon in gridPolygons)
            {
                gridPolygon.Fill(e.Graphics, distance, Face.back, lightSrc);
            }

            lblScore.Text = "Score: " + score.ToString();

            //Draw hold and upcoming pieces
            hold.FillDraw(e.Graphics);
            foreach (Piece2D piece in upcomingPieces)
                piece.FillDraw(e.Graphics);

            foreach (Line2D line in holdGrid)
                line.Draw(e.Graphics, new Pen(Color.Red));

            // Unrotates the pices and lines for the calculations
            foreach (Piece3D piece in pieces)
                piece.UnRotateD(-1 * rotationAngle);
            foreach (Piece3D ghostPiece in ghostPieces)
                ghostPiece.UnRotateD(-1 * rotationAngle);
            foreach (Line3D line in grid)
                line.UnRotateD(-1 * rotationAngle);
            foreach (Polygon3D polygon in gridPolygons)
                polygon.UnRotateD(-1 * rotationAngle);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                originalClick = new Point2D(e.X, e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            // Move light source
            if (e.Button == MouseButtons.Right)
                lightSrc = new Point3D((e.X - ClientSize.Width / 2) * 100, (e.Y - ClientSize.Width / 2) * 100, lightSrc.Z);

            if (e.Button == MouseButtons.Left)
            {
                // calculate a rotation angle
                rotationAngle += (new Point3D(originalClick.Y - e.Y, originalClick.X - e.X, 0) / 3);

                // update the current location of the mouse
                originalClick = new Point2D(e.X, e.Y);
            }
        }

        private void Form1_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Zooms in and out
            if (e.Delta > 0)
                distance *= 1.05;
            else if (e.Delta < 0)
                distance /= 1.05;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Check if there is a piece moving
            bool pieceMoving = false;
            foreach (Piece3D piece in pieces)
                if (piece.Drop == true)
                    pieceMoving = true;

            // If there is no piece still dropping
            if (pieceMoving == false)
            {
                // Check if the player has lost
                for (int i = 0; i < gridSize.X; i++)
                {
                    for (int k = 0; k < gridSize.Z; k++)
                    {
                        if (gameData[i, 1, k] == CellTypes.block && boxDisplayed == false)
                        {
                            // Add one final piece to show loss
                            addRandomPiece();
                            foreach (Piece3D piece in pieces)
                                piece.Drop = false;

                            // Display a messagebox asking if they want to play again once
                            boxDisplayed = true;
                            DialogResult dialogResult = MessageBox.Show("Game Over", "Do you want to play again?", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                boxDisplayed = false;
                                Restart();
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                this.Close();
                            }
                        }

                    }
                }
                // If they have not lost add one more piece
                if (boxDisplayed == false)
                {
                    holdUsed = false;
                    addRandomPiece();

                }
            }

            // Drops the piece lower
            foreach (Piece3D piece in pieces)
            {
                piece.Create(0); // Create the boxes that make up the piece to draw it
                UpdateGamedata();
                piece.DropDown(gameData);
            }

            // Checks if the level should be cleared and then clears it
            ClearLevel();

            polygons = new List<Polygon3D>();
            //ghostPieces = new List<Piece>();
            foreach (Piece3D piece in pieces)
            {
                foreach (Box part in piece.Parts)
                {
                    foreach (Polygon3D side in part.Sides)
                    {
                        polygons.Add(side);
                    }
                }
            }

            // Creating the ghost piece so it can drawn
            foreach (Piece3D ghostPiece in ghostPieces)
            {
                RaiseGhost();
                DropGhost();

                ghostPiece.Create(1);
                foreach (Box ghostPart in ghostPiece.Parts)
                {
                    foreach (Polygon3D ghostSide in ghostPart.Sides)
                    {
                        polygons.Add(ghostSide);
                    }
                }
            }

            this.Invalidate();
        }

        /// <summary>
        /// Brings the ghost piece to the top of the grid so that it can be free to move
        /// </summary>
        private void RaiseGhost()
        {
            foreach (Piece3D ghostPiece in ghostPieces)
            {
                ghostPiece.Raise = true;
                do
                {
                    ghostPiece.RaiseCell();
                } while (ghostPiece.Raise == true);
            }
        }

        /// <summary>
        /// Drop ghost piece down as far as it can go before hitting a piece
        /// </summary>
        private void DropGhost()
        {
            foreach (Piece3D ghostPiece in ghostPieces)
            {
                ghostPiece.Drop = true;
                do
                {
                    ghostPiece.DropCell(gameData);
                } while (ghostPiece.Drop == true);
            }
        }

        /// <summary>
        /// All non-moving pieces are added to the complete gameData
        /// </summary>
        private void UpdateGamedata()
        {
            foreach (Piece3D piece in pieces)
            {
                for (int i = 0; i < piece.GridSize.X; i++)
                {
                    for (int j = 0; j < piece.GridSize.Y; j++)
                    {
                        for (int k = 0; k < piece.GridSize.Z; k++)
                        {
                            if (piece.Drop == false)
                                if (piece.GameData[i, j, k] == CellTypes.block)
                                    gameData[i, j, k] = CellTypes.block;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Clears a level if it full of blocks
        /// </summary>
        private void ClearLevel()
        {
            bool clearLevel = true;
            int levelsCleared = 0;

            // Starts from bottom and goes to the top
            for (int j = (int)gridSize.Y - 1; j > 0; j--)
            {
                clearLevel = true;
                // Determines if a level is full
                for (int i = 0; i < gridSize.X; i++)
                {
                    for (int k = 0; k < gridSize.Z; k++)
                    {
                        if (gameData[i, j, k] != CellTypes.block)
                            clearLevel = false;
                    }
                }
                // If it is it clears that level
                if (clearLevel == true)
                {
                    levelsCleared++;
                    // Clears the level
                    foreach (Piece3D piece in pieces)
                        for (int i = 0; i < gridSize.X; i++)
                        {
                            for (int k = 0; k < gridSize.Z; k++)
                            {
                                gameData[i, j, k] = CellTypes.blank;
                                piece.GameData[i, j, k] = CellTypes.blank;
                            }
                        }
                    // Moves blocks above down 1 y value
                    foreach (Piece3D piece in pieces)
                        for (int l = j - 1; l > 0; l--)
                        {
                            for (int i = 0; i < gridSize.X; i++)
                            {
                                for (int k = 0; k < gridSize.Z; k++)
                                {
                                    if (gameData[i, l, k] == CellTypes.block && gameData[i, l + 1, k] == CellTypes.blank
                                        && piece.GameData[i, l, k] == CellTypes.block && piece.GameData[i, l + 1, k] == CellTypes.blank)
                                    {
                                        gameData[i, l, k] = CellTypes.blank;
                                        piece.GameData[i, l, k] = CellTypes.blank;
                                        gameData[i, l + 1, k] = CellTypes.block;
                                        piece.GameData[i, l + 1, k] = CellTypes.block;
                                    }
                                }
                            }
                        }
                }
            }

            // Adds 100 for every level cleared
            if (levelsCleared != 0)
            {
                for (int i = 1; i <= levelsCleared; i++)
                {
                    score += levelsCleared * 100;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape: // close game with escape
                    this.Dispose();
                    break;
                case Keys.P: // toggle timer
                    timer1.Enabled = !timer1.Enabled;
                    break;
                case Keys.Space: // Drop the piece a cell
                    UpdateGamedata();
                    foreach (Piece3D piece in pieces)
                        if (piece.Drop == true)
                        {
                            piece.DropCell(gameData);
                        }
                    break;
                case Keys.H: //Hold a piece
                    foreach (Piece3D piece in pieces)
                        if (piece.Drop == true && holdUsed == false)
                        {
                            // If the hold is empty add a new random piece. If it isn't add the piece from the hold
                            holdUsed = true;
                            hold = new Piece2D(piece.PieceType, hold.GridSize, hold.CellSize, hold.Location);

                            if (holdEmpty == false)
                            {
                                pieces.RemoveAt(pieces.Count - 1);
                                pieces.Add(new Piece3D(pieceType, gridSize, cellSize, 0, dropSpeed, new Point3D(1, 0, 1), gridHeight));
                                ghostPieces = new List<Piece3D>();
                                ghostPieces.Add(new Piece3D(pieceType, gridSize, cellSize, 0, dropSpeed, new Point3D(1, 0, 1), true, gridHeight));
                            }
                            else if (holdEmpty == true)
                            {
                                pieces.RemoveAt(pieces.Count - 1);
                                addRandomPiece();
                            }

                            holdEmpty = false;
                            pieceType = piece.PieceType;

                            return;
                        }
                    break;
                case Keys.R: // reset camera
                    rotationAngle = new Point3D(0, 0, 0);
                    break;
            }

            // Shift the piece on the Y-Axis
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Right || e.KeyCode == Keys.Left)
            {
                UpdateGamedata();
                bool move = true;
                foreach (Piece3D piece in pieces)
                {
                    piece.MoveShift(e, gameData);
                    move = piece.Move;
                }

                // If the actual piece can't move don't move the ghostpiece
                if (move == true)
                {
                    // Raise ghost above all the other blocks then move it then drop it
                    RaiseGhost();
                    foreach (Piece3D ghostPiece in ghostPieces)
                        ghostPiece.MoveShift(e, gameData);
                    DropGhost();
                }
            }

            //Rotate the piece on all axes
            if (e.KeyCode == Keys.Z || e.KeyCode == Keys.X || e.KeyCode == Keys.C)
            {
                UpdateGamedata();
                bool move = true;
                foreach (Piece3D piece in pieces)
                {
                    piece.Rotate(e, gameData);
                    move = piece.Move;
                }

                // If the actual piece can't rotate don't rotate the ghostpiece
                if (move == true)
                {
                    RaiseGhost();
                    foreach (Piece3D ghostPiece in ghostPieces)
                        ghostPiece.Rotate(e, gameData);
                    DropGhost();
                }
            }
        }

        /// <summary>
        /// Takes the next piece in the upcoming pieces and adds randomly adds one to the upcoming pieces and adds a ghost piece
        /// </summary>
        private void addRandomPiece()
        {
            Random r = new Random();
            int g = r.Next(0, 7);

            pieces.Add(new Piece3D(upcomingPieces[0].PieceType, gridSize, cellSize, 0, dropSpeed, new Point3D(gridSize.X / 2 - 1, 0, gridSize.Z / 2), gridHeight));
            ghostPieces = new List<Piece3D>();
            ghostPieces.Add(new Piece3D(upcomingPieces[0].PieceType, gridSize, cellSize, 0, dropSpeed, new Point3D(gridSize.X / 2 - 1, 0, gridSize.Z / 2), true, gridHeight));
            upcomingPieces.RemoveAt(0);

            upcomingPieces.Add(new Piece2D(g, holdGridSize, piece2DCellSize, new Point2D((ClientRectangle.Width / 2) - 110, (-1 * ClientRectangle.Height / 2) + (2 * piece2DCellSize * holdGridSize.Y))));
            upcomingPieces[0].Location.Y -= piece2DCellSize * holdGridSize.Y;
            upcomingPieces[1].Location.Y -= piece2DCellSize * holdGridSize.Y;
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Restart();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Settings settings = new Settings((int)gridSize.X / 2, cellSize);
            settings.Closing += new CancelEventHandler(Settings_Closing);
            settings.ShowDialog();
        }

        public void Settings_Closing(object sender, CancelEventArgs e)
        {
            Settings settings = (Settings)sender;
            gridSize = new Point3D(settings.GridSize * 2, 15, settings.GridSize * 2);
            cellSize = settings.CellSize;
            Restart();
        }

        private void controlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("X: Rotate on the X-axis\nZ: Rotate on the Z-axis\nC: Rotate on the Y-axis\nH: Hold piece\nSpace: Drop cell\nArrow Keys: Movement\nR: Reset Camera\nEsc: Close", "Controls");
        }
    }
}