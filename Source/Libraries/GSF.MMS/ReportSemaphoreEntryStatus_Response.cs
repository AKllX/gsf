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
    [ASN1Sequence(Name = "ReportSemaphoreEntryStatus_Response", IsSet = false)]
    public class ReportSemaphoreEntryStatus_Response : IASN1PreparedElement
    {
        private static readonly IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(ReportSemaphoreEntryStatus_Response));
        private ICollection<SemaphoreEntry> listOfSemaphoreEntry_;


        private bool moreFollows_;

        [ASN1SequenceOf(Name = "listOfSemaphoreEntry", IsSetOf = false)]
        [ASN1Element(Name = "listOfSemaphoreEntry", IsOptional = false, HasTag = true, Tag = 0, HasDefaultValue = false)]
        public ICollection<SemaphoreEntry> ListOfSemaphoreEntry
        {
            get
            {
                return listOfSemaphoreEntry_;
            }
            set
            {
                listOfSemaphoreEntry_ = value;
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
                false;
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