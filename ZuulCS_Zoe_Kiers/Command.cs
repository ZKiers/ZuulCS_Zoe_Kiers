namespace ZuulCS
{
	public class Command
	{
		public string CommandWord { get; set; }
		public string SecondWord { get; set; }
		public string ThirdWord { get; set; }

		/**
	     * Create a command object. First and second word must be supplied, but
	     * either one (or both) can be null. The command word should be null to
	     * indicate that this was a command that is not recognised by this game.
	     */
		public Command(string firstWord, string secondWord, string thirdWord)
		{
			this.CommandWord = firstWord;
			this.SecondWord = secondWord;
			this.ThirdWord = thirdWord;
		}
		/**
	     * Return true if this command was not understood.
	     */
		public bool IsUnknown()
		{
			return (CommandWord == null);
		}

		/**
	     * Return true if the command has a second word.
	     */
		public bool HasSecondWord()
		{
			return (SecondWord != null);
		}
		public bool HasThirdWord()
		{
			return (ThirdWord != null);
		}
	}
}
