﻿namespace RobotService.Core
{
    using Contracts;
    using Models.Garages;
    using Models.Procedures;
    using Models.Robots;
    using RobotService.Models.Procedures.Contracts;
    using RobotService.Models.Robots.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class Controller : IController
    {
        private readonly Garage _garage;
        private readonly Dictionary<string, IProcedure> _procedures;

        public Controller()
        {
            this._garage = new Garage();
            this._procedures = new Dictionary<string, IProcedure>()
            {
                {"Chip", new Chip()},
                {"TechCheck", new TechCheck()},
                {"Rest", new Rest()},
                {"Work", new Work()},
                {"Charge", new Charge()},
                {"Polish", new Polish()},
            };
        }

        public string Manufacture(string robotType, string name, int energy, int happiness, int procedureTime)
        {
            IRobot robot = null;

            if (robotType == "PetRobot")
            {
                robot = new PetRobot(name, energy, happiness, procedureTime);
            }
            else if (robotType == "WalkerRobot")
            {
                robot = new WalkerRobot(name, energy, happiness, procedureTime);
            }
            else if (robotType == "HouseholdRobot")
            {
                robot = new HouseholdRobot(name, energy, happiness, procedureTime);
            }
            else
            {
                throw new ArgumentException($"{robotType} type doesn't exist");
            }

            this._garage.Manufacture(robot);

            return $"Robot {name} registered successfully";
        }

        public string Chip(string robotName, int procedureTime)
        {
            DoProcedure(robotName, procedureTime);

            return $"{robotName} had chip procedure";
        }

        public string TechCheck(string robotName, int procedureTime)
        {
            DoProcedure(robotName, procedureTime);

            return $"{robotName} had tech check procedure";
        }

        public string Rest(string robotName, int procedureTime)
        {
            DoProcedure(robotName, procedureTime);

            return $"{robotName} had rest procedure";
        }

        public string Work(string robotName, int procedureTime)
        {
            DoProcedure(robotName, procedureTime);

            return $"{robotName} was working for {procedureTime} hours";
        }

        public string Charge(string robotName, int procedureTime)
        {
            DoProcedure(robotName, procedureTime);

            return $"{robotName} had charge procedure";
        }

        public string Polish(string robotName, int procedureTime)
        {
            DoProcedure(robotName, procedureTime);

            return $"{robotName} had polish procedure";
        }

        public string Sell(string robotName, string ownerName)
        {
            var robot = GetRobot(robotName);

            if (robot.IsChipped)
            {
                return $"{ownerName} bought robot with chip";
            }

            return $"{ownerName} bought robot without chip";
        }

        public string History(string procedureType)
        {
            var procedure = this._procedures
                .FirstOrDefault(p => p.Key == procedureType);
            return procedure.Value.History();
        }

        private void DoProcedure(string robotName, int procedureTime)
        {
            var robot = GetRobot(robotName);

            StackFrame frame = new StackFrame(1);
            string name = frame.GetMethod().Name;
            this._procedures[name].DoService(robot, procedureTime);
        }

        private IRobot GetRobot(string robotName)
        {
            IRobot robot = this._garage.Robots.FirstOrDefault(r => r.Key == robotName).Value;

            if (robot == null)
            {
                throw new ArgumentException($"Robot {robotName} does not exist");
            }

            return robot;
        }
    }
}
