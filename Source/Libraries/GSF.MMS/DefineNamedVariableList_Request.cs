//
// This file was generated by the BinaryNotes compiler.
// See http://bnotes.sourceforge.net 
// Any modifications to this file will be lost upon recompilation of the source ASN.1. 
//

using System.Collections.Generic;
using GSF.ASN1;
using GSF.ASN1.Attributes;
using GSF.ASN1.Coders;

namespace GSF.MMS
{
    [ASN1PreparedElement]
    [ASN1Sequence(Name = "DefineNamedVariableList_Request", IsSet = false)]
    public class DefineNamedVariableList_Request : IASN1PreparedElement
    {
        private static readonly IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(DefineNamedVariableList_Request));
        private ICollection<ListOfVariableSequenceType> listOfVariable_;
        private ObjectName variableListName_;

        [ASN1Element(Name = "variableListName", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public ObjectName VariableListName
        {
            get
            {
                return variableListName_;
            }
            set
            {
                variableListName_ = value;
            }
        }


        [ASN1SequenceOf(Name = "listOfVariable", IsSetOf = false)]
        [ASN1Element(Name = "listOfVariable", IsOptional = false, HasTag = true, Tag = 0, HasDefaultValue = false)]
        public ICollection<ListOfVariableSequenceType> ListOfVariable
        {
            get
            {
                return listOfVariable_;
            }
            set
            {
                listOfVariable_ = value;
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
        [ASN1Sequence(Name = "listOfVariable", IsSet = false)]
        public class ListOfVariableSequenceType : IASN1PreparedElement
        {
            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(ListOfVariableSequenceType));
            private AlternateAccess alternateAccess_;

            private bool alternateAccess_present;
            private VariableSpecification variableSpecification_;

            [ASN1Element(Name = "variableSpecification", IsOptional = false, HasTag = false, HasDefaultValue = false)]
            public VariableSpecification VariableSpecification
            {
                get
                {
                    return variableSpecification_;
                }
                set
                {
                    variableSpecification_ = value;
                }
            }

            [ASN1Element(Name = "alternateAccess", IsOptional = true, HasTag = true, Tag = 5, HasDefaultValue = false)]
            public AlternateAccess AlternateAccess
            {
                get
                {
                    return alternateAccess_;
                }
                set
                {
                    alternateAccess_ = value;
                    alternateAccess_present = true;
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

            public bool isAlternateAccessPresent()
            {
                return alternateAccess_present;
            }
        }
    }
}