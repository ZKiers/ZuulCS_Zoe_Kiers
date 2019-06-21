using System.Collections.Generic;
using System;

namespace ZuulCS
{
	public class Game
	{
		private Parser parser;
		private Player player;
		private bool loadedFromSave;
		private bool inCombat;

		public Game()
		{
			loadedFromSave = false;
			player = new Player();
			if (System.IO.File.Exists("Save.JSON")) { player = SaveFile.LoadPlayerFromSaveFile(); loadedFromSave = true; }
			CreateRooms();
			parser = new Parser();
		}

		private void CreateRooms()
		{
			Room outside, theatre, pub, lab, office, treeHouse;
			List<Room> rooms = new List<Room>();

			// create the rooms
			outside = new Room("outside the main entrance of the university");
			theatre = new Room("in a lecture theatre");
			pub = new Room("in the campus pub");
			lab = new Room("in a computing lab");
			office = new Room("in the computing admin office");
			treeHouse = new Room("in the secret tree house built by the seniors");

			//Add rooms to the Rooms List
			rooms.Add(outside);
			rooms.Add(theatre);
			rooms.Add(pub);
			rooms.Add(lab);
			rooms.Add(office);
			rooms.Add(treeHouse);
			// create the items for in the rooms
			Weapon sword = new Weapon("Sword", 4, 50);
			HealthPotion hp1 = new HealthPotion("WeirdPotion", 0.1, 40);

			// initialise room exits and put items in the rooms.
			outside.SetExit("east", theatre);
			outside.SetExit("south", lab);
			outside.SetExit("west", pub);
			outside.SetExit("up", treeHouse);

			theatre.SetExit("west", outside);
			theatre.inventory.AddItem(sword);

			pub.SetExit("east", outside);
			pub.AddEnemy("BadBoi");

			lab.SetExit("north", outside);
			lab.SetExit("east", office);
			lab.inventory.AddItem(hp1);

			office.SetExit("west", lab);
			treeHouse.SetExit("down", outside);
			//assign ID's to all rooms
			for (int i = 0; i < (rooms.Count - 1); i++) { rooms[i].ID = i; }
			if (loadedFromSave)
			{
				for (int i = 0; i < (rooms.Count - 1); i++)
				{
					if (player.CurrentRoom.ID == i) { player.CurrentRoom = rooms[i]; break; }
				}
			} else { player.CurrentRoom = outside; } // start game outside
			

			//lock certain rooms
			treeHouse.SetLocked(office, "TreeKey");
			office.SetLocked(pub, "CoolKey");
		}


		/**
	     *  Main play routine.  Loops until end of play.
	     */
		public void Play()
		{
			PrintWelcome();

			// Enter the main command loop.  Here we repeatedly read commands and
			// execute them until the game is over.
			bool finished = false;
			while (!finished)
			{
				if (player.IsAlive())
				{
					Command command = parser.GetCommand();
					finished = ProcessCommand(command);
				}
				if (!player.IsAlive())
				{
					finished = true;
					TextEffects.ErrorMessage("Game Over");
					TextEffects.ErrorMessage("You've Died");
				}
			}
			SaveFile.GenerateSaveFile(player);
			Console.WriteLine("Thank you for playing.");
			System.Threading.Thread.Sleep(500);
		}

		/**
	     * Print out the opening message for the player.
	     */
		private void PrintWelcome()
		{
			Console.WriteLine();
			Console.WriteLine("Welcome to Zuul!");
			Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
			Console.WriteLine("Type 'help' if you need help.");
			Console.WriteLine();
			Console.WriteLine(player.CurrentRoom.GetLongDescription());
			Console.WriteLine(player.GetHealth());
		}

