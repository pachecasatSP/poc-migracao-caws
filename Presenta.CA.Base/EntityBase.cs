using System;
using System.Collections.Generic;
using System.Text;

namespace Presenta.CA.Base
{
    public abstract class EntityBase
    {
        public string StoredProcedureDeleteName { get; set; }

        public string StoredProcedureInsertName { get; set; }

        public string StoredProcedureListName { get; set; }

        public string StoredProcedureSelectName { get; set; }

        public string StoredProcedureUpdateName { get; set; }
    }
}
