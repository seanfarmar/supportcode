namespace ClassLibrary2
{
    using System;
    using NServiceBus;

    public class MyCommand : ICommand
    {
        public MyCommand(int num, Uri uri)
        {
            Uri = uri;
        }

        public Uri Uri { get; set; }
    }
}
