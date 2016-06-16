using System;
using NServiceBus;

public class MyBusinessExceptionMessage : ICommand
{
    public Guid Id { get; set; }
}