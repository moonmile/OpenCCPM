using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Openccpm.Test.Models;
using System.Linq;
using System.Threading.Tasks;
using Openccpm.Lib.Models;
using Openccpm.Lib.Services;

namespace Openccpm.Test
{
    public class TestBase
    {
        protected openccpm_dbEntities context;

        [TestInitialize]
        public virtual void SetUp()
        {
            context = new openccpm_dbEntities();
            this.CleanUp();
        }
        [TestCleanup]
        public virtual void CleanUp()
        {

            context.Database.ExecuteSqlCommand("delete from TicketItems");
            context.Database.ExecuteSqlCommand("delete from Projects");
        }
    }
}
