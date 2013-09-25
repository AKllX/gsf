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
    [ASN1Sequence(Name = "ModifierStep", IsSet = false)]
    public class ModifierStep : IASN1PreparedElement
    {
        private static readonly IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(ModifierStep));
        private long modifierID_;


        private Modifier modifier_;

        [ASN1Integer(Name = "")]
        [ASN1Element(Name = "modifierID", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public long ModifierID
        {
            get
            {
                return modifierID_;
            }
            set
            {
                modifierID_ = value;
            }
        }

        [ASN1Element(Name = "modifier", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public Modifier Modifier
        {
            get
            {
                return modifier_;
            }
            set
            {
                modifier_ = value;
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