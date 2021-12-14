using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tank2021Server;
using Tank2021SharedContent;
using Tank2021SharedContent.Command;
using Tank2021SharedContent.Command.Commands;
using Tank2021SharedContent.Enums;
using Tank2021SharedContent.Factory;
using Tank2021SharedContent.HubRezults;
using Tank2021SharedContent.Interpreter;
using Tank2021SharedContent.Interpreter.Expressions;
using Tank2021SharedContent.Interpreter.Expressions.Implementations;
using Tank2021SharedContent.Token.Caretaker;
using static Tank2021SharedContent.Interpreter.Context;

namespace Tank2021.Hubs
{
    public class TankHub : Hub
    {
        ICommand player1MoveLeft = new MoveLeft(MapControllerSingleton.getMapController().Map, PlayerType.PLAYER1);
        ICommand player1MoveRight = new MoveRight(MapControllerSingleton.getMapController().Map, PlayerType.PLAYER1);
        ICommand player1MoveUp = new MoveUp(MapControllerSingleton.getMapController().Map, PlayerType.PLAYER1);
        ICommand player1MoveDown = new MoveDown(MapControllerSingleton.getMapController().Map, PlayerType.PLAYER1);

        ICommand player2MoveLeft = new MoveLeft(MapControllerSingleton.getMapController().Map, PlayerType.PLAYER2);
        ICommand player2MoveRight = new MoveRight(MapControllerSingleton.getMapController().Map, PlayerType.PLAYER2);
        ICommand player2MoveUp = new MoveUp(MapControllerSingleton.getMapController().Map, PlayerType.PLAYER2);
        ICommand player2MoveDown = new MoveDown(MapControllerSingleton.getMapController().Map, PlayerType.PLAYER2);

        public async Task ConnectPlayer(PlayerType player, TankType tank)
        {
            var mapController = MapControllerSingleton.getMapController();
            mapController.Map.GetPlayer(player).IsConnected = true;
            InitializePlayer(player, tank);
            if (mapController.Map.GetOppositePlayer(player).IsConnected)
            {
                await InitializeGame();
            } 
        }

        public async Task DisconnectPlayer(PlayerType player)
        {
            var mapController = MapControllerSingleton.getMapController();
            mapController.Map.GetPlayer(player).IsConnected = false; //Do we really need this?
            mapController.ResetGame();

            await Clients.All.SendAsync("GameOver", Helper.GetOppositePlayer(player));
        }


        public async Task InitializeGame()
        {
            var mapController = MapControllerSingleton.getMapController();
            mapController.InitGameStatus();
            mapController.InitGameoverObservable();
            mapController.timer.Enabled = true;
            mapController.Map.BackgroundImageLocation = @"../../../Properties/Resources/background.png";

            await Clients.All.SendAsync("InitializeGame", mapController.Map.ToJson());
        }

        public void InitializePlayer(PlayerType player, TankType tank)
        {
            var mapController = MapControllerSingleton.getMapController();
            var creator = new TankCreator();

            if (player == PlayerType.PLAYER1)
            {
                var player1 = mapController.Map.GetPlayer(player);
                player1.Tank = creator.CreateTank(tank);
            }

            if (player == PlayerType.PLAYER2)
            {
                var player2 = mapController.Map.GetPlayer(player);
                player2.Tank = creator.CreateTank(tank);
            }
        }

        public async Task CheatCode(PlayerType player, string cheatCode)
        {
            var mapController = MapControllerSingleton.getMapController();
            var specificPlayer = mapController.Map.GetPlayer(player);
            var context = new Context(specificPlayer);

            var codes = cheatCode.Split(' ').ToList();
            var expressions = new List<IExpression>();
            for(var index = 0; index < codes.Count; index++)
            {
                switch (codes[index])
                {
                    case "-health":
                        expressions.Add(new HealthRestoreExpression(context, specificPlayer.Tank.Health));
                        break;
                    case "-shoot":
                        expressions.Add(new ShootExpression(context));
                        break;
                    case "-move":
                        if(index + 1 < codes.Count)
                        {
                            var direction = codes[index + 1];
                            Direction parsedDirection;
                            try
                            {
                                parsedDirection = (Direction)Enum.Parse(typeof(Direction), direction, true);
                            }
                            catch
                            {
                                await Clients.All.SendAsync("CommandStatus", new CodeStatus(false, player));
                                return;
                            }
                            var directionExpression = new DirectionExpression(parsedDirection, context);
                            expressions.Add(new MoveExpression(directionExpression, context));

                            codes.RemoveAt(index + 1);
                        }
                        else
                        {
                            await Clients.All.SendAsync("CommandStatus", new CodeStatus(false, player));
                            return;
                        }
                        break;
                    default:
                        await Clients.All.SendAsync("CommandStatus", new CodeStatus(false, player));
                        return;
                }
            }

            if (!expressions.Any())
            {
                await Clients.All.SendAsync("CommandStatus", new CodeStatus(false, player));
                return;
            }

            var caretaker = CaretakerSingleton.getCaretaker();
            caretaker.AddMomento(context.CreateMomento());

            var commandExpression = new CommandExpression(expressions, context);
            commandExpression.Interpret();

            await Clients.All.SendAsync("CommandStatus", new CodeStatus(true, player));
        }

