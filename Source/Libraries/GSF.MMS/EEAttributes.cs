//
// This file was generated by the BinaryNotes compiler.
// See http://bnotes.sourceforge.net 
// Any modifications to this file will be lost upon recompilation of the source ASN.1. 
//

using GSF.ASN1;
using GSF.ASN1.Attributes;
using GSF.ASN1.Coders;
using GSF.ASN1.Types;

namespace GSF.MMS
{
    [ASN1PreparedElement]
    [ASN1Sequence(Name = "EEAttributes", IsSet = false)]
    public class EEAttributes : IASN1PreparedElement
    {
        private static readonly IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(EEAttributes));
        private ApplicationReference clientApplication_;

        private bool clientApplication_present;
        private DisplayEnhancementChoiceType displayEnhancement_;
        private EE_Duration duration_;
        private EE_Class enrollmentClass_;
        private EventActionNameChoiceType eventActionName_;

        private bool eventActionName_present;
        private EventConditionNameChoiceType eventConditionName_;
        private ObjectName eventEnrollmentName_;
        private Unsigned32 invokeID_;

        private bool invokeID_present;
        private bool mmsDeletable_;
        private Unsigned32 remainingAcceptableDelay_;

        private bool remainingAcceptableDelay_present;

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
        public EventConditionNameChoiceType EventConditionName
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


        [ASN1Element(Name = "eventActionName", IsOptional = true, HasTag = true, Tag = 2, HasDefaultValue = false)]
        public EventActionNameChoiceType EventActionName
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


        [ASN1Element(Name = "clientApplication", IsOptional = true, HasTag = true, Tag = 3, HasDefaultValue = false)]
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


        [ASN1Boolean(Name = "")]
        [ASN1Element(Name = "mmsDeletable", IsOptional = false, HasTag = true, Tag = 4, HasDefaultValue = true)]
        public bool MmsDeletable
        {
            get
            {
                return mmsDeletable_;
            }
            set
            {
                mmsDeletable_ = value;
            }
        }


        [ASN1Element(Name = "enrollmentClass", IsOptional = false, HasTag = true, Tag = 5, HasDefaultValue = false)]
        public EE_Class EnrollmentClass
        {
            get
            {
                return enrollmentClass_;
            }
            set
            {
                enrollmentClass_ = value;
            }
        }


        [ASN1Element(Name = "duration", IsOptional = false, HasTag = true, Tag = 6, HasDefaultValue = true)]
        public EE_Duration Duration
        {
            get
            {
                return duration_;
            }
            set
            {
                duration_ = value;
            }
        }


        [ASN1Element(Name = "invokeID", IsOptional = true, HasTag = true, Tag = 7, HasDefaultValue = false)]
        public Unsigned32 InvokeID
        {
            get
            {
                return invokeID_;
            }
            set
            {
                invokeID_ = value;
                invokeID_present = true;
            }
        }


        [ASN1Element(Name = "remainingAcceptableDelay", IsOptional = true, HasTag = true, Tag = 8, HasDefaultValue = false)]
        public Unsigned32 RemainingAcceptableDelay
        {
            get
            {
                return remainingAcceptableDelay_;
            }
            set
            {
                remainingAcceptableDelay_ = value;
                remainingAcceptableDelay_present = true;
            }
        }


        [ASN1Element(Name = "displayEnhancement", IsOptional = false, HasTag = true, Tag = 9, HasDefaultValue = false)]
        public DisplayEnhancementChoiceType DisplayEnhancement
        {
            get
            {
                return displayEnhancement_;
            }
            set
            {
                displayEnhancement_ = value;
            }
        }

