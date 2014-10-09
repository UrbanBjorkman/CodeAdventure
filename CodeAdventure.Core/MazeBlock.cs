using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeAdventure.Core
{
	public class MazeBlock
	{
		public MazeBlockType Type { get; set; }

		public string Symbol()
		{
			switch (Type)
			{
				case MazeBlockType.Debris:
					return "w";
					break;
				case MazeBlockType.Wall:
					return "#";
					break;
				case MazeBlockType.Goal:
					return "G";
					break;
			}
			return " ";
		}

		public int X { get; set; }
		public int Y { get; set; }
	}
}
