using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodeAdventure.Core;

namespace CodeAdventure
{
	class Program
	{




		private static void Main(string[] args)
		{


			var w = 1000;
			var h = 1000;
			
			Console.Clear();

			var maze = new Point[w, h];


			for (int i = 0; i < w; i++)
			{
				for (int j = 0; j < h; j++)
				{
					maze[i, j] = new Point();
					maze[i, j].Symbol = "#";
					maze[i, j].Visited = false;
					maze[i, j].X = i;
					maze[i, j].Y = j;
				}
				Console.Write(Environment.NewLine);
			}

			var totalCells = w*h;
			var pos = new Point(10, 10);
			maze[pos.X, pos.Y].Visited = true;
			maze[pos.X, pos.Y].Symbol = " ";


			var stack = new Stack<Point>();
			var visited = 1;
			stack.Push(pos);
			

			while (stack.Count > 0 )
			{
				var neighbours = GetNeighbours(pos, maze);
				if (neighbours.Any())
				{
					int r = rnd.Next(neighbours.Count);
					pos = neighbours[r];
					maze[pos.X, pos.Y].Visited = true;
					maze[pos.X, pos.Y].Symbol = " ";
					stack.Push(pos);
					visited++;

				}
				else
				{
					pos = stack.Pop();
				}
				//Console.Clear();
				//printMaze(maze, w, h);
				//Console.ReadKey();
				Console.WriteLine(string.Format("Visited: {0}/{1}   Stack:{2}",visited,w*h,stack.Count));
			}

			var mr = new MazeImage();
			mr.RenderMaze(maze, w, h);
			mr.SaveImage(string.Format("c:\\tmp\\maze_{0}.jpg",Guid.NewGuid()),ImageFormat.Png);

			printMaze(maze, w, h);

			Console.ReadKey();
		}

		static Random rnd = new Random();

		public static List<Point> GetNeighbours(Point position, Point[,] maze)
		{
			var neighbours = new List<Point>();
			try
			{
				if (!maze[position.X - 2, position.Y].Visited && !maze[position.X - 1, position.Y + 1].Visited && !maze[position.X - 1, position.Y -1].Visited)
					neighbours.Add(maze[position.X - 1, position.Y]);
			}
			catch { }
			try
			{
				if (!maze[position.X + 2, position.Y].Visited && !maze[position.X + 1, position.Y + 1].Visited && !maze[position.X + 1, position.Y - 1].Visited)
					neighbours.Add(maze[position.X + 1, position.Y]);
			}
			catch { }
			try
			{
				if (!maze[position.X, position.Y + 2].Visited && !maze[position.X - 1, position.Y + 1].Visited && !maze[position.X + 1, position.Y + 1].Visited)
					neighbours.Add(maze[position.X, position.Y + 1]);
			}
			catch { }
			try
			{
				if (!maze[position.X, position.Y -2].Visited && !maze[position.X - 1, position.Y - 1].Visited && !maze[position.X + 1, position.Y - 1].Visited)
					neighbours.Add(maze[position.X , position.Y - 1]);
			}
			catch { }

			neighbours.RemoveAll(r => r.Visited);
			neighbours.RemoveAll(r => r.X == position.X && r.Y == position.Y);

			return neighbours;
		}


		public static void printMaze(Point[,] maze, int w, int h)
		{
			for (int i = 0; i < w; i++)
			{
				for (int j = 0; j < h; j++)
				{
					var block = maze[i, j];
					Console.Write(block.Symbol);
				}
				Console.Write(Environment.NewLine);
			}
		}


	}

	public class MazeImage
	{
		private PictureBox pictureBox1 = new PictureBox();

		public void RenderMaze(Point[,] maze, int w, int h)
		{
			pictureBox1.Size = new Size(210, 110);

			Bitmap flag = new Bitmap(w*10,h*10);
			Graphics flagGraphics = Graphics.FromImage(flag);
			int red = 0;
			int white = 11;
			//while (white <= 100)
			//{
			//	flagGraphics.FillRectangle(Brushes.Red, 0, red, 200, 10);
			//	flagGraphics.FillRectangle(Brushes.White, 0, white, 200, 10);
			//	red += 20;
			//	white += 20;
			//}

			for (int x = 0; x < w; x++)
			{
				for (int y = 0; y < h; y++)
				{
					var rect = new Rectangle();
					rect.Height = 10;
					rect.Width = 10;
					rect.X = x*10;
					rect.Y = y * 10;

					var block = maze[x, y];
					if(block.Symbol == "#")
						flagGraphics.FillRectangle(Brushes.Brown, rect);
					else
						flagGraphics.FillRectangle(Brushes.BurlyWood, rect);

					//Console.Write(block.Symbol);
				}
				//Console.Write(Environment.NewLine);
			}

			pictureBox1.Image = flag;

		}

		public void SaveImage(string file, ImageFormat format)
		{
			pictureBox1.Image.Save(file, format);
		}
	}


	public class Point
	{
		public string Symbol { get; set; }
		public bool Visited { get; set; }
		public int X { get; set; }
		public int Y { get; set; }
		public Point()
		{ }
		public Point(int x, int y)
		{
			X = x;
			y = y;
		}
	}
}
