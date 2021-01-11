using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KZ_ShtrikhM_FiscalDevice
{
    class TableContent
    {
        public int ID;
        public string deviceID;
        public string ticketNR;
        public DateTime transactionDate;
        public string cardNR;
        public TableContent(int ID, string deviceID, string ticketNR, DateTime transactionDate, string cardNR)
        {
            this.ID = ID;
            this.deviceID = deviceID;
            this.ticketNR = ticketNR;
            this.transactionDate = transactionDate;
            this.cardNR = cardNR;
        }
    }
}
