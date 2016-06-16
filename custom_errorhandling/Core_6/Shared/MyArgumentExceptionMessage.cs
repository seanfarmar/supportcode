using System;
using NServiceBus;

public class MyArgumentExceptionMessage : ICommand
{
    public Guid Id { get; set; }
}