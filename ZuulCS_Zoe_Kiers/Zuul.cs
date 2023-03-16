using System;

namespace ZuulCS
{
	class Zuul
	{
		/**
		 * Create and play the Game.
		 */
		public static void Main(string[] args)
		{
			Game game = Game.getInstance();
            game.Play();
		}
	}
}
