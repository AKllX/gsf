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
    [ASN1Sequence(Name = "GeneralManagementParameters", IsSet = false)]
    public class GeneralManagementParameters : IASN1PreparedElement
    {
        private static readonly IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(GeneralManagementParameters));
        private long granularityOfTime_;
        private MMSString localDetail_;


        private SupportForTimeSequenceType supportForTime_;

        [ASN1Element(Name = "localDetail", IsOptional = false, HasTag = true, Tag = 0, HasDefaultValue = false)]
        public MMSString LocalDetail
        {
            get
            {
                return localDetail_;
            }
            set
            {
                localDetail_ = value;
            }
        }

        [ASN1Element(Name = "supportForTime", IsOptional = false, HasTag = true, Tag = 1, HasDefaultValue = false)]
        public SupportForTimeSequenceType SupportForTime
        {
            get
            {
                return supportForTime_;
            }
            set
            {
                supportForTime_ = value;
            }
        }


        [ASN1Integer(Name = "")]
        [ASN1Element(Name = "granularityOfTime", IsOptional = false, HasTag = true, Tag = 4, HasDefaultValue = false)]
        public long GranularityOfTime
        {
            get
            {
                return granularityOfTime_;
            }
            set
            {
                granularityOfTime_ = value;
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

        [ASN1PreparedElement]
        [ASN1Sequence(Name = "supportForTime", IsSet = false)]
        public class SupportForTimeSequenceType : IASN1PreparedElement
        {
            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(SupportForTimeSequenceType));
            private bool timeOfDay_;


            private bool timeSequence_;

            [ASN1Boolean(Name = "")]
            [ASN1Element(Name = "timeOfDay", IsOptional = false, HasTag = true, Tag = 2, HasDefaultValue = false)]
            public bool TimeOfDay
            {
                get
                {
                    return timeOfDay_;
                }
                set
                {
                    timeOfDay_ = value;
                }
            }

            [ASN1Boolean(Name = "")]
            [ASN1Element(Name = "timeSequence", IsOptional = false, HasTag = true, Tag = 3, HasDefaultValue = false)]
            public bool TimeSequence
            {
                get
                {
                    return timeSequence_;
                }
                set
                {
                    timeSequence_ = value;
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