        public async Task UndoCheatCode(PlayerType player)
        {
            var caretaker = CaretakerSingleton.getCaretaker();
            var index = caretaker.GetLastIndex();

            var mapController = MapControllerSingleton.getMapController();
            var context = new Context(mapController.Map.GetPlayer(player));
            var onRestore = false;

            while(index > -1)
            {
                var momento = caretaker.Undo(index);
                if (momento == null)
                    break;

                if (context.RestoreMomento(momento))
                {
                    caretaker.Remove(index);
                    onRestore = true;
                    break;
                }
            }

            if (onRestore)
            {
                context.Undo();
                await Clients.All.SendAsync("CommandUndoStatus", new CodeStatus(true, player));
            }
            else
            {
                await Clients.All.SendAsync("CommandUndoStatus", new CodeStatus(false, player));
            }
        }

        public async Task MoveDown(PlayerType player)
        {
            if(player == PlayerType.PLAYER1)
            {
                CommandInvokerSingleton.GetPlayer1Invoker().execute(player1MoveDown);
            }
            else if (player == PlayerType.PLAYER2)
            {
                CommandInvokerSingleton.GetPlayer2Invoker().execute(player2MoveDown);
            }
            await Clients.All.SendAsync("UpdateMap", MapControllerSingleton.getMapController().Map.ToJson());
        }

        public async Task MoveUp(PlayerType player)
        {
            if (player == PlayerType.PLAYER1)
            {
                CommandInvokerSingleton.GetPlayer1Invoker().execute(player1MoveUp);
            }
            else if (player == PlayerType.PLAYER2)
            {
                CommandInvokerSingleton.GetPlayer2Invoker().execute(player2MoveUp);
            }
            await Clients.All.SendAsync("UpdateMap", MapControllerSingleton.getMapController().Map.ToJson());
        }

        public async Task MoveLeft(PlayerType player)
        {
            if (player == PlayerType.PLAYER1)
            {
                CommandInvokerSingleton.GetPlayer1Invoker().execute(player1MoveLeft);
            }
            else if (player == PlayerType.PLAYER2)
            {
                CommandInvokerSingleton.GetPlayer2Invoker().execute(player2MoveLeft);
            }
            await Clients.All.SendAsync("UpdateMap", MapControllerSingleton.getMapController().Map.ToJson());
        }

        public async Task MoveRight(PlayerType player)
        {
            if (player == PlayerType.PLAYER1)
            {
                CommandInvokerSingleton.GetPlayer1Invoker().execute(player1MoveRight);
            }
            else if (player == PlayerType.PLAYER2)
            {
                CommandInvokerSingleton.GetPlayer2Invoker().execute(player2MoveRight);
            }
            await Clients.All.SendAsync("UpdateMap", MapControllerSingleton.getMapController().Map.ToJson());
        }

        public async Task Undo(PlayerType player)
        {
            if (player == PlayerType.PLAYER1)
            {
                CommandInvokerSingleton.GetPlayer1Invoker().undoLast();
            }
            else if (player == PlayerType.PLAYER2)
            {
                CommandInvokerSingleton.GetPlayer2Invoker().undoLast();
            }
            await Clients.All.SendAsync("UpdateMap", MapControllerSingleton.getMapController().Map.ToJson());
        }

        public async Task Shoot(PlayerType player)
        {
            var mapController = MapControllerSingleton.getMapController();
            mapController.Map.Shoot(player);
            await Clients.All.SendAsync("UpdateMap", mapController.Map.ToJson());
        }
    }
}
