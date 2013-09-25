//
// This file was generated by the BinaryNotes compiler.
// See http://bnotes.sourceforge.net 
// Any modifications to this file will be lost upon recompilation of the source ASN.1. 
//

using System.Collections.Generic;
using GSF.ASN1;
using GSF.ASN1.Attributes;
using GSF.ASN1.Coders;
using GSF.ASN1.Types;

namespace GSF.MMS
{
    [ASN1PreparedElement]
    [ASN1Choice(Name = "Data")]
    public class Data : IASN1PreparedElement
    {
        private static readonly IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(Data));
        private ICollection<Data> array_;
        private bool array_selected;
        private long bcd_;
        private bool bcd_selected;
        private TimeOfDay binary_time_;
        private bool binary_time_selected;


        private BitString bit_string_;
        private bool bit_string_selected;
        private BitString booleanArray_;
        private bool booleanArray_selected;
        private bool boolean_;
        private bool boolean_selected;
        private FloatingPoint floating_point_;
        private bool floating_point_selected;
        private string generalized_time_;
        private bool generalized_time_selected;


        private long integer_;
        private bool integer_selected;
        private MMSString mMSString_;
        private bool mMSString_selected;
        private ObjectIdentifier objId_;
        private bool objId_selected;
        private byte[] octet_string_;
        private bool octet_string_selected;
        private ICollection<Data> structure_;
        private bool structure_selected;


        private long unsigned_;
        private bool unsigned_selected;
        private UtcTime utc_time_;
        private bool utc_time_selected;
        private string visible_string_;
        private bool visible_string_selected;

        [ASN1SequenceOf(Name = "array", IsSetOf = false)]
        [ASN1_MMSDataArray]
        [ASN1Element(Name = "array", IsOptional = false, HasTag = true, Tag = 1, HasDefaultValue = false)]
        public ICollection<Data> Array
        {
            get
            {
                return array_;
            }
            set
            {
                selectArray(value);
            }
        }

        [ASN1SequenceOf(Name = "structure", IsSetOf = false)]
        [ASN1_MMSDataStructure]
        [ASN1Element(Name = "structure", IsOptional = false, HasTag = true, Tag = 2, HasDefaultValue = false)]
        public ICollection<Data> Structure
        {
            get
            {
                return structure_;
            }
            set
            {
                selectStructure(value);
            }
        }

        [ASN1Boolean(Name = "")]
        [ASN1Element(Name = "boolean", IsOptional = false, HasTag = true, Tag = 3, HasDefaultValue = false)]
        public bool Boolean
        {
            get
            {
                return boolean_;
            }
            set
            {
                selectBoolean(value);
            }
        }

        [ASN1BitString(Name = "")]
        [ASN1Element(Name = "bit-string", IsOptional = false, HasTag = true, Tag = 4, HasDefaultValue = false)]
        public BitString Bit_string
        {
            get
            {
                return bit_string_;
            }
            set
            {
                selectBit_string(value);
            }
        }

        [ASN1Integer(Name = "")]
        [ASN1Element(Name = "integer", IsOptional = false, HasTag = true, Tag = 5, HasDefaultValue = false)]
        public long Integer
        {
            get
            {
                return integer_;
            }
            set
            {
                selectInteger(value);
            }
        }


        [ASN1Integer(Name = "")]
        [ASN1Element(Name = "unsigned", IsOptional = false, HasTag = true, Tag = 6, HasDefaultValue = false)]
        public long Unsigned
        {
            get
            {
                return unsigned_;
            }
            set
            {
                selectUnsigned(value);
            }
        }


        [ASN1Element(Name = "floating-point", IsOptional = false, HasTag = true, Tag = 7, HasDefaultValue = false)]
        public FloatingPoint Floating_point
        {
            get
            {
                return floating_point_;
            }
            set
            {
                selectFloating_point(value);
            }
        }


        [ASN1OctetString(Name = "")]
        [ASN1Element(Name = "octet-string", IsOptional = false, HasTag = true, Tag = 9, HasDefaultValue = false)]
        public byte[] Octet_string
        {
            get
            {
                return octet_string_;
            }
            set
            {
                selectOctet_string(value);
            }
        }


