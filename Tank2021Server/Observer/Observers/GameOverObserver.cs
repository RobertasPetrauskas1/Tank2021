using Tank2021SharedContent.Enums;
using Microsoft.AspNetCore.SignalR;
using Tank2021.Hubs;
using System.Threading.Tasks;
using Tank2021SharedContent.Observer.Observers;
using Tank2021SharedContent.Observer.Subjects;

namespace Tank2021Server.Observer.Observers
{
    class GameOverObserver : IObserver
    {
        public Subject Subject { get; set; }
        public IHubContext<TankHub> HubContext { get; set; }
        
        public GameOverObserver(Subject subject, IHubContext<TankHub> hubContext)
        {
            Subject = subject;
            Subject.AttatchObserver(this);
            HubContext = hubContext;
        }

        public async Task Update()
        {
            if(((GameStatus)Subject).Player1Info.TankHealth <= 0)
                await HubContext.Clients.All.SendAsync("GameOver", PlayerType.PLAYER1);
            if (((GameStatus)Subject).Player2Info.TankHealth <= 0)
                await HubContext.Clients.All.SendAsync("GameOver", PlayerType.PLAYER2);

        }
    }
}
