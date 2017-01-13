using CentralSystem.FlowManager.Messaging.Activities;
using CentralSystem.Framework.NServiceBus.Hosting;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCommandsSender
{
    class Program
    {
        static void Main(string[] args)
        {
            BusConfiguration busConfiguration = new BusConfiguration();

            busConfiguration.SetDefaultCrossVerticalsHeadersInitializationConfiguration();

            new BusLoader(busConfiguration).StartSendOnlyBus();

            bool stopFlag = false;

            int testIndex = 0;

            Console.WriteLine("Enter command: 0 - exit, 1 - error command, 2 - create saga, 3 - both");

            while (!stopFlag)
            {
                switch (Console.ReadKey(false).KeyChar)
                {
                    case '0':
                        stopFlag = true;
                        break;

                    case '1':
                        SendErrorCommand(++testIndex);
                        break;

                    case '2':
                        SendCreateSagaCommand(++testIndex);
                        break;

                    case '3':
                        ++testIndex;
                        SendErrorCommand(testIndex);
                        SendCreateSagaCommand(testIndex);
                        break;

                }

            }
        }

        private static void SendErrorCommand(int stepInstanceID)
        {
            //Send command to initiate SLR mechanism
            DoNothingFlowCommand testSLRetryCommand = new DoNothingFlowCommand()
            {
                FlowInstanceID = 1, //Flow instance ID - SLR retry mechanism
                StepInstanceID = stepInstanceID,
                RootObjectID = 1,
                RootObjectType = "D",
                ThrowUnhandledException = true,
                ThrowCancelRetryExceptionAfterSLRetryAttemptsCount = 2000,
            };

            new BusLoader().Send(testSLRetryCommand);

        }

        private static void SendCreateSagaCommand(int flowInstanceID)
        {
            //Send command to initiate SAGA process
            StartNewFlowCommand testStartNewFlowCommand = new StartNewFlowCommand()
            {
                RootObjectID = flowInstanceID + 1, // Started from 2
                RootObjectType = "D",
            };

            new BusLoader().Send(testStartNewFlowCommand);

        }

    }
}
