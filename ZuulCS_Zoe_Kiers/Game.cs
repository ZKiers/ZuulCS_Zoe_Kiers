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
		private static Game instance = null;

		private Game()
		{
			loadedFromSave = false;
			player = new Player(false);
			if (System.IO.File.Exists(SaveFile.saveFileName)) { player = SaveFile.LoadPlayerFromSaveFile(); loadedFromSave = true; }
			CreateRooms();
			parser = new Parser();
		}

		public static Game getInstance()
		{
			if(instance == null)
			{
				instance = new Game();
			}

			return instance;
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
			HealthPotion hp1 = new HealthPotion("Weird Potion", 0.1, 40);
			//Item genericItem = new Item();
			//genericItem.Name = "Ring";

			// initialise room exits and put items in the rooms.
			outside.SetExit("east", theatre);
			outside.SetExit("south", lab);
			outside.SetExit("west", pub);
			outside.SetExit("up", treeHouse);
			//outside.inventory.AddItem(genericItem);

			theatre.SetExit("west", outside);
			theatre.inventory.AddItem(sword);

			pub.SetExit("east", outside);
			pub.AddEnemy("Fritz");

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
			treeHouse.SetLocked(office, "Tree Key");
			office.SetLocked(pub, "Cool Key");
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
					TextEffects.ColoredMessage("Game Over\n", "DarkRed");
					TextEffects.ColoredMessage("You've Died\n", "DarkRed");
				}
			}
			Console.WriteLine("Saving...");
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
			TextEffects.ColoredMessage("You have " + player.Health + " health!\n", "Red");
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
				TextEffects.ColoredMessage("I don't know what you mean...\n", "DarkRed");
				return false;
			}

			string commandWord = command.CommandWord;
			switch (commandWord)
			{
				case "help":
					PrintHelp();
					break;
				case "go":
					if (inCombat) { TextEffects.ColoredMessage("You can't do that in combat!\n", "DarkRed"); break; }
					GoRoom(command);
					break;
				case "quit":
					if (inCombat) { TextEffects.ColoredMessage("You can't do that in combat!\n", "DarkRed"); break; }
					wantToQuit = true;
					break;
				case "look":
					if (inCombat) { TextEffects.ColoredMessage("You can't do that in combat!\n", "DarkRed"); break; }
					TextEffects.CheckNullWriteLine(player.CurrentRoom.GetLongDescription());
					break;
				case "inventory":
					TextEffects.ColoredMessage(player.GetHealth(), "Red");
					TextEffects.SecondaryColoredMessage(player.GetInventoryDesc(), "White", "DarkGray");
					break;
				case "take":
					if (inCombat) { TextEffects.ColoredMessage("You can't do that in combat!\n", "DarkRed"); break; }
					TextEffects.CheckNullWriteLine(player.PickupItem(command));
					break;
				case "drop":
					if (inCombat) { TextEffects.ColoredMessage("You can't do that in combat!\n", "DarkRed"); break; }
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
			Console.WriteLine("╔" + TextEffects.GenerateLineText("═", "Commands", 39));
			parser.ShowCommands();
			Console.Write("╚" + TextEffects.GenerateLine("═", 39) + "\n");
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
				TextEffects.ColoredMessage("Go where?\n", "DarkRed");
				return;
			}

			string direction = command.SecondWord;
			
			// Try to leave current room.
			Room nextRoom = player.CurrentRoom.GetExit(direction);
			if (direction.ToLower() == "back") { nextRoom = player.LastRoom; }

			if (nextRoom == null)
			{
				TextEffects.ColoredMessage("There is no door to " + direction + "!\n", "DarkRed");
			} else if(nextRoom.IsLocked())
			{
				TextEffects.ColoredMessage("This door is locked.\n", "DarkRed");
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
			Random RNG = new Random();
			while (room.Enemies.Count > 0)
			{
				Console.WriteLine("An enemy engages you in combat!");
				Console.WriteLine(room.Enemies[0].DisplayName + " appeared to fight you!");
                Console.WriteLine(room.Enemies[0].statCard(20));
                while (room.Enemies[0].Health > 0)
				{
					Console.WriteLine("What do you do?");
					Command command = parser.GetCommand();
					ProcessCommand(command);
					if (command.CommandWord == "attack" || command.CommandWord == "use" || command.CommandWord == "equip" || command.CommandWord == "unequip")
					{
						if (room.Enemies[0].Health > 0)
						{
							player.Damage(room.Enemies[0].AttackPlayer());
							TextEffects.ColoredMessage(room.Enemies[0].attackDesc, "DarkRed");
                            TextEffects.ColoredMessage("You have " + player.Health + " health!\n", "Red");
                            Console.WriteLine(room.Enemies[0].statCard(20));
                        }
					}
					if (!player.IsAlive()) { break; }
				}
				Console.WriteLine("You've successfully defeated the " + room.Enemies[0].DisplayName + "!");
				switch (RNG.Next(2))
				{
					case 0:
						room.inventory.AddItem(room.Enemies[0].DropWeapon());
						break;
					case 1:
						room.inventory.AddItem(room.Enemies[0].DropHealthPotion());
						break;
					case 3:
						Console.WriteLine(room.Enemies[0].DisplayName + " has not dropped anything!");
						break;
				}
				room.Enemies.RemoveAt(0);
			}
			inCombat = false;
			player.FightingInRoom = null;
		}

	}
}
