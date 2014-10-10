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
using Gif.Components;

namespace CodeAdventure
{
	class Program
	{




		private static void Main(string[] args)
		{


			var w = 50;
			var h = 10;

			Console.Clear();

			var maze = new Point[w, h];

			var imageFilePaths = new List<String>();

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

			var totalCells = w * h;
			var pos = new Point(5, 5);
			maze[pos.X, pos.Y].Visited = true;
			maze[pos.X, pos.Y].Symbol = " ";


			var stack = new Stack<Point>();
			var visited = 1;
			stack.Push(pos);


			while (stack.Count > 0)
			{
				var neighbours = GetNeighbours(pos, maze);
				if (neighbours.Any())
				{
					int r = rnd.Next(neighbours.Count);
					pos = neighbours[r];
					maze[pos.X, pos.Y].Visited = true;
					maze[pos.X, pos.Y].Symbol = " ";
					stack.Push(pos);
					

				}
				else
				{
					pos = stack.Pop();
				}
				//Console.Clear();
				//printMaze(maze, w, h);
				//Console.ReadKey();
				visited++;
				var mr = new MazeImage();

				var filePath = string.Format("c:\\tmp\\maze\\maze_{0}.png", visited);
				imageFilePaths.Add(filePath);
				mr.RenderMaze(maze, w, h, pos);
				mr.SaveImage(filePath, ImageFormat.Png);

				Console.WriteLine(string.Format("Visited: {0}/{1}   Stack:{2}", visited, w * h, stack.Count));
			}

			//var mr = new MazeImage();
			//mr.RenderMaze(maze, w, h);
			//mr.SaveImage(string.Format("c:\\tmp\\maze_{0}.jpg",Guid.NewGuid()),ImageFormat.Png);

			printMaze(maze, w, h);


			/* create Gif */
			//you should replace filepath
			//String[] imageFilePaths = new String[] { "c:\\01.png", "c:\\02.png", "c:\\03.png" };
			String outputFilePath = "c:\\tmp\\test.gif";
			AnimatedGifEncoder e = new AnimatedGifEncoder();
			e.Start(outputFilePath);
			e.SetDelay(100);
			//-1:no repeat,0:always repeat
			e.SetRepeat(0);
			foreach (var imageFilePath in imageFilePaths)
			{
				e.AddFrame(Image.FromFile(imageFilePath));
			}
			e.Finish();
			///* extract Gif */
			//string outputPath = "c:\\";
			//GifDecoder gifDecoder = new GifDecoder();
			//gifDecoder.Read("c:\\test.gif");
			//for (int i = 0, count = gifDecoder.GetFrameCount(); i < count; i++)
			//{
			//	Image frame = gifDecoder.GetFrame(i); // frame i
			//	frame.Save(outputPath + Guid.NewGuid().ToString()
			//						  + ".png", ImageFormat.Png);
			//}



			Console.ReadKey();
		}

		static Random rnd = new Random();

		public static List<Point> GetNeighbours(Point position, Point[,] maze)
		{
			var w = maze.GetLength(0);
			var h = maze.GetLength(1);

			//var neighbours = new List<Point>();


			//var xLowerBound = position.X - 2;
			//if (xLowerBound < 1) xLowerBound = 1;


			//var xUpperBound = position.X + 2;
			//if (xUpperBound >= w) xUpperBound = w -1;

			//var yLowerBound = position.X - 2;
			//if (yLowerBound < 1) yLowerBound = 1;


			//var yUpperBound = position.X + 2;
			//if (yUpperBound >= h) yUpperBound = h -1;


			//for (int x = xLowerBound; x <= xUpperBound; x++)
			//{
			//	for (int y = yLowerBound; y <= yUpperBound; y++)
			//	{
			//		neighbours.Add(maze[x, y]);
			//	}
			//}

			//neighbours.RemoveAll(r => r.Visited);
			//neighbours.RemoveAll(r => r.X == position.X && r.Y == position.Y);

			
			//neighbours.RemoveAll(r => r.X > 0 && r.Y > 0 && maze[r.X - 1, r.Y - 1].Visited);
			//neighbours.RemoveAll(r => r.X > 0 && r.Y < yLowerBound && maze[r.X - 1, r.Y + 1].Visited);
			//neighbours.RemoveAll(r => r.X < xUpperBound && r.Y > 0 && maze[r.X + 1, r.Y - 1].Visited);
			//neighbours.RemoveAll(r => r.X < xUpperBound && r.Y < yUpperBound && maze[r.X + 1, r.Y + 1].Visited);

			var neighbours = new List<Point>();
			try
			{
				if (!maze[position.X - 2, position.Y].Visited && !maze[position.X - 1, position.Y + 1].Visited && !maze[position.X - 1, position.Y - 1].Visited)
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
				if (!maze[position.X, position.Y - 2].Visited && !maze[position.X - 1, position.Y - 1].Visited && !maze[position.X + 1, position.Y - 1].Visited)
					neighbours.Add(maze[position.X, position.Y - 1]);
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

		public void RenderMaze(Point[,] maze, int w, int h, Point position)
		{
			pictureBox1.Size = new Size(w * 5, h * 5);

			Bitmap flag = new Bitmap(w * 5, h * 5);
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
					rect.Height = 5;
					rect.Width = 5;
					rect.X = x * 5;
					rect.Y = y * 5;

					var block = maze[x, y];
					if (block.Symbol == "#")
						flagGraphics.FillRectangle(Brushes.Brown, rect);
					else
						flagGraphics.FillRectangle(Brushes.BurlyWood, rect);

					//Console.Write(block.Symbol);
				}
				//Console.Write(Environment.NewLine);
			}

			var player = new Rectangle();
			player.Height = 5;
			player.Width = 5;
			player.X = position.X * 5;
			player.Y = position.Y * 5;
			flagGraphics.FillRectangle(Brushes.Black, player);

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
