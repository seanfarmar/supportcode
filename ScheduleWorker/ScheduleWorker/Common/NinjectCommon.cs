namespace ScheduleWorker
{
    using Ninject;

    public class NinjectCommon
    {
        public static IKernel Start()
        {
            var kernel = new StandardKernel();

            kernel.Bind<IInventoryDataProvider>().ToConstant(new InventoryDataProvider());

            return kernel;
        }
    }
}