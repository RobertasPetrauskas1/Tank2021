using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tank2021SharedContent.Enums;

namespace Tank2021Client.ChanOfResponsibility
{
    public class LeftKeyHandler : IKeyEventHandler
    {
        HubConnection hubConnection;
        IKeyEventHandler nextHandler;
        PlayerType playerType;

        public LeftKeyHandler(HubConnection hubConnection, PlayerType playerType, IKeyEventHandler nextHandler)
        {
            this.hubConnection = hubConnection;
            this.playerType = playerType;
            this.nextHandler = nextHandler;
        }
        public LeftKeyHandler(HubConnection hubConnection, PlayerType playerType)
        {
            this.hubConnection = hubConnection;
            this.playerType = playerType;
        }

        public async Task HandleAsync(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                await hubConnection.SendAsync("MoveLeft", playerType);
            }
            else
            {
                await nextHandler.HandleAsync(e);
            }
        }

        public void SetNextHandler(IKeyEventHandler nextHandler)
        {
            this.nextHandler = nextHandler;
        }
    }
}
