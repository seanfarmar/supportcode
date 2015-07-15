namespace ScheduleWorker
{
    public class InventoryDataProvider : IInventoryDataProvider
    {
        public string GetInventory()
        {
            return "This is a mock";
        }
    }
}