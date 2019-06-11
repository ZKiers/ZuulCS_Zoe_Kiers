﻿using System;

namespace ZuulCS
{
	public class Game
	{
		private Parser parser;
		private Player player;

		public Game()
		{
			player = new Player();
			createRooms();
			parser = new Parser();
		}

		private void createRooms()
		{
			Room outside, theatre, pub, lab, office, treeHouse;

			// create the rooms
			outside = new Room("outside the main entrance of the university");
			theatre = new Room("in a lecture theatre");
			pub = new Room("in the campus pub");
			lab = new Room("in a computing lab");
			office = new Room("in the computing admin office");
			treeHouse = new Room("in the secret tree house built by the seniors");
			// create the items for in the rooms
			Weapon sword = new Weapon("Sword", 4, 50);

			// initialise room exits and put items in the rooms.
			outside.setExit("east", theatre);
			outside.setExit("south", lab);
			outside.setExit("west", pub);
			outside.setExit("up", treeHouse);

			theatre.setExit("west", outside);
			theatre.inventory.AddItem(sword);

			pub.setExit("east", outside);

			lab.setExit("north", outside);
			lab.setExit("east", office);

			office.setExit("west", lab);

			treeHouse.setExit("down", outside);

			player.SetCurrentRoom(outside);  // start game outside
		}


		/**
	     *  Main play routine.  Loops until end of play.
	     */
		public void play()
		{
			printWelcome();

			// Enter the main command loop.  Here we repeatedly read commands and
			// execute them until the game is over.
			bool finished = false;
			while (!finished)
			{
				Command command = parser.getCommand();
				finished = processCommand(command);
				if (!player.isAlive())
				{
					finished = true;
					TextEffects.ErrorMessage("Game Over");
					TextEffects.ErrorMessage("You've Died");
				}
			}
			Console.WriteLine("Thank you for playing.");
		}

		/**
	     * Print out the opening message for the player.
	     */
		private void printWelcome()
		{
			Console.WriteLine();
			Console.WriteLine("Welcome to Zuul!");
			Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
			Console.WriteLine("Type 'help' if you need help.");
			Console.WriteLine();
			Console.WriteLine(player.GetCurrentRoom().getLongDescription());
			Console.WriteLine(player.getHealth());
		}

		/**
	     * Given a command, process (that is: execute) the command.
	     * If this command ends the game, true is returned, otherwise false is
	     * returned.
	     */
		private bool processCommand(Command command)
		{
			bool wantToQuit = false;

			if (command.isUnknown())
			{
				TextEffects.ErrorMessage("I don't know what you mean...");
				return false;
			}

			string commandWord = command.getCommandWord();
			switch (commandWord)
			{
				case "help":
					printHelp();
					break;
				case "go":
					goRoom(command);
					break;
				case "quit":
					wantToQuit = true;
					break;
				case "look":
					TextEffects.CheckNullWriteLine(player.GetCurrentRoom().getLongDescription());
					TextEffects.CheckNullWriteLine(player.getHealth());
					break;
				case "inventory":
					TextEffects.CheckNullWriteLine(player.GetInventoryDesc());
					break;
				case "take":
					TextEffects.CheckNullWriteLine(player.PickupItem(command));
					break;
				case "drop":
					TextEffects.CheckNullWriteLine(player.DropItem(command));
					break;
				case "use":
					TextEffects.CheckNullWriteLine(player.UseItem(command));
					break;
				case "attack":
					TextEffects.CheckNullWriteLine(player.Attack());
					break;
				case "equip":
					TextEffects.CheckNullWriteLine(player.EquipItem(command));
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
		private void printHelp()
		{
			Console.WriteLine("You are lost. You are alone.");
			Console.WriteLine("You wander around at the university.");
			Console.WriteLine();
			Console.WriteLine("Your command words are:");
			parser.showCommands();
		}

		/**
	     * Try to go to one direction. If there is an exit, enter the new
	     * room, otherwise print an error message.
	     */
		private void goRoom(Command command)
		{
			if (!command.hasSecondWord())
			{
				// if there is no second word, we don't know where to go...
				TextEffects.ErrorMessage("Go where?");
				return;
			}

			string direction = command.getSecondWord();

			// Try to leave current room.
			Room nextRoom = player.GetCurrentRoom().getExit(direction);

			if (nextRoom == null)
			{
				TextEffects.ErrorMessage("There is no door to " + direction + "!");
			}
			else
			{
				player.SetCurrentRoom(nextRoom);
				Console.WriteLine(player.GetCurrentRoom().getLongDescription());
			}
		}

	}
}
