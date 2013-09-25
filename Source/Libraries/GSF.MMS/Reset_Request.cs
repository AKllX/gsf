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
    [ASN1Sequence(Name = "Reset_Request", IsSet = false)]
    public class Reset_Request : IASN1PreparedElement
    {
        private static readonly IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(Reset_Request));
        private Identifier programInvocationName_;

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