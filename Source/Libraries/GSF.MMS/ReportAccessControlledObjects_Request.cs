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
    [ASN1Sequence(Name = "ReportAccessControlledObjects_Request", IsSet = false)]
    public class ReportAccessControlledObjects_Request : IASN1PreparedElement
    {
        private static readonly IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(ReportAccessControlledObjects_Request));
        private Identifier accessControlList_;


        private ObjectName continueAfter_;

        private bool continueAfter_present;
        private ObjectClass objectClass_;

        [ASN1Element(Name = "accessControlList", IsOptional = false, HasTag = true, Tag = 0, HasDefaultValue = false)]
        public Identifier AccessControlList
        {
            get
            {
                return accessControlList_;
            }
            set
            {
                accessControlList_ = value;
            }
        }

        [ASN1Element(Name = "objectClass", IsOptional = false, HasTag = true, Tag = 1, HasDefaultValue = false)]
        public ObjectClass ObjectClass
        {
            get
            {
                return objectClass_;
            }
            set
            {
                objectClass_ = value;
            }
        }

        [ASN1Element(Name = "continueAfter", IsOptional = true, HasTag = true, Tag = 2, HasDefaultValue = false)]
        public ObjectName ContinueAfter
        {
            get
            {
                return continueAfter_;
            }
            set
            {
                continueAfter_ = value;
                continueAfter_present = true;
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

        public bool isContinueAfterPresent()
        {
            return continueAfter_present;
        }
    }
}