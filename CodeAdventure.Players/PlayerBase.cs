using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAdventure.Players
{

	public class MoveRequest
	{
		public void Direction(MoveRequestDirection direction)
		{
			
		}

		public enum MoveRequestDirection
		{
			Up,
			Down,
			Left,
			Right
		}
	}

	public class MoveResponse
	{
		public MoveResultType ResultType { get; set; }
		public int EnergyCost;

		public enum MoveResultType
		{
			Success,
			Failure
		}
	}

	public class PlayerBase
	{

		protected Referee Referee ;


		protected PlayerBase()
		{
		}


		
		protected MoveResponse Move(MoveRequest request)
		{
			
			var moveResponse = new MoveResponse();
			moveResponse.EnergyCost = 50;
			moveResponse.ResultType = MoveResponse.MoveResultType.Success;
			return moveResponse;
		}

		
	}
}
