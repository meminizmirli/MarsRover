using System;
using System.Collections.Generic;
using System.Linq;
using MarsRover.Enums;
using MarsRover.Extentions;
using MarsRover.Models;

namespace MarsRover
{
    static class Program
    {
        private static Dictionary<int, string> _directionDictionary;
        private static Dictionary<int, string> _movementDictionary;
        private static readonly MarsCoordinates MarsCoordinates = new MarsCoordinates();
        static void Main(string[] args)
        {
            _directionDictionary = GeneralExtentions.EnumSelectAsDictionary<Direction>();
            _movementDictionary = GeneralExtentions.EnumSelectAsDictionary<MovementCommand>();
            MarsCoordinates.MinXCoordinate = 0;
            MarsCoordinates.MinYCoordinate = 0;

            Console.WriteLine($"Minimum Coordinates (X Y) : {MarsCoordinates.MinXCoordinate} {MarsCoordinates.MinYCoordinate}");
            bool doWhileBreak;
            do
            {
                doWhileBreak = false;
                Console.Write("Maximum Coordinates (X Y) : ");
                var maxCoordinateInput = Console.ReadLine();
                try
                {
                    var maxCoordinateArray = maxCoordinateInput.Split(' ').Select(int.Parse).ToArray();
                    MarsCoordinates.MaxXCoordinate = maxCoordinateArray[0];
                    MarsCoordinates.MaxYCoordinate = maxCoordinateArray[1];
                }
                catch
                {
                    Console.WriteLine("Wrong Entry! Try Again");
                    Console.WriteLine("Sample introduction : 5 5");
                    doWhileBreak = true;
                }
            } while (doWhileBreak);


            var roboticRovers = new List<RoboticRover>();
            do
            {
                var roboticRoverModel = new RoboticRover();
                do
                {
                    doWhileBreak = false;
                    Console.Write("Robotic Rover (X Y D) : ");
                    var roboticRoverInput = Console.ReadLine();
                    try
                    {
                        var roboticRoverArray = roboticRoverInput.Split(' ').ToArray();
                        var xCoordinate = int.Parse(roboticRoverArray[0]);
                        var yCoordinate = int.Parse(roboticRoverArray[1]);
                        if (xCoordinate > MarsCoordinates.MaxXCoordinate || xCoordinate < MarsCoordinates.MinXCoordinate || yCoordinate > MarsCoordinates.MaxYCoordinate || yCoordinate < MarsCoordinates.MinYCoordinate)
                        {
                            Console.WriteLine($"The coordinates you entered are outside the mars surface. Minimum Coordinate = {MarsCoordinates.MinXCoordinate},{MarsCoordinates.MinYCoordinate}. Maximum Coordinate {MarsCoordinates.MaxXCoordinate},{MarsCoordinates.MaxYCoordinate}");
                            doWhileBreak = true;
                            continue;
                        }
                        roboticRoverModel.XCoordinate = xCoordinate;
                        roboticRoverModel.YCoordinate = yCoordinate;
                        roboticRoverModel.DirectionId = _directionDictionary.First(x => x.Value == roboticRoverArray[2].ToUpper()).Key;
                    }
                    catch
                    {
                        Console.WriteLine("Wrong Entry! Try Again");
                        Console.WriteLine("Sample introduction : 1 2 N");
                        doWhileBreak = true;
                    }
                } while (doWhileBreak);
                do
                {
                    doWhileBreak = false;
                    Console.Write("Movement Command : ");
                    var movementCommandInput = Console.ReadLine();
                    try
                    {
                        var movementCommandArray = movementCommandInput.ToUpper().ToArray();
                        roboticRoverModel.MovementCommads = movementCommandArray.Select(x => _movementDictionary.First(y => y.Value == x.ToString()).Key).ToList();
                    }
                    catch
                    {
                        Console.WriteLine("Wrong Entry! Try Again");
                        Console.WriteLine("Sample introduction : LMLMLMLMM");
                        doWhileBreak = true;
                    }
                } while (doWhileBreak);
                roboticRovers.Add(roboticRoverModel);

                Console.WriteLine("Press f2 to add new record. Or press any key to continue.");
                doWhileBreak = Console.ReadKey().Key == ConsoleKey.F2;

            } while (doWhileBreak);

            Console.WriteLine();
            Console.WriteLine("---------");
            Console.WriteLine();
            roboticRovers.ForEach(MoveRoboticRover);
            roboticRovers.ForEach(x => Console.WriteLine($"Robotic Rover Locaiton : {x.XCoordinate} {x.YCoordinate} {_directionDictionary[x.DirectionId]}"));

            Console.ReadKey();
        }

        static void MoveRoboticRover(RoboticRover rover)
        {
            rover.MovementCommads.ForEach(x =>
            {
                switch (x)
                {
                    case (int) MovementCommand.M:
                        switch (rover.DirectionId)
                        {
                            case (int) Direction.N:
                                if (rover.YCoordinate != MarsCoordinates.MaxYCoordinate)
                                    rover.YCoordinate++;
                                break;
                            case (int) Direction.S:
                                if (rover.YCoordinate != MarsCoordinates.MinYCoordinate)
                                    rover.YCoordinate--;
                                break;
                            case (int) Direction.E:
                                if (rover.XCoordinate != MarsCoordinates.MaxXCoordinate)
                                    rover.XCoordinate++;
                                break;
                            case (int) Direction.W:
                                if (rover.XCoordinate != MarsCoordinates.MinXCoordinate)
                                    rover.XCoordinate--;
                                break;
                        }
                        break;
                    case (int) MovementCommand.L:
                        if (rover.DirectionId != (int) Direction.E)
                            rover.DirectionId++;
                        else
                            rover.DirectionId = (int) Direction.N;
                        break;
                    case (int) MovementCommand.R:
                        if (rover.DirectionId != (int) Direction.N)
                            rover.DirectionId--;
                        else
                            rover.DirectionId = (int) Direction.E;
                        break;
                }
            });
        }
    }
}