        [ASN1String(Name = "",
            StringType = UniversalTags.VisibleString, IsUCS = false)]
        [ASN1Element(Name = "visible-string", IsOptional = false, HasTag = true, Tag = 10, HasDefaultValue = false)]
        public string Visible_string
        {
            get
            {
                return visible_string_;
            }
            set
            {
                selectVisible_string(value);
            }
        }


        [ASN1String(Name = "",
            StringType = UniversalTags.GeneralizedTime, IsUCS = false)]
        [ASN1Element(Name = "generalized-time", IsOptional = false, HasTag = true, Tag = 11, HasDefaultValue = false)]
        public string Generalized_time
        {
            get
            {
                return generalized_time_;
            }
            set
            {
                selectGeneralized_time(value);
            }
        }


        [ASN1Element(Name = "binary-time", IsOptional = false, HasTag = true, Tag = 12, HasDefaultValue = false)]
        public TimeOfDay Binary_time
        {
            get
            {
                return binary_time_;
            }
            set
            {
                selectBinary_time(value);
            }
        }


        [ASN1Integer(Name = "")]
        [ASN1Element(Name = "bcd", IsOptional = false, HasTag = true, Tag = 13, HasDefaultValue = false)]
        public long Bcd
        {
            get
            {
                return bcd_;
            }
            set
            {
                selectBcd(value);
            }
        }


        [ASN1BitString(Name = "")]
        [ASN1Element(Name = "booleanArray", IsOptional = false, HasTag = true, Tag = 14, HasDefaultValue = false)]
        public BitString BooleanArray
        {
            get
            {
                return booleanArray_;
            }
            set
            {
                selectBooleanArray(value);
            }
        }


        [ASN1ObjectIdentifier(Name = "")]
        [ASN1Element(Name = "objId", IsOptional = false, HasTag = true, Tag = 15, HasDefaultValue = false)]
        public ObjectIdentifier ObjId
        {
            get
            {
                return objId_;
            }
            set
            {
                selectObjId(value);
            }
        }


        [ASN1Element(Name = "mMSString", IsOptional = false, HasTag = true, Tag = 16, HasDefaultValue = false)]
        public MMSString MMSString
        {
            get
            {
                return mMSString_;
            }
            set
            {
                selectMMSString(value);
            }
        }


