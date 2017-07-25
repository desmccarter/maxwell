using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.org.hs2.npsdomainobjects.nps.laa
{
    public enum LaaWorkflowStageEnum
    {
        // S2 Process User Tasks
        UPLOAD_S2_FLAB_PLAN = 1,
        ASSIGN_PSC = 2,
        UPLOAD_LR_DATA = 3,
        APPROVE_LR_DATA = 4,
        DETERMINE_ACCESS_METHOD = 5,
        UPLOAD_PLANS = 6,
        APPROVE_PLANS = 7,

        // Notice Process User Tasks
        VALIDATE_NOTICE_PACKS = 99
    }
}
