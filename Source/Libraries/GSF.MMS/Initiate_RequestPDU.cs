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
    [ASN1Sequence(Name = "Initiate_RequestPDU", IsSet = false)]
    public class Initiate_RequestPDU : IASN1PreparedElement
    {
        private static readonly IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(Initiate_RequestPDU));
        private InitRequestDetailSequenceType initRequestDetail_;
        private Integer32 localDetailCalling_;

        private bool localDetailCalling_present;
        private Integer8 proposedDataStructureNestingLevel_;

        private bool proposedDataStructureNestingLevel_present;
        private Integer16 proposedMaxServOutstandingCalled_;


        private Integer16 proposedMaxServOutstandingCalling_;

        [ASN1Element(Name = "localDetailCalling", IsOptional = true, HasTag = true, Tag = 0, HasDefaultValue = false)]
        public Integer32 LocalDetailCalling
        {
            get
            {
                return localDetailCalling_;
            }
            set
            {
                localDetailCalling_ = value;
                localDetailCalling_present = true;
            }
        }

        [ASN1Element(Name = "proposedMaxServOutstandingCalling", IsOptional = false, HasTag = true, Tag = 1, HasDefaultValue = false)]
        public Integer16 ProposedMaxServOutstandingCalling
        {
            get
            {
                return proposedMaxServOutstandingCalling_;
            }
            set
            {
                proposedMaxServOutstandingCalling_ = value;
            }
        }


        [ASN1Element(Name = "proposedMaxServOutstandingCalled", IsOptional = false, HasTag = true, Tag = 2, HasDefaultValue = false)]
        public Integer16 ProposedMaxServOutstandingCalled
        {
            get
            {
                return proposedMaxServOutstandingCalled_;
            }
            set
            {
                proposedMaxServOutstandingCalled_ = value;
            }
        }


        [ASN1Element(Name = "proposedDataStructureNestingLevel", IsOptional = true, HasTag = true, Tag = 3, HasDefaultValue = false)]
        public Integer8 ProposedDataStructureNestingLevel
        {
            get
            {
                return proposedDataStructureNestingLevel_;
            }
            set
            {
                proposedDataStructureNestingLevel_ = value;
                proposedDataStructureNestingLevel_present = true;
            }
        }


        [ASN1Element(Name = "initRequestDetail", IsOptional = false, HasTag = true, Tag = 4, HasDefaultValue = false)]
        public InitRequestDetailSequenceType InitRequestDetail
        {
            get
            {
                return initRequestDetail_;
            }
            set
            {
                initRequestDetail_ = value;
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

        public bool isLocalDetailCallingPresent()
        {
            return localDetailCalling_present;
        }

        public bool isProposedDataStructureNestingLevelPresent()
        {
            return proposedDataStructureNestingLevel_present;
        }

        [ASN1PreparedElement]
        [ASN1Sequence(Name = "initRequestDetail", IsSet = false)]
        public class InitRequestDetailSequenceType : IASN1PreparedElement
        {
            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(InitRequestDetailSequenceType));
            private AdditionalCBBOptions additionalCbbSupportedCalling_;
            private AdditionalSupportOptions additionalSupportedCalling_;
            private string privilegeClassIdentityCalling_;
            private ParameterSupportOptions proposedParameterCBB_;
            private Integer16 proposedVersionNumber_;
            private ServiceSupportOptions servicesSupportedCalling_;

            [ASN1Element(Name = "proposedVersionNumber", IsOptional = false, HasTag = true, Tag = 0, HasDefaultValue = false)]
            public Integer16 ProposedVersionNumber
            {
                get
                {
                    return proposedVersionNumber_;
                }
                set
                {
                    proposedVersionNumber_ = value;
                }
            }


            [ASN1Element(Name = "proposedParameterCBB", IsOptional = false, HasTag = true, Tag = 1, HasDefaultValue = false)]
            public ParameterSupportOptions ProposedParameterCBB
            {
                get
                {
                    return proposedParameterCBB_;
                }
                set
                {
                    proposedParameterCBB_ = value;
                }
            }


            [ASN1Element(Name = "servicesSupportedCalling", IsOptional = false, HasTag = true, Tag = 2, HasDefaultValue = false)]
            public ServiceSupportOptions ServicesSupportedCalling
            {
                get
                {
                    return servicesSupportedCalling_;
                }
                set
                {
                    servicesSupportedCalling_ = value;
                }
            }


            [ASN1Element(Name = "additionalSupportedCalling", IsOptional = false, HasTag = true, Tag = 3, HasDefaultValue = false)]
            public AdditionalSupportOptions AdditionalSupportedCalling
            {
                get
                {
                    return additionalSupportedCalling_;
                }
                set
                {
                    additionalSupportedCalling_ = value;
                }
            }


            [ASN1Element(Name = "additionalCbbSupportedCalling", IsOptional = false, HasTag = true, Tag = 4, HasDefaultValue = false)]
            public AdditionalCBBOptions AdditionalCbbSupportedCalling
            {
                get
                {
                    return additionalCbbSupportedCalling_;
                }
                set
                {
                    additionalCbbSupportedCalling_ = value;
                }
            }


            [ASN1String(Name = "",
                StringType = UniversalTags.VisibleString, IsUCS = false)]
            [ASN1Element(Name = "privilegeClassIdentityCalling", IsOptional = false, HasTag = true, Tag = 5, HasDefaultValue = false)]
            public string PrivilegeClassIdentityCalling
            {
                get
                {
                    return privilegeClassIdentityCalling_;
                }
                set
                {
                    privilegeClassIdentityCalling_ = value;
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
}