        [ASN1Element(Name = "utc-time", IsOptional = false, HasTag = true, Tag = 17, HasDefaultValue = false)]
        public UtcTime Utc_time
        {
            get
            {
                return utc_time_;
            }
            set
            {
                selectUtc_time(value);
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


        public bool isArraySelected()
        {
            return array_selected;
        }


        public void selectArray(ICollection<Data> val)
        {
            array_ = val;
            array_selected = true;


            structure_selected = false;

            boolean_selected = false;

            bit_string_selected = false;

            integer_selected = false;

            unsigned_selected = false;

            floating_point_selected = false;

            octet_string_selected = false;

            visible_string_selected = false;

            generalized_time_selected = false;

            binary_time_selected = false;

            bcd_selected = false;

            booleanArray_selected = false;

            objId_selected = false;

            mMSString_selected = false;

            utc_time_selected = false;
        }


        public bool isStructureSelected()
        {
            return structure_selected;
        }


        public void selectStructure(ICollection<Data> val)
        {
            structure_ = val;
            structure_selected = true;


            array_selected = false;

            boolean_selected = false;

            bit_string_selected = false;

            integer_selected = false;

            unsigned_selected = false;

            floating_point_selected = false;

            octet_string_selected = false;

            visible_string_selected = false;

            generalized_time_selected = false;

            binary_time_selected = false;

            bcd_selected = false;

            booleanArray_selected = false;

            objId_selected = false;

            mMSString_selected = false;

            utc_time_selected = false;
        }


        public bool isBooleanSelected()
        {
            return boolean_selected;
        }


        public void selectBoolean(bool val)
        {
            boolean_ = val;
            boolean_selected = true;


            array_selected = false;

            structure_selected = false;

            bit_string_selected = false;

            integer_selected = false;

            unsigned_selected = false;

            floating_point_selected = false;

            octet_string_selected = false;

            visible_string_selected = false;

            generalized_time_selected = false;

            binary_time_selected = false;

            bcd_selected = false;

            booleanArray_selected = false;

            objId_selected = false;

            mMSString_selected = false;

            utc_time_selected = false;
        }


        public bool isBit_stringSelected()
        {
            return bit_string_selected;
        }


        public void selectBit_string(BitString val)
        {
            bit_string_ = val;
            bit_string_selected = true;


            array_selected = false;

            structure_selected = false;

            boolean_selected = false;

            integer_selected = false;

            unsigned_selected = false;

            floating_point_selected = false;

            octet_string_selected = false;

            visible_string_selected = false;

            generalized_time_selected = false;

            binary_time_selected = false;

            bcd_selected = false;

            booleanArray_selected = false;

            objId_selected = false;

            mMSString_selected = false;

            utc_time_selected = false;
        }


        public bool isIntegerSelected()
        {
            return integer_selected;
        }


        public void selectInteger(long val)
        {
            integer_ = val;
            integer_selected = true;


            array_selected = false;

            structure_selected = false;

            boolean_selected = false;

            bit_string_selected = false;

            unsigned_selected = false;

            floating_point_selected = false;

            octet_string_selected = false;

            visible_string_selected = false;

            generalized_time_selected = false;

            binary_time_selected = false;

            bcd_selected = false;

            booleanArray_selected = false;

            objId_selected = false;

            mMSString_selected = false;

            utc_time_selected = false;
        }


        public bool isUnsignedSelected()
        {
            return unsigned_selected;
        }


        public void selectUnsigned(long val)
        {
            unsigned_ = val;
            unsigned_selected = true;


            array_selected = false;

            structure_selected = false;

            boolean_selected = false;

            bit_string_selected = false;

            integer_selected = false;

            floating_point_selected = false;

            octet_string_selected = false;

            visible_string_selected = false;

            generalized_time_selected = false;

            binary_time_selected = false;

            bcd_selected = false;

            booleanArray_selected = false;

            objId_selected = false;

            mMSString_selected = false;

            utc_time_selected = false;
        }


        public bool isFloating_pointSelected()
        {
            return floating_point_selected;
        }


        public void selectFloating_point(FloatingPoint val)
        {
            floating_point_ = val;
            floating_point_selected = true;


            array_selected = false;

            structure_selected = false;

            boolean_selected = false;

            bit_string_selected = false;

            integer_selected = false;

            unsigned_selected = false;

            octet_string_selected = false;

            visible_string_selected = false;

            generalized_time_selected = false;

            binary_time_selected = false;

            bcd_selected = false;

            booleanArray_selected = false;

            objId_selected = false;

            mMSString_selected = false;

            utc_time_selected = false;
        }


        public bool isOctet_stringSelected()
        {
            return octet_string_selected;
        }


        public void selectOctet_string(byte[] val)
        {
            octet_string_ = val;
            octet_string_selected = true;


            array_selected = false;

            structure_selected = false;

            boolean_selected = false;

            bit_string_selected = false;

            integer_selected = false;

            unsigned_selected = false;

            floating_point_selected = false;

            visible_string_selected = false;

            generalized_time_selected = false;

            binary_time_selected = false;

            bcd_selected = false;

            booleanArray_selected = false;

            objId_selected = false;

            mMSString_selected = false;

            utc_time_selected = false;
        }


        public bool isVisible_stringSelected()
        {
            return visible_string_selected;
        }


        public void selectVisible_string(string val)
        {
            visible_string_ = val;
            visible_string_selected = true;


            array_selected = false;

            structure_selected = false;

            boolean_selected = false;

            bit_string_selected = false;

            integer_selected = false;

            unsigned_selected = false;

            floating_point_selected = false;

            octet_string_selected = false;

            generalized_time_selected = false;

            binary_time_selected = false;

            bcd_selected = false;

            booleanArray_selected = false;

            objId_selected = false;

            mMSString_selected = false;

            utc_time_selected = false;
        }


        public bool isGeneralized_timeSelected()
        {
            return generalized_time_selected;
        }


        public void selectGeneralized_time(string val)
        {
            generalized_time_ = val;
            generalized_time_selected = true;


            array_selected = false;

            structure_selected = false;

            boolean_selected = false;

            bit_string_selected = false;

            integer_selected = false;

            unsigned_selected = false;

            floating_point_selected = false;

            octet_string_selected = false;

            visible_string_selected = false;

            binary_time_selected = false;

            bcd_selected = false;

            booleanArray_selected = false;

            objId_selected = false;

            mMSString_selected = false;

            utc_time_selected = false;
        }


        public bool isBinary_timeSelected()
        {
            return binary_time_selected;
        }


        public void selectBinary_time(TimeOfDay val)
        {
            binary_time_ = val;
            binary_time_selected = true;


            array_selected = false;

            structure_selected = false;

            boolean_selected = false;

            bit_string_selected = false;

            integer_selected = false;

            unsigned_selected = false;

            floating_point_selected = false;

            octet_string_selected = false;

            visible_string_selected = false;

            generalized_time_selected = false;

            bcd_selected = false;

            booleanArray_selected = false;

            objId_selected = false;

            mMSString_selected = false;

            utc_time_selected = false;
        }


        public bool isBcdSelected()
        {
            return bcd_selected;
        }


        public void selectBcd(long val)
        {
            bcd_ = val;
            bcd_selected = true;


            array_selected = false;

            structure_selected = false;

            boolean_selected = false;

            bit_string_selected = false;

            integer_selected = false;

            unsigned_selected = false;

            floating_point_selected = false;

            octet_string_selected = false;

            visible_string_selected = false;

            generalized_time_selected = false;

            binary_time_selected = false;

            booleanArray_selected = false;

            objId_selected = false;

            mMSString_selected = false;

            utc_time_selected = false;
        }


        public bool isBooleanArraySelected()
        {
            return booleanArray_selected;
        }


        public void selectBooleanArray(BitString val)
        {
            booleanArray_ = val;
            booleanArray_selected = true;


            array_selected = false;

            structure_selected = false;

            boolean_selected = false;

            bit_string_selected = false;

            integer_selected = false;

            unsigned_selected = false;

            floating_point_selected = false;

            octet_string_selected = false;

            visible_string_selected = false;

            generalized_time_selected = false;

            binary_time_selected = false;

            bcd_selected = false;

            objId_selected = false;

            mMSString_selected = false;

            utc_time_selected = false;
        }


        public bool isObjIdSelected()
        {
            return objId_selected;
        }


        public void selectObjId(ObjectIdentifier val)
        {
            objId_ = val;
            objId_selected = true;


            array_selected = false;

            structure_selected = false;

            boolean_selected = false;

            bit_string_selected = false;

            integer_selected = false;

            unsigned_selected = false;

            floating_point_selected = false;

            octet_string_selected = false;

            visible_string_selected = false;

            generalized_time_selected = false;

            binary_time_selected = false;

            bcd_selected = false;

            booleanArray_selected = false;

            mMSString_selected = false;

            utc_time_selected = false;
        }


        public bool isMMSStringSelected()
        {
            return mMSString_selected;
        }


        public void selectMMSString(MMSString val)
        {
            mMSString_ = val;
            mMSString_selected = true;


            array_selected = false;

            structure_selected = false;

            boolean_selected = false;

            bit_string_selected = false;

            integer_selected = false;

            unsigned_selected = false;

            floating_point_selected = false;

            octet_string_selected = false;

            visible_string_selected = false;

            generalized_time_selected = false;

            binary_time_selected = false;

            bcd_selected = false;

            booleanArray_selected = false;

            objId_selected = false;

            utc_time_selected = false;
        }


        public bool isUtc_timeSelected()
        {
            return utc_time_selected;
        }


        public void selectUtc_time(UtcTime val)
        {
            utc_time_ = val;
            utc_time_selected = true;


            array_selected = false;

            structure_selected = false;

            boolean_selected = false;

            bit_string_selected = false;

            integer_selected = false;

            unsigned_selected = false;

            floating_point_selected = false;

            octet_string_selected = false;

            visible_string_selected = false;

            generalized_time_selected = false;

            binary_time_selected = false;

            bcd_selected = false;

            booleanArray_selected = false;

            objId_selected = false;

            mMSString_selected = false;
        }
    }
}