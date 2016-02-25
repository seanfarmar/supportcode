namespace Saga
{
    using System.Threading.Tasks;

    internal class ACHRail : IRT
    {
        public Task<RailResult> Fund(RailTransaction rTrans, decimal Amount)
        {
            throw new System.NotImplementedException();
        }

        public bool BusinessValidate(RailTransaction rTrans)
        {
            throw new System.NotImplementedException();
        }
    }
}