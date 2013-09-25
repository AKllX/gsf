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
    [ASN1Choice(Name = "CS_Status_Response")]
    public class CS_Status_Response : IASN1PreparedElement
    {
        private static readonly IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(CS_Status_Response));
        private FullResponseSequenceType fullResponse_;
        private bool fullResponse_selected;


        private NullObject noExtraResponse_;
        private bool noExtraResponse_selected;

        [ASN1Element(Name = "fullResponse", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public FullResponseSequenceType FullResponse
        {
            get
            {
                return fullResponse_;
            }
            set
            {
                selectFullResponse(value);
            }
        }


        [ASN1Null(Name = "noExtraResponse")]
        [ASN1Element(Name = "noExtraResponse", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public NullObject NoExtraResponse
        {
            get
            {
                return noExtraResponse_;
            }
            set
            {
                selectNoExtraResponse(value);
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


        public bool isFullResponseSelected()
        {
            return fullResponse_selected;
        }


        public void selectFullResponse(FullResponseSequenceType val)
        {
            fullResponse_ = val;
            fullResponse_selected = true;


            noExtraResponse_selected = false;
        }


        public bool isNoExtraResponseSelected()
        {
            return noExtraResponse_selected;
        }


        public void selectNoExtraResponse()
        {
            selectNoExtraResponse(new NullObject());
        }


        public void selectNoExtraResponse(NullObject val)
        {
            noExtraResponse_ = val;
            noExtraResponse_selected = true;


            fullResponse_selected = false;
        }

        [ASN1PreparedElement]
        [ASN1Sequence(Name = "fullResponse", IsSet = false)]
        public class FullResponseSequenceType : IASN1PreparedElement
        {
            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(FullResponseSequenceType));
            private ExtendedStatus extendedStatusMask_;
            private ExtendedStatus extendedStatus_;
            private OperationState operationState_;
            private SelectedProgramInvocationChoiceType selectedProgramInvocation_;

            [ASN1Element(Name = "operationState", IsOptional = false, HasTag = true, Tag = 0, HasDefaultValue = false)]
            public OperationState OperationState
            {
                get
                {
                    return operationState_;
                }
                set
                {
                    operationState_ = value;
                }
            }


            [ASN1Element(Name = "extendedStatus", IsOptional = false, HasTag = true, Tag = 1, HasDefaultValue = false)]
            public ExtendedStatus ExtendedStatus
            {
                get
                {
                    return extendedStatus_;
                }
                set
                {
                    extendedStatus_ = value;
                }
            }


            [ASN1Element(Name = "extendedStatusMask", IsOptional = false, HasTag = true, Tag = 2, HasDefaultValue = true)]
            public ExtendedStatus ExtendedStatusMask
            {
                get
                {
                    return extendedStatusMask_;
                }
                set
                {
                    extendedStatusMask_ = value;
                }
            }


            [ASN1Element(Name = "selectedProgramInvocation", IsOptional = false, HasTag = false, HasDefaultValue = false)]
            public SelectedProgramInvocationChoiceType SelectedProgramInvocation
            {
                get
                {
                    return selectedProgramInvocation_;
                }
                set
                {
                    selectedProgramInvocation_ = value;
                }
            }


            public void initWithDefaults()
            {
                ExtendedStatus param_ExtendedStatusMask =
                    new ExtendedStatus(CoderUtils.defStringToOctetString("'1111'B"));
                ExtendedStatusMask = param_ExtendedStatusMask;
            }

            public IASN1PreparedElementData PreparedData
            {
                get
                {
                    return preparedData;
                }
            }

            [ASN1PreparedElement]
            [ASN1Choice(Name = "selectedProgramInvocation")]
            public class SelectedProgramInvocationChoiceType : IASN1PreparedElement
            {
                private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(SelectedProgramInvocationChoiceType));
                private NullObject noneSelected_;
                private bool noneSelected_selected;
                private Identifier programInvocation_;
                private bool programInvocation_selected;


                [ASN1Element(Name = "programInvocation", IsOptional = false, HasTag = true, Tag = 3, HasDefaultValue = false)]
                public Identifier ProgramInvocation
                {
                    get
                    {
                        return programInvocation_;
                    }
                    set
                    {
                        selectProgramInvocation(value);
                    }
                }


                [ASN1Null(Name = "noneSelected")]
                [ASN1Element(Name = "noneSelected", IsOptional = false, HasTag = true, Tag = 4, HasDefaultValue = false)]
                public NullObject NoneSelected
                {
                    get
                    {
                        return noneSelected_;
                    }
                    set
                    {
                        selectNoneSelected(value);
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


                public bool isProgramInvocationSelected()
                {
                    return programInvocation_selected;
                }


                public void selectProgramInvocation(Identifier val)
                {
                    programInvocation_ = val;
                    programInvocation_selected = true;


                    noneSelected_selected = false;
                }


                public bool isNoneSelectedSelected()
                {
                    return noneSelected_selected;
                }


                public void selectNoneSelected()
                {
                    selectNoneSelected(new NullObject());
                }


                public void selectNoneSelected(NullObject val)
                {
                    noneSelected_ = val;
                    noneSelected_selected = true;


                    programInvocation_selected = false;
                }
            }
        }
    }
}