using System;

namespace CodeAdventure.Core
{
	public class MazeGrid
	{
		public MazeBlock[,] Grid;
		public int Height { get; private set; }
		public int Width { get; private set; }


		public MazeGrid(int width, int height)
		{
			Grid = new MazeBlock[width,height];
			Width = width;
			Height = height;

			var r = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));

			for (int i = 0; i < Width; i++)
			{
				for (int j = 0; j < Height; j++)
				{
					
					
					var type = MazeBlockType.EmptyRoom;
					if (r.Next(0, 100) < 10)
					{
						type = MazeBlockType.Wall;
					}
					else if (r.Next(0, 100) < 5)
					{
						type = MazeBlockType.Debris;

					}
					Grid[i,j] = new MazeBlock {Type = type, X = i, Y = j};
				}
			}

			Grid[r.Next(0, Width),r.Next(0, Height)].Type = MazeBlockType.Goal;

		}
	}
}