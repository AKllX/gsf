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
    [ASN1Sequence(Name = "DefineEventEnrollment_Request", IsSet = false)]
    public class DefineEventEnrollment_Request : IASN1PreparedElement
    {
        private static readonly IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(DefineEventEnrollment_Request));
        private AlarmAckRule alarmAcknowledgmentRule_;
        private ApplicationReference clientApplication_;

        private bool clientApplication_present;
        private ObjectName eventActionName_;

        private bool eventActionName_present;
        private ObjectName eventConditionName_;
        private Transitions eventConditionTransitions_;
        private ObjectName eventEnrollmentName_;

        [ASN1Element(Name = "eventEnrollmentName", IsOptional = false, HasTag = true, Tag = 0, HasDefaultValue = false)]
        public ObjectName EventEnrollmentName
        {
            get
            {
                return eventEnrollmentName_;
            }
            set
            {
                eventEnrollmentName_ = value;
            }
        }


        [ASN1Element(Name = "eventConditionName", IsOptional = false, HasTag = true, Tag = 1, HasDefaultValue = false)]
        public ObjectName EventConditionName
        {
            get
            {
                return eventConditionName_;
            }
            set
            {
                eventConditionName_ = value;
            }
        }


        [ASN1Element(Name = "eventConditionTransitions", IsOptional = false, HasTag = true, Tag = 2, HasDefaultValue = false)]
        public Transitions EventConditionTransitions
        {
            get
            {
                return eventConditionTransitions_;
            }
            set
            {
                eventConditionTransitions_ = value;
            }
        }


        [ASN1Element(Name = "alarmAcknowledgmentRule", IsOptional = false, HasTag = true, Tag = 3, HasDefaultValue = false)]
        public AlarmAckRule AlarmAcknowledgmentRule
        {
            get
            {
                return alarmAcknowledgmentRule_;
            }
            set
            {
                alarmAcknowledgmentRule_ = value;
            }
        }


        [ASN1Element(Name = "eventActionName", IsOptional = true, HasTag = true, Tag = 4, HasDefaultValue = false)]
        public ObjectName EventActionName
        {
            get
            {
                return eventActionName_;
            }
            set
            {
                eventActionName_ = value;
                eventActionName_present = true;
            }
        }


        [ASN1Element(Name = "clientApplication", IsOptional = true, HasTag = true, Tag = 5, HasDefaultValue = false)]
        public ApplicationReference ClientApplication
        {
            get
            {
                return clientApplication_;
            }
            set
            {
                clientApplication_ = value;
                clientApplication_present = true;
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

        public bool isEventActionNamePresent()
        {
            return eventActionName_present;
        }

        public bool isClientApplicationPresent()
        {
            return clientApplication_present;
        }
    }
}