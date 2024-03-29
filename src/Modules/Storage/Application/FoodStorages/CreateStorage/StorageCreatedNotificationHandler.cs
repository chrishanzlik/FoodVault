﻿using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Application.FoodStorages.CreateStorage
{
    internal class StorageCreatedNotificationHandler : INotificationHandler<StorageCreatedNotification>
    {
        public Task Handle(StorageCreatedNotification notification, CancellationToken cancellationToken)
        {
            // Doing some stuff outside of transaction, e.g. sending emails
            // Invoke new INTERNALcommands by ICommandScheduler for persisting changes like EmailSent = true

            return Task.CompletedTask;
        }
    }
}
