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
    [ASN1Sequence(Name = "Select_Request", IsSet = false)]
    public class Select_Request : IASN1PreparedElement
    {
        private static readonly IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(Select_Request));
        private ICollection<Identifier> controlled_;

        private bool controlled_present;
        private Identifier controlling_;

        private bool controlling_present;

        [ASN1Element(Name = "controlling", IsOptional = true, HasTag = true, Tag = 0, HasDefaultValue = false)]
        public Identifier Controlling
        {
            get
            {
                return controlling_;
            }
            set
            {
                controlling_ = value;
                controlling_present = true;
            }
        }


        [ASN1SequenceOf(Name = "controlled", IsSetOf = false)]
        [ASN1Element(Name = "controlled", IsOptional = true, HasTag = true, Tag = 1, HasDefaultValue = false)]
        public ICollection<Identifier> Controlled
        {
            get
            {
                return controlled_;
            }
            set
            {
                controlled_ = value;
                controlled_present = true;
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

        public bool isControllingPresent()
        {
            return controlling_present;
        }

        public bool isControlledPresent()
        {
            return controlled_present;
        }
    }
}