        public void initWithDefaults()
        {
            bool param_MmsDeletable =
                false;
            MmsDeletable = param_MmsDeletable;
            EE_Duration param_Duration =
                null;
            Duration = param_Duration;
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

        public bool isInvokeIDPresent()
        {
            return invokeID_present;
        }

        public bool isRemainingAcceptableDelayPresent()
        {
            return remainingAcceptableDelay_present;
        }

        [ASN1PreparedElement]
        [ASN1Choice(Name = "displayEnhancement")]
        public class DisplayEnhancementChoiceType : IASN1PreparedElement
        {
            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(DisplayEnhancementChoiceType));
            private long index_;
            private bool index_selected;


            private NullObject noEnhancement_;
            private bool noEnhancement_selected;
            private string string_;
            private bool string_selected;


            [ASN1String(Name = "",
                StringType = UniversalTags.VisibleString, IsUCS = false)]
            [ASN1Element(Name = "string", IsOptional = false, HasTag = true, Tag = 0, HasDefaultValue = false)]
            public string String
            {
                get
                {
                    return string_;
                }
                set
                {
                    selectString(value);
                }
            }

            [ASN1Integer(Name = "")]
            [ASN1Element(Name = "index", IsOptional = false, HasTag = true, Tag = 1, HasDefaultValue = false)]
            public long Index
            {
                get
                {
                    return index_;
                }
                set
                {
                    selectIndex(value);
                }
            }


            [ASN1Null(Name = "noEnhancement")]
            [ASN1Element(Name = "noEnhancement", IsOptional = false, HasTag = false, HasDefaultValue = false)]
            public NullObject NoEnhancement
            {
                get
                {
                    return noEnhancement_;
                }
                set
                {
                    selectNoEnhancement(value);
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


            public bool isStringSelected()
            {
                return string_selected;
            }


            public void selectString(string val)
            {
                string_ = val;
                string_selected = true;


                index_selected = false;

                noEnhancement_selected = false;
            }


            public bool isIndexSelected()
            {
                return index_selected;
            }


            public void selectIndex(long val)
            {
                index_ = val;
                index_selected = true;


                string_selected = false;

                noEnhancement_selected = false;
            }


            public bool isNoEnhancementSelected()
            {
                return noEnhancement_selected;
            }


            public void selectNoEnhancement()
            {
                selectNoEnhancement(new NullObject());
            }


            public void selectNoEnhancement(NullObject val)
            {
                noEnhancement_ = val;
                noEnhancement_selected = true;


                string_selected = false;

                index_selected = false;
            }
        }

        [ASN1PreparedElement]
        [ASN1Choice(Name = "eventActionName")]
        public class EventActionNameChoiceType : IASN1PreparedElement
        {
            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(EventActionNameChoiceType));
            private ObjectName eventAction_;
            private bool eventAction_selected;


            private NullObject undefined_;
            private bool undefined_selected;

            [ASN1Element(Name = "eventAction", IsOptional = false, HasTag = true, Tag = 0, HasDefaultValue = false)]
            public ObjectName EventAction
            {
                get
                {
                    return eventAction_;
                }
                set
                {
                    selectEventAction(value);
                }
            }


            [ASN1Null(Name = "undefined")]
            [ASN1Element(Name = "undefined", IsOptional = false, HasTag = true, Tag = 1, HasDefaultValue = false)]
            public NullObject Undefined
            {
                get
                {
                    return undefined_;
                }
                set
                {
                    selectUndefined(value);
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


            public bool isEventActionSelected()
            {
                return eventAction_selected;
            }


            public void selectEventAction(ObjectName val)
            {
                eventAction_ = val;
                eventAction_selected = true;


                undefined_selected = false;
            }


            public bool isUndefinedSelected()
            {
                return undefined_selected;
            }


            public void selectUndefined()
            {
                selectUndefined(new NullObject());
            }


            public void selectUndefined(NullObject val)
            {
                undefined_ = val;
                undefined_selected = true;


                eventAction_selected = false;
            }
        }

        [ASN1PreparedElement]
        [ASN1Choice(Name = "eventConditionName")]
        public class EventConditionNameChoiceType : IASN1PreparedElement
        {
            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(EventConditionNameChoiceType));
            private ObjectName eventCondition_;
            private bool eventCondition_selected;


            private NullObject undefined_;
            private bool undefined_selected;

            [ASN1Element(Name = "eventCondition", IsOptional = false, HasTag = true, Tag = 0, HasDefaultValue = false)]
            public ObjectName EventCondition
            {
                get
                {
                    return eventCondition_;
                }
                set
                {
                    selectEventCondition(value);
                }
            }


            [ASN1Null(Name = "undefined")]
            [ASN1Element(Name = "undefined", IsOptional = false, HasTag = true, Tag = 1, HasDefaultValue = false)]
            public NullObject Undefined
            {
                get
                {
                    return undefined_;
                }
                set
                {
                    selectUndefined(value);
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


            public bool isEventConditionSelected()
            {
                return eventCondition_selected;
            }


            public void selectEventCondition(ObjectName val)
            {
                eventCondition_ = val;
                eventCondition_selected = true;


                undefined_selected = false;
            }


            public bool isUndefinedSelected()
            {
                return undefined_selected;
            }


            public void selectUndefined()
            {
                selectUndefined(new NullObject());
            }


            public void selectUndefined(NullObject val)
            {
                undefined_ = val;
                undefined_selected = true;


                eventCondition_selected = false;
            }
        }
    }
}