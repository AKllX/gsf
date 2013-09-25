//
// This file was generated by the BinaryNotes compiler.
// See http://bnotes.sourceforge.net 
// Any modifications to this file will be lost upon recompilation of the source ASN.1. 
//

using GSF.ASN1;
using GSF.ASN1.Attributes;
using GSF.ASN1.Attributes.Constraints;
using GSF.ASN1.Coders;

namespace GSF.MMS
{
    [ASN1PreparedElement]
    [ASN1BoxedType(Name = "Unsigned8")]
    public class Unsigned8 : IASN1PreparedElement
    {
        private static readonly IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(Unsigned8));
        private int val;

        public Unsigned8()
        {
        }

        public Unsigned8(int value)
        {
            Value = value;
        }

        [ASN1Integer(Name = "Unsigned8")]
        [ASN1ValueRangeConstraint(
            Min = 0L,
            Max = 127L
            )]
        public int Value
        {
            get
            {
                return val;
            }
            set
            {
                val = value;
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