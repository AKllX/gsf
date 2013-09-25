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
    [ASN1Sequence(Name = "CreateUnitControl_Request", IsSet = false)]
    public class CreateUnitControl_Request : IASN1PreparedElement
    {
        private static readonly IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(CreateUnitControl_Request));
        private ICollection<Identifier> domains_;
        private ICollection<Identifier> programInvocations_;
        private Identifier unitControl_;

        [ASN1Element(Name = "unitControl", IsOptional = false, HasTag = true, Tag = 0, HasDefaultValue = false)]
        public Identifier UnitControl
        {
            get
            {
                return unitControl_;
            }
            set
            {
                unitControl_ = value;
            }
        }


        [ASN1SequenceOf(Name = "domains", IsSetOf = false)]
        [ASN1Element(Name = "domains", IsOptional = false, HasTag = true, Tag = 1, HasDefaultValue = false)]
        public ICollection<Identifier> Domains
        {
            get
            {
                return domains_;
            }
            set
            {
                domains_ = value;
            }
        }


        [ASN1SequenceOf(Name = "programInvocations", IsSetOf = false)]
        [ASN1Element(Name = "programInvocations", IsOptional = false, HasTag = true, Tag = 2, HasDefaultValue = false)]
        public ICollection<Identifier> ProgramInvocations
        {
            get
            {
                return programInvocations_;
            }
            set
            {
                programInvocations_ = value;
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