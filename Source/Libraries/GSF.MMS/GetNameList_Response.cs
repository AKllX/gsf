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
    [ASN1Sequence(Name = "GetNameList_Response", IsSet = false)]
    public class GetNameList_Response : IASN1PreparedElement
    {
        private static readonly IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(GetNameList_Response));
        private ICollection<Identifier> listOfIdentifier_;


        private bool moreFollows_;

        [ASN1SequenceOf(Name = "listOfIdentifier", IsSetOf = false)]
        [ASN1Element(Name = "listOfIdentifier", IsOptional = false, HasTag = true, Tag = 0, HasDefaultValue = false)]
        public ICollection<Identifier> ListOfIdentifier
        {
            get
            {
                return listOfIdentifier_;
            }
            set
            {
                listOfIdentifier_ = value;
            }
        }

        [ASN1Boolean(Name = "")]
        [ASN1Element(Name = "moreFollows", IsOptional = false, HasTag = true, Tag = 1, HasDefaultValue = true)]
        public bool MoreFollows
        {
            get
            {
                return moreFollows_;
            }
            set
            {
                moreFollows_ = value;
            }
        }


        public void initWithDefaults()
        {
            bool param_MoreFollows =
                true;
            MoreFollows = param_MoreFollows;
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