//
// This file was generated by the BinaryNotes compiler.
// See http://bnotes.sourceforge.net 
// Any modifications to this file will be lost upon recompilation of the source ASN.1. 
//

using GSF.ASN1;
using GSF.ASN1.Attributes;
using GSF.ASN1.Coders;

namespace GSF.MMS
{
    [ASN1PreparedElement]
    [ASN1Sequence(Name = "StopUnitControl_Error", IsSet = false)]
    public class StopUnitControl_Error : IASN1PreparedElement
    {
        private static readonly IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(StopUnitControl_Error));
        private Identifier programInvocationName_;


        private ProgramInvocationState programInvocationState_;

        [ASN1Element(Name = "programInvocationName", IsOptional = false, HasTag = true, Tag = 0, HasDefaultValue = false)]
        public Identifier ProgramInvocationName
        {
            get
            {
                return programInvocationName_;
            }
            set
            {
                programInvocationName_ = value;
            }
        }

        [ASN1Element(Name = "programInvocationState", IsOptional = false, HasTag = true, Tag = 1, HasDefaultValue = false)]
        public ProgramInvocationState ProgramInvocationState
        {
            get
            {
                return programInvocationState_;
            }
            set
            {
                programInvocationState_ = value;
            }
        }


        public void initWithDefaults()
        {
        }


        public IASN1PreparedElementData PreparedData
        {
            get
            {
                return preparedData;
            }
        }
    }
}