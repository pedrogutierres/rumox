﻿using System.Threading;
using Core.Domain.Commands;
using Core.Domain.Events;
using System.Threading.Tasks;

namespace Core.Domain.Interfaces
{
    public interface IMediatorHandler
    {
        Task RaiseEvent<T>(T evento, CancellationToken cancellation = default(CancellationToken)) where T : Event;
        Task SendCommand<T>(T comando, CancellationToken cancellation = default(CancellationToken)) where T : Command;
    }
}