		/**
	     * Given a command, process (that is: execute) the command.
	     * If this command ends the game, true is returned, otherwise false is
	     * returned.
	     */
		private bool ProcessCommand(Command command)
		{
			bool wantToQuit = false;

			if (command.IsUnknown())
			{
				TextEffects.ErrorMessage("I don't know what you mean...");
				return false;
			}

			string commandWord = command.CommandWord;
			switch (commandWord)
			{
				case "help":
					PrintHelp();
					break;
				case "go":
					if (inCombat) { TextEffects.ErrorMessage("You can't do that in combat!"); break; }
					GoRoom(command);
					break;
				case "quit":
					if (inCombat) { TextEffects.ErrorMessage("You can't do that in combat!"); break; }
					wantToQuit = true;
					break;
				case "look":
					if (inCombat) { TextEffects.ErrorMessage("You can't do that in combat!"); break; }
					TextEffects.CheckNullWriteLine(player.CurrentRoom.GetLongDescription());
					break;
				case "inventory":
					TextEffects.CheckNullWriteLine(player.GetHealth());
					TextEffects.CheckNullWriteLine(player.GetInventoryDesc());
					break;
				case "take":
					if (inCombat) { TextEffects.ErrorMessage("You can't do that in combat!"); break; }
					TextEffects.CheckNullWriteLine(player.PickupItem(command));
					break;
				case "drop":
					if (inCombat) { TextEffects.ErrorMessage("You can't do that in combat!"); break; }
					TextEffects.CheckNullWriteLine(player.DropItem(command));
					break;
				case "use":
					TextEffects.CheckNullWriteLine(player.UseItem(command));
					break;
				case "attack":
					if (inCombat) { TextEffects.CheckNullWriteLine(player.AttackEnemy()); break; }
					TextEffects.CheckNullWriteLine(player.Attack());
					break;
				case "equip":
					TextEffects.CheckNullWriteLine(player.EquipItem(command));
					break;
				case "unequip":
					TextEffects.CheckNullWriteLine(player.Unequip());
					break;
			}

			return wantToQuit;
		}
		// implementations of user commands:

		/**
	     * Print out some help information.
	     * Here we print some stupid, cryptic message and a list of the
	     * command words.
	     */
		private void PrintHelp()
		{
			Console.WriteLine("You are lost. You are alone.");
			Console.WriteLine("You wander around at the university.");
			Console.WriteLine();
			Console.WriteLine("Your command words are:");
			parser.ShowCommands();
		}

		/**
	     * Try to go to one direction. If there is an exit, enter the new
	     * room, otherwise print an error message.
	     */
		private void GoRoom(Command command)
		{
			if (!command.HasSecondWord())
			{
				// if there is no second word, we don't know where to go...
				TextEffects.ErrorMessage("Go where?");
				return;
			}

			string direction = command.SecondWord;
			
			// Try to leave current room.
			Room nextRoom = player.CurrentRoom.GetExit(direction);
			if (direction == "back") { nextRoom = player.LastRoom; }

			if (nextRoom == null)
			{
				TextEffects.ErrorMessage("There is no door to " + direction + "!");
			} else if(nextRoom.IsLocked())
			{
				TextEffects.ErrorMessage("This door is locked.");
			}
			else
			{
				if (nextRoom.Enemies.Count > 0)
				{
					InitiateCombat(nextRoom);
				}
				player.LastRoom = player.CurrentRoom;
				player.CurrentRoom = nextRoom;
				Console.WriteLine(player.CurrentRoom.GetLongDescription());
			}
		}
		private void InitiateCombat(Room room)
		{
			inCombat = true;
			player.FightingInRoom = room;
			while (room.Enemies.Count > 0)
			{
				Console.WriteLine("An enemy engages you in combat!");
				Console.WriteLine(room.Enemies[0].DisplayName + " appeared to fight you!");
				while (room.Enemies[0].Health > 0)
				{
					player.Damage(room.Enemies[0].AttackPlayer());
					TextEffects.ErrorMessage(room.Enemies[0].attackDesc);
					if (!player.IsAlive()) { break; }
					Console.WriteLine("What do you do?");
					Command command = parser.GetCommand();
					ProcessCommand(command);
				}
				Console.WriteLine("You've successfully defeated the " + room.Enemies[0].DisplayName + "!");
				room.Enemies.RemoveAt(0);
			}
			inCombat = false;
			player.FightingInRoom = null;
		}

	}
}
