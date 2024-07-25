using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shareholder_System.Helper
{
    public class QrCode
    {



        public static string getQrcodeValue(string ProviderName, string VatNo, DateTime Datetime, string Total, string Vat_amount)

        {


            var providername = getTlvForValue(1, ProviderName);
            var vatNo = getTlvForValue(2, VatNo);
            //var date1 = Datetime;
            //TimeSpan oTS = new TimeSpan(date1.Ticks);

            var date = getTlvForValue(3, Datetime.ToString("u"));
            var total = getTlvForValue(4, Total);
            var vat = getTlvForValue(5, Vat_amount);

            var hexString = providername + vatNo + date + total + vat;

            byte[] resultantArray = new byte[hexString.Length / 2];
            for (int i = 0; i < resultantArray.Length; i++)
            {
                resultantArray[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }
            string encodedStr = Convert.ToBase64String(resultantArray);

            return encodedStr;
        }


        public static string getTlvForValue(int tageName, string tageValue)
        {
            UTF8Encoding utf8 = new UTF8Encoding();

            var tag = tageName.ToString("X2");
            //  var tagVlueLengh = tageValue.Length.ToString("X2");
            byte[] tagValue = Encoding.Default.GetBytes(tageValue);
            var tagValuehexString = BitConverter.ToString(tagValue);
            tagValuehexString = tagValuehexString.Replace("-", "");
            var tagVlueLengh = (tagValuehexString.Length / 2).ToString("X2");
            string hexString = tag + tagVlueLengh + tagValuehexString;

            return hexString;

        }


        public static string encodeQrText(string CompanyName, string TaxNumber, string BillDateTime, string TotalAfterVAT, string TotalVAT)
        {
            //use UTF8 with sallerName to solve arabic issue
            byte[] bytes = Encoding.UTF8.GetBytes(CompanyName);
            string L1 = bytes.Length.ToString("X");
            string tag1Hex = BitConverter.ToString(bytes);
            tag1Hex = tag1Hex.Replace("-", "");

            string L2 = TaxNumber.Length.ToString("X");
            string L3 = BillDateTime.Length.ToString("X");
            string L4 = TotalAfterVAT.Length.ToString("X");
            string L5 = TotalVAT.Length.ToString("X");
            //length tag must be 2 digit like '0C' 
            string hex = "01" + ((L1.Length == 1) ? ("0" + L1) : L1) + tag1Hex +
                         "02" + ((L2.Length == 1) ? ("0" + L2) : L2) + ToHexString(TaxNumber) +
                         "03" + ((L3.Length == 1) ? ("0" + L3) : L3) + ToHexString(BillDateTime) +
                         "04" + ((L4.Length == 1) ? ("0" + L4) : L4) + ToHexString(TotalAfterVAT) +
                         "05" + ((L5.Length == 1) ? ("0" + L5) : L5) + ToHexString(TotalVAT);

            return HexToBase64(hex);
        }

        private static string ToHexString(string str)
        {
            byte[] bytes = Encoding.Default.GetBytes(str);
            string hexString = BitConverter.ToString(bytes);
            hexString = hexString.Replace("-", "");
            return hexString;
        }

        private static string HexToBase64(string strInput)
        {
            try
            {
                var bytes = new byte[strInput.Length / 2];
                for (var i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = Convert.ToByte(strInput.Substring(i * 2, 2), 16);
                }
                return Convert.ToBase64String(bytes);
            }
            catch (Exception)
            {
                return "-1";
            }
        }


    }
}