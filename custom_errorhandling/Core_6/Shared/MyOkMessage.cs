using System;
using NServiceBus;

public class MyOkMessage : ICommand
{
    public Guid Id { get; set; }
}