using Congestion_Tax_Calculator.BusinessLogic.Persistance;
using Congestion_Tax_Calculator.DataAccess.Persistance;
using Congestion_Tax_Calculator.Domain;

namespace Congestion_Tax_Calculator.DataAccess.Repository
{
    public class TollRepository : BaseRepository<TollRecord>, ITollRecordRepository
    {
        public TollRepository(TaxCalculatorDbContext dbContext) : base(dbContext)
        {
        }
    }
}
